<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="PresentationLayer.StoreClerk.PurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script src="/assets/ckeditor/ckeditor.js" type="text/javascript"></script>
<script src="/assets/ckeditor/adapters/jquery.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("textarea").ckeditor();
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_title_container">
    <asp:Label ID="ui_content_title_label" runat="server" Text="Order Stationery" class="content_title"></asp:Label>
</div>
<div>
<table class="table">
<%--<tr>
<td>&nbsp;</td>
<td>
    <asp:Label ID="ui_email_label" runat="server"></asp:Label>
    </td> 
</tr>
<tr>--%>
<td><asp:Label ID="ui_to_label" runat="server" Text="To"></asp:Label></td>
<td><asp:DropDownList runat="server" ID="ui_to_dropdown" ></asp:DropDownList>
<table><tr>
   <td><asp:CheckBox Checked ID="ui_sentmail" runat="server" /></td>
   <td>Send Email</td>
   <td><asp:CheckBox Checked ID="ui_sentsms" runat="server" /></td>
   <td> Send SMS</td>
   </tr></table>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="ui_from_label" runat="server" Text="From"></asp:Label>
    </td>
<td>
    &nbsp;&nbsp;<asp:label ID="ui_from_textbox" runat="server"></asp:label>
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="ui_subject_label" runat="server" Text="Subject"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="ui_subject_textbox" runat="server" style="width:50%;"></asp:TextBox>
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="ui_message_label" runat="server" Text="Email Body"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="ui_message_textbox" Enabled="false" runat="server" TextMode="MultiLine" 
        Height="271px" Width="495px"></asp:TextBox>
    </td>
</tr><tr><td></td><td>
</td></tr>
<tr>
<td></td>
<td>
<asp:Button ID="ui_send_button" runat="server" onclick="ui_send_button_Click" 
        Text="Send & Save" class="btn btn-primary btn-medium" />&nbsp;&nbsp;
    <asp:Button ID="ui_discard_button" runat="server" 
        onclick="ui_discard_button_Click" Text="Discard" class="btn btn-primary btn-medium" />
    </td>
</tr>
</table>
</div>
</asp:Content>
