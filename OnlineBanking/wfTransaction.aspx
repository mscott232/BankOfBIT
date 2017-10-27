<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="wfTransaction.aspx.cs" Inherits="wfTransactionaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Account Number:"></asp:Label>
&nbsp;<asp:Label ID="lblAccountNumber" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Balance:"></asp:Label>
&nbsp;
    <asp:Label ID="lblBalance" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Transaction Type: "></asp:Label>
&nbsp;
    <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlTransactionType_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="Amount:"></asp:Label>
&nbsp;<!-- changed the style to right align the textbox -->
    <asp:TextBox ID="txtAmount" runat="server" style="text-align: right"></asp:TextBox>
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtAmount" ErrorMessage="Value must be between 0.01 and 10,000.00" 
        MaximumValue="10000.00" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Text="To:"></asp:Label>
&nbsp;
    <asp:DropDownList ID="ddlRecipient" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    <br />
    <br />
&nbsp;<asp:Button ID="btnComplete" runat="server" Text="Complete Transaction" 
        onclick="btnComplete_Click" />
    <br />
    <br />
    <asp:Label ID="lblException" runat="server" ForeColor="Red" 
        Text="To display error/exception messages" Visible="False"></asp:Label>
&nbsp;
</asp:Content>

