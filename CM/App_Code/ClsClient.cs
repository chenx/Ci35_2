using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

/// <summary>
/// Functions for a client.
/// </summary>
public class ClsClient
{
    private string _strQuery; 

    /// <summary>
    /// Client Table fields.
    /// </summary>
    public string Case_Id;
    public string Client_Type;
    public string First_Name;
    public string Last_Name;
    public string Attorney;
    public string Paralegal;
    public string Date_Of_Injury;
    public string Statute_Of_Limitation;
    public string Phone_Number;
    public string Address;
    public string Date_Of_Birth;
    public string Social_Security_Number;
    public string Case_Type;
    public string At_Fault_Party;
    public string Settlement_Type;
    public string Settlement_Amount;
    public string Disposition;
    public string Case_Notes;
    public string Date_For_Perspective_Client;
    
    /// <summary>
    /// All columns.
    /// </summary>
    public string[] cols = { 
        "Case_Id", "Client_Type", "First_Name", "Last_Name", "Attorney", "Paralegal",
        "Date_Of_Injury", "Statute_Of_Limitation", "Phone_Number", "Address", 
        "Date_Of_Birth", "Social_Security_Number",
        "Case_Type", "At_Fault_Party", "Settlement_Type", "Settlement_Amount", 
        "Disposition", "Case_Notes",
        "Date_For_Perspective_Client" 
    };

    /// <summary>
    /// Columns used for listing.
    /// </summary>
    public string[] list_cols = { 
        "Case Id", "Client Type", "First Name", "Last Name", "Attorney", "Paralegal",
        "Date Of Injury"
    };

    public string strQuery() { return _strQuery; }

    public ClsClient()
    {
        this.clear();
    }

    private void clear() {
        this.Case_Id = "";
        this.Client_Type = "";
        this.First_Name = "";
        this.Last_Name = "";
        this.Attorney = "";
        this.Paralegal = "";
        this.Date_Of_Injury = "";
        this.Statute_Of_Limitation = "";
        this.Phone_Number = "";
        this.Address = "";
        this.Date_Of_Birth = "";
        this.Social_Security_Number = "";
        this.Case_Type = "";
        this.At_Fault_Party = "";
        this.Settlement_Type = "";
        this.Settlement_Amount = "";
        this.Disposition = "";
        this.Case_Notes = "";
        this.Date_For_Perspective_Client = "";
    }

    public void retrieveDB(string ID)
    {
        this.clear();

        try
        {
            string strConn = new ClsDB().strConn(); 
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                this._strQuery = "SELECT * FROM Client WHERE ID = " + ClsDB.sqlEncode(ID);
                SqlCommand comm = new SqlCommand(this._strQuery, conn);

                conn.Open();
                using (SqlDataReader sdr = comm.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        this.Case_Id = ClsUtil.getStrVal(sdr["Case_Id"]);
                        this.Client_Type = ClsUtil.getStrVal(sdr["Client_Type"]);
                        this.First_Name = ClsUtil.getStrVal(sdr["First_Name"]);
                        this.Last_Name = ClsUtil.getStrVal(sdr["Last_Name"]);
                        this.Attorney = ClsUtil.getStrVal(sdr["Attorney"]);
                        this.Paralegal = ClsUtil.getStrVal(sdr["Paralegal"]);
                        this.Date_Of_Injury = ClsUtil.formatDate(sdr["Date_Of_Injury"].ToString());
                        this.Statute_Of_Limitation = ClsUtil.getStrVal(sdr["Statute_Of_Limitation"]);
                        this.Phone_Number = ClsUtil.getStrVal(sdr["Phone_Number"]);
                        this.Address = ClsUtil.getStrVal(sdr["Address"]);
                        this.Date_Of_Birth = ClsUtil.formatDate(sdr["Date_Of_Birth"].ToString());
                        this.Social_Security_Number = ClsUtil.getStrVal(sdr["Social_Security_Number"]);
                        this.Case_Type = ClsUtil.getStrVal(sdr["Case_Type"]);
                        this.At_Fault_Party = ClsUtil.getStrVal(sdr["At_Fault_Party"]);
                        this.Settlement_Type = ClsUtil.getStrVal(sdr["Settlement_Type"]);
                        this.Settlement_Amount = ClsUtil.getStrVal(sdr["Settlement_Amount"]);
                        this.Disposition = ClsUtil.getStrVal(sdr["Disposition"]);
                        this.Case_Notes = ClsUtil.getStrVal(sdr["Case_Notes"]);
                        this.Date_For_Perspective_Client = ClsUtil.formatDate(sdr["Date_For_Perspective_Client"].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void retrieveRequest(bool IsPostBack, HttpRequest Request)
    {
        this.clear();

        if (IsPostBack)
        {
            this.Case_Id = ClsUtil.getStrValTrim( Request["txtCaseId"] ).ToString();
            this.Client_Type = ClsUtil.getStrValTrim(Request["txtClientType"]).ToString();
            this.First_Name = ClsUtil.getStrValTrim(Request["txtFirstName"]).ToString();
            this.Last_Name = ClsUtil.getStrValTrim(Request["txtLastName"]).ToString();
            this.Attorney = ClsUtil.getStrValTrim(Request["txtAttorney"]).ToString();
            this.Paralegal = ClsUtil.getStrValTrim(Request["txtParalegal"]).ToString();
            this.Date_Of_Injury = ClsUtil.getStrValTrim(Request["txtDateOfInjury"]).ToString();
            this.Statute_Of_Limitation = ClsUtil.getStrValTrim(Request["txtStatuteLimit"]).ToString();
            this.Phone_Number = ClsUtil.getStrValTrim(Request["txtPhone"]).ToString();
            this.Address = ClsUtil.getStrValTrim(Request["txtAddress"]).ToString();
            this.Date_Of_Birth = ClsUtil.getStrValTrim(Request["txtDOB"]).ToString();
            this.Social_Security_Number = ClsUtil.getStrValTrim(Request["txtSSN"]).ToString();
            this.Case_Type = ClsUtil.getStrValTrim(Request["txtCaseType"]).ToString();
            this.At_Fault_Party = ClsUtil.getStrValTrim(Request["txtFaultParty"]).ToString();
            this.Settlement_Type = ClsUtil.getStrValTrim(Request["txtSettleType"]).ToString();
            this.Settlement_Amount = ClsUtil.getStrValTrim(Request["txtSettleAmount"]).ToString();
            this.Disposition = ClsUtil.getStrValTrim(Request["txtDisposition"]).ToString();
            this.Case_Notes = ClsUtil.getStrValTrim(Request["txtCaseNotes"]).ToString();
            this.Date_For_Perspective_Client = ClsUtil.getStrValTrim(Request["txtDateForPersClient"]).ToString();
        }
    }

    public string check_error() {
        string s = "";

        if (this.Case_Id == "") {
            s += "Case Id cannot be empty.<br>";
        }

        if (this.Client_Type == "") {
            s += "Client Type cannot be empty.<br>";
        }

        return s;
    }

    public void update(string ID, string update_user_id)
    {
        if (ID == "") return;

        string err = this.check_error();
        if (err != "") throw new Exception(err);

        this._strQuery = "UPDATE Client SET " +
            "Case_ID = " + ClsDB.sqlEncode( this.Case_Id ) + ", " +
            "Client_Type = " + ClsDB.sqlEncode( this.Client_Type ) + ", " +
            "First_Name = " + ClsDB.sqlEncode( this.First_Name ) + ", " +
            "Last_Name = " + ClsDB.sqlEncode( this.Last_Name ) + ", " +
            "Attorney = " + ClsDB.sqlEncode( this.Attorney ) + ", " +
            "Paralegal = " + ClsDB.sqlEncode( this.Paralegal ) + ", " +
            "Date_Of_Injury = " + ClsDB.sqlEncode( this.Date_Of_Injury ) + ", " +
            "Statute_Of_Limitation = " + ClsDB.sqlEncode( this.Statute_Of_Limitation ) + ", " +
            "Phone_Number = " + ClsDB.sqlEncode( this.Phone_Number ) + ", " +
            "Address = " + ClsDB.sqlEncode( this.Address ) + ", " +
            "Date_Of_Birth = " + ClsDB.sqlEncode( this.Date_Of_Birth ) + ", " +
            "Social_Security_Number = " + ClsDB.sqlEncode( this.Social_Security_Number ) + ", " +
            "Case_Type = " + ClsDB.sqlEncode( this.Case_Type ) + ", " +
            "At_Fault_Party = " + ClsDB.sqlEncode( this.At_Fault_Party ) + ", " +
            "Settlement_Type = " + ClsDB.sqlEncode( this.Settlement_Type ) + ", " +
            "Settlement_Amount = " + ClsDB.sqlEncode( this.Settlement_Amount ) + ", " +
            "Disposition = " + ClsDB.sqlEncode( this.Disposition ) + ", " +
            "Case_Notes = " + ClsDB.sqlEncode( this.Case_Notes ) + ", " +
            "Date_For_Perspective_Client = " + ClsDB.sqlEncode( this.Date_For_Perspective_Client ) + ", " +
            "last_update_user_id = " + ClsDB.sqlEncode( update_user_id  ) + ", " +
            "last_update_datetime = " + ClsDB.sqlEncode(DateTime.Now.ToString()) +
            " WHERE ID = " + ClsDB.sqlEncode(ID);
        ;

        new ClsDB().ExecuteNonQuery(this._strQuery);
    }


    public void insert(string update_user_id)
    {
        string err = this.check_error();
        if (err != "") throw new Exception(err);

        this._strQuery = @"INSERT INTO Client (Case_Id, Client_Type, First_Name, Last_Name, Attorney, Paralegal,
Date_Of_Injury, Statute_Of_Limitation, Phone_Number, Address, Date_Of_Birth, Social_Security_Number,
Case_Type, At_Fault_Party, Settlement_Type, Settlement_Amount, Disposition, Case_Notes,
Date_For_Perspective_Client, [create_user_id], [create_datetime], [disabled]) VALUES (" +
            ClsDB.sqlEncode( this.Case_Id ) + ", " +
            ClsDB.sqlEncode( this.Client_Type ) + ", " +
            ClsDB.sqlEncode( this.First_Name ) + ", " +
            ClsDB.sqlEncode( this.Last_Name ) + ", " +
            ClsDB.sqlEncode( this.Attorney ) + ", " +

            ClsDB.sqlEncode( this.Paralegal ) + ", " +
            ClsDB.sqlEncode( this.Date_Of_Injury ) + ", " +
            ClsDB.sqlEncode( this.Statute_Of_Limitation ) + ", " +
            ClsDB.sqlEncode( this.Phone_Number ) + ", " +
            ClsDB.sqlEncode( this.Address ) + ", " +

            ClsDB.sqlEncode( this.Date_Of_Birth ) + ", " +
            ClsDB.sqlEncode( this.Social_Security_Number ) + ", " +
            ClsDB.sqlEncode( this.Case_Type ) + ", " +
            ClsDB.sqlEncode( this.At_Fault_Party ) + ", " +
            ClsDB.sqlEncode( this.Settlement_Type ) + ", " +

            ClsDB.sqlEncode( this.Settlement_Amount ) + ", " +
            ClsDB.sqlEncode( this.Disposition ) + ", " +
            ClsDB.sqlEncode( this.Case_Notes ) + ", " +
            ClsDB.sqlEncode( this.Date_For_Perspective_Client ) + ", " +

            ClsDB.sqlEncode( update_user_id ) + ", " +
            ClsDB.sqlEncode(DateTime.Now.ToString()) + ", " +
            "0 "
            + ")";

        new ClsDB().ExecuteNonQuery(this._strQuery);
    }


    public static string writeClientTypeList(string id, string name, string value) {
        string s = "<option value=''>-- Selecte One --</option>";

        string selected;
        
        selected = (value == "1" ? " selected" : "");
        s += "<option value='1'" + selected + ">Prospective client</option>";

        selected = (value == "2" ? " selected" : "");
        s += "<option value='2'" + selected + ">Current client</option>";

        selected = (value == "3" ? " selected" : "");
        s += "<option value='3'" + selected + ">Former client</option>";

        s = "<select id='" + id + "' name='" + name + "' onchange='javascript: onClientTypeChange(this.selectedIndex);'>" + s + "</select>";

        return s;
    }

    public static string getClientType(string type_id) {
        string s = "SELECT name FROM ClientType WHERE ID = " + ClsDB.sqlEncode(type_id);
        return new ClsDB().ExecuteScalar(s);
    }


    public static string writeAttorneyList(string id, string name, string value)
    {
        return ClsUtil.writeIdNameList(id, name, value, "Attorney");
    }


    public static string writeParalegalList(string id, string name, string value)
    {
        return ClsUtil.writeIdNameList(id, name, value, "Paralegal");
    }


    /// <summary>
    /// Report function. To output a crystal report.
    ///
    /// Reference:
    /// http://www.codeproject.com/Tips/716161/How-to-Export-Crystal-Report-on-Button-Click-in-to
    /// </summary>
    public string download_pdf(string ID)
    {
        if (ID == "")
        {
            return "<font color='green'>Please select a case.</font>";
        }

        string strQuery = @"select * from [V_Client] WHERE ID = " + ClsDB.sqlEncode(ID);
        DataSet ds = new ClsDB().ExecuteDataSet(strQuery);

        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            return "<font color='red'>Case not found.</font>";
        }

        string Case_ID = ds.Tables[0].Rows[0]["Case_Id"].ToString();
        String Client_Type = ds.Tables[0].Rows[0]["Client_Type"].ToString();
        //Response.Write("Client_Type: " + ds1.Tables[0].Rows[0]["Client_Type"]); //return;

        ReportDocument myReportDocument = new ReportDocument();

        string report_source = (Client_Type == "1") ? "../client/CrystalReport1.rpt" : "../client/CrystalReport2.rpt";
        myReportDocument.Load(HttpContext.Current.Server.MapPath(report_source));
        myReportDocument.SetDataSource(ds.Tables[0]);

        myReportDocument.ExportToHttpResponse
            (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, HttpContext.Current.Response, true, "Case_" + Case_ID);

        //myReportDocument.ExportToHttpResponse
        //    (CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, true, "Client_Details_" + Case_ID);

        //myReportDocument.ExportToHttpResponse
        //    (CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Client_Details_" + Case_ID);

        myReportDocument.Close();
        myReportDocument.Dispose();

        return "";
    }

    public string download_pdf_all()
    {
        string strQuery = @"select * from [V_Client]"; 
        DataSet ds = new ClsDB().ExecuteDataSet(strQuery);

        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            return "<font color='red'>Case not found.</font>";
        }

        string Case_ID = ds.Tables[0].Rows[0]["Case_Id"].ToString();
        String Client_Type = ds.Tables[0].Rows[0]["Client_Type"].ToString();
        //Response.Write("Client_Type: " + ds1.Tables[0].Rows[0]["Client_Type"]); //return;

        ReportDocument myReportDocument = new ReportDocument();

        string report_source = "../client/CrystalReport_all.rpt";
        myReportDocument.Load(HttpContext.Current.Server.MapPath(report_source));
        myReportDocument.SetDataSource(ds.Tables[0]);

        myReportDocument.ExportToHttpResponse
            (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, HttpContext.Current.Response, true, "Case_All");

        //myReportDocument.ExportToHttpResponse
        //    (CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, true, "Client_Details_" + Case_ID);

        //myReportDocument.ExportToHttpResponse
        //    (CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, true, "Client_Details_" + Case_ID);

        myReportDocument.Close();
        myReportDocument.Dispose();

        return "";
    }
}
