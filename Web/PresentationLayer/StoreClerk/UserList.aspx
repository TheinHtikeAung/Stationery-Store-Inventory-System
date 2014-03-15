<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="PresentationLayer.StoreClerk.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Employee List" class="content_title"></asp:Label> 
    </div>

    <asp:GridView ID="ui_user_gridview" runat="server" AutoGenerateColumns="False" 
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
            <asp:BoundField DataField="username" HeaderText="Userame" 
                SortExpression="username"></asp:BoundField>
            <asp:BoundField DataField="firstname" HeaderText="First Name" 
                SortExpression="firstname"></asp:BoundField>
            <asp:BoundField DataField="lastname" HeaderText="Last Name" 
                SortExpression="lastname"></asp:BoundField>
            <asp:BoundField DataField="department_name" HeaderText="Department" 
                SortExpression="department_name"></asp:BoundField>
            <asp:BoundField DataField="type" HeaderText="User Type" 
                SortExpression="type"></asp:BoundField>
            <asp:CommandField SelectText="Edit" ShowSelectButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="actionGetDepartmentSideUserViews" TypeName="PresentationLayer.StoreClerk.UserList">
    </asp:ObjectDataSource>
    <asp:linkbutton runat="server" onclick="CreateNew_Button_Click" class="btn btn-medium btn-primary">Create New</asp:linkbutton>

</asp:Content>
