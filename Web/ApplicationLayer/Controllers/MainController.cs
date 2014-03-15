using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class MainController
    {
        
        protected DataContext data;
        public Message message;

        public MainController() {
            data = new DataContext();
            message = new Message();
        }

        protected Message getNewDefaultMessageForDBOperations(bool condition)
        {
            if (condition)
                return new Message(true, "Success");
            else
                return new Message(false, "Error");    
        }

        protected Message getNewDefaultMessageForException(Exception exception) 
        {
            return new Message(false, (Configs.DEBUG_MODE == true) ? exception.ToString() : exception.Message);  
        }

    }
}
