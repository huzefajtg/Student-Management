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
    [Route("api/snoti")]
    [ApiController]
    public class StudentNotificationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IManager INotification;

        public StudentNotificationController(IMapper map, IManager INotification)
        {
            StudentContext context = new StudentContext();

            this.INotification = INotification;
            this.mapper = map;
        }


        public async Task<string> getName(string type, int id)
        {
            var i = await INotification.getTeacher(id);
            string name = null;
            if (type == "S")
                name = i.FirstName + " " + i.LastName;
            else
                name = i.FirstName + " " + i.LastName;

            return name;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<StudentNotificationResource>> GetNotifications(int id)
        {

            var notifications = await INotification.GetStudentNotification(id);
            var res = mapper.Map<List<StudentNotification>, List<StudentNotificationResource>>(notifications);

            for (int i = 0; i < res.Count; i++)
                res[i].OtherName = await getName(res[i].OtherType, Convert.ToInt32(res[i].OtherId));

            return res;
        }

        [HttpGet("vChange/{id}")]
        public IActionResult seenMsg(int id)//this is messageId
        {
            var res = INotification.retNotificationS(id);

            res.Viwed = !res.Viwed;
            INotification.Saver();
            return Ok(1);
        }
    }
}