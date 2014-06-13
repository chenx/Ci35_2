<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="changepwd.aspx.cs" Inherits="profile_changepwd" Title="My Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="../javascript/profile.js"></script>

<center>

<h2>Change My Password</h2>

<p>
<a href='view.aspx'>View My Profile</a> 
| <a href='edit.aspx'>Edit My Profile</a> 
| Change My Password
</p>


<table>
<tr><td>Old Password: <font color='red'>*</font></td><td><input id="txtOldPwd" type="password" name="txtOldPwd" value=""></td></tr>
<tr><td>New Password: <font color='red'>*</font></td><td><input id="txtNewPwd" type="password" name="txtNewPwd" value=""></td></tr>
<tr><td>New Password (repeat): <font color='red'>*</font></td><td><input id="txtNewPwd2" type="password" name="txtNewPwd2" value=""></td></tr>
<tr><td colspan="2" align="center"><br /><input type="submit" runat="server" id="btnChangePwd" value="Change My Password" /></td></tr>
</table>

<table><tr><td align="center">
<asp:Label ID="msg" ClientIDMode="Static" runat="server"></asp:Label>
</td></tr></table>

</center>

</asp:Content>

