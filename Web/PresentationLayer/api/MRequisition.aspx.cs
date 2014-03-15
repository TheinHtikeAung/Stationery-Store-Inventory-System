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
namespace PresentationLayer.Mobile
{
    public partial class MRequisition : System.Web.UI.Page
    {
        static StationeryController stationaryController = new StationeryController();
        protected void Page_Load(object sender, EventArgs e)
        {
            string method = Request.QueryString["method"];
            if (method == "makeRequisition")
            {
                string accessToken = Request.QueryString["accessToken"];
                int qty = Convert.ToInt32(Request.QueryString["qty"]);
                string stationeryName = Request.QueryString["stName"];

                OAuthController oAuth = new OAuthController();
                User user = oAuth.actionGetUserByToken(accessToken);
                int userId = user.id;



                makeRequisition(userId, stationeryName, qty);

            }
            else if (method == "loadAllStationaries")
            {
                loadAllStationaries();
            }
            else if (method == "getStationaryId")
            {
                System.Diagnostics.Debug.WriteLine("Inside getStationaryId");
                string stationeryName = Request.QueryString["stName"];
                string stName = stationeryName.Replace('_', ' ');
                System.Diagnostics.Debug.WriteLine(" json:" + stName);
                getStationaryId(stName);
            }
            // Response.Write(loadAllStationaries());
        }

        public void loadAllStationaries()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Stationery> stationery_entities = stationaryController.actionGetStationeries();
            List<StationeryModel> stationery_models = new List<StationeryModel>();
            foreach (Stationery s in stationery_entities)
            {
                stationery_models.Add(EntityCopier.Copy(s));
            }
            String str = js.Serialize(stationery_models);
            Response.Write(str);
        }//loadAllStationaries

        public void getStationaryId(string stationaryName)
        {
            StationeryController stController = new StationeryController();
            int id = Convert.ToInt32(stController.actionGetStationeryIdByName(stationaryName));
            System.Diagnostics.Debug.WriteLine("id is :" + id);
        }//getStationaryId()

        public string makeRequisition(int userId, string stationeryName, int quantity)
        {
            StationeryController stController = new StationeryController();
            Requisition requisition = new Requisition();


            // System.Diagnostics.Debug.WriteLine("inside actionGetStationeryIdByName::" + name);
            string newstationeryName = stationeryName.Replace('_', ' ');
            //  System.Diagnostics.Debug.WriteLine("inside actionGetStationeryIdByName::" + newstationeryName);


            int stationeryId = Convert.ToInt32(stController.actionGetStationeryIdByName(newstationeryName));
            requisition.stationery = stationeryId;
            requisition.quantity = quantity;
            requisition.user_obj = userId;
            //hardcoded department id
            requisition.department = 8;
            requisition.user_approved_by = null;
            RequisitionController requisitionController = new RequisitionController();
            Message msg = requisitionController.actionCreateRequisition(requisition);
            if (msg.condition)
            {
                System.Diagnostics.Debug.WriteLine("success");
                return "success";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("fail");
                return "fail";
            }
        }//makeRequisition

    }
}