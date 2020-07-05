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
    [Route("api/tnoti")]
    [ApiController]
    public class TeacherNotificationController : ControllerBase
    {
        private readonly StudentContext db;
        private readonly IMapper mapper;

        public TeacherNotificationController(IMapper map)
        {
            StudentContext context = new StudentContext();
            this.db = context;

            this.mapper = map;
        }

        public string getName(string type,int id)
        {
            if (type == "S")
            {
                var i = db.Students.Where(s => s.StudentId == id).FirstOrDefault();
                string name = i.FirstName + " " + i.LastName;
                return name;
            }
            else
            {
                var i = db.Teachers.Where(s => s.TeacherId == id).FirstOrDefault();
                string name = i.FirstName + " " + i.LastName;
                return name;
            }
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<TeacherNotificationResource>> GetNotifications(int id)
        {
            var notifications = await db.TeacherNotification.Where(tn => tn.TeacherId == id).OrderByDescending(tn => tn.NotiDate).ToListAsync();
            var res= mapper.Map<List<TeacherNotification>, List<TeacherNotificationResource>>(notifications);

            for (int i=0;i<res.Count;i++)
            {
                res[i].OtherName = getName(res[i].OtherType, Convert.ToInt32(res[i].OtherId));
            }

            return res;
        }

        [HttpGet("vChange/{id}")]
        public IActionResult seenMsg(int id)//this is messageId
        {
            var res = db.TeacherNotification.Where(tn => tn.NotificationId == id).FirstOrDefault();
            res.Viwed = !res.Viwed;
            db.SaveChanges();
            return Ok(1);
        }
    }
}