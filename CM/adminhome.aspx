<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="adminhome.aspx.cs" Inherits="adminhome" Title="Site Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<center>
<h2>Admin Home</h2>

<fieldset>
<legend>Admin Tasks</legend>
<br />
<table width="100%"><tr><td>
<ul>
<li><a href='./users/'>Manage Users</a> - create/disable/enable/delete users of this site</li>
<li><a href='./client/admin.aspx'>Manage Clients</a> - disable/enable/delete clients</li>
</ul>
</td></tr></table>

</fieldset>

</center>

</asp:Content>

