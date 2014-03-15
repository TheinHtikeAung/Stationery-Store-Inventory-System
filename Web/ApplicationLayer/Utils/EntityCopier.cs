using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.ViewModels;


namespace ApplicationLayer.Utils
{
    public class EntityCopier
    {
        public static StationeryModel Copy(Stationery objIn) {
            StationeryModel stationery = new StationeryModel();
            stationery.id = objIn.id;
            stationery.code = objIn.code;
            stationery.stationery_name = objIn.stationery_name;
            stationery.category = objIn.category;
            stationery.price = objIn.price;
            stationery.reorder_level = objIn.reorder_level;
            stationery.reorder_quantity = objIn.reorder_quantity;
            stationery.quantity_in_stock = objIn.quantity_in_stock;
            stationery.pending_quantity_to_distribute = objIn.pending_quantity_to_distribute;
            stationery.first_supplier = objIn.first_supplier.Value;
            stationery.second_supplier = objIn.second_supplier.Value;
            stationery.third_supplier = objIn.third_supplier.Value;

            return stationery;
        }


        public static DiscrepancyModel Copy(Discrepancy objIn)
        {
            DiscrepancyModel discrepency = new DiscrepancyModel();
            discrepency.id = objIn.id;
            discrepency.stationery = objIn.stationery;
            discrepency.quantity = objIn.quantity;
            discrepency.remark = objIn.remark;
            //discrepency.datetime_reported = objIn.datetime_reported;
            discrepency.status = objIn.status;
            return discrepency;
        }

    }
}
