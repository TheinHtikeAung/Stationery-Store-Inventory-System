using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.Controllers
{
    public class StatusCheckingController : MainController
    {
        public int actionGetReorderLevelStationeriesCount() { 
            return actionGetReorderLevelStationeryViews().Count();
        }

        public int actionGetApprovedRequsitionsCount() {
            return data.Requisitions.Where(o => (o.status == 1)).ToList().Count();
        }

        public int actionGetZeroCountStationeriesCount() {
            return actionGetZeroCountStationeries().Count();
        }

        public List<Stationery> actionGetZeroCountStationeries() { 
            return data.Stationeries.Where(o => (o.quantity_in_stock - o.pending_quantity_to_distribute <= 0)).ToList();
        }

        public List<StationeryView> actionGetReorderLevelStationeryViews()
        {
            return data.StationeryViews.Where(o => (o.quantity_in_stock - o.pending_quantity_to_distribute <= o.reorder_level)).ToList();
        }

        public int actionGetStationeriesCount() {
            return data.Stationeries.Count();
        }

        public int actionGetDepartmentCount() {
            return data.Departments.Count();
        }

        public int actionGetEmplyoeeCount() {
            return data.Users.Where(o => (o.user_type == 1 || o.user_type == 2)).Count();
        }

        public int actionGetDisbursementTimesCount() {
            return data.Disbursements.ToList().Count();
        }

        public int actionGetSuppliersCount() {
            return data.Suppliers.ToList().Count();
        }
    }
}
