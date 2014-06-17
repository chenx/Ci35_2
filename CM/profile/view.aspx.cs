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

public partial class profile_view : System.Web.UI.Page
{
    ClsProfile p = new ClsProfile();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null || Session["userid"] == "") {
            return;
        }

        this.p.retrieveDB(Session["userid"].ToString());
        this.lblProfile.Text = this.ShowViewForm();
    }

    string ShowViewForm()
    {
        string s = "";
        //string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:</td><td width='150'>&nbsp;" + this.p.first_name + "</td></tr>";
        s += "<tr><td>Last Name:</td><td>&nbsp;" + this.p.last_name + "</td></tr>";
        s += "<tr><td>Email:</td><td>&nbsp;" + this.p.email + "</td></tr>";
        s += "<tr><td>Login:</td><td>&nbsp;" + this.p.login + "</td></tr>";
        s += "<tr><td>Note:</td><td>&nbsp;" + this.p.note + "</td></tr>";

        return "<table class='T1'>" + s + "</table>";
    }
}
