using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApplicationLayer.Utils;
namespace ApplicationLayer
{
    public class CustomUserDelegate
    {
        public CustomUserDelegate(int id, string user_to, string user_by, 
            DateTime date_from, DateTime date_to, int department)
        {
            this.id = id;
            this.user_to = user_to;
            this.user_by = user_by;
            this.date_from = date_from;
            this.date_to = date_to;
            this.department = department;
        }

        public int? id { get; set; }
        public string user_to { get; set; }
        public string user_by { get; set; }
        public DateTime date_from { get; set; }
        public DateTime date_to { get; set; }
        public int? department { get; set; }
    }
}