using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class user_changepwd : System.Web.UI.Page
{
    ClsUser user = new ClsUser();

    private string new_pwd;
    private string new_pwd2;

    protected void Page_Load(object sender, EventArgs e) {
        ClsAuth.check_auth_admin();

        string ID = ClsUtil.getStrVal(Request["id"]);
        this.user.retrieveDB(ID);
        this.lblProfile.Text = this.ShowViewForm();

        if (this.IsPostBack) {
            try {
                this.msg.Text = "";
                this.retrievePostVal();
                //Response.Write(this.db_old_pwd_hash + "=?=" + this.old_pwd );

                string check = ClsUser.validate_pwd(this.new_pwd, this.new_pwd2);
                if (check != "") {
                    this.msg.Text = "<p><font color='red'>" + check + ".</font></p>";
                }
                else {
                    this.update(ID);
                    this.msg.Text = "<p><font color='green'>User password has been updated.</font> </p>";
                }
            }
            catch (Exception ex) {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
            }
        }
        else {
            this.msg.Text = "";
        }
    }
         
    private void retrievePostVal() {
        this.new_pwd = this.new_pwd2 = "";

        if (this.IsPostBack) {
            this.new_pwd = ClsUtil.getStrValTrim(Request["txtNewPwd"]);
            this.new_pwd2 = ClsUtil.getStrValTrim(Request["txtNewPwd2"]);
        }
    }
    
    void update(string ID) {
        if (ID == "") return;

        string strQuery = "UPDATE [User] SET passwd = HASHBYTES('MD5', " + ClsDB.sqlEncode( this.new_pwd ) + ") WHERE ID = " + ID;
        if (ClsDB.DEBUG()) Response.Write(strQuery);

        new ClsDB().ExecuteNonQuery(strQuery);
    }


    ///////////////////////////////////////////////////////////////////
    // Copied from view.aspx.cs
    ///////////////////////////////////////////////////////////////////

    string ShowViewForm() {
        string s = "";
        //string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:</td><td width='150'>" + this.user.first_name + "&nbsp;</td></tr>";
        s += "<tr><td>Last Name:</td><td>" + this.user.last_name + "&nbsp;</td></tr>";
        s += "<tr><td>Email:</td><td>" + this.user.email + "&nbsp;</td></tr>";
        s += "<tr><td>Login:</td><td>" + this.user.login + "&nbsp;</td></tr>";
        s += "<tr><td>Note:</td><td>" + this.user.note + "&nbsp;</td></tr>";

        //s += "<tr><td>Last Updated By:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.last_update_by) + "</td></tr>";
        //s += "<tr><td>Last Updated Datetime:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.last_update_datetime) + "</td></tr>";
        s += "<tr><td>Disabled:</td><td>" + ClsUtil.textboxEncode(this.user.disabled) + "&nbsp;</td></tr>";

        return "<table border='1' cellpadding='3' cellspacing='1'>" + s + "</table>";
    }
}
