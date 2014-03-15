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
using ApplicationLayer;
namespace PresentationLayer.DepartmentHead
{
    public partial class HeadReport : System.Web.UI.Page
    {
        static ReportController rc;
        string departmentName;

        protected void Page_Load(object sender, EventArgs e)
        {
            rc = new ReportController();
            User user = (User)Session["user"];
            if (user == null) {
                return;
            }
            
            else{
                departmentName = rc.getDepartmentNameByUsedID(user.id);
            }
           
            /*
            List<String> departmentNames = rc.getAllDepartmentNames();
            foreach (var departmentName in departmentNames)
                departmentDropDownList.Items.Add(departmentName);
            departmentDropDownList.DataBind();
             * */
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
            User user = (User)HttpContext.Current.Session["user"];
            if (user == null)
            {
                return null;
            }
            
            Dictionary<string, Dictionary<string, double>> reportDict;

            String valuesStr = jsonParam.ToString();

            String[] values = valuesStr.Split(',');
            String quantityType = values[values.Length - 1];

            String[] monthYearValues = new String[values.Length - 1];
            for (int i = 0; i < values.Length - 1; i++)
            {
                monthYearValues[i] = values[i];
            }
            
            String departmentName = new DepartmentController().actionGetDepartmentByID(Convert.ToInt32(user.department)).department_name;
            JavaScriptSerializer js = new JavaScriptSerializer();
            reportDict = rc.getAllDisbursementsByNameAndDates(departmentName, monthYearValues,quantityType);

            return js.Serialize(reportDict);
        }

        //Dictionary<string, Dictionary<string, int>> reportNameDict = rc.getAllDisbursementsByNameAndDates("Pen Blu", new string[] { "12/2013", "10/2010", "4/2011" });


    }
}