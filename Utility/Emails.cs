using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utility
{
    public class Emails
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        //HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A Emails Method to send a mail stored in the database to a specified E-mail Address.
        /// </summary>
        /// <param name="to">The E-mail Address to which the Email is supposed to be send</param>
        /// <param name="from">The E-mail Address from which the Email is supposed to be send</param>
        /// <param name="subject">The Subject of the E-mail</param>
        /// <param name="body">The Body of the E-mail</param>
        /// <returns>True if the E-mail is sent successfully and false if it is not</returns>
        public bool Send_Mail(string to, string from, string subject, string body)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(from);
                mm.To.Add(to);
                mm.Subject = subject;
                mm.IsBodyHtml = true;
                mm.Body = body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.clanstech.com";
                smtp.Port = 25;
                NetworkCredential nc = new NetworkCredential(from, "gurdain_27");
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = nc;
                smtp.Send(mm);
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
                return false;
            }
            return true;
        }
    }
}
