<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Title="Home" Inherits="Default" MasterPageFile="~/Site.master" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    
    <div style="margin-left:10px; margin-right:10px;">

    <center>
    <p><font size="16px">Client Manager</font></p>
    
    <div>
    <asp:Login ID="btnLogin" runat="server" CssClass="LoginControl" BackColor="#F7F7DE" BorderColor="#CCCC99" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" 
            onauthenticate="btnLogin_Authenticate">
    <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
    </asp:Login>
     <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
        
    <% if (! ClsAuth.IsUser()) { %>
    <p><a href="Register.aspx">Register</a> | <a href="Getpwd.aspx">Forgot Password</a></p>
    <% } else { %>
    
    <table><tr><td>
    
    <% if (ClsAuth.IsAdmin()) { %>
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
    <br />
    <% } %>
    
    <fieldset>
    <legend>Tasks</legend>
    <ul>
    <li><a href='client/'>Client Management</a></li>
    </ul>
    </fieldset>
    <br />
    
    <fieldset>
    <legend>Personal</legend>
    <ul>
    <li><a href='profile/view.aspx'>My Profile</a></li>
    <li><a href='profile/changepwd.aspx'>Change My Password</a></li>
    </ul>
    </fieldset>
    
    </td></tr></table>
    
    <% } %>
       
    </center>
        
    </div>
</asp:Content>

