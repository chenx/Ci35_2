﻿<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="changepwd.aspx.cs" Inherits="profile" Title="My Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="../javascript/profile.js"></script>

<center>

<h2>Change User Password</h2>

<p><a href="./">User List</a> | <a href="view.aspx?id=<% Response.Write(Request["id"]); %>">View User Profile</a>
 | <a href="edit.aspx?id=<% Response.Write(Request["id"]); %>">Edit User Profile</a>
 | Change User Password
 </p>


<asp:Label ID="lblProfile" runat="server"></asp:Label><br />


<table>
<tr><td>New Password:</td><td><input id="txtNewPwd" type="password" name="txtNewPwd" value=""></td></tr>
<tr><td>New Password (repeat):</td><td><input id="txtNewPwd2" type="password" name="txtNewPwd2" value=""></td></tr>
<tr><td colspan="2" align="center"><br /><input type="submit" runat="server" id="btnChangePwd" value="Change User Password" /></td></tr>
</table>

<table><tr><td align="center">
<asp:Label ID="msg" ClientIDMode="Static" runat="server"></asp:Label>
</td></tr></table>


</center>

</asp:Content>

