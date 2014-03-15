<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="DiscrepancyMaintenance.aspx.cs" Inherits="PresentationLayer.StoreClerk.DiscrepancyMaintenance" %>
 <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Create Discrepancy" class="content_title"></asp:Label> 
    </div>
    <div style="display:block;text-align:center;width:100%;">
    <table class="table" style="width:400px;margin:0 auto;text-align:left;">
        <tr>
            <td>Stationery * <asp:HiddenField ID="ui_stationery_hidden" runat="server" /></td>
            <td><asp:TextBox ID="ui_stationery_textbox" runat="server" class="stationeries_autocomplete"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Quantity * </td>
            <td><asp:TextBox ID="ui_quantity_textbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Remark</td>
            <td><asp:TextBox ID="ui_remark_textboxt" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="text-align:center;">
    <asp:Button ID="ui_save_button" runat="server" Text="Save" 
        onclick="ui_save_button_Click" class="btn btn-primary" style="margin-right:20px;"/>
    <asp:Button ID="ui_cancle_button" runat="server" Text="Cancle" 
        onclick="ui_cancle_button_Click"  class="btn btn-primary" />
        </div>
            </td>
        </tr>
    </table>
    </div>
    <br />
    
<script type="text/javascript">
    loadAllStationariesAndPutIntoHiddenField("#ContentPlaceHolder1_ui_stationery_hidden");
</script>

    <%--<asp:Label ID="Label1" runat="server" Text="Id" Visible="False"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="idTxt" runat="server" class="form-control" Enabled="False" 
        Visible="False"></asp:TextBox><br />
    Stationary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;
    <asp:TextBox ID="stryTxt" runat="server"></asp:TextBox>
    <br />
    Quantity&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="qtyTxt" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="qtyTxt" Display="Dynamic" ErrorMessage="Field is required!" 
        Font-Size="Small" ForeColor="Red" ToolTip="Field is required!">*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
        ControlToValidate="qtyTxt" ErrorMessage="Please enter only numbers!" 
        Font-Size="Small" ForeColor="Red" ValidationExpression="[+-]*\d*">Please enter only numbers!</asp:RegularExpressionValidator>
    <br />
    Remarks&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="remarkTxt" runat="server" >-</asp:TextBox>&nbsp;&nbsp;
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <asp:Label ID="lblDate" runat="server" Text="Date  " Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
    <asp:TextBox ID="dateTxt" runat="server" Visible="False" ></asp:TextBox>&nbsp;&nbsp;
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dateTxt">
    </asp:CalendarExtender>
    <br /><br />
    <asp:Button ID="cancelBtn" runat="server" Text="Back" 
        onclick="cancelBtn_Click" CausesValidation="False" class="btn btn-inverse" 
        Width="120px"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="saveBtn" runat="server" Text="Save" onclick="saveBtn_Click" 
        class="btn btn-inverse" Width="120px"/>&nbsp;&nbsp;
    <asp:Button ID="delBtn" runat="server" Text="Delete" onclick="delBtn_Click" 
        Visible="False" />&nbsp;&nbsp;
    &nbsp;&nbsp;
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        HeaderText="The following errors occured while processing!" />--%>
    </asp:Content>
