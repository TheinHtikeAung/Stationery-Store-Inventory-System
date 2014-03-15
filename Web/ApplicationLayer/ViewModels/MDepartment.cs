using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.ViewModels
{
    public class MDepartment
    {
        public int id;
        public string department_name;
        public string representative_name;
        public string name;

        public MDepartment(int id, string department_name, string representative_name, string name)
        {
            this.id = id;
            this.department_name = department_name;
            this.representative_name = representative_name;
            this.name = name;
        }
    }
}
