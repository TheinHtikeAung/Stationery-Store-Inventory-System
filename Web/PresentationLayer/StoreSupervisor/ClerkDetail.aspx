<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreSupervisor_Template.Master" AutoEventWireup="true" CodeBehind="ClerkDetail.aspx.cs" Inherits="PresentationLayer.StoreSupervisor.ClerkDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Clerk Detail" class="content_title"></asp:Label> 
    </div>

    <table class="table">
        <tr>
            <td>Username *<asp:hiddenfield runat="server" ID="ui_id_hiddenfield"></asp:hiddenfield></td>
            <td><asp:textbox runat="server" ID="ui_username_textbox"></asp:textbox></td>
        </tr>
        <tr>
            <td>Password *</td>
            <td><asp:textbox type="password" runat="server" ID="ui_password_textbox"></asp:textbox></td>
        </tr>
        <tr>
            <td>First Name</td>
            <td><asp:textbox runat="server" ID="ui_firstname_textbox"></asp:textbox></td>
        </tr>
        
        <tr>
            <td>Last Name</td>
            <td><asp:textbox runat="server" ID="ui_lastname_textbox"></asp:textbox></td>
        </tr>
        <tr>
            <td>Email *</td>
            <td>
            <asp:textbox runat="server" ID="ui_email_textbox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td>Phone Number</td>
            <td>
            <asp:textbox runat="server" ID="ui_phonenumber_textbox"></asp:textbox>
            </td>
        </tr>
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
