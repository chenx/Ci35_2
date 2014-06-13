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
            try
            {
                this.update(ID);
                this.msg.Text = "<p><font color='green'>Your profile has been updated.</font> </p>";
            }
            catch (Exception ex)
            {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
            }
            this.retrievePostVal();
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
        }
    }

    string ShowEditForm()
    {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:" + star + "</td><td><input type='text' maxlength='10' id='txtFirstName' name='txtFirstName' value='" + this.first_name + "'></td></tr>";
        s += "<tr><td>Last Name:" + star + "</td><td><input type='text' maxlength='100' id='txtLastName' name='txtLastName' value='" + ClsUtil.textboxEncode(this.last_name) + "'></td></tr>";
        s += "<tr><td>Email:" + star + "</td><td><input type='text' maxlength='100' id='txtEmail' name='txtEmail' value='" + ClsUtil.textboxEncode(this.email) + "'></td></tr>";
        s += "<tr><td>Login:</td><td>&nbsp;" + ClsUtil.textboxEncode(this.login) + " <input type='hidden' id='txtLogin' name='txtLogin' value='" + ClsUtil.textboxEncode(this.login) + "'></td></tr>";
        s += "<tr><td>Note:</td><td><input type='text' maxlength='100' id='txtNote' name='txtNote' value='" + ClsUtil.textboxEncode(this.note) + "'></td></tr>";

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
            //"Login = " + ClsUtil.sqlEncode(Request["txtLogin"]) + ", " +
            "Note = " + ClsUtil.sqlEncode(Request["txtNote"]) +
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
