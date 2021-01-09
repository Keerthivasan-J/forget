using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;
using ForgetPassword.Models;
namespace ForgetPassword.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ForgetPasswordController : ApiController
    {        
            EXAMSYSTEMEntities entities = new EXAMSYSTEMEntities();
            [HttpGet]
            public HttpResponseMessage forgetPassword_Send_Mail(string email)
            {
                STUDENTDETAIL student = entities.STUDENTDETAILS.Where(t => t.EMAIL == email).FirstOrDefault();
                if (student == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid ID");
                }
                else
                {
                    var token = Guid.NewGuid().ToString();
                    MailAddress to = new MailAddress(student.EMAIL);
                    MailAddress from = new MailAddress("keerthivasan7200@gmail.com");

                    MailMessage message = new MailMessage(from, to);
                    message.Subject = "Testing Mail";
                    message.IsBodyHtml = true;
                    //string url = "http:/localhost:4200/ResetPassword?id=" + student.STUID + "&token=" + token;
                    message.Body = "Reseting <a href='http:/localhost:4200/ResetPassword?id=" + student.STUID + "&token=" + token + "' >Click!</a>";

                    SmtpClient client = new SmtpClient("smtp.elasticemail.com", 2525)
                    {
                        Credentials = new NetworkCredential("keerthivasan0044@gmail.com", "B73FB2954586AA60B9D3582D18E4829F39D5"),
                        EnableSsl = true
                    };

                    try
                    {
                        client.Send(message);

                    }
                    catch (SmtpException ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
                    }
                    List<string> list = new List<string>();
                    list.Add(Convert.ToString(student.STUID));
                    list.Add(token);

                    return Request.CreateResponse<List<string>>(HttpStatusCode.OK, list);
                }
            }

            [HttpPut]
            public HttpResponseMessage savePassword(NewPassword n)
            {
                STUDENTDETAIL s = entities.STUDENTDETAILS.Where(t => t.STUID == n.id).FirstOrDefault();
                if (s == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data cannot be found");
                }
                else
                {
                    s.PASSWORD = n.newPassword;
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "1");
                }

            }
        
    }
}
