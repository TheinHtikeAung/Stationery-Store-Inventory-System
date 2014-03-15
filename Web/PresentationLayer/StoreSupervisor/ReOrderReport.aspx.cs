using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationLayer.Controllers;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections;

namespace PresentationLayer.StoreClerk
{
    public partial class ReOrderReport : System.Web.UI.Page
    {
        static ReportController rc;
        protected void Page_Load(object sender, EventArgs e)
        {
            rc = new ReportController();
            //List<String> categories = rc.getAllStationeryCategories();
            //foreach (var c in categories)
            //    categoryDropDownList.Items.Add(c);
            //categoryDropDownList.DataBind();
        }

        [WebMethod]
        public static string GetAllPurchaseOrders(object jsonParam)
        {
            Dictionary<string, Dictionary<string, double>> reportDict;

            String valuesStr = jsonParam.ToString();

            String[] values = valuesStr.Split(',');
            String[] monthYearValues = new String[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                monthYearValues[i] = values[i];
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            reportDict = rc.getAllPurchaseOrders(monthYearValues);
            return js.Serialize(reportDict);
        }
    }
}