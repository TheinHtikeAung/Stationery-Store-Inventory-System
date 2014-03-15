using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class OAuthController : MainController
    {
        public String actionGetNewTokenByUsernameAndPassword(String username, String password) {

            String encrypted_password = new AuthenticationController().getEncryptedString(password);
            User user = data.Users.Where(o => o.username == username && o.password == encrypted_password).SingleOrDefault();
            if (user == null) {
                return null;
            }
            
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            String token = new string(Enumerable.Repeat(chars, 20)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            
            OAuth oauth = new OAuth();
            oauth.user_obj = user.id;
            oauth.access_token = token;

            data.OAuths.AddObject(oauth);
            if (data.SaveChanges() == 1) {
                return token;
            }
            else {
                return null;
            }
        }

        public User actionGetUserByToken(String token) {
            OAuth oauth = data.OAuths.Where(a => a.access_token == token).SingleOrDefault();
            if (oauth == null) {
                return null;
            }
            return data.Users.Where(o => o.id == (oauth.user_obj)).SingleOrDefault();
        }

        public Message actionDeleteTokenByToken(String token) {
            data.OAuths.DeleteObject(data.OAuths.Where(o => o.access_token == token).SingleOrDefault());
            return new Message((data.SaveChanges() == 1 ? true : false));
        }

    }
}
