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
        if (Session["userid"] == null || Session["userid"] == "") {
            return;
        }
        
        this.retrieve(Session["userid"].ToString());
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

        s += "<tr><td>First Name:" + star + "</td><td width='150'>&nbsp;" + this.first_name + "</td></tr>";
        s += "<tr><td>Last Name:</td><td>&nbsp;" + this.last_name + "</td></tr>";
        s += "<tr><td>Email:</td><td>&nbsp;" + this.email + "</td></tr>";
        s += "<tr><td>Login:</td><td>&nbsp;" + this.login + "</td></tr>";
        s += "<tr><td>Note:</td><td>&nbsp;" + this.note + "</td></tr>";

        return "<table border='1' cellpadding='3' cellspacing='1'>" + s + "</table>";
    }
}
