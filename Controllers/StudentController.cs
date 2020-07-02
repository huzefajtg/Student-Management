using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Controllers.Resources;
using StudentProject.Controllers.Resources.ResourceModels;
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

        [HttpPost("course")]
        public async Task<TeacherStudentResource> AddCourse([FromBody]AddCourseStudent st_course)
        {
            int lowest = 0;
            int tlow;
            //here otherId is CourseId
            var teachers = await db.Teachers.Where(t => t.CourseId == st_course.otherId).ToListAsync();
            int[] teachercount = new int[teachers.Count];
            //to find teacher with lowest students
            foreach(var t in teachers)
            {
                tlow = teachers.IndexOf(t);
                teachercount[tlow] = db.TeacherStudent.Where(ts => ts.TeacherId == t.TeacherId).Count();
                if (teachercount[lowest] > teachercount[tlow] && lowest != tlow)
                {
                    lowest = tlow;
                }
            }

            
            var res = new TeacherStudent();
            res.StudentId = st_course.studentId;
            res.TeacherId = teachers[lowest].TeacherId;
            db.Add(res);
            int x = db.SaveChanges();

            return mapper.Map<TeacherStudent, TeacherStudentResource>(res);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> updateTeacher(int id, [FromBody]StudentResource resource)
        {
            var student = await db.Students
                //.Include(t => t.Course)
                //    .ThenInclude(c => c.Subject)
                .Where(t => t.StudentId == id)
                .FirstOrDefaultAsync();

            mapper.Map<StudentResource, Students>(resource, student);
            db.SaveChanges();
            return Ok(mapper.Map<Students, StudentResource>(student));
        }


        [HttpPost("deleteCourse")]
        public async Task<StudentResource> deleteCourse([FromBody]AddCourseStudent courseStudent)
        {
            //here otherId is TeacherId
            var res = await db.TeacherStudent
                .Where(ts => ts.StudentId == courseStudent.studentId && ts.TeacherId == courseStudent.otherId)
                .FirstOrDefaultAsync();

            if(res!=null)
                db.TeacherStudent.Remove(res);
            db.SaveChanges();
            return await getSDetails(courseStudent.studentId);
        }


        [HttpGet("getStudents")]
        public async Task<List<StudentResource>> getStudents()
        {
            var res = await db.Students.OrderBy(ts => ts.StudentId).ToListAsync();
            return mapper.Map<List<Students>, List<StudentResource>>(res);

        }

        [HttpGet("getTeachers")]
        public async Task<List<TeacherResource>> getTeachers()
        {
            var teachers = await db.Teachers
                    .Include(t=>t.Course)
                        .ThenInclude(c => c.Subject)
                        .OrderBy(ts => ts.TeacherId).ToListAsync();
            var res = mapper.Map<List<Teachers>, List<TeacherResource>>(teachers);
            foreach (var i in res)
            {
                i.HOD = await getHOD(Convert.ToInt32(i.Course.CourseId.ToString()));
            }

            return res;
        }

    }
}