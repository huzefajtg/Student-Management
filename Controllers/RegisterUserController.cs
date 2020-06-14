using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    [Route("api/register")]
    [ApiController]
    public class RegisterUserController : Controller
    {
        private readonly StudentContext db;
        private readonly IMapper mapper;
        public RegisterUserController(IMapper map)
        {
            StudentContext context = new StudentContext();
            this.db = context;

            this.mapper = map;
        }

        [HttpPost]
        public List<LoginInfo> RegisterNewUser([FromBody]RegisterUserResource userResource)
        {
            if (userResource.PersonalInfo.Type == "S")
            {
                string param = "exec Add_Student "
                    + userResource.PersonalInfo.isReg.ToString()
                    + ", '" + userResource.PersonalInfo.FirstName
                    + "', '" + userResource.PersonalInfo.SecondName
                    + "', '" + userResource.PersonalInfo.LastName

                    + "', '" + userResource.PersonalInfo.Gender
                    + "', '" + userResource.PersonalInfo.EmailId
                    + "', '" + userResource.PersonalInfo.ContactNumber
                    + "', '" + userResource.PersonalInfo.ContactAddress
                    + "', '" + userResource.PersonalInfo.Dob

                    + "', '" + userResource.UserInfo.username
                    + "', '" + userResource.UserInfo.password + "'";

                return db.LoginInfo.FromSql<LoginInfo>(param).ToList();

            }

            else
            {
                string param = "exec Add_Teacher "
                    + "0 ,"
                    + userResource.PersonalInfo.isReg.ToString()
                    + ", '" + userResource.PersonalInfo.FirstName
                    + "', '" + userResource.PersonalInfo.SecondName
                    + "', '" + userResource.PersonalInfo.LastName

                    + "', '" + userResource.PersonalInfo.Gender
                    + "', '" + userResource.PersonalInfo.EmailId
                    + "', '" + userResource.PersonalInfo.ContactNumber
                    + "', '" + userResource.PersonalInfo.ContactAddress
                    + "', '" + userResource.PersonalInfo.Dob

                    + "', '" + userResource.UserInfo.username
                    + "', '" + userResource.UserInfo.password + "'";
                return db.LoginInfo.FromSql<LoginInfo>(param).ToList();

            }


        }
    }
}