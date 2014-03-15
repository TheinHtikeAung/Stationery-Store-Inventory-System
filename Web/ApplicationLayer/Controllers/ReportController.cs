using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class ReportController : MainController
    {
        DataContext data = new DataContext();

        public List<string> getAllstationeryNames()
        {
             return data.Stationeries.Select(x => x.stationery_name).ToList();
            
        }

        public string getDepartmentNameByUsedID(int userID)
        {
            string deptId= data.Users.Where(x => x.id == userID).Select(x => x.department).ToString();
            return data.Departments.Where(x => x.id == Convert.ToInt32(deptId)).Select(x => x.department_name).ToString();
        }

        public List<string> getAllDepartmentNames()
        {
            return data.Departments.Select(x => x.department_name).ToList();
        }

        public List<string> getAllStationeryCategories()
        {
            return data.StationeryCategories.Select(x => x.category_name).ToList();
        }

        public Dictionary<string, Dictionary<string, double>> getAllDisbursementsByNameAndDates(string departmentName, string[] dates)
        {
            List<Report_View> reportList = data.Report_View.Where(x => x.department_name == departmentName).ToList();
            return getDictionaryByReportList(reportList, dates);
        }

        public Dictionary<string, Dictionary<string, double>> getAllDisbursementsByNameAndDates(string departmentName, string[] dates, String filterType)
        {
            List<Report_View> reportList = data.Report_View.Where(x => x.department_name == departmentName).ToList();
            return getDictionaryByReportList(reportList, dates, filterType);
        }

        public Dictionary<string, Dictionary<string, double>> getAllDisbursementsByDates(string[] dates)
        {
            List<Report_View> fullReportList = data.Report_View.ToList();
            return getDictionaryByReportList(fullReportList, dates);
        }

        //for clerk
        private Dictionary<string, Dictionary<string, double>> getDictionaryByReportList(List<Report_View> reportList, string[] dates)
        {
            Dictionary<string, Dictionary<string, double>> reportDict = new Dictionary<string, Dictionary<string, double>>();
            //string[] monthStr = { "2/2013", "12/2012", "11/2009" };
            HashSet<string> monthYearSet = new HashSet<string>();
            int[] months = new int[3] { 0, 0, 0 };
            int[] years = new int[3] { 0, 0, 0 };
            for (int i = 0; i < dates.Length; i++)
            {
                string[] splitString = dates[i].Split('/');
                months[i] = Convert.ToInt32(splitString[0]);
                years[i] = Convert.ToInt32(splitString[1]);
                monthYearSet.Add(splitString[0] + "-" + splitString[1]);
            }
            if (reportList != null)
            {
              
                foreach (Report_View rpt in reportList)
                {
                    int month = rpt.distributed_date.Month;
                    int year = rpt.distributed_date.Year;
                    string monthYear = getTimeStr(month) + "-" + year;
                    string categoryName = rpt.category_name;
                    if (monthYearSet.Contains(monthYear))
                    {
                        Dictionary<string, double> dict;
                        if (!reportDict.ContainsKey(categoryName))
                        {
                            dict = new Dictionary<string, double>();
                        }
                        else
                        {
                            dict = reportDict[categoryName];
                        }
                        
                        if (!dict.ContainsKey(monthYear))
                        {
                            dict.Add(monthYear, rpt.fulfilled_quantity);
                        }
                        else
                        {
                            dict[monthYear] = dict[monthYear] + rpt.fulfilled_quantity;
                        }
                        //reportDict.Add(departmentName, dict);
                        reportDict[categoryName] = dict;
                    }
                }
            }
            return reportDict;
        }

        //for dept head
        private Dictionary<string, Dictionary<string, double>> getDictionaryByReportList(List<Report_View> reportList, string[] dates, string filterType)
        {
            Dictionary<string, Dictionary<string, double>> reportDict = new Dictionary<string, Dictionary<string, double>>();
            //string[] monthStr = { "2/2013", "12/2012", "11/2009" };
            HashSet<string> monthYearSet = new HashSet<string>();
            int[] months = new int[3] { 0, 0, 0 };
            int[] years = new int[3] { 0, 0, 0 };
            for (int i = 0; i < dates.Length; i++)
            {
                string[] splitString = dates[i].Split('/');
                months[i] = Convert.ToInt32(splitString[0]);
                years[i] = Convert.ToInt32(splitString[1]);
                monthYearSet.Add(splitString[0] + "-" + splitString[1]);
            }
            if (reportList != null)
            {

                foreach (Report_View rpt in reportList)
                {
                    int month = rpt.distributed_date.Month;
                    int year = rpt.distributed_date.Year;
                    string monthYear = getTimeStr(month) + "-" + year;
                    string categoryName = rpt.category_name;
                    if (monthYearSet.Contains(monthYear))
                    {
                        Dictionary<string, double> dict;
                        if (!reportDict.ContainsKey(categoryName))
                        {
                            dict = new Dictionary<string, double>();
                        }
                        else
                        {
                            dict = reportDict[categoryName];
                        }

                        if (!dict.ContainsKey(monthYear))
                        {
                            if (filterType.Equals("1"))
                            {
                                dict.Add(monthYear, rpt.requested_quantity);
                            }
                            else
                            {
                                dict.Add(monthYear,(rpt.price*rpt.requested_quantity));
                            }
                        }
                        else
                        {
                            if (filterType.Equals("1"))
                            {
                                dict[monthYear] = dict[monthYear] + rpt.requested_quantity;
                            }
                            else
                            {
                                dict[monthYear] = dict[monthYear] + (rpt.price * rpt.requested_quantity);
                            }                            
                        }
                        //reportDict.Add(departmentName, dict);
                        reportDict[categoryName] = dict;
                    }
                }
            }
            return reportDict;
        }

        //purchase reorder report
        public Dictionary<string, Dictionary<string, double>> getAllPurchaseOrders(string[] dates)
        {
            List<ReOrderReport_View> reorderReportViewList = data.ReOrderReport_View.ToList();
            return getDictionaryByReorderReportList(reorderReportViewList,dates);
        }

        private Dictionary<string, Dictionary<string, double>> getDictionaryByReorderReportList(List<ReOrderReport_View> reportList, string[] dates)
        {
            Dictionary<string, Dictionary<string, double>> reportDict = new Dictionary<string, Dictionary<string, double>>();
            //string[] monthStr = { "2/2013", "12/2012", "11/2009" };
            HashSet<string> monthYearSet = new HashSet<string>();
            int[] months = new int[3] { 0, 0, 0 };
            int[] years = new int[3] { 0, 0, 0 };
            for (int i = 0; i < dates.Length; i++)
            {
                string[] splitString = dates[i].Split('/');
                months[i] = Convert.ToInt32(splitString[0]);
                years[i] = Convert.ToInt32(splitString[1]);
                monthYearSet.Add(splitString[0] + "-" + splitString[1]);
            }
            if (reportList != null)
            {

                foreach (ReOrderReport_View rpt in reportList)
                {
                    int month = rpt.po_reorder_date.Month;
                    int year = rpt.po_reorder_date.Year;
                    string monthYear = getTimeStr(month) + "-" + year;
                    string categoryName = rpt.category_name;
                    if (monthYearSet.Contains(monthYear))
                    {
                        Dictionary<string, double> dict;
                        if (!reportDict.ContainsKey(categoryName))
                        {
                            dict = new Dictionary<string, double>();
                        }
                        else
                        {
                            dict = reportDict[categoryName];
                        }

                        if (!dict.ContainsKey(monthYear))
                        {
                            dict.Add(monthYear, rpt.po_reordered_quantity);
                        }
                        else
                        {
                            dict[monthYear] = dict[monthYear] + rpt.po_reordered_quantity;
                        }
                        //reportDict.Add(departmentName, dict);
                        reportDict[categoryName] = dict;
                    }
                }
            }
            return reportDict;
        }
        
        public List<Report_View> getAllReportViews()
        {

            //var result = data.Report_View.GroupBy(u => u.stationery_name).ToList(); 
            //return result;
            return data.Report_View.ToList();
        }

        private string getTimeStr(int timeUnit){
            if(timeUnit.ToString().Length==1){
                return "0"+timeUnit.ToString();
            }else{
                return timeUnit.ToString();
            }
        }

        
        public double getTotalPrice(int depId, string date)
        {
            
            data.Report_View.Where(x => x.department == depId);
            //return data.Report_View.Where(x=>x.department==depId).Where(x=>x.distributed_date.mo)
            return 0.0;
        }
    }
}
