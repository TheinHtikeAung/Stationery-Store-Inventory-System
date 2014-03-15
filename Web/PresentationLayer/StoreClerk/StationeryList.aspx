<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="StationeryList.aspx.cs" Inherits="PresentationLayer.StoreClerk.StationeryList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Stationery List" class="content_title"></asp:Label>
        <div class="stationeries_autocomplete_container" style="float:right;">
        <asp:textbox ID="ui_seationerysearch_box" runat="server" placeholder="Search Stationery" class="stationeries_autocomplete"></asp:textbox>
    </div>
    </div>
    
    <asp:GridView ID="ui_stationeries_gridview" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" PageSize="10"
        DataSourceID="ObjectDataSource1"
        class="table table-hover table-striped" GridLines="None" 
        onselectedindexchanged="ui_stockcard_gridview_SelectedIndexChanged" >
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id"  ItemStyle-CssClass="hide" 
                    HeaderStyle-CssClass="hide" />
            <asp:BoundField DataField="code" HeaderText="Code" SortExpression="code" />
            <asp:BoundField DataField="category_name" HeaderText="Category" SortExpression="category_name" />       
            <asp:BoundField DataField="stationery_name" HeaderText="Name" SortExpression="stationery_name" />
            <asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" />
            <asp:BoundField DataField="reorder_level" HeaderText="Reorder Level" SortExpression="reorder_level" />
            <asp:BoundField DataField="reorder_quantity" HeaderText="Reorder Quantity" SortExpression="reorder_quantity" />       
            <asp:BoundField DataField="unit_of_measure" HeaderText="Unit of Measure" SortExpression="unit_of_measure" />       
            <asp:CommandField SelectText="Edit" ShowSelectButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="actionGetStationeries" TypeName="PresentationLayer.StoreClerk.StationeryList">
    </asp:ObjectDataSource>
    <asp:LinkButton ID="LinkButton1" class="btn btn-medium btn-primary" runat="server" PostBackUrl="~/StoreClerk/StatoneryDetail.aspx">Creat New</asp:LinkButton>
<script  type="text/javascript">
    loadAllStationaries("/StoreClerk/StatoneryDetail.aspx");
</script>
</asp:Content>
