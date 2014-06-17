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

public partial class user_edit : System.Web.UI.Page
{
    ClsUser user = new ClsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        ClsAuth.check_auth_admin();

        string ID = ClsUtil.getStrVal( Request["id"] );
        if (ID == "") {
            this.msg.Text = "Not a valid user.";
            this.form1.Text = "";
            return;
        }

        if (this.IsPostBack)
        {
            this.form1.Text = "";
            try
            {
                this.user.retrieveRequest(this.IsPostBack, Request);
                this.user.update(ID, Session["userid"].ToString());

                this.msg.Text = "<p><font color='green'>This profile has been updated.</font> </p>";
                if (ClsDB.DEBUG()) Response.Write(this.user.strQuery());
            }
            catch (Exception ex)
            {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
            }

            this.user.retrieveDB(ID);
            this.form1.Text = ShowEditForm();
        }
        else
        {
            this.user.retrieveDB(ID);
            this.msg.Text = "";
            this.form1.Text = ShowEditForm();
        }
    }
    
    string ShowEditForm()
    {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:" + star + "</td><td><input type='text' maxlength='10' id='txtFirstName' name='txtFirstName' value='" + this.user.first_name + "'></td></tr>";
        s += "<tr><td>Last Name:" + star + "</td><td><input type='text' maxlength='100' id='txtLastName' name='txtLastName' value='" + ClsUtil.textboxEncode(this.user.last_name) + "'></td></tr>";
        s += "<tr><td>Email:" + star + "</td><td><input type='text' maxlength='100' id='txtEmail' name='txtEmail' value='" + ClsUtil.textboxEncode(this.user.email) + "'></td></tr>";
        s += "<tr><td>Login:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.user.login) + "</td></tr>";
        s += "<tr><td>Note:</td><td><input type='text' maxlength='100' id='txtNote' name='txtNote' value='" + ClsUtil.textboxEncode(this.user.note) + "'></td></tr>";

        s += "<tr><td>User Type:" + star + "</td><td>&nbsp;" + ClsUser.writeUserTypeList("txtUserType", "txtUserType", this.user.gid) + "</td></tr>";

        s += "<tr><td>Created By:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.user.create_by) + "</td></tr>";
        s += "<tr><td>Created Datetime:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.user.create_datetime) + "</td></tr>";
        s += "<tr><td>Last Updated By:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.user.last_update_by) + "</td></tr>";
        s += "<tr><td>Last Updated Datetime:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.user.last_update_datetime) + "</td></tr>";
        s += "<tr><td>Disabled:</td><td>&nbsp;" + ClsUser.writeUserStatusList("txtDisabled", "txtDisabled", this.user.disabled) + "</td></tr>";

        s = "<table class='T1'>" + s + "</table>";

        return "<table>" + s + "</table>" +
            "<br/><input value=\"Submit Change\" type=\"button\" onclick=\"javascript:update();\" />";
    }
}
