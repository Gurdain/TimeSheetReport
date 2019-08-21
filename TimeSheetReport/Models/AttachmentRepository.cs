using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using Utility;

namespace TimeSheetReport.Models
{
    public class AttachmentRepository
    {
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        //HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A EmailQueue method to Insert an object of EmailQueue type in the Database.
        /// </summary>
        /// <param name="book">Email type object</param>
        /// <returns>Unique Email ID assigned while inserting the object in database</returns>
        public int Insert(Attachment attachment)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Attachment_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", attachment.Title);
                cmd.Parameters.AddWithValue("@Path", attachment.Path);
                cmd.Parameters.AddWithValue("@Type", attachment.Type);
                cmd.Parameters.AddWithValue("@TaskId", attachment.TaskId);
                cmd.Parameters.AddWithValue("@UserId", attachment.UserId);
                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                attachment.Id = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                constr.Close();
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    Number = ex.HResult.ToString(),
                    Message = ex.Message,
                    Method = method.Name,
                    Url = HttpContext.Current.Request.Url.AbsoluteUri
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
            return attachment.Id;
        }
        /// <summary>
        /// A EmailQueue method to Read all EmailQueue type entries in the Database.
        /// </summary>
        /// <returns>List of all the Emails in the database</returns>
        public List<Attachment> Read()
        {
            List<Attachment> attachments = new List<Attachment>();
            try
            {
                SqlCommand cmd = new SqlCommand("Attachment_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    //if (dr["deletedOn"] != null)
                    //{
                    //    attachments.Add(new Attachment
                    //    {
                    //        AttachmentId = Convert.ToInt32(dr["AttachmentId"]),
                    //        AttachmentName = Convert.ToString(dr["AttachmentName"]),
                    //        Path = Convert.ToString(dr["Path"]),
                    //        Type = Convert.ToString(dr["Type"]),
                    //        FacultyId = Convert.ToInt32(dr["FacultyId"]),
                    //        createdOn = Convert.ToDateTime(dr["createdOn"]),
                    //        modifiedOn = Convert.ToDateTime(dr["modifiedOn"]),
                    //        deletedOn = Convert.ToDateTime(dr["deletedOn"]),
                    //        isDeleted = Convert.ToBoolean(dr["isDeleted"])
                    //    }
                    //    );
                    //}
                    //else
                    //{
                    attachments.Add(new Attachment
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        Path = Convert.ToString(dr["Path"]),
                        Type = Convert.ToString(dr["Type"]),
                        TaskId = Convert.ToInt32(dr["TaskId"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        createdOn = Convert.ToDateTime(dr["createdOn"]),
                        modifiedOn = Convert.ToDateTime(dr["modifiedOn"]),
                        isDeleted = Convert.ToBoolean(dr["isDeleted"]),
                    }
                    );
                    //}
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
                    Url = HttpContext.Current.Request.Url.AbsoluteUri
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
            return attachments;
        }
        /// <summary>
        /// A EmailQueue method to Read a specific EmailQueue type entry in the Database.
        /// </summary>
        /// <param name="id">Email ID assigned when creating the object</param>
        /// <returns>EmailQueue type object associated with the provided ID</returns>
        public Attachment ReadById(int id)
        {
            Attachment attachment = new Attachment();
            try
            {
                SqlCommand cmd = new SqlCommand("Attachment_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    //if(dr["deletedOn"] != null)
                    //{
                    //    attachment.AttachmentId = id;
                    //    attachment.AttachmentName = Convert.ToString(dr["AttachmentName"]);
                    //    attachment.Path = Convert.ToString(dr["Path"]);
                    //    attachment.Type = Convert.ToString(dr["Type"]);
                    //    attachment.FacultyId = Convert.ToInt32(dr["FacultyId"]);
                    //    attachment.createdOn = Convert.ToDateTime(dr["createdOn"]);
                    //    attachment.modifiedOn = Convert.ToDateTime(dr["modifiedOn"]);
                    //    attachment.deletedOn = Convert.ToDateTime(dr["deletedOn"]);
                    //    attachment.isDeleted = Convert.ToBoolean(dr["isDeleted"]);
                    //}
                    //else
                    //{
                    attachment.Id = id;
                    attachment.Title = Convert.ToString(dr["Title"]);
                    attachment.Path = Convert.ToString(dr["Path"]);
                    attachment.Type = Convert.ToString(dr["Type"]);
                    attachment.TaskId = Convert.ToInt32(dr["TaskId"]);
                    attachment.UserId = Convert.ToInt32(dr["UserId"]);
                    attachment.createdOn = Convert.ToDateTime(dr["createdOn"]);
                    attachment.modifiedOn = Convert.ToDateTime(dr["modifiedOn"]);
                    attachment.isDeleted = Convert.ToBoolean(dr["isDeleted"]);
                    //}
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
                    Url = HttpContext.Current.Request.Url.AbsoluteUri
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
            return attachment;
        }
        /// <summary>
        /// A EmailQueue method to Update a specific EmailQueue type entry in the Database.
        /// </summary>
        /// <param name="book">Email type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(Attachment attachment)
        {
            int result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Attachment_Update", constr);
                cmd.Parameters.AddWithValue("@Id", attachment.Id);
                cmd.Parameters.AddWithValue("@Title", attachment.Title);
                cmd.Parameters.AddWithValue("@Path", attachment.Path);
                cmd.Parameters.AddWithValue("@Type", attachment.Type);
                cmd.Parameters.AddWithValue("@TaskId", attachment.TaskId);
                cmd.Parameters.AddWithValue("@UserId", attachment.UserId);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                result = cmd.ExecuteNonQuery();
                constr.Close();
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    Number = ex.HResult.ToString(),
                    Message = ex.Message,
                    Method = method.Name,
                    Url = HttpContext.Current.Request.Url.AbsoluteUri
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
            if (result >= 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// A EmailQueue method to Delete a specific EmailQueue type entry in the Database.
        /// </summary>
        /// <param name="id">Email ID assigned when creating the object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            int result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Attachment_Delete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                constr.Open();
                result = cmd.ExecuteNonQuery();
                constr.Close();
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    Number = ex.HResult.ToString(),
                    Message = ex.Message,
                    Method = method.Name,
                    Url = HttpContext.Current.Request.Url.AbsoluteUri
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
            if (result >= 1)
            {
                return true;
            }
            return false;
        }
    }
}