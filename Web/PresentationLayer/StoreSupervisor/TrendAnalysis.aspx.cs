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
    public partial class ClerkReport : System.Web.UI.Page
    {
        static ReportController rc;

        protected void Page_Load(object sender, EventArgs e)
        {
            rc = new ReportController();
            List<String> departmentNames = rc.getAllDepartmentNames();
            foreach (var departmentName in departmentNames)
                departmentDropDownList.Items.Add(departmentName);
            departmentDropDownList.DataBind();
            //Dictionary<string, Dictionary<string, int>> reportDict=rc.getAllDisbursementsByDates(new string[]{"12/2013","10/2010","4/2011"});
            //Dictionary<string, Dictionary<string, int>> reportNameDict=rc.getAllDisbursementsByNameAndDates("Pen Blu", new string[] { "12/2013", "10/2010", "4/2011" });
            //monthListBox.Text=de
        }

        protected void toListBoxBtn_click(object sender, EventArgs e)
        {
            
        }

        //protected void showStatistics_Click(object sender, EventArgs e)
        //{
        //    Dictionary<string, Dictionary<string, int>> reportDict = rc.getAllDisbursementsByDates(new string[] { "12/2013", "10/2010", "4/2011" });
        //    Dictionary<string, Dictionary<string, int>> reportNameDict = rc.getAllDisbursementsByNameAndDates("Pen Blu", new string[] { "12/2013", "10/2010", "4/2011" });
            
        //}

        [WebMethod]
        public static string GetAllDisbursements(object jsonParam)
        {
            Dictionary<string, Dictionary<string, double>> reportDict;

            String valuesStr = jsonParam.ToString();

            String[] values = valuesStr.Split(',');
            String departmentName = values[values.Length - 1];

            String[] monthYearValues = new String[values.Length - 1];
            for (int i = 0; i < values.Length - 1; i++)
            {
                monthYearValues[i] = values[i];
            }
            
            //Dictionary<string, string> values = (Dictionary<string, string>)JsonConvert.DeserializeObject(jsonParam.ToString());

            //String valueString = jsonParam.ToString();            
            //String[] values = valueString.Split(',');
            //String[] monthYearValues = new String[3];
            //for(int i=0;i<values.Length;i++){
            //    String[] splitStr = values[i].Split(':');
            //    monthYearValues[i] = splitStr[1];
            //}
            JavaScriptSerializer js = new JavaScriptSerializer();
            //Dictionary<string, Dictionary<string, int>> reportDict = rc.getAllDisbursementsByDates(new string[] { "12/2013", "10/2010", "4/2011" });
            //Dictionary<string, Dictionary<string, int>> reportDict = rc.getAllDisbursementsByDates(monthYearValues);
            
            reportDict = rc.getAllDisbursementsByNameAndDates(departmentName, monthYearValues);
            
            return js.Serialize(reportDict);
        }

        //Dictionary<string, Dictionary<string, int>> reportNameDict = rc.getAllDisbursementsByNameAndDates("Pen Blu", new string[] { "12/2013", "10/2010", "4/2011" });
            
            
    }
}