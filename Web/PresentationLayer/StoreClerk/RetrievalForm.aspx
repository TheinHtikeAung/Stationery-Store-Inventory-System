<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="RetrievalForm.aspx.cs" Inherits="PresentationLayer.StoreClerk.RetrievalForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $(".retrival_detail_btn").click(function () {
            $("#content").css("height", "auto");
        });
    });
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_title_container">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="ui_content_title_label" runat="server" Text="Stationeries Retrieval List" class="content_title"></asp:Label> 
    </div>
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" EnablePartialRendering="true">
 <ContentTemplate>--%>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
         <asp:GridView ID="retrievalGridview_Master" runat="server" 
             AutoGenerateColumns="False" class="table table-hover table-striped" 
             GridLines="None" onrowcommand="retrievalGridView_RowCommand">
             <Columns>
                 <asp:BoundField DataField="id" HeaderStyle-CssClass="hide" HeaderText="ID" 
                     ItemStyle-CssClass="hide" />
                 <asp:BoundField DataField="stationery_name" HeaderText="Stationery Description" 
                     NullDisplayText="-" ReadOnly="True" />
                 <asp:BoundField DataField="Needed" HeaderText="Needed" NullDisplayText="-" />
                 <asp:BoundField DataField="Retrieved" HeaderText="Retrieved" />
                 <asp:TemplateField HeaderStyle-Width="50%" HeaderText="Details">
                     <ItemTemplate>
                         <asp:Button ID="btn_Show" runat="server" 
                             CommandArgument="<%# Container.DataItemIndex %>" CommandName="Details" 
                             CssClass="btn btn-small btn-default retrival_detail_btn" Text="Details &gt;&gt;" 
                             ToolTip="View Details" />
                         <asp:GridView ID="retrievalGridview_Details" runat="server" 
                             AutoGenerateColumns="false" EmptyDateText="" GridLines="None">
                             <Columns>
                                 <asp:BoundField DataField="department_name" HeaderText="Dept Name" 
                                     ItemStyle-HorizontalAlign="Left" ItemStyle-Width="189px" />
                                 <asp:BoundField DataField="Needed" HeaderText="Needed" 
                                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="187px" />
                                 <asp:BoundField DataField="Actual" HeaderText="Actual" 
                                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="187px" />
                             </Columns>
                         </asp:GridView>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
     </ContentTemplate>
        </asp:UpdatePanel>
    <%--</ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger ControlID="btn_Show" EventName="Click" />
    </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
