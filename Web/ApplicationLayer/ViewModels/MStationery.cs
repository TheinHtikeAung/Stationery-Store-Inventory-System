using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.ViewModels
{
    public class MStationery
    {
        public int id;
        public String stationery_name;

        public MStationery(int id, String stationery_name)
        {
            this.id = id;
            this.stationery_name = stationery_name;
        }
    }
}
