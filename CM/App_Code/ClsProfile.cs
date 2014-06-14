using System;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for ClsUser
/// </summary>
public class ClsProfile
{
    private string _strQuery;

    public string ID;
    public string first_name;
    public string last_name;
    public string email;
    public string login;
    public string note;

    public ClsProfile()
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
        note = "";
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
        }
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
        }
    }

    public string check_error() {
        string s = "";

        if (this.first_name == "")
        {
            s += "First name cannot be empty<br>";
        }
        if (this.last_name == "")
        {
            s += "Last name cannot be empty<br>";
        }
        if (!ClsUtil.IsValidEmail(this.email))
        {
            s += "Email is not valid<br>";
        }

        return s;
    }

    public void update(string ID, string update_user_id) {
        if (ID == "") return;

        string err = check_error();
        if (err != "") throw new Exception(err); 

        this._strQuery = "UPDATE [User] SET " +
            "First_Name = " + ClsDB.sqlEncode( this.first_name ) + ", " +
            "Last_Name = " + ClsDB.sqlEncode( this.last_name ) + ", " +
            "Email = " + ClsDB.sqlEncode( this.email ) + ", " +
            "Note = " + ClsDB.sqlEncode( this.note ) + ", " +
            "last_update_user_id = " + ClsDB.sqlEncode( update_user_id ) + ", " +
            "last_update_datetime = " + ClsDB.sqlEncode( DateTime.Now.ToString() ) + 
            " WHERE ID = " + ClsDB.sqlEncode(ID);
        ;

        new ClsDB().ExecuteNonQuery(this._strQuery);
    }
}
