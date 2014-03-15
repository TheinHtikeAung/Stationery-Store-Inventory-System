using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.ViewModels
{
    public class MRetrievelFormMaster_View
    {
        public int id;
        public string stationery_name;
        public int Needed;
        public int Retrieved;
        
        public MRetrievelFormMaster_View(int id, string stationery_name, int Needed, int Retrieved)
        {
            this.id=id;
            this.stationery_name=stationery_name;
            this.Needed=Needed;
            this.Retrieved=Retrieved;
        }
    }
}
