using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utility
{
    public class MailLogRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        //HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A MailLogs method to Insert an object of MailLogs type in the Database.
        /// </summary>
        /// <param name="book">Mail type object</param>
        /// <returns>Unique Mail ID assigned while inserting the object in database</returns>
        public int Insert(MailLog email)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MailLog_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ToAddress", email.ToAddress);
                cmd.Parameters.AddWithValue("@FromAddress", email.FromAddress);
                cmd.Parameters.AddWithValue("@Subject", email.Subject);
                cmd.Parameters.AddWithValue("@Body", email.Body);
                cmd.Parameters.AddWithValue("@EmailStatus", email.EmailStatus);
                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                email.Id = Convert.ToInt32(cmd.Parameters["@Id"].Value);
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
            return email.Id;
        }
        /// <summary>
        /// A MailLogs method to Read all MailLogs type entries in the Database.
        /// </summary>
        /// <returns>List of all the Mails in the database</returns>
        public List<MailLog> Read()
        {
            List<MailLog> mails = new List<MailLog>();
            try
            {
                SqlCommand cmd = new SqlCommand("MailLog_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    mails.Add(new MailLog
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ToAddress = Convert.ToString(dr["ToAddress"]),
                        FromAddress = Convert.ToString(dr["FromAddress"]),
                        Subject = Convert.ToString(dr["Subject"]),
                        Body = Convert.ToString(dr["Body"]),
                        EmailStatus = Convert.ToBoolean(dr["EmailStatus"]),
                        createdOn = Convert.ToDateTime(dr["createdOn"]),
                        modifiedOn = Convert.ToDateTime(dr["modifiedOn"]),
                        //DeletedOn = Convert.ToDateTime(dr["DeletedOn"]),
                        isDeleted = Convert.ToBoolean(dr["isDeleted"])
                    });
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
            return mails;
        }
        /// <summary>
        /// A MailLogs method to Read a specific MailLogs type entry in the Database.
        /// </summary>
        /// <param name="id">Mail ID assigned when creating the object</param>
        /// <returns>MailLogs type object associated with the provided ID</returns>
        public MailLog ReadById(int id)
        {
            MailLog mail = new MailLog();
            try
            {
                SqlCommand cmd = new SqlCommand("MailLog_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    mail.Id = Convert.ToInt32(dr["Id"]);
                    mail.ToAddress = Convert.ToString(dr["ToAddress"]);
                    mail.FromAddress = Convert.ToString(dr["FromAddress"]);
                    mail.Subject = Convert.ToString(dr["Subject"]);
                    mail.Body = Convert.ToString(dr["Body"]);
                    mail.EmailStatus = Convert.ToBoolean(dr["EmailStatus"]);
                    mail.createdOn = Convert.ToDateTime(dr["createdOn"]);
                    mail.modifiedOn = Convert.ToDateTime(dr["modifiedOn"]);
                    //book.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    mail.isDeleted = Convert.ToBoolean(dr["isDeleted"]);
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
            return mail;
        }
        /// <summary>
        /// A MailLogs method to Update a specific MailLogs type entry in the Database.
        /// </summary>
        /// <param name="book">Mail type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(MailLog mail)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MailLog_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", mail.Id);
                cmd.Parameters.AddWithValue("@ToAddress", mail.ToAddress);
                cmd.Parameters.AddWithValue("@FromAddress", mail.FromAddress);
                cmd.Parameters.AddWithValue("@Subject", mail.Subject);
                cmd.Parameters.AddWithValue("@Body", mail.Body);
                cmd.Parameters.AddWithValue("@EmailStatus", mail.EmailStatus);
                constr.Open();
                int r = cmd.ExecuteNonQuery();
                constr.Close();
                if (r == 1)
                {
                    return true;
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
            return false;
        }
        /// <summary>
        /// A MailLogs method to Delete a specific MailLogs type entry in the Database.
        /// </summary>
        /// <param name="id">Mail ID assigned when creating the object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MailLog_Delete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                constr.Open();
                int r = cmd.ExecuteNonQuery();
                constr.Close();
                if (r == 1)
                {
                    return true;
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
            return false;
        }
    }
}
