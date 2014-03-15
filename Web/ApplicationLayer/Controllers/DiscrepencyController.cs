using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.Utils;
using ApplicationLayer.ViewModels;
namespace ApplicationLayer.Controllers
{
    public class DiscrepencyController : MainController
    {
        public List<Discrepancy> actionGetAllDiscrepancies()
        {
            return data.Discrepancies.ToList();
        }

        public void actionCreateNewDiscrepancy(int stationeryId, int quantity, string remarks)
        {
            Discrepancy discrepancy = new Discrepancy();
            discrepancy.stationery = stationeryId;
            discrepancy.quantity = quantity;
            discrepancy.remark = remarks;
            discrepancy.datetime_reported = DateTime.Now;
            data.Discrepancies.AddObject(discrepancy);
            data.SaveChanges();
        }

        public Message actionCreateNewDiscrepancy(Discrepancy discrepancy)
        {
            discrepancy.datetime_reported = DateTime.Now;
            data.Discrepancies.AddObject(discrepancy);

            Stationery stationery = data.Stationeries.Where(o => o.id == discrepancy.stationery).Single();
            stationery.quantity_in_stock = stationery.quantity_in_stock - discrepancy.quantity;

            StockTransaction stockTransaction = new StockTransaction();
            stockTransaction.stationery = stationery.id;
            stockTransaction.quantity = "-" + discrepancy.quantity;
            stockTransaction.description = discrepancy.remark;
            stockTransaction.balance = stationery.quantity_in_stock.ToString();
            stockTransaction.datetime_inserted = DateTime.Now;
            data.StockTransactions.AddObject(stockTransaction);

            try
            {
                if (data.SaveChanges() == 3)
                {
                    return new Message(true);
                }
                else
                {
                    return new Message(false);
                }
            }
            catch (Exception e)
            {
                return new Message(false, Helper.getExceptionMessage(e));
            }

        }

        public void actionUpdateDiscrepancy(int discrepancyId, int stationeryId, int quantity, string remarks, DateTime date_of_report)
        {
            Discrepancy discrepancy = (from s in data.Discrepancies
                                       where s.id == discrepancyId
                                       select s).FirstOrDefault();
            if (discrepancy != null)
            {
                discrepancy.stationery = stationeryId;
                discrepancy.quantity = quantity;
                discrepancy.remark = remarks;
                discrepancy.datetime_reported = date_of_report;
                data.SaveChanges();
            }
        }

        public void actionDeleteDiscrepancy(int discrepancyId)
        {
            Discrepancy discrepancy = (from s in data.Discrepancies
                                       where s.id == discrepancyId
                                       select s).FirstOrDefault();
            if (discrepancy != null)
            {
                data.Discrepancies.DeleteObject(discrepancy);
                data.SaveChanges();
            }
        }

        public Discrepancy actionGetDiscrepancy(int discrepancyId)
        {
            Discrepancy discrepancy = (from s in data.Discrepancies
                                       where s.id == discrepancyId
                                       select s).FirstOrDefault();
            return discrepancy;
        }

        public List<DiscrepancyModel> actionGetPendingDiscrepency()
        {
            List<Discrepancy> discrepancy_entities = data.Discrepancies.Where(o => o.status == 0).ToList();
            List<DiscrepancyModel> stationery_models = new List<DiscrepancyModel>();
            foreach (Discrepancy discrepency in discrepancy_entities)
            {
                stationery_models.Add(EntityCopier.Copy(discrepency));
            }

            return stationery_models;
        }


        public List<Discrepancy_View> actionGetAllDiscrepancyViews()
        {
            return data.Discrepancy_View.ToList();
        }

        public List<Discrepancy_View> actionGetPendingDiscrepancyViews()
        {
            return data.Discrepancy_View.Where(o => (o.status == 0)).ToList();
        }

        public List<Discrepancy_View> actionGetApprovedDiscrepancyViews()
        {
            return data.Discrepancy_View.Where(o => (o.status == 1)).ToList();
        }

        public List<Supervisor_Discrepancy_View> actionGetDiscrepanciesForStoreSupervisor()
        {

            //var supervisorDiscrepancy = from p in data.Supervisor_Discrepancy_View
            //            where p.status == 0
            //            select (from q in data.Supervisor_Discrepancy_View
            //                    where p.price * p.quantity <= 250
            //                    select q);
            return data.Supervisor_Discrepancy_View.Where(x => x.status == 0).Where(x => x.price * x.quantity <= 250).ToList();
             
        }

        public void actionApproveDiscrepancies(int id)
        {
            Discrepancy discrepancy = data.Discrepancies.Where(x => x.id == id).FirstOrDefault();
            discrepancy.status = 1;
            data.SaveChanges();
         }

        public List<Supervisor_Discrepancy_View> actionGetDiscrepanciesForStoreManager()
        {

            //var supervisorDiscrepancy = from p in data.Supervisor_Discrepancy_View
            //            where p.status == 0
            //            select (from q in data.Supervisor_Discrepancy_View
            //                    where p.price * p.quantity <= 250
            //                    select q);
            return data.Supervisor_Discrepancy_View.Where(x => x.status == 0).Where(x => x.price * x.quantity > 250).ToList();

        }
    }
}
