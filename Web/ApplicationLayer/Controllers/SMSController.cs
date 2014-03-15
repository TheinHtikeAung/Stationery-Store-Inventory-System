using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class SMSController
    {
        public SMSController() {
            
        
        }

        public String sendSMS(String to_phonenumber, String message) {
            //return "";            
            String url = "https://rest.nexmo.com/sms/json";
            String postData = "";

            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);

            ASCIIEncoding encoding = new ASCIIEncoding();
            
            postData += "api_key=" + Configs.SMS_API_KEY;
            postData += "&api_secret=" + Configs.SMS_API_SECRET;
            postData += "&from=" + "Logic+Uni";
            postData += "&to=" + to_phonenumber;
            postData += "&text=" + message;    

            byte[] data = encoding.GetBytes(postData);

            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;

            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
    }
}
