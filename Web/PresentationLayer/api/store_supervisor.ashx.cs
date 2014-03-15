using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ApplicationLayer;
using ApplicationLayer.Controllers;
using ApplicationLayer.Utils;
using Newtonsoft.Json;

namespace PresentationLayer.api
{
    /// <summary>
    /// Summary description for store_supervisor
    /// </summary>
    public class store_supervisor : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String method = context.Request.Form["method"];

            if (method == "get_access_token")
            {
                get_access_token(context);
            }
            else if (method == "check_access_token")
            {
                check_access_token(context);
            }
            else if (method == "get_descrepencies")
            {
                get_descrepencies(context);
            }
            else if (method == "approve_discrepency") {
                approve_discrepency(context);
            }
            else if (method == "logout") {
                logout(context);
            }

        }

        public void get_access_token(HttpContext context)
        {
            String username = context.Request.Form["username"];
            String password = context.Request.Form["password"];

            OAuthController oauthController = new OAuthController();
            OAuthViewModel oauth = new OAuthViewModel();
            oauth.access_token = "";
            User user = new AuthenticationController().actionCheckLoginData(username, password);
            if (user != null) {
                if (user.user_type == 5 || user.user_type == 6){
                    oauth.access_token = oauthController.actionGetNewTokenByUsernameAndPassword(username, password);   
                }
            }

            context.Response.Write(JsonConvert.SerializeObject(oauth));
        }

        public void check_access_token(HttpContext context)
        {
            User user = getUserByAccessToken(context.Request.Form["access_token"]);
            Message message = new Message(false);

            if (user != null)
            {
                if (user.user_type == 5 || user.user_type == 6)
                {
                    message.condition = true;
                }
            }
            else
            {
                message.message = "Invilid or Expire Token!";
            }

            context.Response.Write(JsonConvert.SerializeObject(message));
        }

        public void get_descrepencies(HttpContext context) {
            User user = getUserByAccessToken(context.Request.Form["access_token"]);
            
            if(user == null){return;}
            List<Supervisor_Discrepancy_View> ds = new List<Supervisor_Discrepancy_View>();
            DiscrepencyController discripencyController = new DiscrepencyController();
            switch (user.user_type) { 
                case 5:
                    ds = discripencyController.actionGetDiscrepanciesForStoreSupervisor();
                    break;
                case 6:
                    ds = discripencyController.actionGetDiscrepanciesForStoreManager();
                    break;
                default:
                    break;
            }
            List<DiscrepencyViewModel> dvs = new List<DiscrepencyViewModel>();
            foreach (Supervisor_Discrepancy_View d in ds)
            { 
                DiscrepencyViewModel vm = new DiscrepencyViewModel();
                vm.id = d.id;
                vm.stationery_id = d.stationery;
                vm.stationery_name = d.stationery_name;
                vm.quantity = d.quantity;
                vm.price = d.price;
                vm.totalprice = d.totalprice;
                vm.datetime_reported = ((DateTime)d.datetime_reported).ToString(" dd/ MM/ yyyy");
                vm.remark = d.remark;
                dvs.Add(vm);
            }

            context.Response.Write(JsonConvert.SerializeObject(dvs));
        }

        public void approve_discrepency(HttpContext context) {
            
            User user = getUserByAccessToken(context.Request.Form["access_token"]);
            Message message = new Message(false);

            if (user != null)
            {
                if (user.user_type != 5 || user.user_type != 6)
                {
                    DiscrepencyController discrepencyController = new DiscrepencyController();
                    String[] id = context.Request.Form.GetValues("id");

                    for (int i = 0; i < id.Length; i++)
                    {
                        discrepencyController.actionApproveDiscrepancies(Convert.ToInt32(id[i]));
                    }
                    message.condition = true;
                    message.message = "Successfully Approved.";
                } else {
                    message.message = "Access denied!";
                }
            }
            else {
                message.message = "User token is invilid!";
            }
            
            context.Response.Write(JsonConvert.SerializeObject(message));
        }

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

        /*** */
        public User getUserByAccessToken(String access_token) {
            return new OAuthController().actionGetUserByToken(access_token);
        }
        
        
        public class OAuthViewModel {
            public String user_id;
            public String access_token;
        }

        public class DiscrepencyViewModel {
            public int id;
            public int stationery_id;
            public String stationery_name;
            public int quantity;
            public String remark;
            public String datetime_reported;
            public double price;
            public double totalprice;

        }









        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}