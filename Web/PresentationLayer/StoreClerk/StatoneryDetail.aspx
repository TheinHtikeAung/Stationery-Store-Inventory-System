<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="StatoneryDetail.aspx.cs" Inherits="PresentationLayer.StoreClerk.StatoneryDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_title_container">
<asp:Label ID="ui_content_title_label" runat="server" Text="" class="content_title"></asp:Label> 

</div>

    <!--<div class="stationeries_autocomplete_container">
        <asp:textbox ID="ui_seationerysearch_box" runat="server" placeholder="Search Stationery" class="stationeries_autocomplete"></asp:textbox>   
    </div>-->

    <table class="table">
        <tr>
            <td>Code *<asp:hiddenfield runat="server" ID="ui_id_hiddenfield"></asp:hiddenfield></td>
            <td><asp:textbox runat="server" ID="ui_code_textbox"></asp:textbox></td>
        </tr>
        <tr>
            <td>Stationery Name *</td>
            <td><asp:textbox runat="server" ID="ui_name_textbox"></asp:textbox></td>
        </tr>
        <tr>
            <td>Category *</td>
            <td>
                <asp:DropDownList ID="ui_category_dropdown" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Price *</td>
            <td><asp:textbox runat="server" ID="ui_price_textbox"></asp:textbox></td>
        </tr>
        <tr>
            <td>Reorder Level *</td>
            <td><asp:textbox runat="server" ID="ui_reorderlevel_textbox"></asp:textbox></td>
        </tr>
        <tr>
            <td>Reorder Quantity *</td>
            <td><asp:textbox runat="server" ID="ui_reorderquantity_textbox"></asp:textbox></td>
        </tr>
        <tr>
            <td>Quantity in Stock</td>
            <td>
                <asp:textbox runat="server" ID="ui_quantityinstock_label" Enabled="False"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td>Pending Quantity to Distribute</td>
            <td>
            <asp:textbox runat="server" ID="ui_pendingquantity_label" Enabled="False"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td>First Supplier</td>
            <td>
                <asp:DropDownList ID="ui_firstsupplier_dropdown" runat="server">
                </asp:DropDownList>
            
            </td>
        </tr>
        <tr>
            <td>Second Suppliler</td>
            <td>
             <asp:DropDownList ID="ui_secondsupplier_dropdown" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Third Supplier</td>
            <td>
             <asp:DropDownList ID="ui_thirdsupplier_dropdown" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Unit of Measure *</td>
            <td>
            <asp:textbox runat="server" ID="ui_uom_textbox"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td>Bin # </td>
            <td>
            <asp:textbox runat="server" ID="ui_binnum_textbox"></asp:textbox>
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
    <script  type="text/javascript">
        loadAllStationaries("/StoreClerk/StatoneryDetail.aspx");
</script>
</asp:Content>
