using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApplicationLayer.Controllers;
using ApplicationLayer;
using Newtonsoft.Json;
using ApplicationLayer.ViewModels;
using ApplicationLayer.Utils;
namespace PresentationLayer.Mobile
{
    /// <summary>
    /// Summary description for MDepartmentEmployee
    /// </summary>
    public class MDepartmentEmployee : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //  context.Response.ContentType = "text/plain";
            // context.Response.Write("Hello World"); System.Diagnostics.Debug.WriteLine("inside MDepartmentEmployee");
            String method = context.Request.Form["method"];
            if (method == "get_access_token")
            {
                get_access_token(context);
            }
            else if (method == "loadAllPendingRequisitions")
            {
                String accessToken = context.Request.Form["accessToken"];
                string result = loadAllPendingRequisitions(accessToken);
                context.Response.Write(result);
            }
            else if (method == "loadAllApprovedRequisitions")
            {
                String accessToken = context.Request.Form["accessToken"];
                string result = loadAllApprovedRequisitions(accessToken);
                context.Response.Write(result);
            }
            else if (method == "loadAllDistributedRequisitions")
            {
                String accessToken = context.Request.Form["accessToken"];
                string result = loadAllDistributedRequisitions(accessToken);
                context.Response.Write(result);
            }
            else if (method == "loadAllProcessingRequisitions")
            {
                String accessToken = context.Request.Form["accessToken"];
                string result = loadAllProcessingRequisitions(accessToken);
                context.Response.Write(result);
            }
            else if (method == "loadAllRejectedRequisitions")
            {
                String accessToken = context.Request.Form["accessToken"];
                string result = loadAllRejectedRequisitions(accessToken);
                context.Response.Write(result);
            }
            else if (method == "logout")
            {
                logout(context);
            }



        }
        //loadAllPendingRequisitions


        public void logout(HttpContext context)
        {
            User user = getUserByAccessToken(context.Request.Form["access_token"]);
            Message message = new Message(false);

            if (user != null)
            {
                message = new OAuthController().actionDeleteTokenByToken(context.Request.Form["access_token"]);
            }

            context.Response.Write(JsonConvert.SerializeObject(message));
        }

        public User getUserByAccessToken(String access_token)
        {
            return new OAuthController().actionGetUserByToken(access_token);
        }
        public string loadAllPendingRequisitions(String accessToken)
        {
            OAuthController oAuth = new OAuthController();
            User user = oAuth.actionGetUserByToken(accessToken);

            RequisitionController requisitionController = new RequisitionController();

            List<Requisition> requisition_entities = requisitionController.actionGetPendingRequisitionsByUserID(user.id);
            List<RequisitionModel> model = new List<RequisitionModel>();
            StationeryController stationeryController = new StationeryController();
            List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
            int counter = 1;
            foreach (Requisition r in requisition_entities)
            {

                StationaryRequests stationaryRequests = new StationaryRequests();
                stationaryRequests.Quantity = r.quantity;
                stationaryRequests.SNo = counter;
                stationaryRequests.StationaryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                stationaryRequests.RequestedDate = r.datetime_requested.ToString();
                stationeryRequests.Add(stationaryRequests);
                counter++;

            }

            return JsonConvert.SerializeObject(stationeryRequests);
        }
        public string loadAllApprovedRequisitions(String accessToken)
        {
            OAuthController oAuth = new OAuthController();
            User user = oAuth.actionGetUserByToken(accessToken);
            RequisitionController requisitionController = new RequisitionController();
            List<Requisition> requisition_entities = requisitionController.actionGetApprovedRequisitionsByUserID(user.id);
            List<RequisitionModel> model = new List<RequisitionModel>();
            StationeryController stationeryController = new StationeryController();
            List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
            foreach (Requisition r in requisition_entities)
            {
                StationaryRequests stationaryRequests = new StationaryRequests();
                stationaryRequests.Quantity = r.quantity;
                stationaryRequests.StationaryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                stationaryRequests.RequestedDate = r.datetime_requested.ToString();
                stationeryRequests.Add(stationaryRequests);
            }
            return JsonConvert.SerializeObject(stationeryRequests);
        }

        public string loadAllDistributedRequisitions(String accessToken)
        {
            OAuthController oAuth = new OAuthController();
            User user = oAuth.actionGetUserByToken(accessToken);

            RequisitionController requisitionController = new RequisitionController();
            // hard coded user id = 6
            // List<Requisition> requisition_entities =requisitionController.actionGetPendingRequisitionsByUserID(6);
            List<Requisition> requisition_entities = requisitionController.actionGetDistributedRequisitionsByUserID(user.id);
            List<RequisitionModel> model = new List<RequisitionModel>();
            StationeryController stationeryController = new StationeryController();
            List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
            foreach (Requisition r in requisition_entities)
            {
                StationaryRequests stationaryRequests = new StationaryRequests();
                stationaryRequests.Quantity = r.quantity;
                stationaryRequests.StationaryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                stationaryRequests.RequestedDate = r.datetime_requested.ToString();
                stationeryRequests.Add(stationaryRequests);
            }
            return JsonConvert.SerializeObject(stationeryRequests);
        }
        public string loadAllProcessingRequisitions(String accessToken)
        {
            OAuthController oAuth = new OAuthController();
            User user = oAuth.actionGetUserByToken(accessToken);

            RequisitionController requisitionController = new RequisitionController();
            // hard coded user id = 6
            // List<Requisition> requisition_entities =requisitionController.actionGetPendingRequisitionsByUserID(6);
            List<Requisition> requisition_entities = requisitionController.actionGetProcessingRequisitionsByUserID(user.id);
            List<RequisitionModel> model = new List<RequisitionModel>();
            StationeryController stationeryController = new StationeryController();
            List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
            foreach (Requisition r in requisition_entities)
            {
                StationaryRequests stationaryRequests = new StationaryRequests();
                stationaryRequests.Quantity = r.quantity;
                stationaryRequests.StationaryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                stationaryRequests.RequestedDate = r.datetime_requested.ToString();
                stationeryRequests.Add(stationaryRequests);
            }
            return JsonConvert.SerializeObject(stationeryRequests);
        }
        public string loadAllRejectedRequisitions(String accessToken)
        {
            OAuthController oAuth = new OAuthController();
            User user = oAuth.actionGetUserByToken(accessToken);
            RequisitionController requisitionController = new RequisitionController();
            // hard coded user id = 6
            // List<Requisition> requisition_entities =requisitionController.actionGetPendingRequisitionsByUserID(6);
            List<Requisition> requisition_entities = requisitionController.actionGetRejectedRequisitionsByUserID(user.id);
            List<RequisitionModel> model = new List<RequisitionModel>();
            StationeryController stationeryController = new StationeryController();
            List<StationaryRequests> stationeryRequests = new List<StationaryRequests>();
            foreach (Requisition r in requisition_entities)
            {
                StationaryRequests stationaryRequests = new StationaryRequests();
                stationaryRequests.Quantity = r.quantity;
                stationaryRequests.StationaryName = stationeryController.actionGetStationeryByID(r.stationery).stationery_name;
                stationaryRequests.RequestedDate = r.datetime_requested.ToString();
                stationaryRequests.RejectReason = r.reject_remark;
                stationeryRequests.Add(stationaryRequests);
            }
            return JsonConvert.SerializeObject(stationeryRequests);
        }


        public void get_access_token(HttpContext context)
        {
            String username = context.Request.Form["username"];
            String password = context.Request.Form["password"];



            OAuthController oauthController = new OAuthController();
            OAuthViewModel oauth = new OAuthViewModel();
            oauth.access_token = "";
            User user = new AuthenticationController().actionCheckLoginData(username, password);
            System.Diagnostics.Debug.WriteLine("User:" + user + "  ,type:" + user.user_type);
            if (user != null)
            {
                if ((user.user_type == 1) || (user.user_type == 2))
                {
                    oauth.access_token = oauthController.actionGetNewTokenByUsernameAndPassword(username, password);
                }
            }
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(oauth));
            context.Response.Write(JsonConvert.SerializeObject(oauth));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class StationaryRequests
        {
            public string RejectReason { get; set; }
            public string StationaryName { get; set; }
            public int Quantity { get; set; }
            public int SNo { get; set; }
            public String RequestedDate { get; set; }

        }//StationaryRequests
    }
}