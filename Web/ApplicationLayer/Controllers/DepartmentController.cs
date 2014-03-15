using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class DepartmentController : MainController
    {
        public List<Department> actionGetDepartments()
        {
            return data.Departments.ToList();
        }

        public List<GetDepartmentInfo> actionGetDepartmentInfo() 
        {
            return data.GetDepartmentInfoes.ToList();
        }
        
        public Department actionGetDepartmentByID(int id)
        {
            return data.Departments.Where(o => o.id == id).SingleOrDefault();
        }

        public Message actionCreateDepartment(Department objIn)
        {
            try
            {
                data.Departments.AddObject(objIn);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionUpdateDepartment(Department objIn)
        {
            try
            {
                Department department = data.Departments.Where(o => o.id == objIn.id).Single();

                User cuser = data.Users.Where(o => o.id == department.department_head).SingleOrDefault();
                cuser.user_type = 1;
                //objIn.EntityKey = data.Departments.Where(o => o.id == objIn.id).Single().EntityKey;
                department.code = objIn.code;
                department.department_name = objIn.department_name;
                department.department_head = objIn.department_head;
                department.collection_point = objIn.collection_point;
                department.contact_name = objIn.contact_name;
                department.phone = objIn.phone;
                department.representative_name = objIn.representative_name;

                data.Departments.ApplyCurrentValues(department);

                User user = data.Users.Where(o => o.id == department.department_head).SingleOrDefault();
                user.user_type = 3;

                

                data.SaveChanges();
                return this.getNewDefaultMessageForDBOperations(true);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionDeleteDepartmentByID(int idIn)
        {
            try
            {
                //data.Departments.DeleteObject(data.Departments.Where(o => o.id == idIn).Single());
                Department department = (from d in data.Departments where d.id == idIn select d).FirstOrDefault();
                data.Departments.DeleteObject(department);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionChangeCollectionPointAndReptName(int idIn, int point, string reptName)
        {
            try
            {
                Department department = new Department();

                department = data.Departments.Where(o => o.id == idIn).Single();
                department.collection_point = point;
                department.representative_name = reptName;
                data.Departments.ApplyCurrentValues(department);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);

            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }

        }

        public List<User> actionGetUsersOfADepartment(int department_id) {
            return data.Users.Where(o => (o.department == department_id && (o.user_type == 1 || o.user_type == 2 || o.user_type == 3))).ToList();
        }

        public List<CollectionPoint> actionGetAllCollectionPoint()
        {
           var collectionPoint =(from clist in data.CollectionPoints select clist);

           return collectionPoint.ToList<CollectionPoint>();
              
        }

        public List<GetDepartmentForFulfillment> actionGetDepartmentForFulfillment()
        {
            return (from d in data.GetDepartmentForFulfillments select d).ToList();
        }

        public GetDepartmentForFulfillment actionGetDepartmentForFulfillment(int id)
        {
            return (from d in data.GetDepartmentForFulfillments where d.id==id select d).FirstOrDefault();
        } 
   
        //public  IQueryable actionGetDepartmentsWithUserName() 
        //{
        //  var departments=(from d in data.Departments
        //                      join u in data.Users on d.id equals u.department 
       
        //                       select new 
        //    {
        
        //        d.id,
        //        d.code,
        //        d.department_name,
        //        d.contact_name,
        //        d.phone,
        //        u.username,
            
        //    }
        //    );
        //}
    }
}
