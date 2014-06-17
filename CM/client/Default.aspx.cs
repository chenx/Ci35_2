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

public partial class client_default : System.Web.UI.Page
{
    ClsClient client = new ClsClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack) { this.client.download_pdf(ClsUtil.getStrVal(Request["print_id"])); }
        ShowAtoZList();
        ShowClientList();
    }

    void ShowAtoZList() {
        string s = "<a href='default.aspx?ini='>All</a> &nbsp; ";
        
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int i = 0; i < 26; ++i) {
            char c = alphabet[i];

            int ct = new ClsDB().getCount("SELECT COUNT(*) FROM [Client] WHERE disabled='0' AND last_name like '" + c + "%'");

            if (ct > 0)
            {
                s += "<a href='default.aspx?ini=" + c + "'>" + c + "</a> &nbsp; ";
            }
            else {
                s += c + " &nbsp; ";
            }
        }

        this.AtoZ.Text = s;
    }

    void ShowClientList() {
        string strQuery = 
@"SELECT C.ID, C.Case_Id, C.Client_Type, T.name As Client_Type_Name, C.First_Name, C.Last_Name, 
A.name AS Attorney, P.name AS Paralegal, C.Date_Of_Injury 
FROM Client C 
LEFT OUTER JOIN ClientType T ON C.Client_Type = T.ID 
LEFT OUTER JOIN Attorney A ON C.Attorney = A.ID
LEFT OUTER JOIN Paralegal P ON C.Paralegal = P.ID
WHERE C.disabled = 0";

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
                    string color = ((i % 2 == 0) ? " bgcolor='#ffffff'" : "");
                    s += addRow(dt.Rows[i], color);
                }
            }
            s = "<table class='T2'>" + this.addHdr() + s + "</table>";
        }
        ClientList.Text = s;
    }

    string addHdr() {
        string s = "";
        s += "<th>View</th>";
        s += "<th>Edit</th>";
        s += "<th title='Download PDF'>Download</th>";
        for (int i = 0, n = this.client.list_cols.Length; i < n; ++i)
        {
            s += "<th align='center'>&nbsp;" + this.client.list_cols[i] + "&nbsp;</th>";
        }
        s = "<tr bgcolor='#99cc99'>" + s + "</tr>";
        return s;
    }

    string addRow(DataRow sdr, string color) {
        string id = sdr["ID"].ToString();
        string case_id = sdr["Case_Id"].ToString();

        string s = "";
        s += "<td><a href='client_view.aspx?id=" + id + "&src=u'>View</a></td>";
        s += "<td><a href='client_edit.aspx?id=" + id + "&src=u'>Edit</a></td>";
        s += "<td><a href='#' onclick='javascript: do_print(" + id + ");' title='Download PDF'>Download</a></td>";
        s += "<td>&nbsp;" + sdr["Case_Id"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Client_Type_Name"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["First_Name"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Last_Name"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Attorney"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + sdr["Paralegal"].ToString() + "&nbsp;</td>";
        s += "<td>&nbsp;" + ClsUtil.formatDate( sdr["Date_Of_Injury"].ToString() ) + "&nbsp;</td>";
        
        s = "<tr" + color + ">" + s + "</tr>";
        return s;
    }
}
