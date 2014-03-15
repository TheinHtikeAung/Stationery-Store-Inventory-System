<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/DepartmentEmployee_MobileTemplate.Master" AutoEventWireup="true" CodeBehind="Stationeries.aspx.cs" Inherits="PresentationLayer.m.Requisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="page">
        <div data-role="header">
            <a href="/m/Logout.aspx"  data-rel="dialog" data-icon="delete" data-ajax="false">Logout</a>
		    <h1>Stationeries</h1>
            <a href="#" data-icon="refresh" data-iconpos="notext" onclick="loadAllStationaries()"></a>
	    </div><!-- /header -->
        
	    <div data-role="content">
            <script type="text/javascript">
                $(function () { $("#flash-message").flashMessage().delay(1000).fadeOut(400); });
            </script>
        
            <div id="flash-message"></div>
		    <ul id="stationeries" data-role="listview" data-inset="true" data-filter="true" data-filter-reveal="true" data-filter-placeholder="Search Stationeries...">
            </ul>
	    </div><!-- /content -->
    </div><!-- /page -->
    <script type="text/javascript">
        function loadAllStationaries() {
        $.ajax(
        {
            type: "POST",
            // type: "GET",
            url: "/DepartmentEmployee/Requisition.aspx/GetAllStationaries",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                var jsonObj = $.parseJSON(msg.d);
                var stationaryJSONArray = [];
                $("#stationeries").empty();
                for (var x = 0; x < jsonObj.length; x++) {
                    $("#stationeries").append('<li><a data-transition="slide" data-ajax="false" href="Requisition.aspx?id=' + jsonObj[x].id + '">' + jsonObj[x].stationery_name + '</a></li>');

                    //stationaryJSONArray.push({ id: jsonObj[x].id, label: jsonObj[x].stationery_name });
                }
                $("#stationeries").listview("refresh");

            }, error: function (x, e) {

                //$("#error").text("Error occured" + x.responseText);
                //alert("Error occured" + x.responseText);
            }

        }

        );
        } //loadAllStationaries
        loadAllStationaries();
    </script>
</asp:Content>
