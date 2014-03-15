using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ApplicationLayer;

namespace PresentationLayer
{
    public partial class DataFiller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataContext db = new DataContext();

            List<Requisition> rl = db.Requisitions.Where(o => (o.id >= 141 && o.id <= 225)).ToList();

            List<PurchaseOrder> po = db.PurchaseOrders.Where( o => (o.id >= 1 && o.id <=300)).ToList();
            foreach (PurchaseOrder p in po) {
                if(p.po_reordered_quantity > 60){
                    p.po_reordered_quantity -= 20;
                }
            }
            db.SaveChanges();

            return;
            foreach (Requisition r2 in rl) {
                Requisition r1 = new Requisition();
                //copier
                Department department = db.Departments.Where(o => o.id == r2.department).SingleOrDefault();
                r1.stationery = r2.stationery;
                r1.quantity = GetRandomNumber(1, 3);
                r1.user_obj = r2.user_obj;
                r1.status = 1;
                r1.department = department.id;

                r1.datetime_requested = DateTime.Now;
                db.Requisitions.AddObject(r1);
            }

            db.SaveChanges();

            return;
            //Requisition r1 = new Requisition();
            //Requisition r2 = new Requisition();


            ////copier
            //Department department = db.Departments.Where(o => o.id == r2.department).SingleOrDefault();
            //r1.stationery = r2.stationery;
            //r1.quantity = r2.quantity;
            //r1.user_obj = r2.user_obj;
            ////r1.status = r2.status;
            //r1.department = department.id;
            ////r1.user_approved_by = department.department_head;


            

        }

        public int GetRandomStationery() {
            return GetRandomNumber(1, 4);
        }


        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        
    }
}