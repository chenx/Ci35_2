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

public partial class client_edit : System.Web.UI.Page
{
    ClsClient client = new ClsClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            this.form1.Text = "";
            this.client.retrieveRequest(this.IsPostBack, Request);

            try
            {
                this.client.update(Request["id"], Session["userid"].ToString());
                this.msg.Text = "<p><font color='green'>The client has been updated.</font> </p>";
            }
            catch (Exception ex) {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
            }
            if (ClsDB.DEBUG()) this.msg.Text += this.client.strQuery();
            this.form1.Text = ShowEditForm();
        }
        else
        {
            if (Request["ID"] == null) {
                this.msg.Text = "No valid ID is provided.";
                this.form1.Text = "";
            }
            else {
                this.client.retrieveDB(Request["ID"]);
                this.msg.Text = "";
                this.form1.Text = ShowEditForm();
            }
        }
    }


    string ShowEditForm() {
        string s = "";
        string star = "<font color='red'>*</font>";

        s += "<tr><td>Case Id:" + star + "</td><td><input type='text' maxlength='10' id='txtCaseId' name='txtCaseId' value='" + this.client.Case_Id + "' onkeypress=\"javascript: return numberOnly(event);\"></td></tr>";
        s += "<tr><td>Attorney:</td><td>" + ClsClient.writeAttorneyList("txtAttorney", "txtAttorney", this.client.Attorney) + "</td></tr>";
        s += "<tr><td>Paralegal:</td><td>" + ClsClient.writeParalegalList("txtParalegal", "txtParalegal", this.client.Paralegal) + "</td></tr>";
        s += "<tr><td>Date Of Injury:</td><td><input type='text' class='datetime' maxlength='10' id='txtDateOfInjury' name='txtDateOfInjury' value='" + ClsUtil.textboxEncode(this.client.Date_Of_Injury) + "'></td></tr>";
        s += "<tr><td>Statute Of Limitation:</td><td><input type='text' maxlength='100' id='txtStatuteLimit' name='txtStatuteLimit' value='" + ClsUtil.textboxEncode(this.client.Statute_Of_Limitation) + "'></td></tr>";

        s += "<tr><td>Phone Number:</td><td><input type='text' maxlength='100' id='txtPhone' name='txtPhone' value='" + ClsUtil.textboxEncode(this.client.Phone_Number) + "'></td></tr>";
        s += "<tr><td>Address:</td><td><input type='text' maxlength='100' id='txtAddress' name='txtAddress' value='" + ClsUtil.textboxEncode(this.client.Address) + "'></td></tr>";
        s += "<tr><td>Date Of Birth:</td><td><input type='text' class='datetime' maxlength='10' id='txtDOB' name='txtDOB' value='" + ClsUtil.textboxEncode(this.client.Date_Of_Birth) + "'></td></tr>";
        s += "<tr><td>Social Security Number:</td><td><input type='text' maxlength='11' id='txtSSN' name='txtSSN' value='" + ClsUtil.textboxEncode(this.client.Social_Security_Number) + "'></td></tr>";

        s = "<table class='T1'>" + s + "</table>";

        string t = "";

        t += "<tr><td>Client Type:" + star + "</td><td>" + ClsClient.writeClientTypeList("txtClientType", "txtClientType", this.client.Client_Type) + "</td></tr>";
        t += "<tr><td>First Name:</td><td><input type='text' maxlength='100' id='txtFirstName' name='txtFirstName' value='" + ClsUtil.textboxEncode(this.client.First_Name) + "'></td></tr>";
        t += "<tr><td>Last Name:</td><td><input type='text' maxlength='100' id='txtLastName' name='txtLastName' value='" + ClsUtil.textboxEncode(this.client.Last_Name) + "'></td></tr>";
        t += "<tr><td>Case Type:</td><td><input type='text' maxlength='100' id='txtCaseType' name='txtCaseType' value='" + ClsUtil.textboxEncode(this.client.Case_Type) + "'></td></tr>";

        t += "<tr><td>At Fault Party:</td><td><input type='text' maxlength='100' id='txtFaultParty' name='txtFaultParty' value='" + ClsUtil.textboxEncode(this.client.At_Fault_Party) + "'></td></tr>";
        t += "<tr><td>Settlement Type:</td><td><input type='text' maxlength='100' id='txtSettleType' name='txtSettleType' value='" + ClsUtil.textboxEncode(this.client.Settlement_Type) + "'></td></tr>";
        t += "<tr><td>Settlement Amount:</td><td><input type='text' maxlength='100' id='txtSettleAmount' name='txtSettleAmount' value='" + ClsUtil.textboxEncode(this.client.Settlement_Amount) + "'></td></tr>";
        t += "<tr><td>Disposition:</td><td><input type='text' maxlength='100' id='txtDisposition' name='txtDisposition' value='" + ClsUtil.textboxEncode(this.client.Disposition) + "'></td></tr>";

        t += "<tr><td>Case Notes:</td><td><textarea rows='10' cols='30' id='txtCaseNotes' name='txtCaseNotes'>" + this.client.Case_Notes + "</textarea></td></tr>";
        t += "<tr><td>Date For Perspective Client:</td><td><input type='text' class='datetime' maxlength='10' id='txtDateForPersClient' name='txtDateForPersClient' value='" + ClsUtil.textboxEncode(this.client.Date_For_Perspective_Client) + "'></td></tr>";

        t = "<table class='T1'>" + t + "</table>";

        return "<table><tr><td valign='top'>" + s + 
            "</td><td width='50'><br></td><td valign='top'>" + t + "</td></tr></table>" +
            "<br/><input value=\"Submit Change\" type=\"button\" onclick=\"javascript:update();\" />";
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        this.client.download_pdf(ClsUtil.getStrVal(Request["ID"]));
    }
}
