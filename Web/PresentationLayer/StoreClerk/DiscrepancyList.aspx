<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="DiscrepancyList.aspx.cs" Inherits="PresentationLayer.StoreClerk.DiscrepancyList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Pending Descrepencies" class="content_title"></asp:Label> 
    </div>
    <asp:GridView ID="ui_pendingdiscrepencies_gridview" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id"  AllowPaging="True"  PageSize="10"
        DataSourceID="ObjectDataSource1"
        onselectedindexchanged="ui_supplier_gridview_SelectedIndexChanged" 
        class="table table-hover table-striped" EnableTheming="False" 
         GridLines="None" >
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id"  ItemStyle-CssClass="hide" 
                    HeaderStyle-CssClass="hide">
            </asp:BoundField>
            <asp:BoundField DataField="code" HeaderText="Item Code" 
                SortExpression="code"></asp:BoundField>
            <asp:BoundField DataField="stationery_name" HeaderText="Item Name" 
                SortExpression="stationery_name"></asp:BoundField>
            <asp:BoundField DataField="quantity" HeaderText="Quantity" 
                SortExpression="quantity"></asp:BoundField>
            <asp:BoundField DataField="datetime_reported" HeaderText="Date" 
                SortExpression="datetime_reported" dataformatstring="{0:MMMM d, yyyy}" htmlencode="false"></asp:BoundField>
                <asp:BoundField DataField="remark" HeaderText="Remark" 
                SortExpression="remark"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="actionGetPendingDiscrepencyViews" TypeName="PresentationLayer.StoreClerk.DiscrepancyList">
    </asp:ObjectDataSource>
    
    <div class="content_title_container">
        <asp:Label ID="ui_secondcontenttitle_lable" runat="server" Text="Approved Descrepencies" class="content_title"></asp:Label> 
    </div>
    <asp:GridView ID="ui_approveddiscrepencies_gridview" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id"  AllowPaging="True"  PageSize="10"
        DataSourceID="ObjectDataSource2"
        onselectedindexchanged="ui_supplier_gridview_SelectedIndexChanged" 
        class="table table-hover table-striped" EnableTheming="False" 
         GridLines="None" >
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id"  ItemStyle-CssClass="hide" 
                    HeaderStyle-CssClass="hide">
            </asp:BoundField>
            <asp:BoundField DataField="code" HeaderText="Item Code" 
                SortExpression="code"></asp:BoundField>
            <asp:BoundField DataField="stationery_name" HeaderText="Item Name" 
                SortExpression="stationery_name"></asp:BoundField>
            <asp:BoundField DataField="quantity" HeaderText="Quantity" 
                SortExpression="quantity"></asp:BoundField>
            <asp:BoundField DataField="datetime_reported" HeaderText="Date" 
                SortExpression="datetime_reported" dataformatstring="{0:MMMM d, yyyy}" htmlencode="false"></asp:BoundField>
            <asp:BoundField DataField="remark" HeaderText="Remark" 
                SortExpression="remark"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        SelectMethod="actionGetApprovedDiscrepencyViews" TypeName="PresentationLayer.StoreClerk.DiscrepancyList">
    </asp:ObjectDataSource>
    </asp:Content>
