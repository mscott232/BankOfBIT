<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <!-- Updated the heading -->
    <h2>
        Welcome to Bank of BIT
    </h2>
    <!-- Removed one paragraph element and modified the other to the proper description -->
    <p>
        Bank of BIT offers online banking features such as bill payments and money transfers.
    </p>
</asp:Content>
