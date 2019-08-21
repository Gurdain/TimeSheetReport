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
    public class RoleRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        //HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A Role method to Insert an object of Role type in the Database.
        /// </summary>
        /// <param name="role">Parameter of Role type</param>
        /// <returns>Unique Role ID assigned while inserting the object in database</returns>
        public int Insert(Role role)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Role_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", role.Name);
                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                role.Id = Convert.ToInt32(cmd.Parameters["@Id"].Value);
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
            return role.Id;
        }
        /// <summary>
        /// A Role method to Read all the entries of Role type in the Database.
        /// </summary>
        /// <returns>List of all the Role in the database</returns>
        public List<Role> Read()
        {
            List<Role> roles = new List<Role>();
            try
            {
                SqlCommand cmd = new SqlCommand("Role_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    roles.Add(new Role
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
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
            return roles;
        }
        /// <summary>
        /// A Role method to Read a specific entry of Role type in the Database. 
        /// </summary>
        /// <param name="id">Role ID assigned when creating the object</param>
        /// <returns>Role type object associated with the provided ID</returns>
        public Role ReadById(int id)
        {
            Role role = new Role();
            try
            {
                SqlCommand cmd = new SqlCommand("Role_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    role.Id = Convert.ToInt32(dr["Id"]);
                    role.Name = Convert.ToString(dr["Name"]);
                    role.createdOn = Convert.ToDateTime(dr["createdOn"]);
                    role.modifiedOn = Convert.ToDateTime(dr["modifiedOn"]);
                    //role.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    role.isDeleted = Convert.ToBoolean(dr["isDeleted"]);
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
            return role;
        }
        /// <summary>
        /// A Role method to Update a specific entry of Role type in the Database.
        /// </summary>
        /// <param name="role">Role type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(Role role)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Role_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", role.Id);
                cmd.Parameters.AddWithValue("@Name", role.Name);
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
        /// A Role method to Delete a specific entry of Role type in the Database.
        /// </summary>
        /// <param name="id">Role ID assigned when creating the object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Role_Delete", constr);
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