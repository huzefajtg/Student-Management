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
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext db;
        private readonly IMapper mapper;

        public StudentController(IMapper map)
        {
            StudentContext context = new StudentContext();
            this.db = context;

            this.mapper = map;
        }

        [HttpGet("{id}")]
        public async Task<StudentResource> getSDetails(int id)
        {
            var res = await db.TeacherStudent
                       .Include(ts => ts.Student)
                       .Include(ts => ts.Teacher)
                           .ThenInclude(t => t.Course)
                           .Where(ts => ts.StudentId == id).ToListAsync(); //1


            var final = new StudentResource();
            var temp = mapper.Map<List<TeacherStudent>, List<TeacherStudentResource>>(res);

            final.PersonalInfo = temp[0].Student.PersonalInfo;
            final.StudentId = Convert.ToInt32(temp[0].StudentId);

            foreach (var i in temp)
            {
                final.teacherInfo.Add(await GetTeacher(Convert.ToInt32(i.TeacherId)));
            }

            return final;
        }


        public async Task<KeyValuePairResource> getHOD(int courseID)
        {
            var result = await db.Teachers.Where(t => t.CourseId == courseID && t.IsHod == true).FirstOrDefaultAsync();
            return mapper.Map<Teachers, KeyValuePairResource>(result);
        }

        public string getUsername(int id)
        {
            var loginfo = db.LoginInfo.Where(l => (l.Id == id && l.UserType == "T")).FirstOrDefault();
            return loginfo.UserName.ToString();
        }

        public async Task<TeacherResource> GetTeacher(int id)
        {
            //Teacher has=>Personal,Subject->course->subject
            //(from a in db.Teachers join b in db.Courses on a.CourseId equals b.CourseId select new { a, b })
            var teacher = await db.Teachers
                .Include(t => t.Course)
                    .ThenInclude(c => c.Subject)
                .Where(t => t.TeacherId == id)
                .FirstOrDefaultAsync();

            var res = mapper.Map<Teachers, TeacherResource>(teacher);
            res.HOD = await getHOD(Convert.ToInt32(res.Course.CourseId.ToString()));
            res.username = getUsername(id);
            return res;
        }


    }
}