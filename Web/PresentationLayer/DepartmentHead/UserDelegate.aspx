<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/DepartmentHead_Template.Master" AutoEventWireup="true" CodeBehind="UserDelegate.aspx.cs" Inherits="PresentationLayer.StoreClerk.DelegateUser" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content_title_container">
    <asp:Label ID="ui_content_title_label" runat="server" Text="Manage Department Authorized Persons" class="content_title"></asp:Label>
</div>
<div style="text-align:center;display:block;">
    <table  style="margin:0 auto;text-align:left;width:600px;padding:20px;">
        <tr>
            <td style="width:130px;">New Delegate</td>
            <td style="padding-top:10px;width:250px;"><asp:DropDownList ID="ui_emplyoees_dropdown" runat="server"></asp:DropDownList></td>
            <td><asp:Button class="btn" ID="ui_delegate_button" runat="server" onclick="ui_delegate_button_Click" Text="Assign Authority" /></td>
        </tr>
        <tr>
            <td>Remove Delegate</td>
            <td style="padding-top:10px;"><asp:DropDownList ID="ui_delegated_dropdown" runat="server"></asp:DropDownList></td>
            <td><asp:Button class="btn" ID="ui_removedelegated_button" runat="server" Text="Remove Authority" onclick="ui_removedelegated_button_Click" /></td>
        </tr>
    </table>
</div>
</asp:Content>
