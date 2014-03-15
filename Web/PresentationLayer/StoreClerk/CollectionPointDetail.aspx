<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="CollectionPointDetail.aspx.cs" Inherits="PresentationLayer.StoreClerk.CollectionPointDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content_title_container">
<asp:Label ID="ui_content_title_label" runat="server" Text="" class="content_title">
</asp:Label>
</div>
<table class="table">
        <%--<tr>
            <td>id *<asp:hiddenfield runat="server" ID="ui_id_hiddenfield"></asp:hiddenfield></td>
            <td><asp:textbox runat="server" ID="ui_id_textbox"></asp:textbox></td>
        </tr>--%>

             <td>Collection Point *<asp:hiddenfield runat="server" ID="ui_id_hiddenfield"></asp:hiddenfield></td>
             <td><asp:textbox runat="server" ID="ui_collectionPoint_textbox"></asp:textbox></td>
 
   <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="ui_save_button" runat="server" Text="Save" 
                    class="btn btn-medium btn-primary" onclick="ui_save_button_Click"/>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="ui_cancle_button" runat="server" Text="Back" 
                    class="btn btn-medium btn-primary" onclick="ui_cancle_button_Click"/>
            </td>
        </tr>    
</table>
</asp:Content>
