<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="profile_edit" Title="My Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="../javascript/profile.js"></script>

<center>
<h2>Edit My Profile</h2>

<p>
<a href='view.aspx'>View My Profile</a> 
| Edit My Profile
| <a href='changepwd.aspx'>Change My Password</a>
</p>

<table><tr><td align="center">
<asp:Label ID="form1" runat="server"></asp:Label>
<asp:Label ID="msg" ClientIDMode="Static" runat="server"></asp:Label>
</td></tr></table>

</center>

</asp:Content>

