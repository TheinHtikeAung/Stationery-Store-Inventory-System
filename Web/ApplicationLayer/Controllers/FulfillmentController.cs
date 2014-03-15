using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class FulfillmentController : MainController
    {
        //int req_dept;
        RequisitionFullfillment oldRF;
        RequisitionController rc;

        public Message actionChangeAllToFulfillmentByDepartment(int dept_id)
        {
            rc = new RequisitionController();
            List<Requisition> approvedList = rc.actionGetApprovedRequisitionsByDepartmentID(dept_id);

            return proceedAll(approvedList);
        }

        public Message actionChangeAllToFulfillmentByDepartmentAndDateTime(int dept_id, DateTime dateTime)
        {
            rc = new RequisitionController();
            List<Requisition> approvedList = rc.actionGetApprovedRequisitionsByDepartmentAndDateTime(dept_id, dateTime);

            return proceedAll(approvedList);
        }

        public Message actionChangeAllToFulfillmentByDepartmentAndStationery(int dept_id, int stationeryId)
        {
            rc = new RequisitionController();
            List<Requisition> approvedList = rc.actionGetApprovedRequisitionsByDepartmentAndStationery(dept_id, stationeryId);

            return proceedAll(approvedList);
        }

        public Message actionChangeAllToFulfillmentByDepartmentAndStationeryAndTime(int dept_id, int stationeryId, DateTime dateTime)
        {
            rc = new RequisitionController();
            List<Requisition> approvedList = rc.actionGetApprovedRequisitionsByDepartmentAndStationeryAndDateTime(dept_id, stationeryId, dateTime);

            return proceedAll(approvedList);
        }

        public Message actionChangeAllToFulfillment()
        {
            rc = new RequisitionController();
            List<Requisition> approvedList = rc.actionGetApprovedRequisitions();

            return proceedAll(approvedList);
        }


        private Message proceedAll(List<Requisition> approvedList)
        {
            try
            {
                foreach (Requisition item in approvedList)
                {
                    rc.actionRequisitionStatusChangeToProcessing(item.id);

                    if (existingData(item))
                    {
                        oldRF.requested_quantity += item.quantity;
                        oldRF.fulfill_quantity += calculateQty(item.stationery, item.quantity);
                        
                        Console.WriteLine("Updating Requisition Fulfillment successful !"+data.SaveChanges());
                    }
                    else
                    {
                        RequisitionFullfillment newRF = new RequisitionFullfillment();
                        newRF.department = item.department;
                        newRF.stationery = item.stationery;
                        newRF.requested_quantity = item.quantity;
                        newRF.fulfill_quantity = calculateQty(item.stationery, item.quantity);
                        newRF.unfulfill_quantity = 0;
                        data.RequisitionFullfillments.AddObject(newRF);
                        Console.WriteLine("Creating New Requisition Fullfillment successful !" + data.SaveChanges());
                    }
                }
            }
            catch (Exception e)
            { 
            
            }

            return new Message();   
        }

        private bool existingData(Requisition item)
        {
            oldRF = actionGetFulfillmentByDepartmentAndStationery(item.department, item.stationery);

            return (oldRF!=null?true:false);
        }

        public RequisitionFullfillment actionGetFulfillmentByDepartmentAndStationery(int req_dept, int stationery)
        {
            return (from rf in data.RequisitionFullfillments
                    where rf.department==req_dept
                    where rf.stationery==stationery
                    select rf).FirstOrDefault();
        }

        private int calculateQty(int itemId, int qty)
        {
            int affordAbleQty;

            int available_stock=(int) (from s in data.Stationeries
                                       where s.id==itemId
                                       select s.quantity_in_stock-s.pending_quantity_to_distribute)
                                       .FirstOrDefault();

            if (qty <= available_stock)
                affordAbleQty = qty;
            else
                affordAbleQty = available_stock;

            updateStationeryPendingQty(itemId, affordAbleQty);

            return affordAbleQty;
        }

        public void updateStationeryPendingQty(int itemId, int affordAbleQty)
        {
            Stationery stationery = data.Stationeries.Where(s=> (s.id == itemId)).Single();
            stationery.pending_quantity_to_distribute += affordAbleQty;
            Console.WriteLine("Updating stationery qty successful !"+data.SaveChanges());
        }

        public List<Fulfillment_View> actionGetFulfillmentViewByDepartment(int deptId)
        {
            return (from v in data.Fulfillment_View where v.department == deptId where v.requested_quantity!=0 select v).ToList();
        }

        public Fulfillment_View actionGetFulfillmentViewByDepartmentAndStationery(int deptId, int stationeryId)
        {
            return (from v in data.Fulfillment_View where v.department == deptId where v.requested_quantity != 0 where v.stationery == stationeryId select v).SingleOrDefault();
        }

        public void actionUpdateQty(int deptId, int stationeryId, int originalQty, int editableQty)
        {
            oldRF = actionGetFulfillmentByDepartmentAndStationery(deptId, stationeryId);
            oldRF.fulfill_quantity = editableQty;

            int changedQty = originalQty - editableQty;

            Stationery statoinery = (from s in data.Stationeries 
                                    where s.id==stationeryId
                                    select s)
                                    .FirstOrDefault();

            statoinery.pending_quantity_to_distribute -= changedQty;
            data.SaveChanges();
        }

        public void actionDeleteFulfillmentByObject(RequisitionFullfillment rf)
        {
            data.RequisitionFullfillments.DeleteObject(rf);
            data.SaveChanges();
        }

        public void actionUpdateFulfillmentByObject(RequisitionFullfillment rf, int unfulfillQty)
        {
            RequisitionFullfillment requisitionFulfillment = rf;
            requisitionFulfillment.requested_quantity = unfulfillQty;
            requisitionFulfillment.fulfill_quantity = 0;
            requisitionFulfillment.unfulfill_quantity = unfulfillQty;

            data.SaveChanges();
        }

        public List<GetDepartmentFulfillment> actionGetDepartmentFulfillment()
        { 
            return (from g in data.GetDepartmentFulfillments select g).ToList();
        }

        public void updateFulfillQtyForDiscrepancy(int stationeryId,int damageQty)
        {
            // Last record by department
            RequisitionFullfillment rf = (from r in data.RequisitionFullfillments
                                          where r.stationery == stationeryId
                                          orderby r.id descending
                                          select r).FirstOrDefault();
            rf.fulfill_quantity -= damageQty;

            data.SaveChanges();
        }
    }
}
