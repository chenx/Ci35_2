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

public partial class client_admin : System.Web.UI.Page
{
    ClsClient client = new ClsClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        ClsAuth.check_auth_admin();
        if (this.IsPostBack) { this.client.download_pdf(ClsUtil.getStrVal(Request["print_id"])); }
        ShowAtoZList();
        ShowClientList();
    }

    void ShowAtoZList() {
        string s = "<a href='admin.aspx?ini='>All</a> &nbsp; ";
        
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int i = 0; i < 26; ++i) {
            char c = alphabet[i];

            int ct = new ClsDB().getCount("SELECT COUNT(*) FROM [Client] WHERE disabled='0' AND last_name like '" + c + "%'");

            if (ct > 0)
            {
                s += "<a href='admin.aspx?ini=" + c + "'>" + c + "</a> &nbsp; ";
            }
            else {
                s += c + " &nbsp; ";
            }
        }

        this.AtoZ.Text = s;
    }

    void ShowClientList() {
        string strQuery = 
@"SELECT C.ID, C.Case_Id, C.Client_Type, T.name As Client_Type_Name, 
C.First_Name, C.Last_Name, A.name AS Attorney, P.name AS Paralegal, C.Date_Of_Injury, C.disabled 
FROM Client C LEFT OUTER JOIN ClientType T ON C.Client_Type = T.ID
LEFT OUTER JOIN Attorney A ON C.Attorney = A.ID
LEFT OUTER JOIN Paralegal P ON C.Paralegal = P.ID";

        string initial = ClsUtil.getStrVal(Request["ini"]);
        strQuery += " AND Last_Name like '" + initial + "%'";
        strQuery += " ORDER BY C.ID ASC";

        string s = "";
        DataSet ds = new ClsDB().ExecuteDataSet(strQuery);
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    string color = ((i % 2 == 0) ? " bgcolor='#eeeeee'" : "");
                    s += addRow(dt.Rows[i], color);
                }
            }
            s = "<table border='1' cellpadding='2' cellspacing='2'>" + this.addHdr() + s + "</table>";
        }
        ClientList.Text = s;
    }

    string addHdr() {
        string s = "";
        s += "<td>View</td>";
        s += "<td>Edit</td>";
        s += "<td style='background: orange;' title='Admin Only Function'>Disable</td>";
        s += "<td style='background: orange;' title='Admin Only Function'>Delete</td>";
        s += "<td title='Download PDF'>Download</td>";

        for (int i = 0, n = this.client.list_cols.Length; i < n; ++i)
        {
            s += "<td align='center'>&nbsp;" + this.client.list_cols[i] + "&nbsp;</td>";
        }
        s = "<tr bgcolor='#ccccee'>" + s + "</tr>";
        return s;
    }

    /// <summary>
    /// Note: "DataRow sdr" is interchangeable with "SqlDataReader sdr".
    /// </summary>
    /// <param name="sdr"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    string addRow(DataRow sdr, string color)
    {
        string id = sdr["ID"].ToString();
        string case_id = sdr["Case_Id"].ToString();

        string s = "";
        s += "<td><a href='client_view.aspx?id=" + id + "&src=a'>View</a></td>";
        s += "<td><a href='client_edit.aspx?id=" + id + "&src=a'>Edit</a></td>";
        if (sdr["disabled"].ToString() == "True")
        {
            s += "<td><a href='#' onclick='javascript:enable_case(\"" + id + "\", \"" + case_id + "\");'>Enable</a></td>";
        }
        else
        {
            s += "<td><a href='#' onclick='javascript:disable_case(\"" + id + "\", \"" + case_id + "\");'>Disable</a></td>";
        }
        s += "<td><a href='#' onclick='javascript:delete_case_permanently(\"" + id + "\", \"" + case_id + "\");'>Delete</a></td>";

        s += "<td><a href='#' onclick='javascript: do_print(" + id + ");' title='Download PDF'>Download</a></td>";
        s += "<td>&nbsp;" + sdr["Case_Id"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Client_Type_Name"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["First_Name"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Last_Name"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Attorney"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Paralegal"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + ClsUtil.formatDate(sdr["Date_Of_Injury"].ToString()) + "&nbsp;</td>";

        if (sdr["disabled"].ToString() == "True") { color += " style='color: #999999;'"; }
        s = "<tr" + color + ">" + s + "</tr>";
        return s;
    }
}
