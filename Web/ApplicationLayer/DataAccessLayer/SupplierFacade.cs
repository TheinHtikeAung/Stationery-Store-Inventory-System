using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLayer.Utils;
using System.Data.Entity;

namespace ApplicationLayer.DataAccessLayer
{
    class SupplierFacade : MainFacade
    {
        public List<Supplier> selectObjectAll(DataContext db) 
        { 
            return db.Suppliers.ToList();
        }

        public Supplier selectObjectByID(DataContext db, int idIn)
        {
            return db.Suppliers.Where(o => o.id == idIn).SingleOrDefault();
        }

        public void insertObject(DataContext db, Supplier objIn)
        {
            db.Suppliers.AddObject(objIn);
        }
        
        public void updateObject(DataContext db, Supplier objIn)
        {
            throwExceptionIfInvilid(objIn);

            objIn.EntityKey = db.Suppliers.Where(o => o.id == objIn.id).Single().EntityKey;
            db.Suppliers.ApplyCurrentValues(objIn);
        }
        
        public void deleteObjectByID(DataContext db, int idIn)
        {
            db.Suppliers.DeleteObject(db.Suppliers.Where(o => o.id == idIn).Single());
        }

        public void throwExceptionIfInvilid(Supplier objIn)
        {
            bool have = false;

            if (String.IsNullOrEmpty(objIn.supplier_name))
                have = true;

            if (String.IsNullOrEmpty(objIn.code))
                have = true;

            if (have) {
                throw new Exception("One or More fields are invilid.");
            }
        }
        
    }
}
