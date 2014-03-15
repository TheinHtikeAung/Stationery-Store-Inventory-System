<%@ Page    Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="RequisitionFulfillment.aspx.cs" Inherits="PresentationLayer.RequisitionFulfillment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <style type="text/css">
        .hiddencol
        {
            display: none;
        }
       .style1
       {
           width: 182%;
       }
       .style3
       {
           width: 69%
       }
       .style4
       {
           width: 100%;
       }
       .style5
       {
           width: 165px
       }
       .style6
       {
           width: 165px;
           height: 34px;
       }
       .style7
       {
           height: 34px;
       }
    </style>

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

    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic" 
                            ErrorMessage="Exceed than requested Qty" Font-Size="Medium" Operator="LessThanEqual" 
                            ValueToCompare='<%# Bind("requested_quantity", "{0:D}") %>' 
                            ForeColor="Red" ControlToValidate="TextBox1"></asp:CompareValidator>--%>
    <div>
    
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        
        <div class="content_title_container">
            <asp:Label ID="ui_content_title_label" runat="server" Text="" class="content_title"></asp:Label>
            
        </div>
        
        <table class="style4">
            <%--<tr>
                <td class="style5" height="30px">
        
                    Department :</td>
                <td height="30px">
        
                    <asp:Label ID="lblDeptName" runat="server"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td class="style5" height="30px">
        
                    Collection Point
                </td>
                <td height="30px">
        
                    : <asp:Label ID="lblCollectionPoint" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
        
                    Representative Name </td>
                <td class="style7">
        
                    : <asp:Label ID="lblReptName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
        
        <asp:Label ID="lblStatus" runat="server" Text="Status" Visible="False" 
            class="label label-success"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="GridView1" runat="server" GridLines="None"
            AutoGenerateColumns="False" 
            DataKeyNames="id,department,stationery,stationery_name,requested_quantity,fulfill_quantity,unfulfill_quantity" style="margin-top: 15px; text-align: center;" 
            onrowdatabound="GridView1_RowDataBound" 
            onrowcreated="GridView1_RowCreated" 
            onselectedindexchanged="GridView1_SelectedIndexChanged"
            class="table processing_table">
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
                    SortExpression="id" Visible="False" />
                <asp:BoundField DataField="department" HeaderText="department" ReadOnly="True" 
                    SortExpression="department" ItemStyle-CssClass="hiddencol" 
                    HeaderStyle-CssClass="hiddencol">
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />
                </asp:BoundField>
                <asp:BoundField DataField="stationery" HeaderText="stationery" ReadOnly="True" 
                    SortExpression="stationery" ItemStyle-CssClass="hiddencol" 
                    HeaderStyle-CssClass="hiddencol">
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />
                </asp:BoundField>
                <asp:BoundField DataField="stationery_name" HeaderText="Stationery" 
                    ReadOnly="True" SortExpression="stationery_name" >
                <ItemStyle HorizontalAlign="Center" Width="350px" />
                </asp:BoundField>
                <asp:BoundField DataField="requested_quantity" HeaderText="Requested Qty" 
                    ReadOnly="True" SortExpression="requested_quantity" ItemStyle-CssClass="pad10" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fulfill_quantity" HeaderText="fulfill_quantity" 
                    ReadOnly="True" SortExpression="fulfill_quantity" ItemStyle-CssClass="hiddencol" 
                    HeaderStyle-CssClass="hiddencol">
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />
                </asp:BoundField>
                <asp:BoundField DataField="unfulfill_quantity" HeaderText="unfulfill_quantity" 
                    ReadOnly="True" SortExpression="unfulfill_quantity" ItemStyle-CssClass="hiddencol" 
                    HeaderStyle-CssClass="hiddencol">
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="Fulfillment Qty" HeaderStyle-CssClass="validation_column">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" 
                            Text='<%# Bind("fulfill_quantity", "{0:D}") %>' Width="60px"></asp:TextBox>
                            
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="TextBox1" ErrorMessage="<br />* Fill Qty"
                            Display="Dynamic" CssClass="invilid_field"></asp:RequiredFieldValidator>    
                            
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToValidate="TextBox1" 
                            ErrorMessage='<%# "<br />* Only "+Eval("stock_control", "{0:D}")+" left "%>'
                            ValueToCompare='<%# Bind("stock_control", "{0:D}") %>' 
                            Operator="LessThanEqual" SetFocusOnError="True" 
                            Display="Dynamic" Type="Integer" CssClass="invilid_field" ></asp:CompareValidator>
                            
                        <asp:RangeValidator ID="RangeValidator1" runat="server" 
                            ControlToValidate="TextBox1" ErrorMessage="<br />* Invalid Qty" MaximumValue='<%# Eval("requested_quantity", "{0:D}") %>'
                            MinimumValue="0" Display="Dynamic"  CssClass="invilid_field"
                            Type="Integer"></asp:RangeValidator>
                        
                        <%--<asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic" 
                            ErrorMessage="Exceed than requested Qty" Font-Size="Medium" Operator="LessThanEqual" 
                            ValueToCompare='<%# Bind("requested_quantity", "{0:D}") %>' 
                            ForeColor="Red" ControlToValidate="TextBox1"></asp:CompareValidator>--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                
                <asp:BoundField DataField="stock_control" HeaderText="Available Qty" 
                    SortExpression="stock_control" >
                <%--<HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />--%>
                </asp:BoundField>

                <asp:CommandField ShowSelectButton="True" ButtonType="Link" 
                    SelectText="Enroll" ItemStyle-CssClass="hiddencol" 
                    HeaderStyle-CssClass="hiddencol" Visible="False">
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />
                </asp:CommandField>
            </Columns>
            <EmptyDataTemplate>
                No Requisitions.
            </EmptyDataTemplate>
        </asp:GridView>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/assets/images/ajax-loader .gif" />
                <asp:Label ID="Label1" runat="server" Text=" Updating ..."></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <table>
            <tr>
                <td class="style3">

                    <a class="btn btn-primary btn-medium rpf-button" href="RequisitionFulfillmentMaster.aspx" >Back</a>
                    <%--<asp:Button ID="btnBack" runat="server" Text="Back" 
                        class="btn btn-medium btn-inverse" 
                        PostBackUrl="~/StoreClerk/RequisitionFulfillmentMaster.aspx" Width="120px"/>--%>
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Save" 
                        Width="120px"  class="btn btn-primary btn-medium rpf-button"  data-toggle="popover" title="" data-content="And here's some amazing content. It's very engaging. right?" data-original-title="A Title"/>
                        

                    <asp:Button ID="btnFulfillQty" runat="server" 
                         class="btn btn-primary btn-medium rpf-button" onclick="btnFulfillQty_Click" 
                        Text="Fulfill All" Width="120px" />
                    
                </td>
                <td style="text-align: left">
                    <asp:Button class="btn btn-primary btn-medium rpf-button" ID="btnProceed" runat="server" onclick="btnProceed_Click" 
                        Text="Merge" Width="120px" />
                   
                    <asp:Button ID="btnDistribute" runat="server" class="btn btn-danger btn-medium rpf-button"
                        onclick="btnDistribute_Click" Text="Distributed" Width="120px" />

                    
                    </td>
            </tr>
            </table>
            <table style="margin-top:20px;" class="tbl-input-small">
                <tr style="text-align: left"><td>
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl="~/StoreClerk/DiscrepancyMaintenance.aspx" Target="_blank">Report Discrepency</asp:HyperLink>
                    <asp:LinkButton ID="LinkButton1" runat="server" 
                        onclientclick="aspnetForm.target ='_blank';" onclick="LinkButton1_Click" 
                        PostBackUrl="~/StoreClerk/DiscrepancyMaintenance.aspx" Visible="False">Report Discrepency</asp:LinkButton>
                </td></tr>
                <tr><td>
                    <table>
                        <tr>
                            <td><asp:CheckBox ID="chkDateTime" runat="server" AutoPostBack="True" 
                        oncheckedchanged="chkDateTime_CheckedChanged" Text="" /></td>
                            <td><div style="margin:6px 0 0 6px;">Filter with Date / Time</div></td>
                        </tr>
                    </table>
                    
                    <asp:Label ID="Label2" runat="server" Text="Date : " Visible="False"></asp:Label>
                    <asp:TextBox ID="txtDate" runat="server" Height="22px" Visible="False" 
                        Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                        TargetControlID="txtDate">
                    </asp:CalendarExtender>
                    
                    <asp:Label ID="Label3" runat="server" Text="Time : " Visible="False"></asp:Label>
                    <asp:TextBox ID="txtTime" runat="server" MaxLength="2" Visible="False" 
                        Width="20px"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtTime_FilteredTextBoxExtender" 
                        runat="server" FilterType="Numbers" TargetControlID="txtTime" 
                        ValidChars="123456789">
                    </asp:FilteredTextBoxExtender>
                    <asp:Label ID="Label4" runat="server" Text=":" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtTimeMin" runat="server" MaxLength="2" Visible="False" 
                        Width="20px"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtTimeMin_FilteredTextBoxExtender" 
                        runat="server" FilterType="Numbers" TargetControlID="txtTimeMin" 
                        ValidChars="123456789">
                    </asp:FilteredTextBoxExtender>
                    <asp:DropDownList ID="ddlTime" runat="server" Visible="False" Width="70px">
                        <asp:ListItem>AM</asp:ListItem>
                        <asp:ListItem>PM</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    <br />
    
    </ContentTemplate>
    </asp:UpdatePanel>
        
    </div>
    
    <%--</form>--%>
</asp:Content>
