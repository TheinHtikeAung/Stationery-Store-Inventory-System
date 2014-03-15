<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="PurchaseOrders.aspx.cs" Inherits="PresentationLayer.StoreClerk.PurchaseOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type = "text/javascript">
    function Check_Click(objRef) {
        //Get the Row based on checkbox
        var row = objRef.parentNode.parentNode;
        if (objRef.checked) {
            //If checked change color to Aqua
            $(row).addClass("row_checked_bg");
            //row.style.backgroundColor = "grey";
        }
        else {
            $(row).removeClass("row_checked_bg");
            //If not checked change back to original color
            //row.style.backgroundColor = "white";
            /*
            if (row.rowIndex % 2 == 0) {
            //Alternating Row Color
            row.style.backgroundColor = "#C2D69B";
            }
            else {
            row.style.backgroundColor = "white";
            }
            */
        }

        //Get the reference of GridView
        var GridView = row.parentNode;

        //Get all input elements in Gridview
        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {
            //The First element is the Header Checkbox
            var headerCheckBox = inputList[0];

            //Based on all or none checkboxes
            //are checked check/uncheck Header Checkbox
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;

    }
    </script>

    <script type = "text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        //row.style.backgroundColor = "grey";
                        $(row).addClass("row_checked_bg");
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        /*
                        if (row.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                        row.style.backgroundColor = "white";
                        }
                        */
                        $(row).removeClass("row_checked_bg");
                        //row.style.backgroundColor = "white";
                        inputList[i].checked = false;

                    }
                }
            }
        }
</script>

<script type = "text/javascript">
    function MouseEvents(objRef, evt) {
        var checkbox = objRef.getElementsByTagName("input")[0];
        if (evt.type == "mouseover") {
            //objRef.style.backgroundColor = "orange";
        }
        else {
            if (checkbox.checked) {
                //objRef.style.backgroundColor = "grey";
                $(objRef).addClass("row_checked_bg");
            }
            else if (evt.type == "mouseout") {
                /*
                if (objRef.rowIndex % 2 == 0) {
                //Alternating Row Color
                objRef.style.backgroundColor = "#C2D69B";
                }
                else {
                objRef.style.backgroundColor = "white";
                }
                */
                $(objRef).removeClass("row_checked_bg");
                //objRef.style.backgroundColor = "white";
            }
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Purchase Orders" class="content_title"></asp:Label>
</div>

    <asp:GridView ID="ui_pos" runat="server" AutoGenerateColumns="False"   AllowPaging="True"  PageSize="10" 
        DataSourceID="ObjectDataSource1" class="table processing_table" GridLines="None">
        <Columns>
            <asp:TemplateField HeaderText=" ">
                <HeaderTemplate>
                    <asp:CheckBox ID="headSelector" runat="server" onclick = "checkAll(this);"/>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="selector" runat="server" onclick = "Check_Click(this)"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" 
                HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide" >
<HeaderStyle CssClass="hide"></HeaderStyle>

<ItemStyle CssClass="hide"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="stationery_name" HeaderText="Stationery Name" 
                SortExpression="stationery_name" />
            
            <asp:BoundField DataField="supplier_name" HeaderText="Supplier Name" 
                SortExpression="supplier_name" />
            
            <asp:BoundField DataField="po_reordered_quantity" 
                HeaderText="Ordered Quantity" SortExpression="po_reordered_quantity" />
           
            <asp:BoundField DataField="po_reorder_date" HeaderText="Ordered Date" 
                SortExpression="po_reorder_date" dataformatstring="{0:MMMM d, yyyy}" htmlencode="false"/>
        </Columns>
    </asp:GridView>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Arrive" class="btn btn-primary btn-medium"/>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="actionGetWaitingPurchaseOrders" 
        TypeName="PresentationLayer.StoreClerk.PurchaseOrders">
    </asp:ObjectDataSource>

</asp:Content>
