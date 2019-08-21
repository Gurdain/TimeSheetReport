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
    public class TaskRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        //HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A Task method to Insert an object of Task type in the Database.
        /// </summary>
        /// <param name="task">Parameter of Task type</param>
        /// <returns>Unique task ID assigned while inserting the object in database</returns>
        public int Insert(Task task)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Task_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@SubmitBy", task.SubmitBy);
                cmd.Parameters.AddWithValue("@TraineeId", task.TraineeId);
                cmd.Parameters.AddWithValue("@TrainerId", task.TrainerId);
                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                task.Id = Convert.ToInt32(cmd.Parameters["@Id"].Value);
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
            return task.Id;
        }
        /// <summary>
        /// A Task method to Read all the entries of Task type in the Database.
        /// </summary>
        /// <returns>List of all the Tasks in the database</returns>
        public List<Task> Read()
        {
            List<Task> tasks = new List<Task>();
            try
            {
                SqlCommand cmd = new SqlCommand("Task_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    tasks.Add(new Task
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = Convert.ToString(dr["Title"]),
                        Description = Convert.ToString(dr["Description"]),
                        SubmitBy = Convert.ToDateTime(dr["SubmitBy"]),
                        TraineeId = Convert.ToInt32(dr["TraineeId"]),
                        TrainerId = Convert.ToInt32(dr["TrainerId"]),
                        StatusId = Convert.ToInt32(dr["StatusId"]),
                        Extension = Convert.ToBoolean(dr["Extension"]),
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
            return tasks;
        }
        /// <summary>
        /// A Task method to Read a specific entry of Task type in the Database. 
        /// </summary>
        /// <param name="id">Task ID assigned when creating the object</param>
        /// <returns>Task type object associated with the provided ID</returns>
        public Task ReadById(int id)
        {
            Task task = new Task();
            try
            {
                SqlCommand cmd = new SqlCommand("Task_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    task.Id = Convert.ToInt32(dr["Id"]);
                    task.Title = Convert.ToString(dr["Title"]);
                    task.Description = Convert.ToString(dr["Description"]);
                    task.SubmitBy = Convert.ToDateTime(dr["SubmitBy"]);
                    task.TraineeId = Convert.ToInt32(dr["TraineeId"]);
                    task.TrainerId = Convert.ToInt32(dr["TrainerId"]);
                    task.StatusId = Convert.ToInt32(dr["StatusId"]);
                    task.Extension = Convert.ToBoolean(dr["Extension"]);
                    task.createdOn = Convert.ToDateTime(dr["createdOn"]);
                    task.modifiedOn = Convert.ToDateTime(dr["modifiedOn"]);
                    //task.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    task.isDeleted = Convert.ToBoolean(dr["isDeleted"]);
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
            return task;
        }
        /// <summary>
        /// A Task method to Update a specific entry of Task type in the Database.
        /// </summary>
        /// <param name="task">Task type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(Task task)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Task_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", task.Id);
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@SubmitBy", task.SubmitBy);
                cmd.Parameters.AddWithValue("@TraineeId", task.TraineeId);
                cmd.Parameters.AddWithValue("@TrainerId", task.TrainerId);
                cmd.Parameters.AddWithValue("@StatusId", task.StatusId);
                cmd.Parameters.AddWithValue("@Extension", task.Extension);
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
        /// A Task method to Delete a specific entry of Task type in the Database.
        /// </summary>
        /// <param name="id">Task ID assigned when creating the object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Task_Delete", constr);
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
        /// <summary>
        /// A Task method to Undelete a specific Task type entry in the Database.
        /// </summary>
        /// <param name="id">Task ID assigned when creating the object</param>
        /// <returns>True if the Recovery was successful and False if it was not</returns>
        public bool Undelete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Task_Undelete", constr);
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