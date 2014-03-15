<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/DepartmentEmployee_MobileTemplate.Master" AutoEventWireup="true" CodeBehind="Requisition.aspx.cs" Inherits="PresentationLayer.m.MakeRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    

</script>
    <div data-role="page">
        <div data-role="header" id="header_bar">
		    <a href="#" data-icon="arrow-l" onclick="window.history.back()">Back</a>
            <h1>Make Request</h1>
            
	    </div><!-- /header -->
        
	    <div data-role="content">
            <%--<form method="post" id="requisition_form" action="/m/DepartmentEmployee/RequisitionHandler.aspx" data-ajax="false">--%>
                <script type="text/javascript">
                    $(function () { $("#flash-message").flashMessage(); });
                </script>
                <div id="flash-message"></div>

                 <h3><asp:Label ID="ui_stationery_name" runat="server" Text="Label"></asp:Label></h3>
                 <table class="table">
                    <tr><td style="width:40%;">Category</td><td><asp:Label ID="ui_category" class="bold" runat="server" Text=""></asp:Label></td></tr>
                    <tr><td>Measurement</td><td><asp:Label ID="ui_uom" class="bold" runat="server" Text=""></asp:Label></td></tr>
                    <tr><td>Unit Pirce</td><td><span style="font-size:12px;">SGD</span> <asp:Label ID="ui_price" class="bold" runat="server" Text=""></asp:Label></td></tr>
                    <tr><td>Total</td><td><span style="font-size:12px;">SGD</span> <asp:Label ID="ui_total" class="bold" runat="server" Text=""></asp:Label></tr>
                 </table>
                    
                 <asp:HiddenField ID="ui_id_hidden" runat="server" />         
                 <asp:HiddenField ID="ui_price_hidden_forjscalculation" runat="server" /> 
                 <br />
                 <label for="slider-1">Quantity:</label><br />
                 <asp:TextBox onchange="calculateTotal()" ID="ui_quantity" runat="server" min="1" max="15" value="1" type="range"></asp:TextBox>
                 <br /><br />
                 <div style="text-align:center">
                 
                 <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" />
             </div>
            <%--</form>--%>
	    </div><!-- /content -->

    </div><!-- /page -->
    
</asp:Content>
