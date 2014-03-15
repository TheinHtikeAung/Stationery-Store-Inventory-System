using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class AuthenticationController : MainController
    {
        public User actionCheckLoginData(String username, String password)
        {
            try
            {
                String encrypted_password = this.getEncryptedString(password);

                return data.Users.Where(o => (o.username == username && o.password == encrypted_password)).Single();
                
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public String getEncryptedString(String password) {
            //return password;
            return GetMD5HashData(GetSHA1HashData(password));
        }

        private string GetMD5HashData(string data)
        {
            //create new instance of md5
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            //convert the input text to array of bytes
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();

        }

        private string GetSHA1HashData(string data)
        {
            //create new instance of md5
            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }
    }
}
