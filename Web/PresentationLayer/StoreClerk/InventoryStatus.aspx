<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="InventoryStatus.aspx.cs" Inherits="PresentationLayer.StoreClerk.InventoryStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content_title_container">
    <asp:Label ID="ui_content_title_label" runat="server" Text="Re-order Level Stationeries" class="content_title"></asp:Label>
</div>
<ul>
<li>
    <asp:Label ID="ui_reorderlevel_label" runat="server" Text=""></asp:Label> items are need to reorder.
</li>
<li>
    <asp:Label ID="ui_zerolevel_lable" runat="server" Text=""></asp:Label> items are totally lack in stock.
</li>    
</ul>
<asp:GridView ID="ui_inventory_gridview" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False"
    DataSourceID="ObjectDataSource1"
    class="table table-hover table-striped" GridLines="None" onselectedindexchanged="ui_inventory_gridview_SelectedIndexChanged" 
        >
    <Columns>
        <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
            SortExpression="id"  ItemStyle-CssClass="hide" 
                HeaderStyle-CssClass="hide" >
<HeaderStyle CssClass="hide"></HeaderStyle>

<ItemStyle CssClass="hide"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="code" HeaderText="Code" SortExpression="code" />
        <asp:BoundField DataField="stationery_name" HeaderText="Name" SortExpression="stationery_name" />
        <asp:BoundField DataField="reorder_level" HeaderText="Reorder Level" SortExpression="reorder_level" />
        <asp:BoundField DataField="reorder_quantity" HeaderText="Reorder Qty" SortExpression="reorder_quantity" />       
        <asp:BoundField DataField="quantity_in_stock" HeaderText="Qty in Stock" SortExpression="quantity_in_stock" />       
        <asp:BoundField DataField="pending_quantity_to_distribute" HeaderText="Pending Qty" SortExpression="pending_quantity_to_distibute" />       
        <asp:BoundField DataField="available_quantity" HeaderText="Available Qty" SortExpression="available_quantity" />
        <asp:CommandField SelectText="Order" ShowSelectButton="True"></asp:CommandField>
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    SelectMethod="actionGetReorderLevelStationeryViews" TypeName="PresentationLayer.StoreClerk.InventoryStatus">
</asp:ObjectDataSource>

</asp:Content>
