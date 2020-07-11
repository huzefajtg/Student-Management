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
using StudentProject.Extension.Interface;

namespace StudentProject.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IManager ILogin;
        private readonly IMapper mapper;
        public LoginController(IMapper map, IManager ILogin)
        {
            StudentContext context = new StudentContext();

            this.ILogin = ILogin;
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
            //var db = new StudentContext();
            //var user = await db.LoginInfo.Where(log => log.UserName == username).ToListAsync();
            //if (user.Count == 1)
            //    return true;
            //else return false;

            return await ILogin.isValidUser(username);
        }

        //[HttpPost]
        //public async Task<IEnumerable<LoginIdTypeResource>> CheckUser(UsernamePasswordResource ob)
        //{
        //    //var db = new StudentContext();
        //    //var user = await db.LoginInfo.Where(log =>log.UserName == ob.username 
        //    //                                        && log.UserPassword == ob.password)
        //    //                                            .ToListAsync();

        //    var user = await ILogin.GetLoginInfos(ob);
        //    if (user.Count == 1)
        //    {
        //        return mapper.Map<List<LoginInfo>, List<LoginIdTypeResource>>(user);
        //    }
        //    else return null;
        //}


        [HttpPost]
        public async Task<LoginIdTypeResource> CheckUser(UsernamePasswordResource ob)
        {
            //var db = new StudentContext();
            //var user = await db.LoginInfo.Where(log =>log.UserName == ob.username 
            //                                        && log.UserPassword == ob.password)
            //                                            .ToListAsync();

            var user = await ILogin.GetLoginInfos(ob);

            if (user != null)
            {
                return mapper.Map<LoginInfo, LoginIdTypeResource>(user);
            }
            else return null;
        }


        //=============================Forgot Password======================
        [HttpPost("forgot")]
        public async Task<int> email(UsernamePasswordResource username)
        {
            string toEmail = null;
            string name = null;
            var s = new Students();
            var t = new Teachers();
            //var user = con.LoginInfo.Where(li => li.UserName == u.username).FirstOrDefault();
            var user = await ILogin.GetLoginInfos(username.username);
            if (user != null)
            {
                if (user.UserType == "S")
                {
                    s = await ILogin.getStudent(user.Id);
                    name = s.FirstName + " " + s.LastName;
                    toEmail = ILogin.getEmail(user.Id, true);
                }
                else
                {
                    t = await ILogin.getTeacher(user.Id);
                    name = t.FirstName + " " + t.LastName;
                    toEmail = ILogin.getEmail(user.Id, false);
                    //toEmail = con.Teachers.Where(s => s.TeacherId == user.Id).FirstOrDefault().EmailId;
                }
            }

            if (toEmail == null)
            {
                return 0;
            }

            string sendermail = "huzefagaliakotwala@gmail.com";
            string senderpswd = "sfsdf";

            SmtpClient cli = new SmtpClient("smtp.gmail.com", 587);

            cli.EnableSsl = true;
            cli.Timeout = 10000; 
            cli.DeliveryMethod = SmtpDeliveryMethod.Network;
            cli.UseDefaultCredentials = false;
            cli.Credentials = new NetworkCredential(sendermail, senderpswd);

            Random r = new Random();
            int otp = r.Next(000111, 999999);

            string body = "<h2>Dear " + name + ",</h2><br><p>2Your OTP generated at " +
                DateTime.Now.ToString() + " for Password change is: " + otp + "</p><br><h5>Thank You for using " +
                "<strong>Student Management System</strong> By Huzefa Galiakotwala<br>Regards<br>Huzefa Galiakotwala</h5>";

            MailMessage mail = new MailMessage(sendermail, toEmail, "OTP for Password Change", body);
            mail.IsBodyHtml = true;
            mail.BodyEncoding = UTF8Encoding.UTF8;
            cli.Send(mail);

            return otp;
        }

        [HttpPut("update")]
        public async Task<int> UpdateUser(UsernamePasswordResource newUser)
        {
            var user = await ILogin.GetLoginInfos(newUser.username);
            mapper.Map<UsernamePasswordResource, LoginInfo>(newUser, user);
            return ILogin.Saver();
        }
    }
}