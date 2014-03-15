<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="PresentationLayer.StoreClerk.CategoryList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Category List" class="content_title"></asp:Label> 
    </div>

    <asp:GridView ID="ui_category_gridview" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" 
        onselectedindexchanged="ui_category_gridview_SelectedIndexChanged" 
        class="table table-hover table-striped" GridLines="None">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id"  ItemStyle-CssClass="hide" 
                    HeaderStyle-CssClass="hide">
            </asp:BoundField>
            <asp:BoundField DataField="category_name" HeaderText="Category Name" 
                SortExpression="category_name"></asp:BoundField>
            
            <asp:CommandField SelectText="Edit" ShowSelectButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:linkbutton runat="server" onclick="CreateNew_Button_Click" class="btn btn-medium btn-primary">Create New</asp:linkbutton>

</asp:Content>
