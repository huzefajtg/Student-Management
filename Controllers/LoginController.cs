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
    }
}