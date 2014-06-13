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

public partial class home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClsAuth.check_auth_admin();

        if (this.IsPostBack)
        {
            this.form1.Text = "";
            try
            {
                this.add();
                this.msg.Text = "<font color='green'>The new user has been added.</font> <br/><br/><a href='new.aspx'>Add Another New User</a>";
            }
            catch (Exception ex) {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
                this.form1.Text = ShowNewForm();
            }
            
        }
        else
        {
            this.msg.Text = "";
            this.form1.Text = ShowNewForm();
        }
    }

    string ShowNewForm() {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:" + star + "</td><td><input type='text' maxlength='100' id='txtFirstName' name='txtFirstName' value='" + getPostVal("txtFirstName") + "'></td></tr>";
        s += "<tr><td>Last Name:" + star + "</td><td><input type='text' maxlength='100' id='txtLastName' name='txtLastName' value='" + getPostVal("txtLastName") + "'></td></tr>";
        s += "<tr><td>Email:" + star + "</td><td><input type='text' maxlength='100' id='txtEmail' name='txtEmail' value='" + getPostVal("txtEmail") + "'></td></tr>";
        s += "<tr><td>Login:" + star + "</td><td><input type='text' maxlength='100' id='txtLogin' name='txtLogin' value='" + getPostVal("txtLogin") + "'></td></tr>";
        s += "<tr><td>Password:" + star + "</td><td><input type='password' maxlength='100' id='txtPwd' name='txtPwd' value='" + getPostVal("txtPwd") + "'></td></tr>";
        s += "<tr><td>Password (repeat):" + star + "</td><td><input type='password' maxlength='100' id='txtPwd2' name='txtPwd2' value='" + getPostVal("txtPwd2") + "'></td></tr>";
        s += "<tr><td>Note:</td><td><input type='text' maxlength='100' id='txtNote' name='txtNote' value='" + getPostVal("txtNote") + "'></td></tr>";
        s += "<tr><td>User Type:" + star + "</td><td>" + ClsUser.writeUserTypeList("txtUserType", "txtUserType", getPostVal("txtUserType")) + "</td></tr>";
        //s += "<tr><td>Disabled:</td><td>" + ClsUser.writeUserStatusList("txtDisabled", "txtDisabled", getPostVal("txtDisabled")) + "</td></tr>";

        s = "<table border='1' cellpadding='3' cellspacing='1'>" + s + "</table>";

        return "<table><tr><td valign='top'>" + s + "</td></table>" +
            "<br/><input value=\"Add New User\" type=\"button\" onclick=\"javascript:add();\" />";
    }

    private string getPostVal(string o) {
        if (this.IsPostBack && Request[o] != null)
        {
            return Request[o].ToString().Trim();
        }
        else {
            return "";
        }
    }

        
    void add() {
        ClsUtil u = ClsUtil.Instance();
        string strQuery = @"INSERT INTO [User] (first_name, last_name, email, login, passwd, note, gid,
                            [create_user_id], [create_datetime]) VALUES (" +
            ClsUtil.sqlEncode(Request["txtFirstName"]) + ", " +
            ClsUtil.sqlEncode(Request["txtLastName"]) + ", " +
            ClsUtil.sqlEncode(Request["txtEmail"]) + ", " +
            ClsUtil.sqlEncode(Request["txtLogin"]) + ", " +
            "HASHBYTES('MD5', " + ClsUtil.sqlEncode( Request["txtPwd"] ) + "), " +
            ClsUtil.sqlEncode(Request["txtNote"]) + ", " +
            ClsUtil.sqlEncode(Request["txtUserType"]) + ", " +
            ClsUtil.sqlEncode(Session["userid"].ToString()) + ", " +
            ClsUtil.sqlEncode(DateTime.Now.ToString()) + 
            ")";

        if (ClsUtil._DEBUG) Response.Write(strQuery);

        string strConn = u.strConn();
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(strConn);
            SqlCommand comm = new SqlCommand(strQuery, conn);

            conn.Open();
            comm.ExecuteNonQuery();
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
        finally {
            if (conn != null) conn.Close();
        }        
    }

}
