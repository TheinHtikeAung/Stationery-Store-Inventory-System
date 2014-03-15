using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers 
{
    public class UserDelegateController:MainController
    {
        public List<CustomUserDelegate> getCustomUserDelegateListByLoginUserId(int loginUserId)
        {
            List<CustomUserDelegate> customs = new List<CustomUserDelegate>();
            foreach (UserDelegate u in getUserDelegateList(loginUserId))
            {
                CustomUserDelegate c = new CustomUserDelegate(u.id, getUserName(u.user_to),
                    getUserName(u.user_by), u.date_from, u.date_to, u.department);

                customs.Add(c);
            }

            return customs;
        }

        public CustomUserDelegate getCustomerUserDelegateById(int id)
        {
            UserDelegate u = getUserDelegateById(id);
            
            return new CustomUserDelegate(u.id, getUserName(u.user_to),
                    getUserName(u.user_by), u.date_from, u.date_to, u.department);
        }

        private List<UserDelegate> getUserDelegateList(int loginUserId)
        {
            return (from u in data.UserDelegates where u.user_by==loginUserId select u).ToList();
        }

        private UserDelegate getUserDelegateById(int id)
        {
            return (from u in data.UserDelegates where u.id == id select u).FirstOrDefault();
        }

        private string getUserName(int userId)
        {
            return (from u in data.Users where u.id == userId select u.username).FirstOrDefault();
        }

        public int getUserDepartmentId(int userId)
        {
            return (int)(from u in data.Users where u.id == userId select u.department).FirstOrDefault();
        }

        private int getUserId(string username)
        {
            return (from u in data.Users where u.username == username select u.id).FirstOrDefault();
        }

        public void actionSaveUserDelegate(string user_to, int user_by, string date_from, string date_to)
        {
            UserDelegate u = new UserDelegate();
            u.user_to = getUserId(user_to);
            u.user_by = user_by;
            u.date_from = Helper.CovertToCustomDateTime(date_from);
            u.date_to = Helper.CovertToCustomDateTime(date_to);
            u.department = getUserDepartmentId(user_by);

            data.UserDelegates.AddObject(u);
            data.SaveChanges();
        }

        public void actionUpdateUserDelegate(int id,string user_to, string date_from, string date_to)
        {
            UserDelegate u = getUserDelegateById(id);
            u.user_to = getUserId(user_to);
            u.date_from = Helper.CovertToCustomDateTime(date_from);
            u.date_to = Helper.CovertToCustomDateTime(date_to);

            data.SaveChanges();
        }

        public void actionDeleteUserDelegate(int id)
        {
            UserDelegate u = getUserDelegateById(id);
            data.UserDelegates.DeleteObject(u);
            data.SaveChanges();
        }
    }
}
