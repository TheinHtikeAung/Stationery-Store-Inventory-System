using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ApplicationLayer.Controllers;
using ApplicationLayer;
using ApplicationLayer.ViewModels;
using Newtonsoft.Json;

namespace PresentationLayer.api
{
    /// <summary>
    /// Summary description for mfulfillment
    /// </summary>
    public class mfulfillment : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string token = context.Request.Form["token"];
            string method=context.Request.Form["method"];

            if(token!=null)
            {
                
                if(method=="getJSONByDepartment")
                {
                    getJSONByDepartment(context);
                }
                else if (method == "getJSONByItem")
                {
                    getJSONByItem(context);
                }
                else if (method == "updateFulfillQty")
                {
                    updateFulfillQty(context);
                }
                else if (method == "updateToDisbursement")
                {
                    updateToDisbursement(context);
                }
                else if (method == "getJSONByRetrievalMaster")
                {
                    getJSONByRetrievalMaster(context);
                }
                else if (method == "getJSONByRetrievalDetail")
                {
                    getJSONByRetrievalDetail(context); 
                }
                else if(method=="getJSONByStationery")
                {
                    getJSONByStationery(context);
                }
                else if (method == "createNewDiscrepancy")
                {
                    createNewDiscrepancy(context);
                }
                else if (method == "getJSONByDepartmentAndItem")
                {
                    getJSONByDepartmentAndItem(context); 
                }
            }

            if (method == "get_access_token")
            {
                get_access_token(context);
            }
            else if (method == "logout")
            {
                logout(context);
            }
        }

        public void getJSONByDepartment(HttpContext context)
        {
            List<MDepartment> mList = new List<MDepartment>();
            List<GetDepartmentForFulfillment> list = new DepartmentController().actionGetDepartmentForFulfillment();

            foreach (GetDepartmentForFulfillment i in list)
            {
                mList.Add(new MDepartment(i.id,i.department_name,i.representative_name,i.name));
            }

            context.Response.Write(JsonConvert.SerializeObject(mList));
            context.Response.End();
        }

        public void getJSONByItem(HttpContext context)
        {
            int deptId = Convert.ToInt32(context.Request.Form["deptId"]);

            List<MFulfillment_View> mList = new List<MFulfillment_View>();
            List<Fulfillment_View> list= new FulfillmentController().actionGetFulfillmentViewByDepartment(deptId);

            foreach (Fulfillment_View i in list)
            {
                mList.Add(new MFulfillment_View(i.id,i.department,i.stationery,i.stationery_name,i.requested_quantity,i.fulfill_quantity,i.unfulfill_quantity,(int)i.stock_control));
            }

            context.Response.Write(JsonConvert.SerializeObject(mList));
            context.Response.End();
        }

        public void updateFulfillQty(HttpContext context)
        {
            int deptId = Convert.ToInt32(context.Request.Form["deptId"]);
            int stationeryId = Convert.ToInt32(context.Request.Form["stationeryId"]);
            int originalQty = Convert.ToInt32(context.Request.Form["originalQty"]);
            int editableQty = Convert.ToInt32(context.Request.Form["editableQty"]);

            new FulfillmentController().actionUpdateQty(deptId, stationeryId, originalQty, editableQty);

            context.Response.ContentType = "text/plain";
            context.Response.Write("Update quantity successful.");
            context.Response.End();
        }

        public void updateToDisbursement(HttpContext context)
        {
            int deptId = Convert.ToInt32(context.Request.Form["deptId"]);
            int stationeryId = Convert.ToInt32(context.Request.Form["stationeryId"]);

            new DisbursementController().actionUpdateToDisbursement(deptId, stationeryId);
        }

        public void getJSONByRetrievalMaster(HttpContext context)
        {
            List<MRetrievelFormMaster_View> mList = new List<MRetrievelFormMaster_View>();
            List<RetrievelFormMaster_View> list = new RequisitionFulfillmentController().actionGetAllRequisitionFulfillmentsForMaster();

            foreach (RetrievelFormMaster_View i in list)
	        {
		        mList.Add(new MRetrievelFormMaster_View(i.id,i.stationery_name,(int)i.Needed,(int)i.Retrieved));
	        }

            context.Response.Write(JsonConvert.SerializeObject(mList));
            context.Response.End();
        }

        public void getJSONByRetrievalDetail(HttpContext context)
        {
            int stationeryId = Convert.ToInt32(context.Request.Form["stationeryId"]);

            List<MRetrievalFormDetails_View> mList = new List<MRetrievalFormDetails_View>();
            List<RetrievalFormDetails_View> list = new RequisitionFulfillmentController().actionGetAllRequisitionFulfillmentsForDetails(stationeryId);

            foreach (RetrievalFormDetails_View i in list)
	        {
                mList.Add(new MRetrievalFormDetails_View(i.id,i.department_name,(int)i.Needed,(int)i.Actual,i.stationery));
	        }

            context.Response.Write(JsonConvert.SerializeObject(mList));
            context.Response.End();
        }

        public void getJSONByStationery(HttpContext context)
        {
            List<MStationery> mList = new List<MStationery>();
            List<Stationery> list = new StationeryController().actionGetStationeries();

            foreach (Stationery i in list)
            {
                mList.Add(new MStationery(i.id, i.stationery_name));
            }

            context.Response.Write(JsonConvert.SerializeObject(mList));
            context.Response.End();
        }

        public void createNewDiscrepancy(HttpContext context)
        {
            int stationeryId = Convert.ToInt32(context.Request.Form["stationeryId"]);
            int quantity = Convert.ToInt32(context.Request.Form["quantity"]);
            string remark = context.Request.Form["remark"];

            Discrepancy d = new Discrepancy();
            d.stationery = stationeryId;
            d.quantity = quantity;
            d.remark = remark;

            new DiscrepencyController().actionCreateNewDiscrepancy(d);

            context.Response.ContentType = "text/plain";
            context.Response.Write("Successfully recorded as discrepancy item.");
            context.Response.End();
        }

        public void getJSONByDepartmentAndItem(HttpContext context)
        {
            int deptId = Convert.ToInt32(context.Request.Form["deptId"]);
            int stationeryId = Convert.ToInt32(context.Request.Form["stationeryId"]);

            Fulfillment_View i = new FulfillmentController().actionGetFulfillmentViewByDepartmentAndStationery(deptId, stationeryId);
            MFulfillment_View m = new MFulfillment_View(i.id, i.department, i.stationery, i.stationery_name, i.requested_quantity, i.fulfill_quantity, i.unfulfill_quantity, (int)i.stock_control);

            context.Response.Write(JsonConvert.SerializeObject(m));
            context.Response.End();
        }

        public void get_access_token(HttpContext context)
        {
            String username = context.Request.Form["username"];
            String password = context.Request.Form["password"];

            OAuthController oauthController = new OAuthController();
            OAuthViewModel oauth=null;
        
            User user = new AuthenticationController().actionCheckLoginData(username, password);
            if (user != null)
            {
                if (user.user_type == 4)
                {
                    oauth=new OAuthViewModel(oauthController.actionGetNewTokenByUsernameAndPassword(username, password));
                }
            }

            context.Response.Write(JsonConvert.SerializeObject(oauth));
        }

        public void logout(HttpContext context)
        {
            String token = context.Request.Form["token"];
            User user = getUserByAccessToken(token);
            //Message message = new Message(false);

            if (user != null)
            {
                new OAuthController().actionDeleteTokenByToken(token);
            }

            //context.Response.Write(JsonConvert.SerializeObject(message));
        }

        public User getUserByAccessToken(String access_token)
        {
            return new OAuthController().actionGetUserByToken(access_token);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class OAuthViewModel
    {
        public String token;

        public OAuthViewModel(String accessToken)
        {
            this.token=accessToken;
        }
    }
}