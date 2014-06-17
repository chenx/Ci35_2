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

public partial class client_view : System.Web.UI.Page
{
    ClsClient client = new ClsClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["ID"] == null) {
            this.msg.Text = "No valid ID is provided.";
            this.form1.Text = "";
        }
        else {
            this.client.retrieveDB(Request["ID"]);
            this.msg.Text = "";
            if (ClsDB.DEBUG()) this.msg.Text += this.client.strQuery();
            this.form1.Text = ShowViewForm();
        }
    }

    string ShowViewForm() {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>Case Id:" + star + "</td><td width='150'>" + this.client.Case_Id + "&nbsp;</td></tr>";
        if (this.client.Client_Type != "1")
        {
            s += "<tr><td>Attorney:</td><td>" + ClsUtil.getNameById("Attorney", this.client.Attorney) + "&nbsp;</td></tr>";
        }
        s += "<tr><td>Paralegal:</td><td>" + ClsUtil.getNameById("Paralegal", this.client.Paralegal) + "&nbsp;</td></tr>";
        s += "<tr><td>Date Of Injury:</td><td>" + this.client.Date_Of_Injury + "&nbsp;</td></tr>";
        s += "<tr><td>Statute Of Limitation:</td><td>" + this.client.Statute_Of_Limitation + "&nbsp;</td></tr>";

        if (this.client.Client_Type != "1")
        {
            s += "<tr><td>Phone Number:</td><td>" + this.client.Phone_Number + "&nbsp;</td></tr>";
            s += "<tr><td>Address:</td><td>" + this.client.Address + "&nbsp;</td></tr>";
            s += "<tr><td>Date Of Birth:</td><td>" + this.client.Date_Of_Birth + "&nbsp;</td></tr>";
            s += "<tr><td>Social Security Number:</td><td>" + this.client.Social_Security_Number + "&nbsp;</td></tr>";
        }

        s = "<table class='T1'>" + s + "</table>";

        string t = "";

        t += "<tr><td>Client Type:" + star + "</td><td width='250'>" + ClsClient.getClientType(this.client.Client_Type) + "&nbsp;</td></tr>";
        t += "<tr><td>First Name:</td><td>" + this.client.First_Name + "&nbsp;</td></tr>";
        t += "<tr><td>Last Name:</td><td>" + this.client.Last_Name + "&nbsp;</td></tr>";
        t += "<tr><td>Case Type:</td><td>" + this.client.Case_Type + "&nbsp;</td></tr>";

        if (this.client.Client_Type != "1")
        {
            t += "<tr><td>At Fault Party:</td><td>" + this.client.At_Fault_Party + "&nbsp;</td></tr>";
            t += "<tr><td>Settlement Type:</td><td>" + this.client.Settlement_Type + "&nbsp;</td></tr>";
            t += "<tr><td>Settlement Amount:</td><td>" + this.client.Settlement_Amount + "&nbsp;</td></tr>";
        }
        t += "<tr><td>Disposition:</td><td>" + this.client.Disposition + "&nbsp;</td></tr>";

        if (this.client.Client_Type != "2" && this.client.Client_Type != "3")
        {
            t += "<tr><td>Case Notes:</td><td><pre class='text_area'>" + this.client.Case_Notes + "</pre>&nbsp;</td></tr>";
            t += "<tr><td>Date For Perspective Client:</td><td>" + this.client.Date_For_Perspective_Client + "&nbsp;</td></tr>";
        }

        t = "<table class='T1'>" + t + "</table>";

        return "<table><tr><td valign='top'>" + s + 
            "</td><td width='50'><br></td><td valign='top'>" + t + "</td></tr></table>";
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        this.client.download_pdf(ClsUtil.getStrVal(Request["ID"]));
    }
}
