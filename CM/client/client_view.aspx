<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="client_view.aspx.cs" Inherits="client_view" Title="Client Management" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<center>
<h2>Client Profile</h2>
<p><a href="./<% if (ClsUtil.getStrVal(Request["src"]) == "a") Response.Write("admin.aspx"); %>">Client List</a> 
| View Client Profile
| <a href="./client_edit.aspx?id=<% Response.Write( Request["id"] + "&src=" + Request["src"] ); %>">Edit Client Profile</a>
| <asp:LinkButton ID="btnPrint" runat="server" OnClick="btnPDF_Click" Text="Download" ToolTip="Download PDF"></asp:LinkButton>
</p>
</center>

<center>

<table><tr><td align="center">
<asp:Label ID="msg" runat="server"></asp:Label>
<asp:Label ID="form1" runat="server"></asp:Label>
</td></tr></table>

</center>


</asp:Content>

