using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Net.Mail;

namespace ApplicationLayer.Utils
{
    public class Helper
    {
        public static String getExceptionMessage(Exception objIn) {
            return (Configs.DEBUG_MODE == true) ? objIn.ToString() : objIn.Message;
        }

       public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static DateTime CovertToCustomDateTime(String date, String time, String min, String tt)
        {
            String dateTime = date + " " + time + ":" + min + " " + tt;

            return DateTime.ParseExact(dateTime, "M/d/yyyy H:mm tt", null);
        }

        public static DateTime CovertToCustomDateTime(String date)
        {
            return DateTime.ParseExact(date, "M/d/yyyy", null);
        }

        public static bool sendMail(String to, String from, String subject, String body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(from);
                mailMessage.IsBodyHtml = true;
                //mailMessage.CC.Add("theinhtikeaung35@facebook.com");
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                // SmtpClient smtpClient = new SmtpClient("mail.singnet.com.sg");

                SmtpClient smtpClient = new SmtpClient("lynx.iss.nus.edu.sg");
                smtpClient.Send(mailMessage);
                //Response.Write("E-mail sent!");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
