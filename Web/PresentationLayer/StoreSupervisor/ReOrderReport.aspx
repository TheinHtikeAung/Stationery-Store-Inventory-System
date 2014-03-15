<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/StoreSupervisor_Template.Master" AutoEventWireup="true" CodeBehind="ReOrderReport.aspx.cs" Inherits="PresentationLayer.StoreClerk.ReOrderReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/assets/js/js_for_reports/Chart.js"></script>
<script type="text/javascript" src="/assets/js/js_for_reports/jQuery.js"></script>
<script type="text/javascript" src="/assets/js/js_for_reports/json.js"></script>

<script type="text/javascript" src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script> 
<script type="text/javascript" src='https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/jquery-ui.min.js'></script>
<script type="text/javascript" src="/assets/js/js_for_reports/jquery.mtz.monthpicker.js"></script>
<link rel='stylesheet' href='http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/themes/base/jquery-ui.css'/>

    <style type="text/css">
      td.mtz-monthpicker-month {
        text-align:center;
      }
      select.mtz-monthpicker:focus {
        outline: none;
      }
      
      #reportTable
      {
          visibility:hidden;
      }
      
      #noDataFoundLbl
      {
          visibility:hidden;
          font-size:large;
      }
    </style>

    

    <script type="text/javascript">
        $(function () {
            options = {
                pattern: 'mm/yyyy', // Default is 'mm/yyyy' and separator char is not mandatory
                selectedYear: 2013,
                startYear: 2000,
                finalYear: 2013
            };
            $('#default_widget').monthpicker(options);
            //*** Not Working!!! resolve it later
//            $('#default_widget').monthpicker().bind('monthpicker-change-year', function (e, year) {
//                $('#default_widget').monthpicker('disableMonths', []); // (re)enables all
//                if (year === '2013') {
//                    $('#last_widget').monthpicker('disableMonths', [10,11,12]);
//                }
//                
//            });
        });
    

//       $(document).ready(function () {
//           $('#ContentPlaceHolder1_showBtn').click(function () {
//               if (!($("#ContentPlaceHolder1_categoryDropDownList").val())) {
//                   alert("Please Fill Stationery");
//                   return;
//               }
//               retrieveBarChartData();

//           });

//       });

        function retrieveBarChartData() {
            //alert("Hello");
            /*var stationeryName = documentdocument.getElementById("ContentPlaceHolder1_categoryDropDownList");
            alert(stationeryName.value);*/


            var monthListBox = document.getElementById("ContentPlaceHolder1_monthListBox");
            if (monthListBox.length < 1) {
                alert("Please select atleast 1 month");
                return false;
            } else {
                var setobj = {};
                for (i = 0; i < monthListBox.length; i++) {
                    if (setobj[monthListBox[i].value.toString()]) {
                        alert("Please select different months for comparison");
                        return false;
                    } else {
                        setobj[monthListBox[i].value.toString()] = true;
                    }
                }
            }

            var barCharJsonText = "";
            for (i = 0; i < monthListBox.length; i++) {
                if (i > 0) {
                    barCharJsonText = barCharJsonText + ",";
                }
                barCharJsonText = barCharJsonText + monthListBox[i].value;
            }
                //alert(barCharJsonText);
            $.ajax(
            {
                type: "POST",
                // type: "GET",
                url: "ReOrderReport.aspx/GetAllPurchaseOrders",
                //data: "{\"jsonParam\" :{" + barCharJsonText + "}}",
                //data: "{\"jsonParam\" :" + JsonConvert.SerializeObject(barCharJsonArr)  + "}",
                //data: jsonParam,
                data: "{\"jsonParam\":\"" + barCharJsonText + "\"}",
                //data: "{" + barCharJsonText + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    var jsonObj = $.parseJSON(msg.d);
                    //alert(jsonObj["Pen Blu"]["12/2013"]);
                    //soft code bar chart using above variable!!
                    var barChartData = {
                        labels: [],
                        datasets: [
                                {
                                    fillColor: "rgba(220,220,220,0.5)",
                                    strokeColor: "rgba(220,220,220,1)",
                                    data: []
                                },
    	                        {
    	                            fillColor: "rgba(151,187,205,0.5)",
    	                            strokeColor: "rgba(151,187,205,1)",
    	                            data: []
    	                        },
                                {
                                    fillColor: "rgba(111,127,210,0.5)",
                                    strokeColor: "rgba(111,127,210,1)",
                                    data: []
                                }
                            ]
                    }

                    var categorycount = 0;
                    var categories = new Array();
                    barChartData.labels = new Array();
                    for (var category in jsonObj) {
                        //alert(category);
                        barChartData.labels.push(category);
                        categories.push(category);
                        categorycount++;
                    }

                    var monthyears = new Array();
                    for (i = 0; i < monthListBox.length; i++) {
                        monthyears.push(monthListBox[i].value.toString().replace("/", "-"));
                        barChartData.datasets[i].data = new Array();
                    }

                    for (j = 0; j < categorycount; j++) {
                        for (i = 0; i < monthListBox.length; i++) {
                            barChartData.datasets[i].data[j] = '0';
                        }
                    }

                    var max_val = 0;
                    for (var category in jsonObj) {
                        var categoryindex = categories.indexOf(category);
                        for (var monthyear in jsonObj[category]) {
                            var monthindex = monthyears.indexOf(monthyear);
                            barChartData.datasets[monthindex].data[categoryindex] = jsonObj[category][monthyear];

                            if (barChartData.datasets[monthindex].data[categoryindex] > max_val) {
                                max_val = barChartData.datasets[monthindex].data[categoryindex];
                            }

                        }
                    }
                    
                    plotBarChart(barChartData, max_val);
                    loadAllRecords(barChartData, monthyears);

                }, error: function (x, e) {
                    //                $("#error").text("Error occured" + x.responseText);
                    //                alert("Error occured" + x.responseText);
                    //alert("Error");
                }
            });
            
        
            return false;
        }

        function plotBarChart(data, max_val) {
            var steps = 30;
            
            var myLine = new Chart(document.getElementById("canvas").getContext("2d")).Bar(data,
            {
//                scaleOverride: true,
//                scaleSteps: steps,
//                scaleStepWidth: Math.ceil(max_val / steps),
//                scaleStartValue: 0
            }
            );
            
	    } //barChart

	    function loadAllRecords(barChartData, monthyears) {
	        var tablerow = '';
	        $('#stationaryTable tbody').html('');
	        for (var i = 0; i < barChartData.labels.length; i++) {
	            var categoryName = barChartData.labels[i];
	            for (var j = 0; j < monthyears.length; j++) {
	                if (j == 0) {
	                    tableRow = '<tr><td rowspan=' + monthyears.length+'>' + categoryName + '</td>' +
                                           '<td>' + monthyears[j] + '</td><td>' + barChartData.datasets[j].data[i] + '</td>/tr>';
	                } else {
	                    tableRow += '<tr><td>' + monthyears[j] + '</td><td>' + barChartData.datasets[j].data[i] + '</td>/tr>';
	                }
	            }
	            if (tableRow == '') {
	                document.getElementById("noDataFoundLbl").style.visibility = "visible";
	                document.getElementById("dataDisplay").style.visibility = "hidden";
	            }
	            else {
	                $(tableRow).appendTo('#stationaryTable tbody');
	                document.getElementById("reportTable").style.visibility = "visible";
	            }
	        }
	    } //loadAllRecords
 
       function toListBoxBtn_click() {
           var listBox = document.getElementById("default_widget");
           var monthListBox = document.getElementById("ContentPlaceHolder1_monthListBox");
           if (monthListBox.length < 3) {
               var selected_date = listBox.value;
               monthListBox.add(new Option(selected_date));
           } else {
                alert("Maximum allowed no of months is only 3 at this time");
           }
	        return false;
	    }

	    function removeBtn_click() {
	        var monthListBox = document.getElementById("ContentPlaceHolder1_monthListBox");
	        if (monthListBox.length > 0) {
	            monthListBox.remove(monthListBox.selectedIndex);
                //monthListBox.add(new Option(selected_date));
	        } else {
	            
	        }
	        return false;
	    }

	    function removeAllBtn_click() {
	        var monthListBox = document.getElementById("ContentPlaceHolder1_monthListBox");
	        if (monthListBox.length > 0) {
	            monthListBox.options.length = 0;
	        } else {
	            
	        }
	        return false;
	    }

	    function Print() {
	        try {
                /*
	            var printContent = document.getElementById("divPrint").innerText;
	            window.print(printContent);
                */
	            window.print();
	        }
	        catch (e) {
	            alert(e.Data);
	            self.print();
	        }
	    }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Reorders Report" class="content_title"></asp:Label> 
    </div>
    <div class="noPrint">
    <asp:Table ID="Table1" runat="server" class="table" style="text-align:left;">    
    <asp:TableRow>    
        <%--<asp:TableCell RowSpan="3">            
            <asp:DropDownList ID="categoryDropDownList" AppendDataBoundItems="true" runat="server">
                <asp:ListItem Value="" Selected="True" Text="--All--" />   
            </asp:DropDownList>
           </asp:TableCell>--%>
        <asp:TableCell RowSpan="3" style="width:280px;">
             <asp:Label ID="dates" runat="server" Text="Select Month"></asp:Label>
             <input type="text" id="default_widget"/>
        </asp:TableCell>
        <asp:TableCell RowSpan="3">
            <asp:Button ID="toListBoxBtn" runat="server" CssClass="btn btn-small" style="width:40px;" Text=">" Font-Bold="True" ForeColor="#003300" OnClientClick="return toListBoxBtn_click();" ToolTip="Add"/><br />
            <asp:Button ID="removeBtn" runat="server" CssClass="btn btn-small" style="width:40px;" Text="<" Font-Bold="True" 
            ForeColor="#003300" OnClientClick="return removeBtn_click();" ToolTip="Remove"/><br />
            <asp:Button ID="removeAllBtn" runat="server" Text="<<" CssClass="btn btn-small" style="width:40px;" Font-Bold="True" 
        ForeColor="#003300" OnClientClick="return removeAllBtn_click();" ToolTip="Remove All"/>
        </asp:TableCell>
        <asp:TableCell RowSpan="3">
            <asp:Label ID="errLbl" runat="server" Text="" Visible="false" Font-Size="Small" ForeColor="Red"></asp:Label>
        <asp:ListBox ID="monthListBox" runat="server"></asp:ListBox>
        </asp:TableCell>        
    </asp:TableRow>
    </asp:Table>

    <asp:Table ID="Table2" runat="server" Height="50px">
        <asp:TableRow>
             <asp:TableCell>
                <asp:Button ID="showBtn" runat="server" CssClass="btn btn-medium btn-info" Text="Show Statistics" OnClientClick=" return retrieveBarChartData();"/>   
             </asp:TableCell>  
             <asp:TableCell>
                <input type="button" value="Print" class="btn btn-medium btn-info" onclick="Print()" />
             </asp:TableCell>  
        </asp:TableRow>        
    </asp:Table>
    </div>

    <div class="yesPrint">
    <div id="label">
    <label id="noDataFoundLbl" title="No Orders Found!!" ></label>
        </div>
       <div id="dataDisplay">
            <br />
            <br />
           <canvas id="canvas" height="600px" width="1000px"></canvas>

           <br />
           <br />
           <p></p>
           <div id="reportTable">
                <table id='stationaryTable' class="table table-hover table-striped">
                <thead><tr><td>Category</td><td>Date</td><td>Re-Ordered Quantity</td></tr></thead>
                <tbody>
                </tbody>
                </table>
            </div>
        </div>
    </div>    
</asp:Content>
