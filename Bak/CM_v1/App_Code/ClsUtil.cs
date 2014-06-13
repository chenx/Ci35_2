using System;
using System.Data;
using System.Data.SqlClient;
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
/// Summary description for ClsUtil
/// </summary>
public class ClsUtil
{
    public static bool _DEBUG = false;
    private string _strConn;
    private static ClsUtil _SiteUtil = null;

    private ClsUtil()
    {
        _DEBUG = StrToBool(ConfigurationSettings.AppSettings["DEBUG"]);
        _strConn = ConfigurationManager.ConnectionStrings["connCM"].ConnectionString;
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

    public bool DEBUG() {
        return _DEBUG;
    }

    public string strConn() {
        return _strConn;
    }

    // assume: query is to get count.
    public int getCount(string strQuery) {
        string s = getScalar(strQuery);
        return Convert.ToInt32(s);
    }

    public string getScalar(string strQuery) {
        string ret = "";
        string strConn = this.strConn(); 
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = comm.ExecuteReader();
            if (sdr.Read()) {
                ret = sdr[0].ToString();
            }
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
        finally {
            if (sdr != null) sdr.Close();
            conn.Close();
        }

        return ret;
    }

    // The field is of type varbinary, e.g., for password field.
    public byte[] getDBVarbinary(string strQuery)
    {
        byte[] ret = null;
        string strConn = this.strConn();
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                ret = (byte[])sdr[0];
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (sdr != null) sdr.Close();
            conn.Close();
        }

        return ret;
    }

    /// <summary>
    /// Static utility functions.
    /// </summary>

    public static bool StrToBool(string v) {
        return v.ToLower() == "true";
    }

    public static string sqlEncode(string s) {
        if (s == null || s == "") return "''";

        s = s.Replace("'", "''");
        s = "'" + s + "'";
        return s;
    }

    public static string getStrVal(object o) {
        if (o == null) return "";
        else return o.ToString();
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
}
