<%@ Page Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="client_new.aspx.cs" Inherits="client_new" Title="Client Management" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="../javascript/client.js"></script>

<center>
<h2>Add New Client</h2>
<p><a href="./">Client List</a></p>
</center>

<center>

<table><tr><td align="center">
<asp:Label ID="msg" runat="server"></asp:Label>
<asp:Label ID="form1" runat="server"></asp:Label>
</td></tr></table>

</center>

  
<script type="text/javascript">
    $(document).ready(function() {
        $("input.datetime").datepicker({
            onSelect: function() { this.focus(); }
        });
    });
</script>

</asp:Content>

