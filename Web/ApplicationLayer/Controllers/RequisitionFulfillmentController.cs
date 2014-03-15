using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.Controllers
{
    public class RequisitionFulfillmentController : MainController
    {
        public List<RetrievelFormMaster_View> actionGetAllRequisitionFulfillmentsForMaster()
        {
            return data.RetrievelFormMaster_View.ToList();
        }

        public List<RetrievalFormDetails_View> actionGetAllRequisitionFulfillmentsForDetails(int stationeryId)
        {
            return data.RetrievalFormDetails_View.Where(x=>x.stationery==stationeryId).ToList();
        }

    }

    
}
