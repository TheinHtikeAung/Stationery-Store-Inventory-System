using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class DisbursementController : MainController
    {
        int disbursementId=-1;
        int unfulfillQty;
        RequisitionFullfillment rf;
        Disbursement disbursement;
        DisbursementDetail disbursementDetail;

        public void actionUpdateToDisbursement(int req_dept, int stationery)
        {
            FulfillmentController fc = new FulfillmentController();

            rf=fc.actionGetFulfillmentByDepartmentAndStationery(req_dept, stationery);

            // -------------- Disbursement Table Process ----------
            disbursement = actionGetDisbursementByDepartmentAndTodayDate(req_dept);

            if (disbursement == null)
                actionCreateDisbursementTable(req_dept, getDeptRept(req_dept));

            disbursementId=actionGetDisbursementIdByDepartmentAndDate(req_dept, disbursement.distributed_date);
            

            // -------------- Disbursement Detail Table Process ----------
            disbursementDetail = actionGetDisbursementDetailByDepartmentAndStationery(stationery);

            if (disbursementDetail == null)
                actionCreateDisbursementDetailTable();
            else
                actionUpdateDisbursementDetailTable();
            
            // -------------- Update Stationery Process ----------
            new StationeryController().actionUpdateQty(rf.stationery, rf.fulfill_quantity);
                
            // -------------- Update StockTransaction Process ----------
            new StockTransactionController().actionCreateStockTransaction(stationery, new StationeryController().actionGetQtyInStock(stationery),
                "Disbursement", "- "+rf.fulfill_quantity.ToString());

            // -------------- Remaining Requisition Fulfillment Process ----------
            unfulfillQty = rf.requested_quantity - rf.fulfill_quantity;
            if (unfulfillQty == 0)
                fc.actionDeleteFulfillmentByObject(rf);
            else
                fc.actionUpdateFulfillmentByObject(rf, unfulfillQty);

            // -------------- Update Requisition Status Process ----------
            new RequisitionController().actionRequisitionStatusChangeToDistributedByDepartment(stationery, req_dept);
        }

        public void actionCreateDisbursementTable(int req_dept, String deptRept)
        {
            disbursement = new Disbursement();
            disbursement.department = req_dept;
            disbursement.department_rept = deptRept;
            disbursement.distributed_date = DateTime.Now;
            data.Disbursements.AddObject(disbursement);
            data.SaveChanges();
        }

        public Disbursement actionGetDisbursementByDepartmentAndTodayDate(int req_dept)
        {
            return (from d in data.Disbursements
                    where d.department == req_dept
                    where d.distributed_date.Year == DateTime.Now.Year
                    where d.distributed_date.Month == DateTime.Now.Month
                    where d.distributed_date.Day == DateTime.Now.Day
                    select d).FirstOrDefault();
        }

        public int actionGetDisbursementIdByDepartmentAndDate(int req_dept, DateTime date)
        {
            return (int)(from d in data.Disbursements
                    where d.department == req_dept
                    where d.distributed_date.Year == date.Year
                    where d.distributed_date.Month == date.Month
                    where d.distributed_date.Day == date.Day
                    select d.id).FirstOrDefault();
        }

        public DisbursementDetail actionGetDisbursementDetailByDepartmentAndStationery(int stationery)
        {
            return (from dd in data.DisbursementDetails
                    where dd.disbursement == disbursementId
                    where dd.stationery == stationery
                    select dd).FirstOrDefault();
        }

        public void actionCreateDisbursementDetailTable()
        {
            disbursementDetail = new DisbursementDetail();
            disbursementDetail.disbursement = disbursementId;
            disbursementDetail.stationery = rf.stationery;
            disbursementDetail.requested_quantity = rf.requested_quantity;
            disbursementDetail.fulfilled_quantity = rf.fulfill_quantity;
            disbursementDetail.previous_unfulfilled_quantity = rf.unfulfill_quantity;
            data.DisbursementDetails.AddObject(disbursementDetail);
            data.SaveChanges();
        }

        public void actionUpdateDisbursementDetailTable()
        {
            //disbursementDetail.requested_quantity += rf.requested_quantity;
            disbursementDetail.fulfilled_quantity += rf.fulfill_quantity;
            //disbursementDetail.previous_unfulfilled_quantity += rf.unfulfill_quantity;

            data.SaveChanges();
        }

        public String getDeptRept(int req_dept)
        {
            return (from d in data.Departments
                    where d.id==req_dept
                    select d.representative_name).SingleOrDefault();
        }
    }
}
