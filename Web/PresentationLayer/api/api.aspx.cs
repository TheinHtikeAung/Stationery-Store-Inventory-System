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

using ApplicationLayer.ViewModels;
using ApplicationLayer.Utils;

namespace PresentationLayer.api
{
    public partial class api : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string get_all_stationaries()
        {  
            StationeryController stationaryController = new StationeryController();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Stationery> stationery_entities = stationaryController.actionGetStationeries();
            List<StationeryModel> stationery_models = new List<StationeryModel>();

            foreach (Stationery s in stationery_entities)
            {
                stationery_models.Add(EntityCopier.Copy(s));
            }
            String str = js.Serialize(stationery_models);
            return str;
        }
    
    
    }
}