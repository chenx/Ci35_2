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

public partial class profile : System.Web.UI.Page
{
    private string first_name;
    private string last_name;
    private string email;
    private string login;
    private string passwd;
    private string note;
    private string gid;
    private string create_by;
    private string create_datetime;
    private string last_update_by;
    private string last_update_datetime;
    private string disabled;

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
                this.update(ID);
                this.msg.Text = "<p><font color='green'>This profile has been updated.</font> </p>";
            }
            catch (Exception ex)
            {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
            }
            //this.retrievePostVal();
            this.retrieve(ID);
            this.form1.Text = ShowEditForm();
        }
        else
        {
            this.retrieve(ID);
            this.msg.Text = "";
            this.form1.Text = ShowEditForm();
        }
    }

    private void retrieve(string ID)
    {
        ClsUtil u = ClsUtil.Instance();
        string strQuery = "SELECT * FROM [User] WHERE ID = " + ClsUtil.sqlEncode(ID);

        if (ClsUtil._DEBUG) Response.Write(strQuery);

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
    }

    private void retrievePostVal()
    {
        if (this.IsPostBack) { 
            this.first_name = ClsUtil.getPostVal(Request["txtFirstName"]);
            this.last_name = ClsUtil.getPostVal(Request["txtLastName"]);
            this.email = ClsUtil.getPostVal(Request["txtEmail"]);
            this.login = ClsUtil.getPostVal(Request["txtLogin"]);
            this.note = ClsUtil.getPostVal(Request["txtNote"]);
            this.gid = ClsUtil.getPostVal(Request["txtGid"]);

            this.create_by = ClsUtil.getPostVal(Request["create_User_id"]);
            this.create_datetime = ClsUtil.getPostVal(Request["create_datetime"]);
            this.last_update_by = ClsUtil.getPostVal(Request["last_update_User_id"]);
            this.last_update_datetime = ClsUtil.getPostVal(Request["last_update_datetime"]);
            this.disabled = ClsUtil.getPostVal(Request["disabled"]);
        }
    }

    string ShowEditForm()
    {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:" + star + "</td><td><input type='text' maxlength='10' id='txtFirstName' name='txtFirstName' value='" + this.first_name + "'></td></tr>";
        s += "<tr><td>Last Name:" + star + "</td><td><input type='text' maxlength='100' id='txtLastName' name='txtLastName' value='" + ClsUtil.textboxEncode(this.last_name) + "'></td></tr>";
        s += "<tr><td>Email:" + star + "</td><td><input type='text' maxlength='100' id='txtEmail' name='txtEmail' value='" + ClsUtil.textboxEncode(this.email) + "'></td></tr>";
        s += "<tr><td>Login:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.login) + "</td></tr>";
        s += "<tr><td>Note:</td><td><input type='text' maxlength='100' id='txtNote' name='txtNote' value='" + ClsUtil.textboxEncode(this.note) + "'></td></tr>";

        s += "<tr><td>User Type:" + star + "</td><td>&nbsp;" + ClsUser.writeUserTypeList("txtUserType", "txtUserType", this.gid) + "</td></tr>";

        s += "<tr><td>Created By:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.create_by) + "</td></tr>";
        s += "<tr><td>Created Datetime:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.create_datetime) + "</td></tr>";
        s += "<tr><td>Last Updated By:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.last_update_by) + "</td></tr>";
        s += "<tr><td>Last Updated Datetime:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.last_update_datetime) + "</td></tr>";
        s += "<tr><td>Disabled:</td><td>&nbsp;" + ClsUser.writeUserStatusList("txtDisabled", "txtDisabled", this.disabled) + "</td></tr>";

        s = "<table border='1' cellpadding='3' cellspacing='1'>" + s + "</table>";

        return "<table>" + s + "</table>" +
            "<br/><input value=\"Submit Change\" type=\"button\" onclick=\"javascript:update();\" />";
    }


    void update(string ID)
    {
        if (ID == "") return;

        ClsUtil u = ClsUtil.Instance();
        string strQuery = "UPDATE [User] SET " +
            "First_Name = " + ClsUtil.sqlEncode(Request["txtFirstName"]) + ", " +
            "Last_Name = " + ClsUtil.sqlEncode(Request["txtLastName"]) + ", " +
            "Email = " + ClsUtil.sqlEncode(Request["txtEmail"]) + ", " +
            "Note = " + ClsUtil.sqlEncode(Request["txtNote"]) + ", " +
            "gid = " + ClsUtil.sqlEncode(Request["txtUserType"]) + ", " +
            "last_update_user_id = " + ClsUtil.sqlEncode( Session["userid"].ToString() ) + ", " +
            "last_update_datetime = " + ClsUtil.sqlEncode( DateTime.Now.ToString() ) + ", " +
            "disabled = " + ClsUtil.sqlEncode(Request["txtDisabled"]) + 

            " WHERE ID = " + ID;
        ;

        //if (ClsUtil._DEBUG) Response.Write(strQuery);

        string strConn = u.strConn();
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(strConn);
            SqlCommand comm = new SqlCommand(strQuery, conn);

            conn.Open();
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (conn != null) conn.Close();
        }
    }
}
