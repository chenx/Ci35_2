<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="profile" Title="My Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<center>
<h2>My Profile</h2>

<p>View My Profile 
| <a href='edit.aspx'>Edit My Profile</a>
| <a href='changepwd.aspx'>Change My Password</a>
</p>

<asp:Label ID="lblProfile" runat="server"></asp:Label>

</center>

</asp:Content>

