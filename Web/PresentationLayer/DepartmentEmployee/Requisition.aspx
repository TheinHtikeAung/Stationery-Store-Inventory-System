<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatesWeb/DepartmentEmployee_Template.Master" AutoEventWireup="true" CodeBehind="Requisition.aspx.cs" Inherits="PresentationLayer.DepartmentEmployee.order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ui-autocomplete {
      max-height: 300px;
      overflow-y: auto;
      /* prevent horizontal scrollbar */
      overflow-x: hidden;
    }
    /* IE 6 doesn't support max-height
* we use height instead, but this forces the menu to always be this tall
*/
    * html .ui-autocomplete {
      height: 100px;
    }
</style>

  <script  type="text/javascript">
      var RequisitionModel = function (stationery, quantity) {
          this.stationery = stationery;
          this.quantity = quantity;
      }
      function makeRequisition() {
          var stId = $('#stationaryId').text();
          var qty = $('#quantity').val();
          var reqModelText = stId + "." + qty;
          $.ajax({
              type: "POST",
              url: "Requisition.aspx/MakeRequisition",
              data: "{'jsonParam' :" + reqModelText + "}",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              async: true,
              cache: false,
              success: function (msg) {
                  // alert("MESSAGE:" + msg.d);

                  if (msg.d == "success") {
                      $('#myModal').modal('hide');
                      // alert("requisition successful");
                      $('#sucessRequisition').fadeIn('slow');
                      $('#sucesspRequisition').delay(1000).fadeOut();
                    removeRowsFromTable('pendingTable');
                      loadPendingTable();
                  } else {
                      //failRequisition
                      $("body").html(msg.d);
                      $('#myModal').modal('hide');
                      // alert("requisition fail");
                      $('#failRequisition').fadeIn('slow');
                      $('#failRequisition').delay(1000).fadeOut();
                  }



              },
              error: function (x, e) {
                  //alert("error occured" + x.responseText);
              }
          });        //ajax
          $('#stationaryId').val('');
          $('#quantity').val('');
          $('#tags').val('');
      } //makeRequisition


      function loadAllStationaries() {
          $.ajax(
        {
            type: "POST",
            // type: "GET",
            url: "Requisition.aspx/GetAllStationaries",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                var jsonObj = $.parseJSON(msg.d);
                var stationaryJSONArray = [];
                for (var x = 0; x < jsonObj.length; x++) {
                    stationaryJSONArray.push({ id: jsonObj[x].id, label: jsonObj[x].stationery_name });
                }
                $("#tags").autocomplete({
                    source: stationaryJSONArray,
                    select: function (event, ui) {

                        //  alert("Stationary Id:" + ui.item.id);
                        $('#stationaryId').text(ui.item.id);
                    }
                }); //autocomplete

            }, error: function (x, e) {
                //$("#error").text("Error occured" + x.responseText);
                //alert("Error occured" + x.responseText);
            }

        }

        );


      } //loadAllStationaries


      function loadPendingTable() {
         //  alert("HI");
       
          $.ajax(
        {
            type: "POST",
            url: "Requisition.aspx/loadPendingRequests",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                var jsonObj = $.parseJSON(msg.d);
                // alert("CHECK: " + jsonObj);
                // var stationaryJSONArray = [];
                for (var x = 0; x < jsonObj.length; x++) {
                    //stationaryJSONArray.push({ id: jsonObj[x].Id, label: jsonObj[x].StationaryName });
                    //  stationaryJSONArray.push({ id: jsonObj[x].id, label: jsonObj[x].stationery_name });
                    //  alert("st name:" + jsonObj[x].StrationeryName);
                    var htmlRow = "<tr><td>" + jsonObj[x].SNo + "</td><td>" + jsonObj[x].StrationeryName + "</td><td>" + jsonObj[x].Quantity + "</td><td>" + jsonObj[x].RequestedDate + "</td></tr";
                    // alert(htmlRow);
                    $(htmlRow).appendTo("#pendingTable tbody");

                } //for

            },
            error: function (x, e) {
                //alert("error occured" + x.responseText);
            }
        });  //ajax

    } //loadPendingTable

    function removeRowsFromTable(tableId) {
        var elementTable = document.getElementById(tableId);
        var tableRows = elementTable.getElementsByTagName('tr');
        var rowCount = tableRows.length;

        for (var x = rowCount-1; x > 0; x--) {
            tableRows[x].parentNode.removeChild(tableRows[x]);
         }

     }//removeRowsFromTable


      function loadApprovedTable() {
          $.ajax(
        {
            type: "POST",
            url: "Requisition.aspx/loadApprovedRequests",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {

                var jsonObj = $.parseJSON(msg.d);
                for (var x = 0; x < jsonObj.length; x++) {
                    var htmlRow = "<tr><td>" + (x + 1) + "</td><td>" + jsonObj[x].StrationeryName + "</td><td>" + jsonObj[x].Quantity + "</td><td>" + jsonObj[x].RequestedDate + "</td></tr";
                    $(htmlRow).appendTo("#approvedTable tbody");

                } //for

            },
            error: function (x, e) {
                //alert("error occured" + x.responseText);
            }
        });   //ajax

      } //loadApprovedTable

      function loadProcessingTable() {
          $.ajax(
        {
            type: "POST",
            url: "Requisition.aspx/loadProcessingRequests",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {

                var jsonObj = $.parseJSON(msg.d);
                for (var x = 0; x < jsonObj.length; x++) {
                    var htmlRow = "<tr><td>" + (x + 1) + "</td><td>" + jsonObj[x].StrationeryName + "</td><td>" + jsonObj[x].Quantity + "</td><td>" + jsonObj[x].RequestedDate + "</td></tr";
                    $(htmlRow).appendTo("#processingTable tbody");

                } //for

            },
            error: function (x, e) {
                //alert("error occured" + x.responseText);
            }
        });   //ajax

      } //loadProcessingTable

      function loadDistributedTable() {
          $.ajax(
        {
            type: "POST",
            url: "Requisition.aspx/loadDistributedRequests",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {

                var jsonObj = $.parseJSON(msg.d);
                for (var x = 0; x < jsonObj.length; x++) {
                    var htmlRow = "<tr><td>" + (x + 1) + "</td><td>" + jsonObj[x].StrationeryName + "</td><td>" + jsonObj[x].Quantity + "</td><td>" + jsonObj[x].RequestedDate + "</td></tr";
                    $(htmlRow).appendTo("#distributedTable tbody");

                } //for

            },
            error: function (x, e) {
                //alert("error occured" + x.responseText);
            }
        });   //ajax

      } //loadDistributedTable

      function loadRejectedTable() {
          // alert("Inside Load Reject Table");
          $.ajax(
        {
            type: "POST",
            url: "Requisition.aspx/loadRejectedRequests",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                // alert("inside success");
                var jsonObj = $.parseJSON(msg.d);
                for (var x = 0; x < jsonObj.length; x++) {
                    var htmlRow = "<tr><td>" + (x + 1) + "</td><td>" + jsonObj[x].StrationeryName + "</td><td>" + jsonObj[x].Quantity + "</td><td>" + jsonObj[x].RequestedDate + "</td><td>" + jsonObj[x].RejectReason + "</td></tr";
                    // alert("Reject:" + htmlRow);
                    $(htmlRow).appendTo("#rejectedTable tbody");

                } //for

            },
            error: function (x, e) {
                //alert("error occured" + x.responseText);
            }
        });     //ajax

      } //loadDistributedTable

      $(document).ready(function () {
          loadPendingTable();
          loadApprovedTable();
          loadProcessingTable();
          loadDistributedTable();
          loadRejectedTable();
          $('#myTab a[href="#approved"]').tab('show');
          $('#myTab a[href="#processing"]').tab('show');
          $('#myTab a[href="#distributed"]').tab('show');
          $('#myTab a[href="#rejected"]').tab('show');
          $('#myTab a[href="#pending"]').tab('show');
          loadAllStationaries();
          $('#requisition').click(function () {
              //(" button is clicked");
              if (!($("#tags").val())) {
                  alert("Please Fill Stationery");
                  return;
              }

              makeRequisition();

          }
        ); //requisition



      });                     //ready

      $(document).ready(function () {
          $('#quantity').bind("keyup paste", function () {
              setTimeout(jQuery.proxy(function () {
                  this.val(this.val().replace(/[^0-9]/g, ''));
              }, $(this)), 0);
          });
      });
      

  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<!-- Alert Fade in out  -->
<div style="display:none" id="sucessRequisition">
<div class ="alert alert-success" >
<p>Thank you. Your request will proceed shortly.</p>
</div>
</div> 

<div style="display:none" id="failRequisition">
<div class ="alert alert-error" >
<p>Problems occured while making requisitions, please try again</p>
</div>
</div>

<!-- Alert Fade in out  --> 
     <form class="form-inline">
					<fieldset id="requisition-area">
						<%--<legend style="text-align:left;margin-bottom:40px;">Request Stationary</legend>--%>
						<!-- <div class="ui-widget">-->
							<input type="text" placeholder="Stationary" id="tags" style="width:300px;height:24px;" /> &nbsp;&nbsp;
						    <input type="hidden" id="stationaryId" /> 
							<input type="text" placeholder="Qty" id="quantity" style="width:60px;height:24px;" /> &nbsp;&nbsp;
						<a href="#myModal" role="button" class="btn btn-medium btn-default" style="float:right;width:65px;height:22px;border:1px solid #999;padding-top:6px;" data-toggle="modal"> Request </a>
                    </fieldset>
				</form>

                <div class="content_title_container">
        <asp:Label ID="ui_content_title_label" runat="server" Text="Requisition History" class="content_title"></asp:Label> 
    </div>

                   <!-- body ends -->
                   <!-- body -->
         
          <!-- Approve Modal start -->
		<div id="myModal" class="modal hide fade" tabindex="-1" role="dialog"
			aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"
					aria-hidden="true">x</button>
				<h3 id="myModalLabel">Stationary Requisition</h3>
			</div>
			<div class="modal-body">
				<p id="popup_txt">Are you sure , you want to request for stationary?</p>
			</div>
			<div class="modal-footer">
				<button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
				<button class="btn btn-primary" id="requisition">Request</button>
			</div>
		</div>
        <!-- Approve  end -->

         
         <ul class="nav nav-tabs" id="myTab" style="width:100%;">
          <li class="active"><a href="#pending" data-toggle="tab">Pending</a></li>    
            <li ><a href="#approved" data-toggle="tab">Approved</a></li>
              <li ><a href="#processing" data-toggle="tab">Processing</a></li>
              <li ><a href="#rejected" data-toggle="tab">Rejected</a></li>
               <li ><a href="#distributed" data-toggle="tab">Distributed</a></li>     
         </ul>
         <div class="tab-content">
          <div class="tab-pane active" id="pending">
          <table id="pendingTable" class="table table-hover">
          <thead><tr><td>#</td><td>Stationary</td><td>Quantity</td><td>Requested Date</td></tr></thead>
          <tbody></tbody>
          </table>
          
          
          
          </div><!-- Pending-->  
            <div class="tab-pane " id="approved"> 
            
            <table id="approvedTable" class="table table-hover">
          <thead><tr><td>#</td><td>Stationary</td><td>Quantity</td><td>Requested Date</td></tr></thead>
          <tbody></tbody>
          </table>
          </div><!--approved -->
           <div class="tab-pane " id="processing"> 
           <table id="processingTable" class="table">
          <thead><tr><td>#</td><td>Stationary</td><td>Quantity</td><td>Requested Date</td></tr></thead>
          <tbody></tbody>
          </table>

           </div><!-- processing--> 
                
             <div class="tab-pane " id="distributed"> 
             <table id="distributedTable" class="table table-hover">
          <thead><tr><td>#</td><td>Stationary</td><td>Quantity</td><td>Requested Date</td></tr></thead>
          <tbody></tbody>
          </table>
           </div><!-- distributed-->   
           <div class="tab-pane " id="rejected"> 
             <table id="rejectedTable" class="table table-hover">
          <thead><tr><td>#</td><td>Stationary</td><td>Quantity</td><td>Req. Date</td><td>Remark</td></tr></thead>
          <tbody></tbody>
          </table>
           </div><!-- Rejected-->    
         </div>       
</asp:Content> 