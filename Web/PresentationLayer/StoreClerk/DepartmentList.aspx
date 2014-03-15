<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Inherits="PresentationLayer.StoreClerk.DepartmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Department List" class="content_title"></asp:Label>
    </div>
    <asp:GridView ID="ui_departments_gridview" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" 
        DataSourceID="ObjectDataSource1"
        onselectedindexchanged="ui_supplier_gridview_SelectedIndexChanged" 
        class="table table-hover table-striped" EnableTheming="False" 
         GridLines="None" >
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id"  ItemStyle-CssClass="hide" 
                    HeaderStyle-CssClass="hide">
            </asp:BoundField>
            <asp:BoundField DataField="code" HeaderText="Dept Code" 
                SortExpression="code"></asp:BoundField>
            <asp:BoundField DataField="department_name" HeaderText="Department Name" 
                SortExpression="department_name"></asp:BoundField>
            <asp:BoundField DataField="username" HeaderText="Department Head" 
                SortExpression="username"></asp:BoundField>
            <asp:BoundField DataField="phone" HeaderText="Phone" 
                SortExpression="phone"></asp:BoundField>

            <asp:CommandField SelectText="Edit" ShowSelectButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="actionGetDepartments" TypeName="PresentationLayer.StoreClerk.DepartmentList">
    </asp:ObjectDataSource>
    <asp:linkbutton ID="Linkbutton1" runat="server" onclick="CreateNew_Button_Click" class="btn btn-medium btn-primary">Create New</asp:linkbutton>

    <%--<asp:GridView ID="GridView1" runat="server" onrowcommand="GridView1_RowCommand" 
        AutoGenerateColumns="False" class="table table-hover table-striped" GridLines="None">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide" />
            <asp:BoundField DataField="code" HeaderText="Code" />
            <asp:BoundField DataField="department_name" HeaderText="Department Name" />
            <asp:BoundField DataField="contact_name" HeaderText="Contact Name" 
                NullDisplayText="-" />
            <asp:BoundField DataField="phone" HeaderText="Phone No." />
            
            <asp:BoundField DataField="department_head" HeaderText="Department Head Id" ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide"  />
            <asp:BoundField DataField="username" HeaderText="Department Head" />
            <asp:BoundField DataField="collection_point" HeaderText="Collection Point Id"  ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide" />
            
            <asp:BoundField DataField="name" HeaderText="Collection Point Address"  ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide" />
            <asp:BoundField DataField="representative_name" 
                HeaderText="Representative Name"  ItemStyle-CssClass="hide" HeaderStyle-CssClass="hide" />
            <asp:CommandField ShowSelectButton="True" SelectText="Edit"/>
        </Columns>
    </asp:GridView>
    <br /><br />
    <asp:LinkButton ID="LinkButton1" class="btn btn-medium btn-primary" runat="server" PostBackUrl="~/StoreClerk/AddNewDepartment.aspx">Creat New</asp:LinkButton>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server">
    </asp:EntityDataSource>--%>

    </asp:Content>
