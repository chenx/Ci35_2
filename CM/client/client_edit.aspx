<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="client_edit.aspx.cs" Inherits="client_edit" Title="Client Management" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="../javascript/util.js"></script>
<script type="text/javascript" src="../javascript/client.js"></script>

<center>
<h2>Edit Client Profile</h2>
<p><a href="./<% if (ClsUtil.getStrVal(Request["src"]) == "a") Response.Write("admin.aspx"); %>">Client List</a> 
| <a href="./client_view.aspx?id=<% Response.Write( Request["id"] + "&src=" + Request["src"] ); %>">View Client Profile</a>
| Edit Client Profile
| <asp:LinkButton ID="btnPrint" runat="server" OnClick="btnPDF_Click" Text="Download" ToolTip="Download PDF"></asp:LinkButton>
</p>
</center>

<center>

<table><tr><td align="center">
<asp:Label ID="form1" runat="server"></asp:Label>
<asp:Label ID="msg" ClientIDMode="Static" runat="server"></asp:Label>
</td></tr></table>

</center>

<script type="text/javascript">
    $(document).ready(function() {
        $('#txtClientType').trigger('change');

        $('input.datetime').datepicker({
            onSelect: function() { this.focus(); }
        });
    });
</script>


</asp:Content>

