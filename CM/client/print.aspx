<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="print.aspx.cs" Inherits="client_print" Title="Client Management" EnableViewState="false"%>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="../javascript/client.js"></script>


<center>
<h2>Print Client Information</h2>
</center>

<center>
<p><a href="./">Client List</a></p>

<p>
Case ID: <asp:Label ID="listCases" runat="server"></asp:Label>
<asp:Button ID="btnPrint" runat="server" Text="Display Report" onclick="btnPrint_Click" usesubmitbehavior="false" />
<asp:Button ID="btnPDF" runat="server" Text="Download" onclick="btnPDF_Click" ToolTip="Download PDF" usesubmitbehavior="false" />
<asp:Button ID="btnAll" runat="server" Text="Download All Cases" onclick="btnAll_Click" usesubmitbehavior="false" Visible="false" />
</p>
<asp:Label ID="msg" runat="server"></asp:Label>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="True" DisplayGroupTree="False" Height="50px" BorderWidth="1"
        ReportSourceID="CrystalReportSource1" Width="350px" BorderColor="Black" />
</center>

<script type="text/javascript">
</script>

</asp:Content>

