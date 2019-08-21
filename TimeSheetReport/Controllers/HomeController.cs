using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TimeSheetReport.Models;
using Utility;

namespace TimeSheetReport.Controllers
{
    public class HomeController : Controller
    {
        UsersRepository usersrepo = new UsersRepository();
        TaskRepository taskrepo = new TaskRepository();
        AttachmentRepository attachrepo = new AttachmentRepository();
        StatusRepository statusrepo = new StatusRepository();
        RoleRepository rolerepo = new RoleRepository();
        EmailQueueRepository emailrepo = new EmailQueueRepository();
        MailLogRepository mailrepo = new MailLogRepository();
        ExceptionRepository exceptionrepo = new ExceptionRepository();

        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // Method to Create/Insert a User in the database.
        public ActionResult Users_Insert()
        {
            List<Role> data = rolerepo.Read();
            ViewBag.Data = data;
            return View("Users_Insert");
        }
        // Method to Retreive all the Users in the database.
        public ActionResult Users_Read()
        {
            List<Users> data = usersrepo.Read();
            ViewBag.Data = rolerepo.Read();
            return View("Users_Read", data);
        }
        // Method to Retreive a specific User.
        public ActionResult Users_ReadById(int id)
        {
            Users data = usersrepo.ReadById(id);
            return View("Users_ReadById", data);
        }
        // Method to Update a User.
        public ActionResult Users_Update(int id)
        {
            Users data = usersrepo.ReadById(id);
            ViewBag.Data = rolerepo.Read();
            return View("Users_Update", data);
        }
        // Method to Login a User.
        public ActionResult Users_Login()
        {
            return View("Users_Login");
        }
        // Method to Logout a User.
        public ActionResult Users_Logout()
        {
            Session.Clear();
            return RedirectToAction("Users_Login");
        }
        // Method to return Manager's Dashboard.
        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }
        // Method to return Trainee's and Trainer's Dashboard.
        public ActionResult Task_Dashboard()
        {
            return View("Task_Dashboard");
        }
        // Method to Delete/Remove a user from the database.
        public ActionResult Users_Delete(int id)
        {
            Users data = usersrepo.ReadById(id);
            return View("Users_Delete", data);
        }
        // Method to Insert/Create a User in the database.
        [HttpPost]
        public ActionResult Users_Insert(Users user)
        {
            List<Users> users = usersrepo.Read();
            List<Role> data = rolerepo.Read();
            for (int i = 0; i < users.Count; i++)
            {
                if (!users[i].isDeleted)
                {
                    if (user.Email == users[i].Email)
                    {
                        ViewBag.Message = "Another account with the same E-mail already exists.";
                        ViewBag.Data = data;
                        return View("Users_Insert");
                    }
                    else if (user.Phone == users[i].Phone)
                    {
                        ViewBag.Message = "Another account with the same Phone Number already exists.";
                        ViewBag.Data = data;
                        return View("Users_Insert");
                    }
                }
            }
            int success = usersrepo.Insert(user);
            if (success >= 1)
            {
                ViewBag.Message = "Successfully Registered";
                return View("Dashboard");
            }
            ViewBag.Message = "An error occurred while making your account";
            ViewBag.Data = data;
            return View("Users_Insert");
        }
        // Method to Update/Edit a User in the database.
        [HttpPost]
        public ActionResult Users_Update(Users user)
        {
            bool success = usersrepo.Update(user);
            List<Users> data = usersrepo.Read();
            ViewBag.Data = rolerepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Users_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Users_Read", data);
        }
        // Method to Delete/Remove a User from the database.
        [HttpPost]
        public ActionResult Users_Delete(Users user)
        {
            bool success = usersrepo.Delete(user.Id);
            List<Users> data = usersrepo.Read();
            ViewBag.Data = rolerepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Users_Read", data);
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Users_Read", data);
        }
        // Method to Login a User or Admin.
        [HttpPost]
        public ActionResult Users_Login(string email, string password)
        {
            Users user = usersrepo.Login(email, password);
            Session["roleId"] = user.RoleId;
            Session["userName"] = user.Name;
            Session["userId"] = user.Id;
            if (user.RoleId == 1)
            {
                return View("Dashboard");
            }
            else if (user.RoleId == 2 || user.RoleId == 3)
            {
                TaskViewModel data = new TaskViewModel();
                data.users = usersrepo.Read();
                data.statuses = statusrepo.Read();
                return View("Task_Dashboard", data);
            }
            ViewBag.Message = "Incorrect E-mail or Password";
            return View("Users_Login");
        }
        // Method to Create/Insert a Task in the database.
        public ActionResult Task_Insert()
        {
            ViewBag.Data = usersrepo.Read().Where(t => t.RoleId == 3);
            return View("Task_Insert");
        }
        // Method to Retreive all the Tasks in the database.
        public ActionResult Task_Read()
        {
            TaskViewModel data = new TaskViewModel();
            data.users = usersrepo.Read();
            data.statuses = statusrepo.Read();
            data.tasks = taskrepo.Read();
            data.attachments = attachrepo.Read();
            return View("Task_Read", data);
        }
        // Method to Retreive all the Tasks in the database.
        public ActionResult Task_ReadPending()
        {
            TaskViewModel data = new TaskViewModel();
            data.users = usersrepo.Read();
            data.statuses = statusrepo.Read();
            data.tasks = taskrepo.Read();
            data.attachments = attachrepo.Read();
            return View("Task_ReadPending", data);
        }
        // Method to Retreive a specific Task.
        public ActionResult Task_ReadById(int id)
        {
            Task data = taskrepo.ReadById(id);
            return View("Task_ReadById", data);
        }
        // Method to Update a Task.
        public ActionResult Task_Update(int id)
        {
            TaskViewModel data = new TaskViewModel();
            data.tasks = taskrepo.Read();
            data.task = taskrepo.ReadById(id);
            data.users = new List<Users>();
            Dictionary<int, Users> users = new Dictionary<int, Users>();
            //users.Add(usersrepo.ReadById(data.task.TraineeId));
            for (int i = 0; i < data.tasks.Count; i++)
            {
                if (data.tasks[i].TrainerId == data.task.TrainerId && !users.ContainsKey(data.tasks[i].TraineeId))
                {
                    users.Add(data.tasks[i].TraineeId, usersrepo.ReadById(data.tasks[i].TraineeId));
                }
            }
            data.users = users.Select(dic => dic.Value).ToList();
            data.statuses = statusrepo.Read();
            return View("Task_Update", data);
        }
        // Method to Delete/Remove a Task from the database.
        public ActionResult Task_Delete(int id)
        {
            Task data = taskrepo.ReadById(id);
            return View("Task_Delete", data);
        }
        // Method to Mark a Task in the database as Done.
        public ActionResult Task_Done(int id)
        {
            Task task = taskrepo.ReadById(id);
            task.StatusId = 2;
            bool success = taskrepo.Update(task);
            TaskViewModel data = new TaskViewModel();
            data.users = usersrepo.Read();
            data.statuses = statusrepo.Read();
            data.tasks = taskrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Task_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Task_Read", data);
        }
        // Method to Mark a Task in the database as Failed.
        public ActionResult Task_Failed(int id)
        {
            Task task = taskrepo.ReadById(id);
            task.StatusId = 4;
            bool success = taskrepo.Update(task);
            TaskViewModel data = new TaskViewModel();
            data.users = usersrepo.Read();
            data.statuses = statusrepo.Read();
            data.tasks = taskrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Task_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Task_Read", data);
        }
        // Method to Mark a Task in the database as Active.
        public ActionResult Task_Active(int id)
        {
            Task task = taskrepo.ReadById(id);
            task.StatusId = 1;
            bool success = taskrepo.Update(task);
            TaskViewModel data = new TaskViewModel();
            data.users = usersrepo.Read();
            data.statuses = statusrepo.Read();
            data.tasks = taskrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Task_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Task_Read", data);
        }
        // Method to Insert/Create a Task in the database.
        [HttpPost]
        public ActionResult Task_Insert(Task task)
        {
            task.TrainerId = Convert.ToInt32(Session["userId"]);
            int success = taskrepo.Insert(task);
            if (success >= 1)
            {
                ViewBag.Message = "Entry Created Successfully";
                return View("Task_Dashboard");
            }
            ViewBag.Message = "An error occurred while making the Entry";
            return View("Task_Dashboard");
        }
        // Method to Update/Edit a Task in the database.
        [HttpPost]
        public ActionResult Task_Update(Task task)
        {
            TaskViewModel data = new TaskViewModel();
            data.users = usersrepo.Read();
            data.statuses = statusrepo.Read();
            data.tasks = taskrepo.Read();
            bool success;
            if (task.StatusId == 2)
            {
                task.isDeleted = true;
                success = taskrepo.Update(task);
            }
            else
            {
                success = taskrepo.Update(task);
            }
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Task_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Task_Read", data);
        }
        // Method to Extend the Time for a Task to be completed.
        public ActionResult Task_Extend(int id)
        {
            TaskViewModel data = new TaskViewModel();
            Task task = taskrepo.ReadById(id);
            if (!task.Extension)
            {
                taskrepo.Undelete(id);
                task.SubmitBy = task.SubmitBy.AddDays(1);
                task.Extension = true;
                task.StatusId = 1;
                taskrepo.Update(task);
                data.users = usersrepo.Read();
                data.statuses = statusrepo.Read();
                data.tasks = taskrepo.Read();
                data.attachments = attachrepo.Read();
                ViewBag.Message = "Time Extented";
                return View("Task_Read", data);
            }
            else if (task.Extension)
            {
                data.users = usersrepo.Read();
                data.statuses = statusrepo.Read();
                data.tasks = taskrepo.Read();
                data.attachments = attachrepo.Read();
                ViewBag.Message = "Time already Extended";
                return View("Task_Read", data);
            }
            data.users = usersrepo.Read();
            data.statuses = statusrepo.Read();
            data.tasks = taskrepo.Read();
            data.attachments = attachrepo.Read();
            ViewBag.Message = "Encountered an Error while extending time";
            return View("Task_Read", data);
        }
        // Method to Delete/Remove a Task from the database.
        [HttpPost]
        public ActionResult Task_Delete(Task task)
        {
            TaskViewModel data = new TaskViewModel();
            bool success = usersrepo.Delete(task.Id);
            data.users = usersrepo.Read();
            data.statuses = statusrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Task_Read", data);
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Task_Read", data);
        }
        // Method to Create/Insert a Attachment in the database.
        public ActionResult Attachment_Insert()
        {
            ViewBag.Data = taskrepo.Read();
            return View("Attachment_Insert");
        }
        // Method to Retreive all the Attachments in the database.
        public ActionResult Attachment_Read()
        {
            List<Attachment> data = attachrepo.Read();
            ViewBag.Data = taskrepo.Read();
            return View("Attachment_Read", data);
        }
        // Method to Retreive a specific Attachment.
        public ActionResult Attachment_ReadById(int id)
        {
            Attachment data = attachrepo.ReadById(id);
            return View("Attachment_ReadById", data);
        }
        // Method to Update a Attachment.
        public ActionResult Attachment_Update(int id)
        {
            Attachment data = attachrepo.ReadById(id);
            ViewBag.Data = taskrepo.Read();
            return View("Attachment_Update", data);
        }
        // Method to Delete/Remove a Attachment from the database.
        public ActionResult Attachment_Delete(int id)
        {
            Attachment data = attachrepo.ReadById(id);
            return View("Attachment_Delete", data);
        }
        // Method to Insert/Create a Attachment in the database.
        [HttpPost]
        public ActionResult Attachment_Insert(HttpPostedFileBase file, int id)
        {
            if (file != null && file.ContentLength > 0)
            {
                Attachment attachment = new Attachment();
                attachment.Title = Path.GetFileName(file.FileName);
                attachment.Type = Path.GetExtension(file.FileName);
                attachment.Path = Path.Combine(Server.MapPath("~/Attachments/"), attachment.Title);
                attachment.UserId = Convert.ToInt32(Session["userId"]);
                attachment.TaskId = id;
                file.SaveAs(attachment.Path);
                attachrepo.Insert(attachment);
                bool success = Trainer_Notification(id, true);
                if (success)
                {
                    Task task = taskrepo.ReadById(id);
                    task.StatusId = 3;
                    taskrepo.Update(task);
                    ViewBag.Message = "Task sent for review. Check your Dashboard for any updates";
                    return View("Task_Dashboard");
                }
            }
            ViewBag.Message = "Encountered an Error while uploading";
            return View("Task_Dashboard");
        }
        // Method to Update/Edit a Attachment in the database.
        [HttpPost]
        public ActionResult Attachment_Update(Attachment attachment)
        {
            bool success = attachrepo.Update(attachment);
            List<Attachment> data = attachrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Attachment_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Attachment_Read", data);
        }
        // Method to Delete/Remove a Attachment from the database.
        [HttpPost]
        public ActionResult Attachment_Delete(Attachment attachment)
        {
            bool success = attachrepo.Delete(attachment.Id);
            List<Attachment> data = attachrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Attachment_Read", data);
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Attachment_Read", data);
        }
        // Method to Create/Insert a Status in the database.
        public ActionResult Status_Insert()
        {
            return View("Status_Insert");
        }
        // Method to Retreive all the Statuses in the database.
        public ActionResult Status_Read()
        {
            List<Status> data = statusrepo.Read();
            return View("Attachment_Read", data);
        }
        // Method to Retreive a specific Status.
        public ActionResult Status_ReadById(int id)
        {
            Status data = statusrepo.ReadById(id);
            return View("Status_ReadById", data);
        }
        // Method to Update a Status.
        public ActionResult Status_Update(int id)
        {
            Status data = statusrepo.ReadById(id);
            return View("Status_Update", data);
        }
        // Method to Delete/Remove a Status from the database.
        public ActionResult Status_Delete(int id)
        {
            Status data = statusrepo.ReadById(id);
            return View("Status_Delete", data);
        }
        // Method to Insert/Create a Status in the database.
        [HttpPost]
        public ActionResult Status_Insert(Status status)
        {
            int success = statusrepo.Insert(status);
            if (success >= 1)
            {
                ViewBag.Message = "Entry Created Successfully";
                return View("Dashboard");
            }
            ViewBag.Message = "An error occurred while making the Entry";
            return View("Dashboard");
        }
        // Method to Update/Edit a Status in the database.
        [HttpPost]
        public ActionResult Status_Update(Status status)
        {
            bool success = statusrepo.Update(status);
            List<Status> data = statusrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Status_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Status_Read", data);
        }
        // Method to Delete/Remove a Status from the database.
        [HttpPost]
        public ActionResult Status_Delete(Status status)
        {
            bool success = statusrepo.Delete(status.Id);
            List<Status> data = statusrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Status_Read", data);
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Status_Read", data);
        }
        // Method to Create/Insert a Role in the database.
        public ActionResult Role_Insert()
        {
            return View("Role_Insert");
        }
        // Method to Retreive all the Roles in the database.
        public ActionResult Role_Read()
        {
            List<Role> data = rolerepo.Read();
            return View("Role_Read", data);
        }
        // Method to Retreive a specific Role.
        public ActionResult Role_ReadById(int id)
        {
            Role data = rolerepo.ReadById(id);
            return View("Role_ReadById", data);
        }
        // Method to Update a Role.
        public ActionResult Role_Update(int id)
        {
            Role data = rolerepo.ReadById(id);
            return View("Role_Update", data);
        }
        // Method to Delete/Remove a Role from the database.
        public ActionResult Role_Delete(int id)
        {
            Role data = rolerepo.ReadById(id);
            return View("Role_Delete", data);
        }
        // Method to Insert/Create a Role in the database.
        [HttpPost]
        public ActionResult Role_Insert(Role role)
        {
            int success = rolerepo.Insert(role);
            if (success >= 1)
            {
                ViewBag.Message = "Entry Created Successfully";
                return View("Dashboard");
            }
            ViewBag.Message = "An error occurred while making the Entry";
            return View("Dashboard");
        }
        // Method to Update/Edit a Role in the database.
        [HttpPost]
        public ActionResult Role_Update(Role role)
        {
            bool success = rolerepo.Update(role);
            List<Role> data = rolerepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Role_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Role_Read", data);
        }
        // Method to Delete/Remove a Role from the database.
        [HttpPost]
        public ActionResult Role_Delete(Role role)
        {
            bool success = rolerepo.Delete(role.Id);
            List<Role> data = rolerepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Role_Read", data);
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Role_Read", data);
        }
        // Method to notify the Trainer of any Task completion.
        public bool Trainer_Notification(int id, bool done)
        {
            try
            {
                Task data = taskrepo.ReadById(id);
                Users trainee = usersrepo.ReadById(data.TraineeId);
                Users trainer = usersrepo.ReadById(data.TrainerId);
                EmailQueue email = new EmailQueue();
                if (done)
                {
                    email.ToAddress = trainer.Email;
                    email.FromAddress = "gurdain.singh@clanstech.com";
                    email.Subject = trainee.Name + " Task Update";
                    email.Body = string.Format("Dear "
                            + trainer.Name
                            + ",<BR><BR>         The Task that you assigned to " + trainee.Name +
                            " has been completed. Kindly check whether it is done properly or not.<BR><BR>Sincerely,<BR>Clanstech Team.");
                    int r = emailrepo.Insert(email);
                    if (r >= 1)
                    {
                        return true;
                    }
                }
                else
                {
                    email.ToAddress = trainer.Email;
                    email.FromAddress = "gurdain.singh@clanstech.com";
                    email.Subject = trainee.Name + " Task Update";
                    email.Body = string.Format("Dear "
                            + trainer.Name
                            + ",<BR><BR>         The Task that you assigned to " + trainee.Name +
                            " is not completed on time. Kindly take appropriate action.<BR><BR>Sincerely,<BR>Clanstech Team.");
                    emailrepo.Insert(email);
                }
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    Number = ex.HResult.ToString(),
                    Message = ex.Message,
                    Method = method.Name,
                    Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri
                };
                int r = exceptionrepo.Exception_Create(exception);
                if (r == 0)
                {
                    exceptionrepo.Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return false;
        }
        // Method to check all the Tasks for their submit date.
        public static void CheckTask()
        {

            SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

            TaskRepository taskrepo = new TaskRepository();
            ExceptionRepository exceptionrepo = new ExceptionRepository();
            HomeController hc = new HomeController();

            try
            {
                List<Task> tasks = taskrepo.Read();
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (!tasks[i].isDeleted)
                    {
                        int done = DateTime.Compare(tasks[i].SubmitBy, DateTime.Now);
                        if (done < 0)
                        {
                            hc.Trainer_Notification(tasks[i].Id, false);
                            tasks[i].StatusId = 4;
                            tasks[i].isDeleted = true;
                            taskrepo.Update(tasks[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    Number = ex.HResult.ToString(),
                    Message = ex.Message,
                    Method = method.Name,
                    Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri
                };
                int r = exceptionrepo.Exception_Create(exception);
                if (r == 0)
                {
                    exceptionrepo.Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
        }
        // Method to Send a E-mail from the queue of unsend E-mails in the database.
        public static void SendEmailFromQueue()
        {
            SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

            EmailQueueRepository emailrepo = new EmailQueueRepository();
            MailLogRepository mailrepo = new MailLogRepository();
            ExceptionRepository exceptionrepo = new ExceptionRepository();

            try
            {
                Emails email = new Emails();
                List<EmailQueue> emails = emailrepo.Read();
                for (int i = 0; i < emails.Count; i++)
                {
                    if (!emails[i].isDeleted && emails[i].Tries < 6)
                    {
                        emailrepo.Delete(emails[i].Id);
                        while (emails[i].Tries < 6)
                        {
                            if (email.Send_Mail(emails[i].ToAddress, emails[i].FromAddress, emails[i].Subject, emails[i].Body))
                            {
                                MailLog mail = new MailLog()
                                {
                                    ToAddress = emails[i].ToAddress,
                                    FromAddress = emails[i].FromAddress,
                                    Subject = emails[i].Subject,
                                    Body = emails[i].Body,
                                    EmailStatus = true
                                };
                                mailrepo.Insert(mail);
                                break;
                            }
                            emails[i].Tries++;
                            emailrepo.Undelete(emails[i].Id);
                        }
                        emailrepo.Update(emails[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    Number = ex.HResult.ToString(),
                    Message = ex.Message,
                    Method = method.Name,
                    Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri
                };
                int r = exceptionrepo.Exception_Create(exception);
                if (r == 0)
                {
                    exceptionrepo.Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
        }

    }
}