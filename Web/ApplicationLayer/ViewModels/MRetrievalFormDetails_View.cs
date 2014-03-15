using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.ViewModels
{
    public class MRetrievalFormDetails_View
    {
        public int id;
        public string department_name;
        public int Needed;
        public int Actual;
        public int stationery;

        public MRetrievalFormDetails_View(int id, string department_name, int Needed, int Actual, int stationery)
        {
            this.id = id;
            this.department_name = department_name;
            this.Needed = Needed;
            this.Actual = Actual;
            this.stationery = stationery;
        }
    }
}
