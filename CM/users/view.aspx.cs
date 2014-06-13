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

public partial class user_view : System.Web.UI.Page
{
    ClsUser user = new ClsUser();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ClsAuth.check_auth_admin();

        ID = ClsUtil.getStrVal(Request["id"]);
        if (ID == "") {
            return;
        }

        this.user.retrieveDB(ID);
        this.lblProfile.Text = this.ShowViewForm();
    }
    
    string ShowViewForm()
    {
        string s = "";
        //string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:</td><td width='150'>" + this.user.first_name + "&nbsp;</td></tr>";
        s += "<tr><td>Last Name:</td><td>" + this.user.last_name + "&nbsp;</td></tr>";
        s += "<tr><td>Email:</td><td>" + this.user.email + "&nbsp;</td></tr>";
        s += "<tr><td>Login:</td><td>" + this.user.login + "&nbsp;</td></tr>";
        s += "<tr><td>Note:</td><td>" + this.user.note + "&nbsp;</td></tr>";

        s += "<tr><td>Created By:</td><td>" + ClsUtil.textboxEncode(this.user.create_by) + "&nbsp;</td></tr>";
        s += "<tr><td>Created Datetime:</td><td>" + ClsUtil.textboxEncode(this.user.create_datetime) + "&nbsp;</td></tr>";
        s += "<tr><td>Last Updated By:</td><td>" + ClsUtil.textboxEncode(this.user.last_update_by) + "&nbsp;</td></tr>";
        s += "<tr><td>Last Updated Datetime:</td><td>" + ClsUtil.textboxEncode(this.user.last_update_datetime) + "&nbsp;</td></tr>";
        s += "<tr><td>Disabled:</td><td>" + ClsUtil.textboxEncode(this.user.disabled) + "&nbsp;</td></tr>";

        return "<table border='1' cellpadding='3' cellspacing='1'>" + s + "</table>";
    }
}
