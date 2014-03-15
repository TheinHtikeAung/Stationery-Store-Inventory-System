using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.Utils;
using ApplicationLayer.ViewModels;
namespace ApplicationLayer.Controllers
{
    public class StationeryController : MainController
    {
        public List<Stationery> actionGetStationeries()
        {
            List<Stationery> stationeries = data.Stationeries.ToList();
            return stationeries;
        }

        public List<StationeryView> actionGetStationeryViews()
        {
            List<StationeryView> stationeries = data.StationeryViews.ToList();
            return stationeries;
        }

        public Stationery actionGetStationeryByID(int id)
        {
            return data.Stationeries.Where(o => o.id == id).SingleOrDefault();
        }

        public Message actionCreateStationery(Stationery objIn)
        {
            try
            {
                data.Stationeries.AddObject(objIn);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionUpdateStationery(Stationery objIn)
        {
            try
            {
                //objIn.EntityKey = data.Stationeries.Where(o => o.id == objIn.id).Single().EntityKey;
                Stationery stationery = data.Stationeries.Where(o => o.id == objIn.id).Single();
                stationery.id = objIn.id;
                stationery.stationery_name = objIn.stationery_name;
                stationery.code = objIn.code;
                stationery.price = objIn.price;
                stationery.reorder_level = objIn.reorder_level;
                stationery.reorder_quantity = objIn.reorder_quantity;
                stationery.quantity_in_stock = objIn.quantity_in_stock;
                stationery.pending_quantity_to_distribute = objIn.pending_quantity_to_distribute;
                stationery.first_supplier = objIn.first_supplier;
                stationery.second_supplier = objIn.second_supplier;
                stationery.third_supplier = objIn.third_supplier;
                stationery.unit_of_measure = objIn.unit_of_measure;
                stationery.bin = objIn.bin;

                data.Stationeries.ApplyCurrentValues(stationery);
                data.SaveChanges();
                return this.getNewDefaultMessageForDBOperations(true);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionDeleteStationeryByID(int idIn)
        {
            try
            {
                data.Stationeries.DeleteObject(data.Stationeries.Where(o => o.id == idIn).Single());
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        
        /* Stationery Category */
        public List<StationeryCategory> actionGetStationeryCategories()
        {
            return data.StationeryCategories.ToList();
        }

        public StationeryCategory actionGetStationeryCategoryByID(int id)
        {
            return data.StationeryCategories.Where(o => o.id == id).SingleOrDefault();
        }

        public Message actionCreateStationeryCategory(StationeryCategory objIn)
        {
            try
            {
                data.StationeryCategories.AddObject(objIn);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionUpdateStationeryCategory(StationeryCategory objIn)
        {
            try
            {
                StationeryCategory category = data.StationeryCategories.Where(o => o.id == objIn.id).Single();
                objIn.EntityKey = category.EntityKey;
                data.StationeryCategories.ApplyCurrentValues(objIn);
                data.SaveChanges();
                return this.getNewDefaultMessageForDBOperations(true);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionDeleteStationeryCategoryByID(int idIn)
        {
            try
            {
                data.StationeryCategories.DeleteObject(data.StationeryCategories.Where(o => o.id == idIn).Single());
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public void actionUpdateQty(int stationeryId, int disbutedQty)
        { 
            Stationery stationery = (from s in data.Stationeries
                                    where s.id==stationeryId
                                    select s).FirstOrDefault();

            stationery.quantity_in_stock -= disbutedQty;
            stationery.pending_quantity_to_distribute -= disbutedQty;

            data.SaveChanges();
        }

        public int actionGetQtyInStock(int stationeryId)
        {
            return (from s in data.Stationeries
                    where s.id == stationeryId
                    select s.quantity_in_stock).FirstOrDefault();
        }

        public void actionUpdateDiscrepancyQuantityInStock(int stationeryId, int quantityToUpdate)
        {
            Stationery stationery = (from s in data.Stationeries
                                     where s.id == stationeryId
                                     select s).FirstOrDefault();
            stationery.quantity_in_stock += quantityToUpdate;

            //Update for fulfillment Qty
            int diffQty = stationery.quantity_in_stock - stationery.pending_quantity_to_distribute;
            if (diffQty<0)
            {
                stationery.pending_quantity_to_distribute -= Math.Abs(diffQty);
                new FulfillmentController().updateFulfillQtyForDiscrepancy(stationeryId, Math.Abs(diffQty));
            }

            data.SaveChanges();
        }
        public int actionGetStationeryIdByName(string name)
        {
            Stationery stationery = (from s in data.Stationeries
                                     where s.stationery_name == name
                                     select s).FirstOrDefault();
            return stationery.id;
        }


        //ellen add begin
        //public void actionUpdateDiscrepancyQuantityInStock(int stationeryId, int quantityToUpdate)
        //{
        //    Stationery stationery = (from s in data.Stationeries
        //                             where s.id == stationeryId
        //                             select s).FirstOrDefault();
        //    stationery.quantity_in_stock += quantityToUpdate;
        //    data.SaveChanges();
        //}
        //public int actionGetStationeryIdByName(string name)
        //{
        //    Stationery stationery = (from s in data.Stationeries
        //                             where s.stationery_name == name
        //                             select s).FirstOrDefault();
        //    return stationery.id;
        //}


        //public int actionGetQtyInStock(int stationeryId)
        //{
        //    return (from s in data.Stationeries
        //            where s.id == stationeryId
        //            select s.quantity_in_stock).FirstOrDefault();
        //}


        public string[] getStatoineryString(String prefixText, int count)
        {
            return (from d in data.Stationeries
                    where d.stationery_name.StartsWith(prefixText)
                    select
                        d.stationery_name).Take(count).ToArray();
        }



        public List<Supplier> actionGetSuppliersByItem(String name)
        {
            SupplierController supController = new SupplierController();
            var y = from x in data.Stationeries
                    where x.stationery_name == name
                    select x;

            List<Supplier> myList = new List<Supplier>();
            foreach (Stationery a in y)
            {
                myList.Add(supController.actionGetSuppierByID(Convert.ToInt32(a.first_supplier)));
                myList.Add(supController.actionGetSuppierByID(Convert.ToInt32(a.second_supplier)));
                myList.Add(supController.actionGetSuppierByID(Convert.ToInt32(a.third_supplier)));

            }
            return myList;
        }

        public void actionUpdateQtyInStock(int stationeryId, int amount)
        {

            Stationery stationery = (from s in data.Stationeries
                                     where s.id == stationeryId
                                     select s).FirstOrDefault();

            stationery.quantity_in_stock += amount;
            data.SaveChanges();

        }

        public String actionGetStationeryQuantityStocked(int Statid)
        {
            var quantityStocked = (from q in data.Stationeries
                                   where q.id == Statid
                                   select
                                       q.quantity_in_stock).FirstOrDefault();

            return quantityStocked.ToString();


        }

        public String actionGetStatIdbyItemName(String name)
        {
            var StatId = (from st in data.Stationeries
                          where st.stationery_name == name
                          select st.id).FirstOrDefault();
            return StatId.ToString();

        }
        //ellen add end


        public List<Supplier> actionGetSuppliersOfAStationery(int stationery_id)
        {
            Stationery stationery = data.Stationeries.Where(o => o.id == stationery_id).SingleOrDefault();
            List<Supplier> suppliers = new List<Supplier>();
            suppliers.Add(data.Suppliers.Where(o => o.id == stationery.first_supplier).SingleOrDefault());
            suppliers.Add(data.Suppliers.Where(o => o.id == stationery.second_supplier).SingleOrDefault());
            suppliers.Add(data.Suppliers.Where(o => o.id == stationery.third_supplier).SingleOrDefault());

            return suppliers;

        }

    }
}
