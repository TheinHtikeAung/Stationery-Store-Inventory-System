<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreClerk_Template.Master" AutoEventWireup="true" CodeBehind="AddNewDepartment.aspx.cs" Inherits="PresentationLayer.StoreClerk.AddNewDepartment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <asp:Label ID="Label1" runat="server" Text="Department    Details"></asp:Label>
   <br />
    <asp:Label ID="Label2" runat="server" Text="code"></asp:Label>  
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Text="phone"></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label4" runat="server" Text="department_name"></asp:Label>
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    <br />
     <asp:Label ID="Label5" runat="server" Text="contact_name"></asp:Label>
     <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label6" runat="server" Text="department_head"></asp:Label>
    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label7" runat="server" Text="collection_point"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
    <br />
    <asp:Label ID="Label8" runat="server" Text="representative_name"></asp:Label>
    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
    <br/>
    <asp:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click1" 
        Height="29px" Width="71px" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="Clean up files" />
  
    

</asp:Content>
