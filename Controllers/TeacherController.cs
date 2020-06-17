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
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly StudentContext db;
        private readonly IMapper mapper;
        public TeacherController(IMapper map)
        {
            StudentContext context = new StudentContext();
            this.db = context;

            this.mapper = map;
        }

        public KeyValuePairResource getHOD(int courseID)
        {
            var result = db.Teachers.Where(t => t.CourseId == courseID && t.IsHod == true).FirstOrDefault();
            return mapper.Map<Teachers, KeyValuePairResource>(result);
        }

        public string getUsername(int id)
        {
            var loginfo = db.LoginInfo.Where(l => (l.Id == id && l.UserType == "T")).FirstOrDefault();
            return loginfo.UserName.ToString();
        }

        [HttpGet("{id}")]
        public async Task<TeacherResource> GetTeacher(int id)
        {
            //Teacher has=>Personal,Subject->course->subject
            //(from a in db.Teachers join b in db.Courses on a.CourseId equals b.CourseId select new { a, b })
            var teacher = await db.Teachers
                .Include(t => t.Course)
                    .ThenInclude(c => c.Subject)
                .Where(t => t.TeacherId == id)
                .FirstOrDefaultAsync();

            var res= mapper.Map<Teachers, TeacherResource>(teacher);
            res.HOD = getHOD(Convert.ToInt32(res.CourseId.ToString()));
            res.username = getUsername(id);
            return res;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateTeacher(int id, [FromBody]TeacherResource resource)
        {
            var teacher = await db.Teachers
                .Where(t => t.TeacherId == id)
                .FirstOrDefaultAsync();

            mapper.Map<TeacherResource, Teachers>(resource, teacher);

            updateDetails();
            return Ok(mapper.Map<Teachers, TeacherResource>(teacher));
        }

        public async void updateDetails()
        {
            int x= await db.SaveChangesAsync(); ;
        }
    }
}