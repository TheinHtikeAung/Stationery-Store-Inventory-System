<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="StockCard.aspx.cs" Inherits="PresentationLayer.StoreClerk.StockCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="(Content Title)" class="content_title"></asp:Label>
    </div>
    <div class="stationeries_autocomplete_container">
        <asp:textbox ID="ui_seationerysearch_box" runat="server" placeholder="Search Stationery" class="stationeries_autocomplete"></asp:textbox>
    </div>

    <asp:Panel ID="ui_stationeryinfo_panel" runat="server">
        <h4 style="padding-left:10px;">Stationery Info</h4>
        <table class="table table-stripe" style="width:300px;">
            <tr>
                <td style="width:100px;">Item Code</td>
                <td><asp:Label ID="ui_stationery_code_label" runat="server" Text="()"></asp:Label></td>
            </tr>
            <tr>
                <td>Item Name</td>
                <td><b><asp:Label ID="ui_stationery_name_label" runat="server" Text="()"></asp:Label></b></td>
            </tr>
            <tr>
                <td>Bin</td>
                <td><asp:Label ID="ui_stationery_bin_label" runat="server" Text="()"></asp:Label></td>
            </tr>
            <tr>
                <td>UOM</td>
                <td><asp:Label ID="ui_stationery_uom_label" runat="server" Text="()"></asp:Label> </td>
            </tr>
            <tr>
                <td>1st Supplier</td>
                <td><asp:Label ID="ui_stationery_supplier1_label" runat="server" Text="()"></asp:Label> </td>
            </tr>
            <tr>
                <td>2st Supplier</td>
                <td><asp:Label ID="ui_stationery_supplier2_label" runat="server" Text="()"></asp:Label></td>
            </tr>
            <tr>
                <td>3st Supplier</td>
                <td><asp:Label ID="ui_stationery_supplier3_label" runat="server" Text="()"></asp:Label></td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    
    <asp:GridView ID="ui_stockcard_gridview" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" PageSize="10"
        DataSourceID="ObjectDataSource1"
        class="table table-hover table-striped" GridLines="None" >
        <Columns>
            <asp:BoundField DataField="datetime_inserted" HeaderText="Date" SortExpression="date_inserted"  dataformatstring="{0:MMMM d, yyyy}" htmlencode="false"/>
            <asp:BoundField DataField="description" HeaderText="Department / Supplier" SortExpression="description" />
            <asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="quantity" />
            <asp:BoundField DataField="balance" HeaderText="Balance" SortExpression="balance" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="actionGetStockTransactionByStationeryId" TypeName="PresentationLayer.StoreClerk.StockCard">
    </asp:ObjectDataSource>


<script  type="text/javascript">
    loadAllStationaries("StockCard.aspx");
</script>


</asp:Content>
