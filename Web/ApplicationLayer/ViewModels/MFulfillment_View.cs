using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.ViewModels
{
    public class MFulfillment_View
    {
        public int id;
        public int department;
        public int stationery;
        public string stationery_name;
        public int requested_quantity;
        public int fulfill_quantity;
        public int unfulfill_quantity;
        public int stock_control;

        public MFulfillment_View(int id, int department, int stationery, string stationery_name,
            int request_quantity, int fulfill_quantity, int unfulfill_quantity, int stock_control)
        {
            this.id = id;
            this.department = department;
            this.stationery = stationery;
            this.stationery_name = stationery_name;
            this.requested_quantity = request_quantity;
            this.fulfill_quantity = fulfill_quantity;
            this.unfulfill_quantity = unfulfill_quantity;
            this.stock_control = stock_control;
        }
    }
}
