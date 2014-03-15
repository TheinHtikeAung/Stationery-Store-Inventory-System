using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationLayer.Utils
{
    public class Message
    {
        public bool condition;
        public String message;
        
        public int effected_id;

        public Message() { }

        public Message(bool condition)
        {
            this.condition = condition;
        }

        public Message(bool condition, String message){
            this.condition = condition;
            this.message    = message;
        }

        public void set(bool condition, String message) {
            this.condition = condition;
            this.message = message;
        }

        //---------------------------------------------------------------------

        public static Message getMessageWithNormalSuccessStatus() {
            return new Message(true, "Successful");
        }

        public static Message getMessageWithNormalErrorStatus() {
            return new Message(false, "Error");
        }

        
    }
}
