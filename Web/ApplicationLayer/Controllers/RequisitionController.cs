using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationLayer.Utils;

namespace ApplicationLayer.Controllers
{
    public class RequisitionController : MainController
    {
        public Message actionCreateRequisition(Requisition objIn)
        {
            objIn.datetime_requested = DateTime.Now;
            objIn.status = 0;

            try
            {
                data.Requisitions.AddObject(objIn);

                Stationery stationery = data.Stationeries.Where(o => o.id == objIn.stationery).SingleOrDefault();
                User employee = data.Users.Where(o=> o.id == objIn.user_obj).SingleOrDefault();
                Department dept = data.Departments.Where(o=> o.id == objIn.department).SingleOrDefault();
                User head = data.Users.Where(o=> o.id == dept.department_head).SingleOrDefault();

                String email_title = "Stationery Requesting from " + employee.firstname + " " + employee.lastname;
                String email_body       = "<p>Hello " + head.firstname + " " + head.lastname + ",</p>" +
                                          "<p>The following item is requested from " + employee.firstname + " " + employee.lastname + ".</p>" +
                                          "<p><b>Stationery Name : " + stationery.stationery_name + "<br />" +
                                          "Quantity : " + objIn.quantity + " " + stationery.unit_of_measure+ "</b></p>" +
                                          "<p>Please click the following link to login to your account and approve.<br /> " +
                                          Configs.APP_URL_ROOT+
                                          "<br />Thank you,<br/> Logic University.<p>This is system generated mail. Please do not reply.</p>";
                Helper.sendMail(head.email, "no-reply@logic-university.com", email_title, email_body);

                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionRequisitionStatusChangeToApproved(int requisition_id, int user_id) 
        {
            try
            {
                Requisition requisition = data.Requisitions.Where(o => (o.id == requisition_id)).Single();
                requisition.status = 1;
                requisition.user_approved_by = user_id;
                data.Requisitions.ApplyCurrentValues(requisition);


                Stationery stationery = data.Stationeries.Where(o => o.id == requisition.stationery).SingleOrDefault();
                User employee = data.Users.Where(o => o.id == requisition.user_obj).SingleOrDefault();
                Department dept = data.Departments.Where(o => o.id == requisition.department).SingleOrDefault();
                User user_approve = data.Users.Where(o => o.id == requisition.user_approved_by).SingleOrDefault();


                String email_title = "Your requested stationery is approved.";
                String email_body = "<p>Hello " + employee.firstname + " " + employee.lastname + ",</p>" +
                                          "<p>The following item your requested is approved by " + user_approve.firstname + " " + user_approve.lastname + ".</p>" +
                                          "<p><b>Stationery Name : " + stationery.stationery_name + "<br />" +
                                          "Quantity : " + requisition.quantity + " " + stationery.unit_of_measure + "</b></p>" +
                                          "<p>We will distribute your request shortly.</p>" +
                                          "<br />Thank you,<br/> Logic University.<p>This is system generated mail. Please do not reply.</p>";
                Helper.sendMail(employee.email, "no-reply@logic-university.com", email_title, email_body);

                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionRequisitionStatusChangeToRejected(int requisition_id)
        {
            try
            {
                Requisition requisition = data.Requisitions.Where(o => (o.id == requisition_id)).Single();
                requisition.status = 2;
                data.Requisitions.ApplyCurrentValues(requisition);

                Stationery stationery = data.Stationeries.Where(o => o.id == requisition.stationery).SingleOrDefault();
                User employee = data.Users.Where(o => o.id == requisition.user_obj).SingleOrDefault();
                Department dept = data.Departments.Where(o => o.id == requisition.department).SingleOrDefault();
                User approve_usr = data.Users.Where(o => o.id == requisition.user_approved_by).SingleOrDefault();

                String email_title = "Your requested stationery is rejected.";
                String email_body = "<p>Hello " + approve_usr.firstname + " " + approve_usr.lastname + ",</p>" +
                                          "<p>The following item your requested is rejected by " + approve_usr.firstname + " " + approve_usr.lastname + ".</p>" +
                                          "<p><b>Stationery Name : " + stationery.stationery_name + "<br />" +
                                          "Quantity : " + requisition.quantity + " " + stationery.unit_of_measure + "</b></p>" +
                                          "<p>The reason is - ." + requisition.reject_remark + "</p>" +
                                          "<br />Thank you,<br/> Logic University.<p>This is system generated mail. Please do not reply.</p>";
                Helper.sendMail(employee.email, "no-reply@logic-university.com", email_title, email_body);
                
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionRequisitionStatusChangeToRejectedWithRemark(int requisition_id, String remark)
        {
            try
            {
                Requisition requisition = data.Requisitions.Where(o => (o.id == requisition_id)).Single();
                requisition.status = 2;
                requisition.reject_remark = remark;
                data.Requisitions.ApplyCurrentValues(requisition);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionRequisitionStatusChangeToRejectedWithRemarkAndRejectedBy(int requisition_id, String remark, int rejected_by)
        {
            try
            {
                Requisition requisition = data.Requisitions.Where(o => (o.id == requisition_id)).Single();
                requisition.status = 2;
                requisition.reject_remark = remark;
                requisition.user_approved_by = rejected_by;
                data.Requisitions.ApplyCurrentValues(requisition);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionRequisitionStatusChangeToProcessing(int requisition_id)
        {
            try
            {
                Requisition requisition = data.Requisitions.Where(o => (o.id == requisition_id)).Single();
                requisition.status = 3;
                data.Requisitions.ApplyCurrentValues(requisition);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        public Message actionRequisitionStatusChangeToDistributed(int requisition_id)
        {
            try
            {
                Requisition requisition = data.Requisitions.Where(o => (o.id == requisition_id)).Single();
                requisition.status = 4;
                data.Requisitions.ApplyCurrentValues(requisition);
                return this.getNewDefaultMessageForDBOperations(data.SaveChanges() == 1);
            }
            catch (Exception e)
            {
                return this.getNewDefaultMessageForException(e);
            }
        }

        /******************/
        public List<Requisition> actionGetPendingRequisitionsByUserID(int user_id)
        {
            return data.Requisitions.Where(o => (o.status == 0 && o.user_obj == user_id)).OrderByDescending(o => o.id).ToList();
        }

        public List<Requisition> actionGetApprovedRequisitions()
        {
            return data.Requisitions.Where(o => (o.status == 1)).ToList();
        }

        public List<Requisition> actionGetApprovedRequisitionsByUserID(int user_id)
        {
            return data.Requisitions.Where(o => (o.status == 1 && o.user_obj == user_id)).OrderByDescending(o => o.id).ToList();
        }

        public List<Requisition> actionGetRejectedRequisitionsByUserID(int user_id)
        {
            return data.Requisitions.Where(o => (o.status == 2 && o.user_obj == user_id)).OrderByDescending(o => o.id).ToList();
        }

        public List<Requisition> actionGetProcessingRequisitionsByUserID(int user_id)
        {
            return data.Requisitions.Where(o => (o.status == 3 && o.user_obj == user_id)).OrderByDescending(o => o.id).ToList();
        }

        public List<Requisition> actionGetDistributedRequisitionsByUserID(int user_id)
        {
            return data.Requisitions.Where(o => (o.status == 4 && o.user_obj == user_id)).OrderByDescending(o => o.id).ToList();
        }

        /******************/

        public List<Requisition> actionGetApprovedRequisitionsByDepartmentID(int department_id)
        {
            return (from r in data.Requisitions
                    where r.department == department_id
                    where r.status == 1
                    select r
                   ).ToList();
            //return data.Requisitions.Where(r => ((data.Users.Where(u => (u.department == department_id)).Select(u => u.id).Contains(r.user_obj)) && r.status == 1)).ToList();
        }

        public void actionRequisitionStatusChangeToDistributedByDepartment(int stationery, int department)
        {
            try
            {
                List<Requisition> requisitions=(from r in data.Requisitions
                                                where r.status==3 
                                                where r.stationery==stationery
                                                where r.department==department
                                                select r).ToList();
                foreach (Requisition r in requisitions)
                {
                    r.status = 4;
                    data.SaveChanges();
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
        }


        public List<Requisition> actionGetApprovedRequisitionsByDepartmentAndDateTime(int department_id, DateTime dateTime)
        {
            return (from r in data.Requisitions
                    where r.department == department_id
                    where r.status == 1
                    where r.datetime_requested <= dateTime
                    select r).ToList();
        }

        public List<Requisition> actionGetApprovedRequisitionsByDepartmentAndStationery(int department_id, int stationeryId)
        {
            return (from r in data.Requisitions
                    where r.department == department_id
                    where r.stationery == stationeryId
                    where r.status == 1
                    select r).ToList();
        }

        public List<Requisition> actionGetApprovedRequisitionsByDepartmentAndStationeryAndDateTime(int department_id, int stationeryId, DateTime dateTime)
        {
            return (from r in data.Requisitions
                    where r.department == department_id
                    where r.stationery == stationeryId
                    where r.status == 1
                    where r.datetime_requested <= dateTime
                    select r).ToList();
        }


        public List<RequisitionView> actionGetPendingRequisitionViewsByDepartmentID(int department_id) {
            return data.RequisitionViews.Where(o => (o.department == department_id && o.status == 0)).ToList();
        }

        public List<RequisitionView> actionGetApprovedRequisitionViewsByDepartmentID(int department_id)
        {
            return data.RequisitionViews.Where(o => (o.department == department_id && o.status == 1)).ToList();
        }

        public List<RequisitionView> actionGetRejectedRequisitionViewsByDepartmentID(int department_id)
        {
            return data.RequisitionViews.Where(o => (o.department == department_id && o.status == 2)).ToList();
        }

        public List<RequisitionView> actionGetProcessingRequisitionViewsByDepartmentID(int department_id)
        {
            return data.RequisitionViews.Where(o => (o.department == department_id && o.status == 3)).ToList();
        }

        public List<RequisitionView> actionGetDistributedRequisitionViewsByDepartmentID(int department_id)
        {
            return data.RequisitionViews.Where(o => (o.department == department_id && o.status == 4)).ToList();

        }
    }
}
