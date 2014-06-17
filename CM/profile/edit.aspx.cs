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

public partial class profile_edit : System.Web.UI.Page
{
    ClsProfile p = new ClsProfile();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null || Session["userid"] == "")
        {
            this.msg.Text = "Not a valid user.";
            this.form1.Text = "";
            return;
        }
        string ID = Session["userid"].ToString();

        if (this.IsPostBack)
        {
            this.form1.Text = "";
            this.p.retrieveRequest(this.IsPostBack, Request);
            try
            {
                this.p.update(ID, ID);
                this.msg.Text = "<p><font color='green'>Your profile has been updated.</font> </p>";
                if (ClsDB.DEBUG()) Response.Write(this.p.strQuery());
            }
            catch (Exception ex)
            {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
            }
            this.form1.Text = ShowEditForm();
        }
        else
        {
            this.p.retrieveDB(ID);
            this.msg.Text = "";
            this.form1.Text = ShowEditForm();
        }
    }

    string ShowEditForm()
    {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:" + star + "</td><td><input type='text' maxlength='10' id='txtFirstName' name='txtFirstName' value='" + this.p.first_name + "'></td></tr>";
        s += "<tr><td>Last Name:" + star + "</td><td><input type='text' maxlength='100' id='txtLastName' name='txtLastName' value='" + ClsUtil.textboxEncode(this.p.last_name) + "'></td></tr>";
        s += "<tr><td>Email:" + star + "</td><td><input type='text' maxlength='100' id='txtEmail' name='txtEmail' value='" + ClsUtil.textboxEncode(this.p.email) + "'></td></tr>";
        s += "<tr><td>Login:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.p.login) + " <input type='hidden' id='txtLogin' name='txtLogin' value='" + ClsUtil.textboxEncode(this.p.login) + "'></td></tr>";
        s += "<tr><td>Note:</td><td><input type='text' maxlength='100' id='txtNote' name='txtNote' value='" + ClsUtil.textboxEncode(this.p.note) + "'></td></tr>";

        s = "<table class='T1'>" + s + "</table>";

        return "<table>" + s + "</table>" +
            "<br/><input value=\"Submit Change\" type=\"button\" onclick=\"javascript:update();\" />";
    }
}
