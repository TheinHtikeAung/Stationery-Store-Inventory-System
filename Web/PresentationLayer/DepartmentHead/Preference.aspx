<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/DepartmentHead_Template.Master" AutoEventWireup="true" CodeBehind="Preference.aspx.cs" Inherits="PresentationLayer.DepartmentHead.Preference" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content_title_container">
    <asp:Label ID="ui_content_title_label" runat="server" Text="Department Preference" class="content_title"></asp:Label>
</div>
<div style="text-align:center;display:block;">
<div style="margin:0 auto;text-align:left;width:400px;padding:20px;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <%--<tr>
                    <td class="input-medium" colspan="2" height="30px">
                        <asp:Label ID="lblStatus" runat="server" Visible="False" 
                            class="label label-success" Font-Size="Small" Height="20px"></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td class="span2" style="width: 150px">
                        <asp:Label ID="Label1" runat="server" Text="Collection Point : "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCollectionPoint" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="span2">
                        <asp:Label ID="Label2" runat="server" Text="Representative : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRepresentative" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtRepresentative" ErrorMessage="* Please fill this field" 
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2" style="width: 117px">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                            class="btn btn-default" Width="120px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
</div>
</asp:Content>
