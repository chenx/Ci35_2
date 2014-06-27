using System.Web;


/// <summary>
/// User authentication functions.
/// </summary>
public class ClsAuth
{
    public ClsAuth()
    {

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

    public static string getMenuUserName() { 
        string s = "Home";
        if (ClsAuth.IsUser())
        {
            s = ClsUtil.CapitalizeFirstLetter(HttpContext.Current.Session["username"].ToString());
        }
        return s;
    }

    // Add user name to the right side of menu bar.
    public static string addMenuUserName(string s) {
        if (ClsAuth.IsUser())
        {
            s = "<table cellpadding='0' cellspacing='0' style='border: 0px; width: 100%; background-color: #6666ff;'>" +
                "<tr><td>" + s + "</td>" +
                "<td align='right' style='color: #dedeff; font-weight:bold;'> " + 
                ClsUtil.CapitalizeFirstLetter( HttpContext.Current.Session["username"].ToString() ) + "&nbsp;&nbsp;</td></tr></table>";
        }
        return s;
    }
}
