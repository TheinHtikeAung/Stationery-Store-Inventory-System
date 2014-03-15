<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="SupplierList.aspx.cs" Inherits="PresentationLayer.StoreClerk.SupplierList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Supplier List" class="content_title"></asp:Label> 
    </div>

    <asp:GridView ID="ui_supplier_gridview" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id"
                
    onselectedindexchanged="ui_supplier_gridview_SelectedIndexChanged" 
    class="table table-hover table-striped" GridLines="None">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id"  ItemStyle-CssClass="hide" 
                    HeaderStyle-CssClass="hide">
            </asp:BoundField>
            <asp:BoundField DataField="code" HeaderText="Item Code" SortExpression="code">
            </asp:BoundField>
            <asp:BoundField DataField="supplier_name" HeaderText="Supplier Name" 
                SortExpression="supplier_name"></asp:BoundField>
            <asp:BoundField DataField="contact_name" HeaderText="Contact Name" 
                SortExpression="contact_name"></asp:BoundField>
            <asp:BoundField DataField="phone_number" HeaderText="Phone Number" 
                SortExpression="phone_number"></asp:BoundField>
            <asp:BoundField DataField="fax_number" HeaderText="Fax Number" 
                SortExpression="fax_number"></asp:BoundField>
            <asp:BoundField DataField="email" HeaderText="Email" 
                SortExpression="email"></asp:BoundField>
            <%--<asp:BoundField DataField="address" HeaderText="Address" 
                SortExpression="address"></asp:BoundField>--%>
            <asp:CommandField SelectText="Edit" ShowSelectButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>

    <asp:linkbutton runat="server" onclick="CreateNew_Button_Click" class="btn btn-medium btn-primary">Create New</asp:linkbutton>
</asp:Content>
