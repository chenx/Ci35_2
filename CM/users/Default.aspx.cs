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

public partial class user_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClsAuth.check_auth_admin();
        ShowList();
    }

    void ShowList() {
        string strQuery = 
@"SELECT U.ID, U.First_Name, U.Last_Name, U.email, U.login, 
U.passwd, U.note, U.gid, G.name As UserType, U.disabled
FROM [User] U LEFT OUTER JOIN UserGroup G ON U.gid = G.ID ORDER BY U.ID ASC";

        string s = "";
        DataSet ds = new ClsDB().ExecuteDataSet(strQuery);
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    string color = ((i % 2 == 0) ? " bgcolor='#ffffff'" : "");
                    s += addRow(dt.Rows[i], color);
                }
            }
            s = "<table class='T1'>" + this.addHdr() + s + "</table>";
        }
        ClientList.Text = s;

        /*
         * Get same result as above code.
         * 
        ClsUtil u = ClsUtil.Instance();
        string ret = "";
        string strConn = u.strConn();
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = comm.ExecuteReader();

            int ct = 0;
            while (sdr.Read()) {
                ++ct;
                string color = ((ct % 2 == 0) ? " bgcolor='#eeeeee'" : "");
                ret += addRow(sdr, color);
            }
            ret = "<table border='1' cellpadding='2' cellspacing='2'>" + this.addHdr() + ret + "</table>";
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
        finally {
            if (sdr != null) sdr.Close();
            conn.Close();
        }

        ClientList.Text = ret;
        */
    }

    string addHdr() {
        string s = "";
        s += "<td>View</td>";
        s += "<td>Edit</td>";
        s += "<td>Disable</td>";
        s += "<td>Delete</td>";
        s += "<td align='center'>&nbsp;ID&nbsp;</td>";
        s += "<td align='center'>&nbsp;First Name&nbsp;</td>";
        s += "<td align='center'>&nbsp;Last Name&nbsp;</td>";
        s += "<td align='center'>&nbsp;Email&nbsp;</td>";
        s += "<td align='center'>&nbsp;Login&nbsp;</td>";
        s += "<td align='center'>&nbsp;User Type&nbsp;</td>";
        s += "<td align='center'>&nbsp;Disabled&nbsp;</td>";
        s = "<tr bgcolor='#99cc99'>" + s + "</tr>";
        return s;
    }

    string addRow(DataRow sdr, string color) {
        string id = sdr["ID"].ToString();

        string s = "";
        s += "<td><a href='view.aspx?id=" + id + "'>View</a></td>";
        s += "<td><a href='edit.aspx?id=" + id + "'>Edit</a></td>";
        if (sdr["disabled"].ToString() == "True")
        {
            s += "<td><a href='#' onclick='javascript:enable_user(\"" + id + "\");'>Enable</a></td>";
        }
        else {
            s += "<td><a href='#' onclick='javascript:disable_user(\"" + id + "\");'>Disable</a></td>";
        }
        s += "<td><a href='#' onclick='javascript:delete_user_permanently(\"" + id + "\");'>Delete</a></td>";

        s += "<td>&nbsp;" + id + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["First_Name"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Last_Name"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Email"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Login"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["UserType"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["disabled"].ToString() + "&nbsp;</td>";

        if (sdr["disabled"].ToString() == "True") { color += " style='color: #999999;'"; }
        s = "<tr" + color + ">" + s + "</tr>";
        return s;
    }
}
