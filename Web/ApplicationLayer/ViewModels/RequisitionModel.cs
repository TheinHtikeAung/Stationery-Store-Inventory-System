using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.ViewModels
{
    public class RequisitionModel
    {
        public int id;
        public int stationery;
        public int quantity;
        public int user_obj;
        public int status;
        public string reject_remark;
        public DateTime datetime_requested;
        public int department;
        public int user_approved_by;
        

    }
}
