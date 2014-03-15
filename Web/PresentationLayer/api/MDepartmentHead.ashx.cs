using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer.ViewModels;
using System.Web.Script.Serialization;
using System.Collections;
using ApplicationLayer.Controllers;
using ApplicationLayer;
using ApplicationLayer.Utils;
using System.Text;
using Newtonsoft.Json;
//// hi this is the same what you are working on.
namespace PresentationLayer.Mobile
{
    /// <summary>
    /// Summary description for MDepartmentHead
    /// </summary>
    public class MDepartmentHead : IHttpHandler
    {
        private static List<UserRequisition> userRequisitions = new List<UserRequisition>();
        public void ProcessRequest(HttpContext context)
        {

            // System.Diagnostics.Debug.WriteLine("########### inside MDepartmentHead ######");
            string method = context.Request.Form["method"];
            //  System.Diagnostics.Debug.WriteLine("########### Method="+method);
            if (method == "get_access_token")
            {
                // System.Diagnostics.Debug.WriteLine("########### $$$$$$$$$$$$$$$$ ######");
                get_access_token(context);
            }
            else if (method == "showAllPendingRequisition")
            {
                string accessToken = context.Request.Form["access_token"];
                // System.Diagnostics.Debug.WriteLine("########### TOKANN::" + accessToken);
                context.Response.Write(showAllPendingRequisition(accessToken));
            }
            else if (method == "approveRequisition")
            {
                string accessToken = context.Request.Form["accessToken"];
                int reqId = Convert.ToInt32(context.Request.Form["requisitionId"].Trim());
                string result = approveRequisition(accessToken, reqId);
                context.Response.Write(result);
                //
            }
            else if (method == "rejectRequisition")
            {
                string accessToken = context.Request.Form["accessToken"];
                int reqId = Convert.ToInt32(context.Request.Form["requisitionId"].Trim());
                string reason = context.Request.Form["reason"];
                string result = rejectRequisition(accessToken, reqId, reason);
                context.Response.Write(result);
            }
            else if (method == "approveAllRequisition")
            {
                string accessToken = context.Request.Form["access_token"];
                string jsonArray = context.Request.Form["requisitionIds"];

                var json = JsonConvert.DeserializeObject<List<RequisitionId>>(jsonArray);
                List<RequisitionId> requisitionsIds = json.ToList<RequisitionId>();
                string result = approveAllRequisitions(accessToken, requisitionsIds);

                context.Response.Write(result);
            }
            else if (method == "loadAllApprovedRequisitions")
            {
                string accessToken = context.Request.Form["access_token"];
                string result = showAllApprovedRequisitions(accessToken);
                context.Response.Write(result);
            }
            else if (method == "loadAllRejectedRequisitions")
            {
                string accessToken = context.Request.Form["access_token"];
                string result = showAllRejectedRequisitions(accessToken);
                context.Response.Write(result);
            }
            else if (method == "logout")
            {
                logout(context);
            }
        }//processRequest

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
        public string showAllApprovedRequisitions(string accessToken)
        {
            OAuthController oAuth = new OAuthController();
            User user = oAuth.actionGetUserByToken(accessToken);
            RequisitionController requisitionController = new RequisitionController();
            List<RequisitionView> requisitionDepartmentWise = requisitionController.actionGetApprovedRequisitionViewsByDepartmentID(Convert.ToInt32(user.department));
            List<Requisition> requisitions = new List<Requisition>();
            foreach (RequisitionView rView in requisitionDepartmentWise)
            {
                Requisition requisition = new Requisition();
                requisition.EmployeeName = rView.firstname + " " + rView.lastname;
                requisition.Quantity = rView.quantity;
                requisition.RequisitionDate = rView.datetime_requested;
                requisition.ReqDate = rView.datetime_requested.ToString("MMMM dd, yyyy");
                requisition.StationaryName = rView.stationery_name;
                requisitions.Add(requisition);
            }
            return JsonConvert.SerializeObject(requisitions);
        }//showAllApprovedRequisitions

        public string showAllRejectedRequisitions(string accessToken)
        {
            OAuthController oAuth = new OAuthController();
            User user = oAuth.actionGetUserByToken(accessToken);
            RequisitionController requisitionController = new RequisitionController();

            List<RequisitionView> requisitionDepartmentWise = requisitionController.actionGetRejectedRequisitionViewsByDepartmentID(Convert.ToInt32(user.department));

            List<Requisition> requisitions = new List<Requisition>();
            foreach (RequisitionView rView in requisitionDepartmentWise)
            {
                Requisition requisition = new Requisition();
                requisition.EmployeeName = rView.firstname + " " + rView.lastname;
                requisition.Quantity = rView.quantity;
                requisition.RequisitionDate = rView.datetime_requested;
                requisition.ReqDate = rView.datetime_requested.ToString("MMMM dd, yyyy");
                requisition.StationaryName = rView.stationery_name;
                requisitions.Add(requisition);
            }
            return JsonConvert.SerializeObject(requisitions);
        }
        public string approveAllRequisitions(string accessToken, List<RequisitionId> requisitionsIds)
        {
            OAuthController oAuth = new OAuthController();
            User user = oAuth.actionGetUserByToken(accessToken);
            RequisitionController requisitionController = new RequisitionController();
            try
            {
                for (int i = 0; i < requisitionsIds.Count(); i++)
                {
                    RequisitionId requisitionId = requisitionsIds[i];
                    requisitionController.actionRequisitionStatusChangeToApproved(requisitionId.reqId, user.id);
                }
                return "success";
            }
            catch (Exception e)
            {
                return "fail";
            }

        }

        public string approveRequisition(string accessToken, int reqId)
        {
            try
            {
                OAuthController oAuth = new OAuthController();
                User user = oAuth.actionGetUserByToken(accessToken);
                //get userId 
                RequisitionController requisitionController = new RequisitionController();
                requisitionController.actionRequisitionStatusChangeToApproved(reqId, user.id);
                return "success";
            }
            catch (Exception e)
            {
                return "fail";
            }
        }//approveRequisition

        public string rejectRequisition(string accessToken, int reqId, string reason)
        {
            System.Diagnostics.Debug.WriteLine("inside rejectRequisition: accessToken" + accessToken + " ,reqId:" + reqId + "  ,reason:" + reason);
            try
            {
                OAuthController oAuth = new OAuthController();
                User user = oAuth.actionGetUserByToken(accessToken);
                //get userId 
                RequisitionController requisitionController = new RequisitionController();
                //  requisitionController.actionRequisitionStatusChangeToRejected(reqId);
                requisitionController.actionRequisitionStatusChangeToRejectedWithRemarkAndRejectedBy(reqId, reason, user.id);
                return "success";
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception occured in rejectRequisition.." + e.Message);
                return "fail";
            }
        }//rejectRequisition

        public void get_access_token(HttpContext context)
        {
            System.Diagnostics.Debug.WriteLine("########### inside get_access_token######");
            String username = context.Request.Form["username"];
            String password = context.Request.Form["password"];
            OAuthController oauthController = new OAuthController();
            OAuthViewModel oauth = new OAuthViewModel();
            oauth.access_token = "";
            User user = new AuthenticationController().actionCheckLoginData(username, password);
            if (user != null)
            {
                System.Diagnostics.Debug.WriteLine("###########User type:" + user.user_type);
                if ((user.user_type == 2) || (user.user_type == 3))
                {
                    oauth.access_token = oauthController.actionGetNewTokenByUsernameAndPassword(username, password);
                }
            }

            context.Response.Write(JsonConvert.SerializeObject(oauth));
        }
        public string showAllPendingRequisition(string accessToken)
        {
            RequisitionController requisitionController = new RequisitionController();
            JavaScriptSerializer js = new JavaScriptSerializer();
            OAuthController oAuth = new OAuthController();
            User user = oAuth.actionGetUserByToken(accessToken);
            // int userId = user.id;         
            List<RequisitionView> requisitionDepartmentWise = requisitionController.actionGetPendingRequisitionViewsByDepartmentID(Convert.ToInt32(user.department));
            List<UserRequisition> userRequisitionsList = new List<UserRequisition>();
            foreach (RequisitionView r in requisitionDepartmentWise)
            {
                if (userRequisitionsList.Count == 0)
                {
                    UserRequisition userRequisition = new UserRequisition();
                    userRequisition.EmployeeName = r.firstname + " " + r.lastname;
                    userRequisition.UserId = r.user_obj;
                    Requisition requisition = new Requisition();
                    requisition.ReqId = r.requisition_id;
                    requisition.Quantity = r.quantity;
                    requisition.StationaryName = r.stationery_name;
                    userRequisition.UserRequisitions.Add(requisition);
                    userRequisitionsList.Add(userRequisition);
                }
                else
                {
                    bool found = false;
                    //check if requisition from the same user is pending, if yes group the requisition based on that user id
                    foreach (UserRequisition uR in userRequisitionsList)
                    {
                        if (r.user_obj == uR.UserId)
                        {
                            found = true;
                            Requisition requisition = new Requisition();
                            requisition.ReqId = r.requisition_id;
                            requisition.Quantity = r.quantity;
                            requisition.StationaryName = r.stationery_name;
                            requisition.RequisitionDate = r.datetime_requested;
                            uR.UserRequisitions.Add(requisition);
                            break;
                        }

                    }//foreach
                    //else simply add the userrequisition to the list, grouping not required
                    if (!found)
                    {
                        UserRequisition userRequisition = new UserRequisition();
                        userRequisition.EmployeeName = r.firstname + " " + r.lastname;
                        userRequisition.UserId = r.user_obj;
                        Requisition requisition = new Requisition();
                        requisition.ReqId = r.requisition_id;
                        requisition.Quantity = r.quantity;
                        requisition.StationaryName = r.stationery_name;
                        userRequisition.UserRequisitions.Add(requisition);
                        userRequisitionsList.Add(userRequisition);
                    }//if
                }//else
            }//forEach

            System.Diagnostics.Debug.WriteLine("json serialized:" + JsonConvert.SerializeObject(userRequisitionsList));
            return JsonConvert.SerializeObject(userRequisitionsList);
            /// return js.Serialize(userRequisitionsList);
        }//showAllPendingRequisition



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class Requisition
        {
            private int reqId;
            private string stationaryName;
            private int quantity;

            private DateTime requisitionDate;
            private string reqDate;

            public string ReqDate { get; set; }
            public string EmployeeName { get; set; }
            public Requisition() { }
            public Requisition(int reqId, string stationaryName, int quantity)
            {
                this.reqId = reqId;
                this.stationaryName = stationaryName;
                this.quantity = quantity;
            }
            public int ReqId
            {
                get { return reqId; }
                set { reqId = value; }
            }

            public string StationaryName
            {
                get { return stationaryName; }
                set { stationaryName = value; }
            }

            public int Quantity
            {
                get { return quantity; }
                set { quantity = value; }
            }

            public DateTime RequisitionDate
            {
                get { return requisitionDate; }
                set { requisitionDate = value; }
            }

        }//Requisition

        public class UserRequisition
        {
            private int userId;
            private string employeeName;
            private List<Requisition> userRequisitions = new List<Requisition>();

            public UserRequisition()
            {

            }
            public UserRequisition(string employeeName, List<Requisition> userRequisitions)
            {
                this.employeeName = employeeName;
                this.userRequisitions = userRequisitions;
            }
            public int UserId
            {
                get { return userId; }
                set { userId = value; }
            }
            public string EmployeeName
            {
                get { return employeeName; }
                set { employeeName = value; }
            }
            public List<Requisition> UserRequisitions
            {
                get { return userRequisitions; }
                set { userRequisitions = value; }

            }
        }//UserRequisitions

        public class RequisitionId
        {
            public int reqId { get; set; }
        }
    }
}