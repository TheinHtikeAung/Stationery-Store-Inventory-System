using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.ViewModels
{
    public class DiscrepancyModel
    {
        public int id;
        public int stationery;
        public int quantity;
        public String remark;
        public DateTime datetime_reported;

        public int status;

        public int Id { get; set; }
        public int Stationery { get; set; }
        public int Quantity { get; set; }
        public String Remark { get; set; }
        public DateTime Datetime_reported { get; set; }
        public int Status{ get; set; }
    }
}
