<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="RequisitionFulfillmentMaster.aspx.cs" Inherits="PresentationLayer.StoreClerk.RequisitionFulfillmentMaster" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script type = "text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                //row.style.backgroundColor = "grey";
                $(row).addClass("row_checked_bg");
            }
            else {
                //If not checked change back to original color
                //row.style.backgroundColor = "white";
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
                        //row.style.backgroundColor = "red";
                        inputList[i].checked = true;
                    }
                    else {
                        $(row).removeClass("row_checked_bg");
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
            //*objRef.style.backgroundColor = "grey"; //Orange
            //*objRef.style.color = "white";
        }
        else {
            if (checkbox.checked) {
                //*objRef.style.backgroundColor = "grey";
                //*objRef.style.fontWeight = "bold"
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
                //*objRef.style.backgroundColor = "white";
                //*objRef.style.color = "DarkBlue";
                objRef.style.fontWeight = "normal"
            }
        }
    }
</script>

    <style type="text/css">
        .fullfillment-departments-table table tr{height:50px;}
        .fullfillment-departments-table table td{vertical-align:middle;}
        .style1
        {
            width: 100%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >

<div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Requistions from Departments" class="content_title"></asp:Label>
</div>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--<table class="style1">
                <tr>
                    <td height="40px">
                        <asp:Label ID="lblStatus" runat="server" class="label label-success" 
                            Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>--%>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                class="fullfillment-departments-table table processing_table" GridLines="None" DataKeyNames="department_name" 
                onrowcreated="GridView1_RowCreated" onrowdatabound="GridView1_RowDataBound" 
                onselectedindexchanged="GridView1_SelectedIndexChanged"
                >
                <Columns>
                    <asp:TemplateField HeaderText=" ">
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" onclick="checkAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="selector" runat="server" onclick="Check_Click(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="department_name" HeaderText="Department Name" 
                        ReadOnly="True" SortExpression="department_name">
                    <ControlStyle Width="30%" />
                    <ItemStyle Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="id" />
                    
                    <asp:CommandField ButtonType="Link" SelectText="Detail" ShowSelectButton="True">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemStyle CssClass="hiddencol" />
                    </asp:CommandField>
                    <asp:BoundField HeaderText="Collection Point" DataField="name" >
                    <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Representative" DataField="representative_name" >
                    <ItemStyle Width="20%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            
            <asp:Button ID="btnProcess" runat="server" class="btn btn-medium btn-primary" 
                onclick="btnProcess_Click" Text="Merge" />
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" 
                Visible="False" />
            <br />
           <table style="margin:10px 0 0 0;">
            <tr>
                <td><asp:CheckBox ID="chkDateTime" runat="server" AutoPostBack="True" oncheckedchanged="chkDateTime_CheckedChanged" Text="" /></td>
                <td><div style="margin:6px 0 0 6px;">Filter with Date/Time</div></td>
            </tr>
           </table>
           
            <table style="margin-top:20px;" class="tbl-input-small">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Date : " Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" Visible="False"></asp:TextBox>
                        <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                            TargetControlID="txtDate">
                        </asp:CalendarExtender>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Time : " Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTime" runat="server" MaxLength="2" Visible="False" 
                            Width="30px"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="txtTime_FilteredTextBoxExtender" 
                            runat="server" FilterType="Numbers" TargetControlID="txtTime" 
                            ValidChars="123456789">
                        </asp:FilteredTextBoxExtender>
                        <asp:Label ID="Label4" runat="server" Text=":" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtTimeMin" runat="server" Visible="False" Width="30px"></asp:TextBox>
                        <asp:DropDownList ID="ddlTime" runat="server" Visible="False" Width="70px">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
    <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
