<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/DepartmentHead_Template.Master" AutoEventWireup="true" CodeBehind="ApproveRequisition.aspx.cs" Inherits="PresentationLayer.DepartmentHead.requisition_processing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <script  type="text/javascript">
     $(document).ready(function () {
         // alert('hi');
         //code to check and uncheck employeee
         $('.checkPendingEmployee').click(function () {
             //  alert("hello");
             var id = this.id;
             //alert("check employee is called with id:" + id);
             if ($(this).is(':checked')) {
                 $('.checkPendingStationary' + '.' + id).prop('checked', true);
             } //checked
             else {
                 $('.checkPendingStationary' + '.' + id).removeAttr('checked');
             }
         });

         $('#myTab a[href="#approved"]').tab('show');
         $('#myTab a[href="#rejected"]').tab('show');
         $('#myTab a[href="#pending"]').tab('show');
         loadAllPendingRequisitions();
         loadAllApprovedRequisitions();
         loadAllRejectedRequisitions();
         $('#checkAllPending').click(
      function () {
          // alert("Check all is clicked");

          if ($(this).is(':checked')) {
              $('.checkPendingEmployee').prop('checked', true);
              $('.checkPendingStationary').prop('checked', true);
          } //checkeding
          else {
              $('.checkPendingEmployee').removeAttr('checked');
              $('.checkPendingStationary').removeAttr('checked');
          }
      }); //checkAll


         $('#checkAllApproved').click(
      function () {
          // alert("Check all is clicked");

          if ($(this).is(':checked')) {
              $('.checkApprovedEmployee').prop('checked', true);
              $('.checkApprovedStationary').prop('checked', true);
          } //checked
          else {
              $('.checkApprovedEmployee').removeAttr('checked');
              $('.checkApprovedStationary').removeAttr('checked');
          }
      }); //checkAll

         //button click events
         $('#approveButton').click(function () {
             var approvedRequisitionJSONArray = [];
             if ($('.checkPendingStationary').is(':checked')) {
                 $('.checkPendingStationary').each(function () {
                     // alert('Hii');
                     if ($(this).is(':checked')) {
                         var id = $(this).attr('id');
                         approvedRequisitionJSONArray.push({ "id": id });
                     } //if   
                 }); //checkPendingStationary
                 //alert('Check::' + $.toJSON(approvedRequisitionJSONArray));
                 //
                 $.ajax(
        {
            type: "POST",
            url: "ApproveRequisition.aspx/approveRequisitions",
            data: "{'jsonParam' : " + $.toJSON(approvedRequisitionJSONArray) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                //DoSomething be a popup or alert to show successful deletion..

                removeRowsFromTable("pendingTable");
                removeRowsFromTable("approvedTable");
                // alert('hello before pending requisitions');
                loadAllPendingRequisitions();
                loadAllApprovedRequisitions();
            }, error: function (x, e) {
                //$("#error").text("Error occured" + x.responseText);
                //alert("Error occured" + x.responseText);
            }
            // alert("check point ..after deletion");
        }

        );

             } else {
                 //alert('Please select atleast one for approval'); 
             }

         }); //approveButton

         $('#rejectButton').click(function () {
             //alert("Reject button is clicked");


             var rejectReason = $("#rejectReason").val();
             $("#rejectModal").modal("hide");
             //  alert("Reject Reason:" + rejectReason);
             var rejectedRequisitionJSONArray = [];
             if ($('.checkPendingStationary').is(':checked')) {
                 $('.checkPendingStationary').each(function () {
                     // alert('Hii');
                     if ($(this).is(':checked')) {
                         var id = $(this).attr('id');
                         rejectedRequisitionJSONArray.push({ "id": id });
                     } //if   
                 }); //checkPendingStationary
                 // alert('Check::' + $.toJSON(rejectedRequisitionJSONArray));
                 //
                 $.ajax(
        {
            type: "POST",
            url: "ApproveRequisition.aspx/rejectRequisitions",
            data: "{'jsonParam' : " + $.toJSON(rejectedRequisitionJSONArray) + ",'reason':" + $.toJSON(rejectReason) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                //DoSomething be a popup or alert to show successful deletion..
                removeRowsFromTable("pendingTable");
                removeRowsFromTable("rejectedTable");
                loadAllRejectedRequisitions();
                loadAllPendingRequisitions();
                // loadAllApprovedRequisitions();
            }, error: function (x, e) {
                //$("#error").text("Error occured" + x.responseText);
                alert("Error occured" + x.responseText);
            }
            // alert("check point ..after deletion");
        }
        );
             } else {
                 // alert('Please select atleast one for rejection'); 
             }
         }); //rejectButton

     });                                     //ready
     function loadAllPendingRequisitions() {
         $.ajax(
        {
            type: "POST",
            url: "ApproveRequisition.aspx/LoadAllPendingRequests",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                var records = $.parseJSON(msg.d);
                for (var i = 0; i < records.length; i++) {
                    var record = records[i];
                    var id = i + 1;
                    var requisitions = record.UserRequisitions;
                    for (var j = 0; j < requisitions.length; j++) {
                        var tableRow;
                        if (j == 0) {
                            tableRow = '<tr> <td rowspan=' + requisitions.length + '>' + record.EmployeeName + '</td>' +
                           '<td><input type="checkbox" class="checkPendingStationary" id="' + requisitions[j].ReqId + '">' + requisitions[j].StationaryName + '</td><td>' + requisitions[j].Quantity + '</td></tr>';
                        } else {
                            tableRow = '<tr><td><input type="checkbox" class="checkPendingStationary" id="' + requisitions[j].ReqId + '">' + requisitions[j].StationaryName + '</td><td>' + requisitions[j].Quantity + '</td></tr>';
                        }
                        $(tableRow).appendTo('#pendingTable tbody');

                    } //inner for
                } //outer for 
            },
            error: function (x, e) {
                //alert("error occured  #######" + x.responseText);
            }
        }); //ajax

     } //loadPendingTable

     function removeRowsFromTable(tableId) {
         var elementTable = document.getElementById(tableId);
         var tableRows = elementTable.getElementsByTagName('tr');
         var rowCount = tableRows.length;

         for (var x = rowCount - 1; x > 0; x--) {
             tableRows[x].parentNode.removeChild(tableRows[x]);
         }
     }

     function loadAllApprovedRequisitions() {
         // alert('Approved req is called');
         $.ajax(
        {
            type: "POST",
            url: "ApproveRequisition.aspx/LoadAllApprovedRequests",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                var records = $.parseJSON(msg.d);
                for (var i = 0; i < records.length; i++) {
                    var record = records[i];
                    var id = i + 1;
                    var requisitions = record.UserRequisitions;
                    for (var j = 0; j < requisitions.length; j++) {
                        var tableRow;
                        if (j == 0) {
                            tableRow = '<tr> <td rowspan=' + requisitions.length + '>' + record.EmployeeName + '</td>' +
                           '<td>' + requisitions[j].StationaryName + '</td><td>' + requisitions[j].Quantity + '</td></tr>';
                        } else {
                            tableRow = '<tr><td>' + requisitions[j].StationaryName + '</td><td>' + requisitions[j].Quantity + '</td></tr>';
                        }
                        $(tableRow).appendTo('#approvedTable tbody');
                    } //for inner
                } //forouter
                //approvedTable

            },
            error: function (x, e) {
                //alert("error occured" + x.responseText);
            }
        });          //ajax

     } //loadAllApprovedRequisitions



     function loadAllRejectedRequisitions() {
         //alert('rejected');
         $.ajax(
        {
            type: "POST",
            url: "ApproveRequisition.aspx/LoadAllRejectedRequests",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                var records = $.parseJSON(msg.d);
                for (var i = 0; i < records.length; i++) {
                    var record = records[i];
                    var id = i + 1;
                    var requisitions = record.UserRequisitions;
                    for (var j = 0; j < requisitions.length; j++) {
                        var tableRow;
                        if (j == 0) {
                            tableRow = '<tr> <td rowspan=' + requisitions.length + '>' + record.EmployeeName + '</td>' +
                           '<td>' + requisitions[j].StationaryName + '</td><td>' + requisitions[j].Quantity + '</td></tr>';
                        } else {
                            tableRow = '<tr><td>' + requisitions[j].StationaryName + '</td><td>' + requisitions[j].Quantity + '</td></tr>';
                        }
                        $(tableRow).appendTo('#rejectedTable tbody');

                    } //inner for
                } //outer for 
            },
            error: function (x, e) {
                //alert("error occured  #######" + x.responseText);
            }
        }); //ajax

     } //loadRejectedTable

     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <ul class="nav nav-tabs" id="myTab">
    <li class="active"><a href="#pending" data-toggle="tab">Pending</a></li>    
    <li ><a href="#approved" data-toggle="tab">Approved</a></li>             
    <li> <a href="#rejected" data-toggle="tab">Rejected</a></li>
 </ul>
      <div class="tab-content">

          <div class="tab-pane active" id="pending">
           <form class="form-inline">
           <!-- <label class="checkbox">
<input type="checkbox" id="checkAllApproved"> Select All
</label>-->
           
           <%--<table class="commands-bar">
            <tr>
                <td><div style="width:215px;text-align:right;"></div></td>
                <td><div style="padding:5px 0 0 0px;width:80px;">Select All</div></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                
            </tr>
           </table>--%>
           
           </form> 
       <table  id='pendingTable' class='table'>
        <thead><tr><td class="empname_col">Employee Name</td><td class="item_col">
        <input type="checkbox" id="checkAllPending"/>
        <span style="margin: 0 5px 0 0;">Stationary</span>
        <button class="btn btn-success" style="margin:0 5px;" type="button" id="approveButton">Approve</button>
      <!--  <button class="btn btn-default" style="margin:0 5px;" id="rejectButton" type="button">Reject</button>-->
        <a href="#rejectModal" role="button" class="btn btn-default" style="margin:0 5px;" data-toggle="modal"> Reject </a>
                  

        </td><td class="qty_col">Quantity</td></tr></thead>
         <tbody>
        </tbody>
       </table>
          </div><!-- pendingRequisitionTable-->  
           <div class="tab-pane active" id="approved" >
          
        <table  id='approvedTable' class='table table-hover'>
<thead><tr><td class="empname_col">Employee Name</td><td class="item_col">Stationary</td><td class="qty_col">Quantity</td></tr></thead>
<tbody>
</tbody>
</table>
          </div><!-- approvedRequisitionTable-->
           <div class="tab-pane active" id="rejected">
        <table  id='rejectedTable' class='table table-hover'>
<thead><tr><td class="empname_col">Employee Name</td><td class="item_col">Stationary</td><td class="qty_col">Quantity</td></tr></thead>
<tbody>
</tbody>
</table>
          </div><!-- rejectedRequisitionTable-->  
       </div>
       
       <!-- Reject start -->
		<div id="rejectModal" class="modal hide fade" tabindex="-1" role="dialog"
			 aria-hidden="true">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"
					aria-hidden="true">x</button>
				<h3 id="H1">Reject Requisition</h3>
			</div>
			<div class="modal-body">
				<p id="p1">Please provide the reason for rejection.</p>
                <p><textarea rows="3" cols="20" id="rejectReason"></textarea></p>
			</div>
			<div class="modal-footer">
				<button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
				<button class="btn btn-primary" id="rejectButton">Reject</button>
                 <!--  <button class="btn btn-default" style="margin:0 5px;" id="rejectButton" type="button">Reject</button>-->
			</div>
		</div>
        <!-- Reject Modal end -->
       
 
</asp:Content>