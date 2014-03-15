using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.ViewModels
{
    public class StationeryModel
    {
        public int id;
        public string code;
        public string stationery_name;
        public int category;
        public double price;
        public int reorder_level;
        public int reorder_quantity;
        public int quantity_in_stock;
        public int pending_quantity_to_distribute;
        public int first_supplier;
        public int second_supplier;
        public int third_supplier;

        public int Id { get; set; }
        public int Code { get; set; }
    }
}
