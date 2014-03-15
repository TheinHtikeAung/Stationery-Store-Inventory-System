<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PresentationLayer.StoreClerk.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>
        Welcome <asp:Label ID="ui_welcome_text" runat="server" Text="Label"></asp:Label>.
    </h4>

    <table class="table">
        <tr>
            <td>
                <h5>
                    <u>Here are some issues need to solve.</u>
                </h5>
                <div>
                    <p>
                        <asp:Label ID="ui_inventorystats_label" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="ui_0items_label" style="color:#e74c3c;" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="ui_pendingrequests_label" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </td>
            <td>
                <h5>
                    <u>Store Status</u>
                </h5>
                <div>
                    <p>
                        Total <asp:Label ID="ui_departments_count" runat="server" Text=""></asp:Label> departments.
                     </p>
                    <p>
                        Total <asp:Label ID="ui_customers_count" runat="server" Text=""></asp:Label> customers.
                    </p>
                    <p>
                        Total <asp:Label ID="ui_stationeries_count" runat="server" Text=""></asp:Label> stationeries.
                    </p>
                    <p>
                        Total <asp:Label ID="ui_disbursemetn_count" runat="server" Text=""></asp:Label> times distributed to departments.
                    </p>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
