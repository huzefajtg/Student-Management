using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Controllers.Resources;
using StudentProject.Extension.Interface;
using StudentProject.Models;

namespace StudentProject.Controllers
{
    [Route("api/tnoti")]
    [ApiController]
    public class TeacherNotificationController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly IManager INotification;

        public TeacherNotificationController(IMapper map, IManager INotification)
        {
            StudentContext context = new StudentContext();

            this.INotification = INotification;
            this.mapper = map;
        }

        public async Task<string> getName(string type,int id)
        {
            var i = await INotification.getStudent(id);
            string name = null;
            if (type == "S")
                name = i.FirstName + " " + i.LastName;
            else
            {
                //var i = db.Teachers.Where(s => s.TeacherId == id).FirstOrDefault();
                //var i = await INotification.getStudent(id);
                name = i.FirstName + " " + i.LastName;
                //return name;
            }
            return name;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<TeacherNotificationResource>> GetNotifications(int id)
        {
            //var notifications = await db.TeacherNotification.Where(tn => tn.TeacherId == id).OrderByDescending(tn => tn.NotiDate).ToListAsync();
            var notifications = await INotification.GetTeacherNotification(id);

            var res = mapper.Map<List<TeacherNotification>, List<TeacherNotificationResource>>(notifications);

            for (int i=0;i<res.Count;i++)
            {
                res[i].OtherName = await getName(res[i].OtherType, Convert.ToInt32(res[i].OtherId));
            }

            return res;
        }

        [HttpGet("vChange/{id}")]
        public IActionResult seenMsg(int id)//this is messageId
        {
            //var res = db.TeacherNotification.Where(tn => tn.NotificationId == id).FirstOrDefault();
            var res = INotification.retNotificationT(id);
            res.Viwed = !res.Viwed;
            //db.SaveChanges();
            INotification.Saver();
            return Ok(1);
        }
    }
}