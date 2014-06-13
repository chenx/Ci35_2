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

public partial class user_new : System.Web.UI.Page
{
    ClsUser user = new ClsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        ClsAuth.check_auth_admin();

        // This is to avoid the double submission problem when refreshing a submitted page.
        if (Request["ok"] != null)
        {
            this.form1.Text = "";
            this.msg.Text = "<font color='green'>The new user has been added.</font> <br/><br/><a href='new.aspx'>Add Another New User</a>";
            return;
        }

        this.user.retrieveRequest(this.IsPostBack, Request);
        if (this.IsPostBack)
        {
            this.form1.Text = "";
            try
            {
                this.insert();
                Response.Redirect("new.aspx?ok=1");
                //this.msg.Text = "<font color='green'>The new user has been added.</font> <br/><br/><a href='new.aspx'>Add Another New User</a>";
            }
            catch (Exception ex) {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
                this.form1.Text = ShowNewForm();
            }
            
        }
        else
        {
            this.msg.Text = "";
            this.form1.Text = ShowNewForm();
        }
    }

    string ShowNewForm() {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:" + star + "</td><td><input type='text' maxlength='100' id='txtFirstName' name='txtFirstName' value='" + this.user.first_name + "'></td></tr>";
        s += "<tr><td>Last Name:" + star + "</td><td><input type='text' maxlength='100' id='txtLastName' name='txtLastName' value='" + this.user.last_name + "'></td></tr>";
        s += "<tr><td>Email:" + star + "</td><td><input type='text' maxlength='100' id='txtEmail' name='txtEmail' value='" + this.user.email + "'></td></tr>";
        s += "<tr><td>Login:" + star + "</td><td><input type='text' maxlength='100' id='txtLogin' name='txtLogin' value='" + this.user.login + "'></td></tr>";
        s += "<tr><td>Password:" + star + "</td><td><input type='password' maxlength='100' id='txtPwd' name='txtPwd' value='" + ClsUtil.getPostVal(Request["txtPwd"]) + "'></td></tr>";
        s += "<tr><td>Password (repeat):" + star + "</td><td><input type='password' maxlength='100' id='txtPwd2' name='txtPwd2' value='" + ClsUtil.getPostVal(Request["txtPwd2"]) + "'></td></tr>";
        s += "<tr><td>Note:</td><td><input type='text' maxlength='100' id='txtNote' name='txtNote' value='" + this.user.note + "'></td></tr>";
        s += "<tr><td>User Type:" + star + "</td><td>" + ClsUser.writeUserTypeList("txtUserType", "txtUserType", this.user.gid) + "</td></tr>";
        s += "<tr><td>Disabled:</td><td>" + ClsUser.writeUserStatusList("txtDisabled", "txtDisabled", this.user.disabled) + "</td></tr>";

        s = "<table border='1' cellpadding='3' cellspacing='1'>" + s + "</table>";

        return "<table><tr><td valign='top'>" + s + "</td></table>" +
            "<br/><input value=\"Add New User\" type=\"button\" onclick=\"javascript:add();\" />";
    }

    // Note this is complicated by password, so needs special handling.
    void insert() {
        string s = ClsUser.validate_pwd(ClsUtil.getPostVal(Request["txtPwd"]), ClsUtil.getPostVal(Request["txtPwd2"]));
        if (s != "") {
            throw new Exception(s);
        }

        string strQuery = @"INSERT INTO [User] (first_name, last_name, email, login, passwd, note, gid, disabled, 
                            [create_user_id], [create_datetime]) VALUES (" +
            ClsDB.sqlEncode( this.user.first_name ) + ", " +
            ClsDB.sqlEncode( this.user.last_name ) + ", " +
            ClsDB.sqlEncode( this.user.email ) + ", " +
            ClsDB.sqlEncode( this.user.login ) + ", " +
            "HASHBYTES('MD5', " + ClsDB.sqlEncode( Request["txtPwd"] ) + "), " +
            ClsDB.sqlEncode( this.user.note ) + ", " +
            ClsDB.sqlEncode( this.user.gid ) + ", " +
            ClsDB.sqlEncode( this.user.disabled ) + ", " +
            ClsDB.sqlEncode(Session["userid"].ToString()) + ", " +
            ClsDB.sqlEncode(DateTime.Now.ToString()) + 
            ")";

        if (ClsDB.DEBUG()) Response.Write(strQuery);
        new ClsDB().ExecuteNonQuery( strQuery );
    }

}
