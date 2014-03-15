<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="StationeryAdjustment.aspx.cs" Inherits="PresentationLayer.StoreClerk.OrderArrivalCreation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content_title_container">
    <asp:Label ID="ui_content_title_label" runat="server" Text="Stationery Adjustment" class="content_title"></asp:Label>
</div>
<div style="text-align:center;display:block;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="margin:0 auto;text-align:center;">
        <ContentTemplate>
        <asp:Label ID="lblStatus" runat="server" Text="Label" Visible="False"></asp:Label>
        <table style="margin:0 auto;text-align:left;width:400px;" class="table">
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Stationery"></asp:Label></td>
                <td><asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" 
                ontextchanged="TextBox1_TextChanged"></asp:TextBox>
                <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" 
                    CompletionInterval="0" MinimumPrefixLength="0" 
                    ServiceMethod="GetCompletionList" ServicePath="StationeryAdjustment.aspx" 
                    TargetControlID="TextBox1" UseContextKey="True">
                </asp:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Quantity"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Remark"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Supplier"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="Save" runat="server" onclick="Save_Click" Text="Save" class="btn btn-primary btn-medium" />&nbsp;&nbsp;
                    
                    <asp:Button ID="Button1" runat="server" Text="Go to Dashboard" 
                        onclick="Button1_Click" class="btn btn-medium"/>
                </td>
            </tr>
        </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
    
</asp:Content>
