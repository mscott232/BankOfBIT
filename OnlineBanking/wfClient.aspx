<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="wfClient.aspx.cs" Inherits="wfClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Client: "></asp:Label>
    <asp:Label ID="lblClientName" runat="server" Font-Bold="True" Text="Label"></asp:Label>
    <asp:GridView ID="gvAccounts" runat="server" AutoGenerateSelectButton="True" 
        AutoGenerateColumns="False" Height="123px" 
        onselectedindexchanged="gvAccounts_SelectedIndexChanged" Width="400px">
        <Columns>
            <asp:BoundField DataField="AccountNumber" HeaderText="Account Number">
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Notes" HeaderText="Account Notes">
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Balance" DataFormatString="{0:c}" 
                HeaderText="Balance">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblExchangeRate" runat="server" 
        Text="To eventually display exchange rate"></asp:Label>
    <br />
    <asp:Label ID="lblErrorException" runat="server" ForeColor="Red" 
        Text="To display error exception messages" Visible="False"></asp:Label>
</asp:Content>

