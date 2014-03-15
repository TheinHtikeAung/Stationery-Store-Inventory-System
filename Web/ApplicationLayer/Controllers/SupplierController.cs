using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;

using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class SupplierController : MainController
    { 
        public List<Supplier> actionGetSuppliers() 
        {
            return data.Suppliers.ToList();
        }

        public Supplier actionGetSuppierByID(int id) 
        {
            return data.Suppliers.Where(o => o.id == id).SingleOrDefault();
        }

        public Message actionCreateSupplier(Supplier objIn)
        {
            try 
            {
                data.Suppliers.AddObject(objIn);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e) 
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionUpdateSupplier(Supplier objIn)
        {
            try
            {
                Supplier supplier = data.Suppliers.Where(o => o.id == objIn.id).Single();
                objIn.EntityKey = data.Suppliers.Where(o => o.id == objIn.id).Single().EntityKey;
                data.Suppliers.ApplyCurrentValues(objIn);
                data.SaveChanges();
                return this.getNewDefaultMessageForDBOperations(true);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionDeleteSupplierByID(int idIn) 
        {
            try
            {
                data.Suppliers.DeleteObject(data.Suppliers.Where(o => o.id == idIn).Single());
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            } 
            catch (Exception e) 
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

    }
}
