<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="CollectionPointList.aspx.cs" Inherits="PresentationLayer.StoreClerk.CollectionPointList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class ="content_title_container">
<asp:Label ID="ui_content_title_label" runat="server" Text="Collection Point List" class="content_title"></asp:Label>
</div>
<asp:GridView ID="ui_collectionPoint_gridview" runat="server" AutoGenerateColumns="false" DataKeyNames="id" OnSelectedIndexChanged="ui_collectionPoint_gridview_SelectedIndexChanged" class="table table-hover table-striped" GridLines="None">
<Columns>
<asp:BoundField DataField="id" HeaderText="CollectionPoint ID" ReadOnly="True" 
                SortExpression="id"  ItemStyle-CssClass="hide" 
                    HeaderStyle-CssClass="hide">
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="CollectionPoint" 
                SortExpression="Collection_Point"></asp:BoundField>
            <asp:CommandField SelectText="Edit" ShowSelectButton="True"></asp:CommandField>
</Columns>
</asp:GridView> 
<asp:LinkButton runat="server" class="btn btn-medium btn-primary" OnClick="CreateNew_Button_Click">Create New</asp:LinkButton>
</asp:Content>
