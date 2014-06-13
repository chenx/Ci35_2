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

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class client_print : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.CrystalReportViewer1.Dispose(); 
        //GC.Collect(); return;
 
        this.CrystalReportViewer1.Visible = false;
        this.listCases.Text = this.writeClientIdNameList("listCase", "listCase", ClsUtil.getStrVal(Request["listCase"]));
        if (this.IsPostBack) {
            bind();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        bind();
    }

    private void bind() {
        string ID = ClsUtil.getStrVal(Request["listCase"]);
        if (ID == "") {
            this.msg.Text = "<font color='green'>Please select a case.</font>";
            return;
        }
        this.msg.Text = "";

        string strQuery = "SELECT * FROM V_Client WHERE ID = " + ClsDB.sqlEncode(ID);
        DataSet ds = new ClsDB().ExecuteDataSet(strQuery);

        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            this.msg.Text = "<font color='red'>Case not found.</font>";
            return;
        }

        String Client_Type = ds.Tables[0].Rows[0]["Client_Type"].ToString();
        //Response.Write("Client_Type: " + ds1.Tables[0].Rows[0]["Client_Type"]); //return;

        ReportDocument myReportDocument = new ReportDocument();

        string report_source = (Client_Type == "1") ? "CrystalReport1.rpt" : "CrystalReport2.rpt";

        myReportDocument.Load(Server.MapPath(report_source));
        myReportDocument.SetDataSource(ds.Tables[0]);

        this.CrystalReportViewer1.ReportSource = myReportDocument;
        this.CrystalReportViewer1.DisplayToolbar = true;
        this.CrystalReportViewer1.Visible = true;
        //myReportDocument.Close();
        //myReportDocument.Dispose();
        //this.CrystalReportViewer1.Dispose();

        GC.Collect();
    }


    protected void btnPDF_Click(object sender, EventArgs e)
    {
        new ClsClient().download_pdf(ClsUtil.getStrVal(Request["listCase"]));
    }


    protected void btnAll_Click(object sender, EventArgs e)
    {
        new ClsClient().download_pdf_all();
    }


    public string writeClientIdNameList(string id, string name, string value)
    {
        string s = "<option value=''>-- Selecte One --</option>";

        string selected;

        DataSet ds = new ClsDB().ExecuteDataSet("SELECT ID, Case_ID FROM [Client]");

        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                string v_id = dt.Rows[i][0].ToString();
                string v_name = dt.Rows[i][1].ToString();
                selected = (value == v_id) ? " selected" : "";
                s += "<option value='" + v_id + "'" + selected + ">Case " + v_name + "</option>";
            }
        }

        s = "<select id='" + id + "' name='" + name + "'>" + s + "</select>";

        return s;
    }
}
