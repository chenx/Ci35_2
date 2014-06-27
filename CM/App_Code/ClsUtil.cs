using System;
using System.Data;
using System.Configuration;
using System.Web.Security;


/// <summary>
/// General utility functions.
/// </summary>
public class ClsUtil
{
    public static bool _DEBUG = false;
    private static ClsUtil _SiteUtil = null;

    private ClsUtil()
    {
        _DEBUG = StrToBool(ConfigurationSettings.AppSettings["DEBUG"]);
    }
    
    /// <summary>
    /// Singleton. Return only one instance of ClsUtil for the site.
    /// </summary>
    public static ClsUtil Instance() {
        if (_SiteUtil == null) {
            _SiteUtil = new ClsUtil();
        }
        return _SiteUtil;
    }

    /// <summary>
    /// Site wide variables, initialized once at start up.
    /// </summary>

    public static bool DEBUG() {
        return _DEBUG;
    }

    /// <summary>
    /// Static utility functions.
    /// </summary>

    public static bool StrToBool(string v) {
        return v.ToLower() == "true";
    }

    public static string getStrVal(object o) {
        if (o == null) return "";
        else return o.ToString();
    }

    public static string getStrValTrim(object o)
    {
        if (o == null) return "";
        else return o.ToString().Trim();
    }

    public static string textboxEncode(string s) {
        if (s == null || s == "") return "";
        return s.Replace("'", "&#39;");
    }

    public static string formatDate(string s) {
        if (s == null || s == "") return s;
        DateTime t = Convert.ToDateTime(s);
        return t.ToShortDateString();
    }

    public static string getPostVal(bool IsPostBack, object request_o)
    {
        if (IsPostBack && (request_o != null))
        {
            return request_o.ToString().Trim();
        }
        else
        {
            return "";
        }
    }

    public static string getPostVal(object request_o)
    {
        return (request_o == null) ? "" : request_o.ToString().Trim();
    }

   
    public static string CapitalizeFirstLetter(string s)
    {
        if (String.IsNullOrEmpty(s))
            return s;
        if (s.Length == 1)
            return s.ToUpper();
        return s.Remove(1).ToUpper() + s.Substring(1);
    }

    /// <summary>
    /// Database related functions.
    /// </summary>

    public static string getNameById(string table, string ID)
    {
        string sql = "SELECT [name] FROM [" + table + "] WHERE [ID] = " + ClsDB.sqlEncode(ID);
        return new ClsDB().ExecuteScalar(sql);
    }


    public static string writeIdNameList(string id, string name, string value, string DbTable)
    {
        string s = "<option value=''>-- Selecte One --</option>";

        string selected;

        DataSet ds = new ClsDB().ExecuteDataSet("SELECT ID, name FROM [" + DbTable + "]");

        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                string v_id = dt.Rows[i]["ID"].ToString();
                string v_name = dt.Rows[i]["name"].ToString();
                selected = (value == v_id) ? " selected" : "";
                s += "<option value='" + v_id + "'" + selected + ">" + v_name + "</option>";
            }
        }

        s = "<select id='" + id + "' name='" + name + "'>" + s + "</select>";

        return s;
    }

    public static string MD5(string s)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5");
    }
    
    public static bool IsValidEmail(string email)
    {
        if (email.Trim() == "") return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
