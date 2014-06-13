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
/// Summary description for ClsUser
/// </summary>
public class ClsUser
{
    private string _strQuery;

    public string ID;
    public string first_name;
    public string last_name;
    public string email;
    public string login;
    public string passwd;
    public string note;
    public string gid;

    public string create_by;
    public string create_datetime;
    public string last_update_by;
    public string last_update_datetime;
    public string disabled;

    public string new_pwd;
    public string new_pwd2;

    public ClsUser()
	{
        this.clear();
    }

    public string strQuery() { return this._strQuery; }

    public void clear() {
        _strQuery = "";

        ID = "";
        first_name = "";
        last_name = "";
        email = "";
        login = "";
        passwd = "";
        note = "";
        gid = "";
        create_by = "";
        create_datetime = "";
        last_update_by = "";
        last_update_datetime = "";
        disabled = "0";
    }

    public void retrieveDB(string ID)
    {
        this.clear();
        this._strQuery = "SELECT * FROM [User] WHERE ID = " + ClsDB.sqlEncode(ID);

        DataTable dt = new ClsDB().ExecuteDataTable(this._strQuery);
        if (dt == null) return;

        if (dt.Rows.Count > 0) {
            DataRow sdr = dt.Rows[0];

            this.first_name = ClsUtil.getStrVal(sdr["first_name"]);
            this.last_name = ClsUtil.getStrVal(sdr["last_name"]);
            this.email = ClsUtil.getStrVal(sdr["email"]);
            this.login = ClsUtil.getStrVal(sdr["login"]);
            this.note = ClsUtil.getStrVal(sdr["note"]);
            this.gid = ClsUtil.getStrVal(sdr["gid"]);

            this.create_by = ClsUser.getUserNameById(ClsUtil.getStrVal(sdr["create_User_id"]));
            this.create_datetime = ClsUtil.getStrVal(sdr["create_datetime"]);
            this.last_update_by = ClsUser.getUserNameById(ClsUtil.getStrVal(sdr["last_update_User_id"]));
            this.last_update_datetime = ClsUtil.getStrVal(sdr["last_update_datetime"]);
            this.disabled = ClsUtil.getStrVal(sdr["disabled"]);
        }

        /*
         * Get same result as above code.
         * 
        ClsUtil u = ClsUtil.Instance();
        string strConn = u.strConn();
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(strConn);
            SqlCommand comm = new SqlCommand(strQuery, conn);

            conn.Open();
            using (SqlDataReader sdr = comm.ExecuteReader())
            {
                if (sdr.Read())
                {
                    this.first_name = ClsUtil.getStrVal(sdr["first_name"]);
                    this.last_name = ClsUtil.getStrVal(sdr["last_name"]);
                    this.email = ClsUtil.getStrVal(sdr["email"]);
                    this.login = ClsUtil.getStrVal(sdr["login"]);
                    this.note = ClsUtil.getStrVal(sdr["note"]);
                    this.gid = ClsUtil.getStrVal(sdr["gid"]);

                    this.create_by = ClsUser.getUserNameById(ClsUtil.getStrVal(sdr["create_User_id"]));
                    this.create_datetime = ClsUtil.getStrVal(sdr["create_datetime"]);
                    this.last_update_by = ClsUser.getUserNameById(ClsUtil.getStrVal(sdr["last_update_User_id"]));
                    this.last_update_datetime = ClsUtil.getStrVal(sdr["last_update_datetime"]);
                    this.disabled = ClsUtil.getStrVal(sdr["disabled"]);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (conn != null) conn.Close();
        }
        */
    }

    public void retrieveRequest(bool IsPostBack, HttpRequest Request) {
        this.clear();

        if (IsPostBack)
        {
            this.first_name = ClsUtil.getStrValTrim(Request["txtFirstName"]);
            this.last_name = ClsUtil.getStrValTrim(Request["txtLastName"]);
            this.email = ClsUtil.getStrValTrim(Request["txtEmail"]);
            this.login = ClsUtil.getStrValTrim(Request["txtLogin"]);
            this.note = ClsUtil.getStrValTrim(Request["txtNote"]);
            this.gid = ClsUtil.getStrValTrim(Request["txtUserType"]);

            //this.create_by = ClsUtil.getStrValTrim(Request["create_User_id"]);
            //this.create_datetime = ClsUtil.getStrValTrim(Request["create_datetime"]);
            //this.last_update_by = ClsUtil.getStrValTrim(Request["last_update_User_id"]);
            //this.last_update_datetime = ClsUtil.getStrValTrim(Request["last_update_datetime"]);
            this.disabled = ClsUtil.getStrValTrim(Request["txtDisabled"]);
            if (this.disabled == "") this.disabled = "0";
        }
    }

    public void update(string ID, string update_user_id) {
        if (ID == "") return;

        string disable = (this.disabled == "") ? "" : "disabled = " + ClsDB.sqlEncode(this.disabled);

        this._strQuery = "UPDATE [User] SET " +
            "First_Name = " + ClsDB.sqlEncode( this.first_name ) + ", " +
            "Last_Name = " + ClsDB.sqlEncode( this.last_name ) + ", " +
            "Email = " + ClsDB.sqlEncode( this.email ) + ", " +
            "Note = " + ClsDB.sqlEncode( this.note ) + ", " +
            "gid = " + ClsDB.sqlEncode( this.gid ) + ", " +
            "last_update_user_id = " + ClsDB.sqlEncode( update_user_id ) + ", " +
            "last_update_datetime = " + ClsDB.sqlEncode(DateTime.Now.ToString()) + ", " +
            disable +
            " WHERE ID = " + ID;
        ;

        new ClsDB().ExecuteNonQuery(this._strQuery);
    }
    
    public static string validate_pwd(string p1, string p2) {
        string s = "";
        if (p1 != p2) {
            s = "New passwords not match.";
        }
        if (p1.Length < 6)
        {
            s = "Password must be at least 6 characters in length.";
        }
        return s;
    }

    public static string writeUserTypeList(string id, string name, string value) {
        string s = "<option value=''>-- Selecte One --</option>";

        string selected;
        
        selected = (value == "1" ? " selected" : "");
        s += "<option value='1'" + selected + ">Admin</option>";

        selected = (value == "2" ? " selected" : "");
        s += "<option value='2'" + selected + ">User</option>";

        s = "<select id='" + id + "' name='" + name + "'>" + s + "</select>";

        return s;
    }

    public static string getUserType(string type_id) {
        string s = "SELECT name FROM UserType WHERE ID = " + ClsDB.sqlEncode(type_id);
        return new ClsDB().ExecuteScalar(s);
    }


    public static string writeUserStatusList(string id, string name, string value) {
        string s = "<option value=''>-- Selecte One --</option>";

        string selected;
        value = value.ToLower();

        selected = ((value == "false" || value == "0") ? " selected" : "");
        s += "<option value='0'" + selected + ">False</option>";

        selected = ((value == "true" || value == "1") ? " selected" : "");
        s += "<option value='1'" + selected + ">True</option>";

        s = "<select id='" + id + "' name='" + name + "'>" + s + "</select>";

        return s;
    }

    public static string getUserNameById(string ID) {
        string s = "SELECT login FROM [User] WHERE ID = " + ClsDB.sqlEncode(ID);
        return new ClsDB().ExecuteScalar(s);
    }

}
