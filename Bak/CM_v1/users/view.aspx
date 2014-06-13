<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="profile" Title="Site Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<center>
<h2>User Profile</h2>

<p><a href="./">User List</a> 
| View User Profile
| <a href="edit.aspx?id=<% Response.Write(Request["id"]); %>">Edit User Profile</a>
| <a href="changepwd.aspx?id=<% Response.Write(Request["id"]); %>">Change User Password</a>
</p>

<asp:Label ID="lblProfile" runat="server"></asp:Label>

</center>

</asp:Content>

