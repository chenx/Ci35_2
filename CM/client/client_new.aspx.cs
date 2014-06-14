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

public partial class client_new : System.Web.UI.Page
{
    ClsClient client = new ClsClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            this.form1.Text = "";
            try
            {
                this.client.retrieveRequest(this.IsPostBack, Request);
                this.client.insert(Session["userid"].ToString());
                this.msg.Text = "<font color='green'>The new client has been added.</font> <br/><br/><a href='client_new.aspx'>Add Another New Client</a>";
                if (ClsDB.DEBUG()) this.msg.Text += "<br/>" + this.client.strQuery();
            }
            catch (Exception ex) {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
                this.form1.Text = ShowNewForm();
            }
        }
        else
        {
            this.msg.Text = "";
            this.client.Case_Id = this.getNextCaseId().ToString();
            this.form1.Text = ShowNewForm();
        }
    }

    private int getNextCaseId() {
        string s = new ClsDB().ExecuteScalar("SELECT Max(Case_Id) FROM [Client]");
        return Convert.ToInt32(s) + 1;
    }

    string ShowNewForm() {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>Case Id:" + star + "</td><td><input type='text' maxlength='10' id='txtCaseId' name='txtCaseId' value='" + this.client.Case_Id + "' onkeypress=\"javascript: return numberOnly(event);\"></td></tr>";
        s += "<tr id='trAttorney'><td>Attorney:</td><td>" + ClsClient.writeAttorneyList("txtAttorney", "txtAttorney", this.client.Attorney) + "</td></tr>";
        s += "<tr><td>Paralegal:</td><td>" + ClsClient.writeParalegalList("txtParalegal", "txtParalegal", this.client.Paralegal) + "</td></tr>";
        s += "<tr><td>Date Of Injury:</td><td><input type='text' class='datetime' maxlength='10' id='txtDateOfInjury' name='txtDateOfInjury' value='" + this.client.Date_Of_Injury + "'></td></tr>";
        s += "<tr><td>Statute Of Limitation:</td><td><input type='text' maxlength='100' id='txtStatuteLimit' name='txtStatuteLimit' value='" + this.client.Statute_Of_Limitation + "'></td></tr>";
        s += "<tr><td>Phone Number:</td><td><input type='text' maxlength='100' id='txtPhone' name='txtPhone' value='" + this.client.Phone_Number + "'></td></tr>";
        s += "<tr><td>Address:</td><td><input type='text' maxlength='100' id='txtAddress' name='txtAddress' value='" + this.client.Address + "'></td></tr>";
        s += "<tr><td>Date Of Birth:</td><td><input type='text' class='datetime' maxlength='10' id='txtDOB' name='txtDOB' value='" + this.client.Date_Of_Birth + "'></td></tr>";
        s += "<tr><td>Social Security Number:</td><td><input type='text' maxlength='11' id='txtSSN' name='txtSSN' value='" + this.client.Social_Security_Number + "'></td></tr>";
        
        s = "<table border='1' cellpadding='3' cellspacing='1'>" + s + "</table>";

        string t = "";

        t += "<tr><td>Client Type:" + star + "</td><td>" + ClsClient.writeClientTypeList("txtClientType", "txtClientType", this.client.Client_Type) + "</td></tr>";
        t += "<tr><td>First Name:</td><td><input type='text' maxlength='100' id='txtFirstName' name='txtFirstName' value='" + this.client.First_Name + "'></td></tr>";
        t += "<tr><td>Last Name:</td><td><input type='text' maxlength='100' id='txtLastName' name='txtLastName' value='" + this.client.Last_Name + "'></td></tr>";
        t += "<tr><td>Case Type:</td><td><input type='text' maxlength='100' id='txtCaseType' name='txtCaseType' value='" + this.client.Case_Type + "'></td></tr>";
        t += "<tr><td>At Fault Party:</td><td><input type='text' maxlength='100' id='txtFaultParty' name='txtFaultParty' value='" + this.client.At_Fault_Party + "'></td></tr>";
        t += "<tr><td>Settlement Type:</td><td><input type='text' maxlength='100' id='txtSettleType' name='txtSettleType' value='" + this.client.Settlement_Type + "'></td></tr>";
        t += "<tr><td>Settlement Amount:</td><td><input type='text' maxlength='100' id='txtSettleAmount' name='txtSettleAmount' value='" + this.client.Settlement_Amount + "'></td></tr>";
        t += "<tr><td>Disposition:</td><td><input type='text' maxlength='100' id='txtDisposition' name='txtDisposition' value='" + this.client.Disposition + "'></td></tr>";
        t += "<tr><td>Case Notes:</td><td><textarea rows='10' cols='30' id='txtCaseNotes' name='txtCaseNotes'>" + this.client.Case_Notes + "</textarea></td></tr>";
        t += "<tr><td>Date For Perspective Client:</td><td><input type='text' class='datetime' maxlength='10' id='txtDateForPersClient' name='txtDateForPersClient' value='" + this.client.Date_For_Perspective_Client + "'></td></tr>";

        t = "<table border='1' cellpadding='3' cellspacing='1'>" + t + "</table>";

        return "<table><tr><td valign='top'>" + s + 
            "</td><td width='50'><br></td><td valign='top'>" + t + "</td></tr></table>" +
            "<br/><input value=\"Add New Client\" type=\"button\" onclick=\"javascript:add();\" />";
    }
}
