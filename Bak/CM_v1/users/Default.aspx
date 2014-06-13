<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="home" Title="Site Admin" EnableViewState="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="../javascript/user.js"></script>


<center>
<h2>User List</h2>
</center>

<center>
<p><a href='new.aspx'>Add New User</a></p>
<table><tr><td>
<asp:Label ID="ClientList" runat="server"></asp:Label>
</td></tr></table>
</center>

<script type="text/javascript">
</script>

</asp:Content>

