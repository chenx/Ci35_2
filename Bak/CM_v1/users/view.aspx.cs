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
    private string ID;
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

        ID = ClsUtil.getStrVal(Request["id"]);
        if (ID == "") {
            return;
        }

        this.retrieve(ID);
        this.lblProfile.Text = this.ShowViewForm();
    }

    private void retrieve(string ID)
    {
        ClsUtil u = ClsUtil.Instance();
        string strQuery = "SELECT * FROM [User] WHERE ID = " + ClsUtil.sqlEncode( ID );

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
                    this.first_name = ClsUtil.getStrVal( sdr["first_name"] );
                    this.last_name = ClsUtil.getStrVal( sdr["last_name"] );
                    this.email = ClsUtil.getStrVal( sdr["email"] );
                    this.login = ClsUtil.getStrVal( sdr["login"] );
                    this.note = ClsUtil.getStrVal( sdr["note"] );
                    this.gid = ClsUtil.getStrVal( sdr["gid"] );

                    this.create_by = ClsUser.getUserNameById(ClsUtil.getStrVal(sdr["create_User_id"]));
                    this.create_datetime = ClsUtil.getStrVal(sdr["create_datetime"]);
                    this.last_update_by = ClsUser.getUserNameById( ClsUtil.getStrVal(sdr["last_update_User_id"]) );
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


    string ShowViewForm()
    {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>First Name:</td><td width='150'>" + this.first_name + "&nbsp;</td></tr>";
        s += "<tr><td>Last Name:</td><td>" + this.last_name + "&nbsp;</td></tr>";
        s += "<tr><td>Email:</td><td>" + this.email + "&nbsp;</td></tr>";
        s += "<tr><td>Login:</td><td>" + this.login + "&nbsp;</td></tr>";
        s += "<tr><td>Note:</td><td>" + this.note + "&nbsp;</td></tr>";

        s += "<tr><td>Created By:</td><td>" + ClsUtil.textboxEncode(this.create_by) + "&nbsp;</td></tr>";
        s += "<tr><td>Created Datetime:</td><td>" + ClsUtil.textboxEncode(this.create_datetime) + "&nbsp;</td></tr>";
        s += "<tr><td>Last Updated By:</td><td>" + ClsUtil.textboxEncode(this.last_update_by) + "&nbsp;</td></tr>";
        s += "<tr><td>Last Updated Datetime:</td><td>" + ClsUtil.textboxEncode(this.last_update_datetime) + "&nbsp;</td></tr>";
        s += "<tr><td>Disabled:</td><td>" + ClsUtil.textboxEncode(this.disabled) + "&nbsp;</td></tr>";

        return "<table border='1' cellpadding='3' cellspacing='1'>" + s + "</table>";
    }
}
