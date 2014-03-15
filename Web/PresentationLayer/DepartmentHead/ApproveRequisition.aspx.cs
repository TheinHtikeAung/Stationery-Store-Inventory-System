using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer.ViewModels;
using ApplicationLayer.Utils;
using ApplicationLayer.Controllers;
using ApplicationLayer;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections;
namespace PresentationLayer.DepartmentHead
{
    public partial class requisition_processing : System.Web.UI.Page
    {
        private static List<UserRequisition> userRequisitions = new List<UserRequisition>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Requisition requistion1 = new Requisition(1, "stationary1", 1);
            //Requisition requistion2 = new Requisition(2, "stationary2", 1);
            //Requisition requistion3 = new Requisition(3, "stationary3", 1);
            //List<Requisition> reqs1 = new List<Requisition>();
            //reqs1.Add(requistion1);
            //reqs1.Add(requistion2);
            //reqs1.Add(requistion3);

            //Requisition requistion4 = new Requisition(4, "stationary4", 1);
            //Requisition requistion5 = new Requisition(5, "stationary5", 1);
            //List<Requisition> reqs2 = new List<Requisition>();
            //reqs2.Add(requistion4);
            //reqs2.Add(requistion5);

            //UserRequisition userRequistion1 = new UserRequisition("Name1", reqs1);
            //UserRequisition userRequistion2 = new UserRequisition("Name2", reqs2);
            //userRequisitions.Add(userRequistion1);
            //userRequisitions.Add(userRequistion2);
        }

        [WebMethod]
        public static string LoadAllPendingRequests()
        {
            User sUser = (User)HttpContext.Current.Session["user"];
            if (sUser == null)
            {
                return null;
            }

            UserController userController = new UserController();
            User dUser = userController.actionGetUserByID(sUser.id);


            RequisitionController requisitionController = new RequisitionController();
            JavaScriptSerializer js = new JavaScriptSerializer();



            List<RequisitionView> requisitionDepartmentWise = requisitionController.actionGetPendingRequisitionViewsByDepartmentID(Convert.ToInt32(dUser.department));
            System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ requisitions by department wise:" + requisitionDepartmentWise.Count());

            List<UserRequisition> userRequisitionsList = new List<UserRequisition>();
            foreach (RequisitionView r in requisitionDepartmentWise)
            {

                //System.Diagnostics.Debug.WriteLine(" Dept:" + r.department + "User id:" + r.user_id + " Name" + r.firstname + " Req Id:" + r.requisition_id + " Stationary Name" + r.stationery_name + " Quantity " + r.quantity);


                if (userRequisitionsList.Count == 0)
                {
                    UserRequisition userRequisition = new UserRequisition();
                    //Since there are no requisitions made so no question of grouping based on user id
                    userRequisition.EmployeeName = r.firstname + " " + r.lastname;
                    userRequisition.UserId = r.user_obj;
                    Requisition requisition = new Requisition();
                    requisition.ReqId = r.requisition_id;
                    requisition.Quantity = r.quantity;
                    requisition.StationaryName = r.stationery_name;
                    // requisition.RequisitionDate = r.r
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
                            // requisition.RequisitionDate = r.r
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
                    }

                }


            }


            System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ loadAllPendingRequests:" + js.Serialize(userRequisitionsList));
            return js.Serialize(userRequisitionsList);
        }//loadAllPendingRequests


        [WebMethod]
        public static string LoadAllApprovedRequests()
        {
            User sUser = (User)HttpContext.Current.Session["user"];
            if (sUser == null)
            {
                return null;
            }

            UserController userController = new UserController();
            User dUser = userController.actionGetUserByID(sUser.id);


            RequisitionController requisitionController = new RequisitionController();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<RequisitionView> requisitionDepartmentWise = requisitionController.actionGetApprovedRequisitionViewsByDepartmentID(Convert.ToInt32(dUser.department));
            System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ requisitions by department wise:" + requisitionDepartmentWise.Count());
            List<UserRequisition> userRequisitionsList = new List<UserRequisition>();
            foreach (RequisitionView r in requisitionDepartmentWise)
            {
                if (userRequisitionsList.Count == 0)
                {
                    UserRequisition userRequisition = new UserRequisition();
                    //Since there are no requisitions made so no question of grouping based on user id
                    userRequisition.EmployeeName = r.firstname + " " + r.lastname;
                    userRequisition.UserId = r.user_obj;
                    Requisition requisition = new Requisition();
                    requisition.ReqId = r.requisition_id;
                    requisition.Quantity = r.quantity;
                    requisition.StationaryName = r.stationery_name;
                    // requisition.RequisitionDate = r.r
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
                            // requisition.RequisitionDate = r.r
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
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ loadAllPendingRequests:" + js.Serialize(userRequisitionsList));
            return js.Serialize(userRequisitionsList);
        }
        //LoadAllApprovedRequests

        [WebMethod]
        public static string LoadAllRejectedRequests()
        {
            User sUser = (User)HttpContext.Current.Session["user"];
            if (sUser == null)
            {
                return null;
            }

            UserController userController = new UserController();
            User dUser = userController.actionGetUserByID(sUser.id);


            RequisitionController requisitionController = new RequisitionController();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<RequisitionView> requisitionDepartmentWise = requisitionController.actionGetRejectedRequisitionViewsByDepartmentID(Convert.ToInt32(dUser.department));
            System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ requisitions by department wise:" + requisitionDepartmentWise.Count());
            List<UserRequisition> userRequisitionsList = new List<UserRequisition>();
            foreach (RequisitionView r in requisitionDepartmentWise)
            {
                if (userRequisitionsList.Count == 0)
                {
                    UserRequisition userRequisition = new UserRequisition();
                    //Since there are no requisitions made so no question of grouping based on user id
                    userRequisition.EmployeeName = r.firstname + " " + r.lastname;
                    userRequisition.UserId = r.user_obj;
                    Requisition requisition = new Requisition();
                    requisition.ReqId = r.requisition_id;
                    requisition.Quantity = r.quantity;
                    requisition.StationaryName = r.stationery_name;
                    // requisition.RequisitionDate = r.r
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
                            // requisition.RequisitionDate = r.r
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
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ loadAllPendingRequests:" + js.Serialize(userRequisitionsList));
            return js.Serialize(userRequisitionsList);
        }
        //LoadAllApprovedRequests




        [WebMethod]
        public static void approveRequisitions(List<RequisitionID> jsonParam)
        {

            User sUser = (User)HttpContext.Current.Session["user"];
            if (sUser == null)
            {
                return;
            }

            UserController userController = new UserController();
            User dUser = userController.actionGetUserByID(sUser.id);

            JavaScriptSerializer js = new JavaScriptSerializer();
            // System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ approveRequisitions:" + js.Serialize(jsonParam));
            System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ approveRequisitions:" + jsonParam.Count());

            //actionRequisitionStatusChangeToApproved(int requisition_id) 
            RequisitionController requisitionController = new RequisitionController();

            foreach (RequisitionID requisitionId in jsonParam)
            {
                System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ param:" + requisitionId.Id);
                requisitionController.actionRequisitionStatusChangeToApproved(Convert.ToInt32(requisitionId.Id), dUser.id);
            }
        }//approveRequisitions

        [WebMethod]
        public static void rejectRequisitions(List<RequisitionID> jsonParam, string reason)
        {
            System.Diagnostics.Debug.WriteLine("REASON FOR REJECTION:" + reason);
            User sUser = (User)HttpContext.Current.Session["user"];
            if (sUser == null)
            {
                return;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            // System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ approveRequisitions:" + js.Serialize(jsonParam));
            //  System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ approveRequisitions:" + jsonParam.Count());

            //actionRequisitionStatusChangeToApproved(int requisition_id) 
            RequisitionController requisitionController = new RequisitionController();

            foreach (RequisitionID requisitionId in jsonParam)
            {
                // System.Diagnostics.Debug.WriteLine("check @@@@@@@@@@@@ param:" + requisitionId.Id);
                // requisitionController.actionRequisitionStatusChangeToRejected(Convert.ToInt32(requisitionId.Id));
                requisitionController.actionRequisitionStatusChangeToRejectedWithRemarkAndRejectedBy(Convert.ToInt32(requisitionId.Id), reason, sUser.id);
            }
        }//rejectRequisitions

        public class Requisition
        {
            private int reqId;
            private string stationaryName;
            private int quantity;
            private DateTime requisitionDate;

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

        public class RequisitionID
        {
            private string id;
            public string Id
            {
                get { return id; }
                set { id = value; }
            }
        }//StationaryID

    }
}