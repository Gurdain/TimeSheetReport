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
    public class UsersRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        //HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A User method to Insert an object of User type in the Database.
        /// </summary>
        /// <param name="user">Parameter of User type</param>
        /// <returns>Unique user ID assigned while inserting the object in database</returns>
        public int Insert(Users user)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Users_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                cmd.Parameters.AddWithValue("@DOB", user.DOB);
                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                user.Id = Convert.ToInt32(cmd.Parameters["@Id"].Value);
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
            return user.Id;
        }
        /// <summary>
        /// A User method to Read all the entries of User type in the Database.
        /// </summary>
        /// <returns>List of all the Users in the database</returns>
        public List<Users> Read()
        {
            List<Users> users = new List<Users>();
            try
            {
                SqlCommand cmd = new SqlCommand("Users_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    users.Add(new Users
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Email = Convert.ToString(dr["Email"]),
                        Password = Convert.ToString(dr["Password"]),
                        RoleId = Convert.ToInt32(dr["RoleId"]),
                        DOB = Convert.ToDateTime(dr["DOB"]),
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
            return users;
        }
        /// <summary>
        /// A User method to Read a specific entry of User type in the Database. 
        /// </summary>
        /// <param name="id">User ID assigned when creating the object</param>
        /// <returns>User type object associated with the provided ID</returns>
        public Users ReadById(int id)
        {
            Users user = new Users();
            try
            {
                SqlCommand cmd = new SqlCommand("Users_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    user.Id = Convert.ToInt32(dr["Id"]);
                    user.Name = Convert.ToString(dr["Name"]);
                    user.Phone = Convert.ToString(dr["Phone"]);
                    user.Email = Convert.ToString(dr["Email"]);
                    user.Password = Convert.ToString(dr["Password"]);
                    user.RoleId = Convert.ToInt32(dr["RoleId"]);
                    user.DOB = Convert.ToDateTime(dr["DOB"]);
                    user.createdOn = Convert.ToDateTime(dr["createdOn"]);
                    user.modifiedOn = Convert.ToDateTime(dr["modifiedOn"]);
                    //user.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    user.isDeleted = Convert.ToBoolean(dr["isDeleted"]);
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
            return user;
        }
        /// <summary>
        /// A User method to Update a specific entry of User type in the Database.
        /// </summary>
        /// <param name="user">User type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(Users user)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Users_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                cmd.Parameters.AddWithValue("@DOB", user.DOB);
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
        /// A User method to Delete a specific entry of User type in the Database.
        /// </summary>
        /// <param name="id">User ID assigned when creating the object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Users_Delete", constr);
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
        /// A User method to Login a User whose Account is Activated.
        /// </summary>
        /// <param name="email">The E-mail used to Register</param>
        /// <param name="password">The password used in the Registration</param>
        /// <returns>User type object that has the same E-mail and Password</returns>
        public Users Login(string email, string password)
        {
            Users user = new Users();
            try
            {
                SqlCommand cmd = new SqlCommand("Users_Login", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    user.Id = Convert.ToInt32(dr["Id"]);
                    user.Name = Convert.ToString(dr["Name"]);
                    user.Phone = Convert.ToString(dr["Phone"]);
                    user.Email = Convert.ToString(dr["Email"]);
                    user.Password = Convert.ToString(dr["Password"]);
                    user.RoleId = Convert.ToInt32(dr["RoleId"]);
                    user.DOB = Convert.ToDateTime(dr["DOB"]);
                    user.createdOn = Convert.ToDateTime(dr["createdOn"]);
                    user.modifiedOn = Convert.ToDateTime(dr["modifiedOn"]);
                    //user.deletedOn = Convert.ToDateTime(dr["deletedOn"]);
                    user.isDeleted = Convert.ToBoolean(dr["isDeleted"]);
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
            return user;
        }
    }
}