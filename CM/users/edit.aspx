<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="user_edit" Title="Site Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="../javascript/util.js"></script>
<script type="text/javascript" src="../javascript/user.js"></script>

<center>
<h2>Edit User Profile</h2>

<p><a href="./">User List</a> 
| <a href="view.aspx?id=<% Response.Write(Request["id"]); %>">View User Profile</a>
| Edit User Profile
| <a href="changepwd.aspx?id=<% Response.Write(Request["id"]); %>">Change User Password</a>
</p>

<table><tr><td align="center">
<asp:Label ID="form1" runat="server"></asp:Label>
<asp:Label ID="msg" ClientIDMode="Static" runat="server"></asp:Label>
</td></tr></table>



</center>

</asp:Content>

