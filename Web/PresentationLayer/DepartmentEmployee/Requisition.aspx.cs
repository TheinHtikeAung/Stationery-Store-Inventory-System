using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer.Controllers;
using ApplicationLayer;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections;
//using Newtonsoft.Json;
using ApplicationLayer.ViewModels;
using ApplicationLayer.Utils;

namespace PresentationLayer.DepartmentEmployee
{
 
    public partial class order : System.Web.UI.Page
    {
        static StationeryController stationaryController = new StationeryController();
        protected void Page_Load(object sender, EventArgs e)
        {
            //TextBox1.Text = controller.actionGetPendingRequisitionsByUserID(6).Count().ToString();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        

        [WebMethod(EnableSession=true)]
        public static string MakeRequisition(object jsonParam)
        {
           
            User user = (User)HttpContext.Current.Session["user"];
            if (user == null)
            {
                return null;
            }
            int empId = user.id;
            
            String valueString = jsonParam.ToString();
            string[] values = valueString.Split('.');
           int stId= Convert.ToInt16(values[0]);
            int qty= Convert.ToInt16(values[1]);
            
            int depId = Convert.ToInt32(user.department);
            Requisition requisition = new Requisition();
            requisition.stationery = stId;
            requisition.quantity = qty;
            requisition.user_obj = empId;
            requisition.department = depId;
            requisition.user_approved_by = null;
            RequisitionController requisitionController = new RequisitionController();
           Message msg= requisitionController.actionCreateRequisition(requisition);
           if (msg.condition)
           {
               //return msg.message;
               return "success";
           }
           else
           {
               //return msg.message;
               return "fail";
           }

           
          // System.Diagnostics.Debug.WriteLine(" inside MakeRequisition " + msg);
           // return "Hi this is response from MakeRequisition";

        }//

        [WebMethod]
        public static string GetAllStationaries()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Stationery> stationery_entities = stationaryController.actionGetStationeries();
            System.Diagnostics.Debug.WriteLine(" inside getAllStationaries  current count: " + stationery_entities.Count);
            List<StationeryModel> stationery_models = new List<StationeryModel>();

            foreach (Stationery s in stationery_entities) {
                stationery_models.Add(EntityCopier.Copy(s));
            }
            String str = js.Serialize(stationery_models);
            //String str = js.Serialize(stationaryList);
           // System.Diagnostics.Debug.WriteLine("total:" + stationaryController.actionGetStationeries().Count());
           // System.Diagnostics.Debug.WriteLine("check:" + str);

            //stationaryList
            return str;
        }//getAllStationaries
        //loadAllRequisitions
        [WebMethod]
        public static string loadPendingRequests()
        {
            User user = (User)HttpContext.Current.Session["user"];
            if (user == null) {
                return null;
            }
            int empId = user.id;
            
            //--------------------------------------------------------------

            JavaScriptSerializer js = new JavaScriptSerializer();
            RequisitionController requisitionController = new RequisitionController();
           
            List<Requisition> requisition_entities = requisitionController.actionGetPendingRequisitionsByUserID(empId);
            List<RequisitionModel> model = new List<RequisitionModel>();
            StationeryController stationeryController = new StationeryController();
            List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
            int counter = 1;
            foreach (Requisition r in requisition_entities)
            {

                StationaryRequests stationaryRequests = new StationaryRequests();
                stationaryRequests.Quantity = r.quantity;
                stationaryRequests.SNo = counter;
                stationaryRequests.StrationeryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                stationaryRequests.RequestedDate = r.datetime_requested.ToString("MMMM dd, yyyy");
                stationeryRequests.Add(stationaryRequests);
                counter++;

            }

            return js.Serialize(stationeryRequests);
        }//loadPendingRequests
        //loadApprovedRequests

         [WebMethod]
        public static string loadApprovedRequests()
        {
            User user = (User)HttpContext.Current.Session["user"];
            if (user == null)
            {
                return null;
            }
            int empId = user.id;

            //--------------------------------------------------------------

            JavaScriptSerializer js = new JavaScriptSerializer();
            RequisitionController requisitionController = new RequisitionController();
            // hard coded user id = 6
           // List<Requisition> requisition_entities =requisitionController.actionGetPendingRequisitionsByUserID(6);
            List<Requisition> requisition_entities = requisitionController.actionGetApprovedRequisitionsByUserID(empId);
            List<RequisitionModel> model = new List<RequisitionModel>();
            StationeryController stationeryController = new StationeryController();
            List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
            foreach (Requisition r in requisition_entities)
            {
                StationaryRequests stationaryRequests = new StationaryRequests();
                stationaryRequests.Quantity = r.quantity;
                stationaryRequests.StrationeryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                stationaryRequests.RequestedDate = r.datetime_requested.ToString("MMMM dd, yyyy");
                stationeryRequests.Add(stationaryRequests);
            }
           
            return js.Serialize(stationeryRequests);
        }//loadApprovedRequests

         [WebMethod]
         public static string loadProcessingRequests()
         {
             User user = (User)HttpContext.Current.Session["user"];
             if (user == null)
             {
                 return null;
             }
             int empId = user.id;

             //--------------------------------------------------------------


             JavaScriptSerializer js = new JavaScriptSerializer();
             RequisitionController requisitionController = new RequisitionController();
             // hard coded user id = 6
             // List<Requisition> requisition_entities =requisitionController.actionGetPendingRequisitionsByUserID(6);
             List<Requisition> requisition_entities = requisitionController.actionGetProcessingRequisitionsByUserID(empId);
             List<RequisitionModel> model = new List<RequisitionModel>();
             StationeryController stationeryController = new StationeryController();
             List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
             foreach (Requisition r in requisition_entities)
             {
                 StationaryRequests stationaryRequests = new StationaryRequests();
                 stationaryRequests.Quantity = r.quantity;
                 stationaryRequests.StrationeryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                 stationaryRequests.RequestedDate = r.datetime_requested.ToString("MMMM dd, yyyy");
                 stationeryRequests.Add(stationaryRequests);
             }
           //  System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ approved:" + js.Serialize(stationeryRequests));
             return js.Serialize(stationeryRequests);
         }//loadProcessingRequests

         [WebMethod]
         public static string loadDistributedRequests()
         {

             User user = (User)HttpContext.Current.Session["user"];
             if (user == null)
             {
                 return null;
             }
             int empId = user.id;

             //--------------------------------------------------------------


             JavaScriptSerializer js = new JavaScriptSerializer();
             RequisitionController requisitionController = new RequisitionController();
             // hard coded user id = 6
             // List<Requisition> requisition_entities =requisitionController.actionGetPendingRequisitionsByUserID(6);
             List<Requisition> requisition_entities = requisitionController.actionGetDistributedRequisitionsByUserID(empId);
             List<RequisitionModel> model = new List<RequisitionModel>();
             StationeryController stationeryController = new StationeryController();
             List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
             foreach (Requisition r in requisition_entities)
             {
                 StationaryRequests stationaryRequests = new StationaryRequests();
                 stationaryRequests.Quantity = r.quantity;
                 stationaryRequests.StrationeryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                 stationaryRequests.RequestedDate = r.datetime_requested.ToString("MMMM dd, yyyy");
                 stationeryRequests.Add(stationaryRequests);
             }
             //  System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ approved:" + js.Serialize(stationeryRequests));
             return js.Serialize(stationeryRequests);
         }//loadDistributedRequests


         [WebMethod]
         public static string loadRejectedRequests()
         {
             User user = (User)HttpContext.Current.Session["user"];
             if (user == null)
             {
                 return null;
             }
             int empId = user.id;

             //--------------------------------------------------------------


             JavaScriptSerializer js = new JavaScriptSerializer();
             RequisitionController requisitionController = new RequisitionController();
             // hard coded user id = 6
             // List<Requisition> requisition_entities =requisitionController.actionGetPendingRequisitionsByUserID(6);
             List<Requisition> requisition_entities = requisitionController.actionGetRejectedRequisitionsByUserID(empId);
             List<RequisitionModel> model = new List<RequisitionModel>();
             StationeryController stationeryController = new StationeryController();
             List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
             foreach (Requisition r in requisition_entities)
             {
                 StationaryRequests stationaryRequests = new StationaryRequests();
                 stationaryRequests.Quantity = r.quantity;
                 stationaryRequests.StrationeryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                 stationaryRequests.RequestedDate = r.datetime_requested.ToString("MMMM dd, yyyy");
                 stationaryRequests.RejectReason = r.reject_remark;
                 stationeryRequests.Add(stationaryRequests);
                 
             }
              // System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ Rejected:" + js.Serialize(stationeryRequests));
             return js.Serialize(stationeryRequests);
         }//loadDistributedRequests
        public class StationaryRequests {

            private int sNo;
            private String strationeryName;
           private int quantity;
           private String requestedDate;
           private String rejectReason;

           public string RejectReason { get; set; }
           public string StrationeryName {
               get { return strationeryName; }
               set { strationeryName = value; }
           }

           public int Quantity {
               get { return quantity; }
               set { quantity = value; }
           }

           public int SNo
           {
               get { return sNo; }
               set { sNo = value; }
           }
           public String RequestedDate
           {
               get { return requestedDate; }
               set { requestedDate = value; }
           }

        }//StationaryRequests
        
    }
}