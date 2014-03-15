<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreSupervisor_Template.Master" AutoEventWireup="true" CodeBehind="DiscrepencyList.aspx.cs" Inherits="PresentationLayer.StoreSupervisor.DiscrepencyList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type = "text/javascript">
    function Check_Click(objRef) {
        //Get the Row based on checkbox
        var row = objRef.parentNode.parentNode;
        if (objRef.checked) {
            //If checked change color to Aqua
            $(row).addClass("row_checked_bg");
        }
        else {
            //If not checked change back to original color
            $(row).removeClass("row_checked_bg");
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
                     inputList[i].checked = false;

                 }
             }
         }
     }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Reported Discrepancies" class="content_title"></asp:Label> 
    </div>
    
    <asp:GridView  runat="server" ID="discrepancyListView" AutoGenerateColumns="False" GridLines="None"
        DataKeyNames="id"  AllowPaging="True"  PageSize="10"
        onselectedindexchanged="discrepancyListView_SelectedIndexChanged"
        OnPageIndexChanging="discrepancyListView_PageIndexChanging" 
        class="table" style="width:100%" EnableTheming="False" >
        <Columns>
        <asp:TemplateField HeaderText=" ">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headSelector" runat="server" onclick = "checkAll(this);"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="selector" runat="server" onclick = "Check_Click(this)"/>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id"  ItemStyle-CssClass="hide" 
                    HeaderStyle-CssClass="hide">
<HeaderStyle CssClass="hide"></HeaderStyle>

<ItemStyle CssClass="hide"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="stationery_name" HeaderText="Stationery Name" 
                SortExpression="stationery_name"></asp:BoundField>
            <asp:BoundField DataField="quantity" HeaderText="Quantity" 
                SortExpression="quantity"></asp:BoundField>
            <asp:BoundField DataField="price" HeaderText="Unit Price" 
                SortExpression="price"></asp:BoundField>
            <asp:BoundField DataField="totalprice" HeaderText="Total Price" 
                SortExpression="totalprice" />
            <asp:BoundField DataField="datetime_reported" HeaderText="Date of Report" 
                SortExpression="datetime_reported" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
            <asp:BoundField DataField="remark" HeaderText="Remarks" 
                SortExpression="remark" NullDisplayText="-"></asp:BoundField>
            <asp:CheckBoxField />
        </Columns>
        <EmptyDataTemplate>
                No Discrepancies found.
            </EmptyDataTemplate>
     </asp:GridView>
    <br />
    <asp:linkbutton ID="Linkbutton1" CssClass="btn btn-default" runat="server" onclick="ApproveAll_Button_Click" 
        class="btn btn-medium btn-primary">Approve</asp:linkbutton>
    
    </asp:Content>

