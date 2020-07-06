using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Controllers.Resources;
using StudentProject.Models;

using System.Net;
using System.Net.Mail;
using System.Text;


namespace StudentProject.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly StudentContext con;
        private readonly IMapper mapper;
        public LoginController(IMapper map)
        {
            StudentContext context = new StudentContext();
            this.con = context;

           
            this.mapper = map;
        }


        //[HttpGet]
        //public async Task<IEnumerable<CourseSubject>> GetCourses()
        //{

        //    return await con.CourseSubject.ToListAsync();
        //}


        [HttpGet("{username}")]
        public async Task<bool> CheckUsername(string username)
        {
            var db = new StudentContext();
            var user = await db.LoginInfo.Where(log => log.UserName == username).ToListAsync();
            if (user.Count == 1)
                return true;
            else return false;
        }

        [HttpPost]
        public async Task<IEnumerable<LoginIdTypeResource>> CheckUser(UsernamePasswordResource ob)
        {
            var db = new StudentContext();
            var user = await db.LoginInfo.Where(log =>log.UserName == ob.username 
                                                    && log.UserPassword == ob.password)
                                                        .ToListAsync();
            if (user.Count == 1)
            {
                return mapper.Map<List<LoginInfo>, List<LoginIdTypeResource>>(user);
            }
            else return null;
        }


        //=============================Forgot Password======================
        [HttpPost("forgot")]
        public int email(UsernamePasswordResource u)
        {
            string toEmail =null;
            var user = con.LoginInfo.Where(li => li.UserName == u.username).FirstOrDefault();

            if (user != null)
            {
                if (user.UserType == "S")
                    toEmail = con.Students.Where(s => s.StudentId == user.Id).FirstOrDefault().EmailId;
                else
                    toEmail = con.Teachers.Where(s => s.TeacherId == user.Id).FirstOrDefault().EmailId;
            }

            if (toEmail == null)
            {
                return 0;
            }


            string sendermail = "huzefagaliakotwala@gmail.com";
            string senderpswd = "9898498885";

            SmtpClient cli = new SmtpClient("smtp.gmail.com", 587);

            cli.EnableSsl = true;
            cli.Timeout = 10000;
            cli.DeliveryMethod = SmtpDeliveryMethod.Network;
            cli.UseDefaultCredentials = false;
            cli.Credentials = new NetworkCredential(sendermail, senderpswd);

            Random r = new Random();
            int otp = r.Next(000111, 999999);
            string body = "<h1>Dear,</h1><br/> Your OTP generated at " +
                DateTime.Now.ToString() + " for Password change is: " + otp;

            MailMessage mail = new MailMessage(sendermail, toEmail, "OTP for Password Change", body);
            mail.IsBodyHtml = true;
            mail.BodyEncoding = UTF8Encoding.UTF8;
            cli.Send(mail);

            return otp;
        }
    }
}