<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="client_default" Title="Client Management" EnableViewState="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="../javascript/client.js"></script>


<center>
<h2>Client List</h2>
</center>

<center>
<asp:Label ID="AtoZ" runat="server" style="font-size: 16px;"></asp:Label>

<p><a href='client_new.aspx'>Add New Client</a> | <a href="print.aspx">Print Client Information</a></p>
<table><tr><td>
<asp:Label ID="ClientList" runat="server"></asp:Label>
</td></tr></table>
</center>

<input type="hidden" id="print_id" name="print_id" value="" />

</asp:Content>

