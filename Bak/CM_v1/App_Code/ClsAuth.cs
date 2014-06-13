using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for ClsAuth
/// </summary>
public class ClsAuth
{
	public ClsAuth()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool IsAdmin() {
        return HttpContext.Current.Session["role"] != null && HttpContext.Current.Session["role"].ToString() == "admin";
    }

    public static bool IsUser() {
        return HttpContext.Current.Session["username"] != null && HttpContext.Current.Session["username"].ToString() != "";
    }

    // Used by admin pages.
    public static void check_auth_admin() {
        if (!ClsAuth.IsAdmin()) { HttpContext.Current.Response.Redirect("~/"); }
    }

    public static void check_auth_user()
    {
        if (!ClsAuth.IsUser()) { HttpContext.Current.Response.Redirect("~/"); }
    }

    // Add user name to the right side of menu bar.
    public static string addMenuUserName(string s) {
        if (ClsAuth.IsUser())
        {
            s = "<table cellpadding='0' cellspacing='0' style='border: 0px; width: 100%; background-color: #6666cc;'>" +
                "<tr><td>" + s + "</td>" +
                "<td align='right' style='color: #dedeff; font-weight:bold;'> " + 
                ClsUtil.CapitalizeFirstLetter( HttpContext.Current.Session["username"].ToString() ) + "&nbsp;&nbsp;</td></tr></table>";
        }
        return s;
    }
}
