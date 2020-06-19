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
            res.HOD =await getHOD(Convert.ToInt32(res.CourseId.ToString()));
            res.username = getUsername(id);
            return res;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateTeacher(int id, [FromBody]TeacherResource resource)
        {
            var teacher = await db.Teachers
                //.Include(t => t.Course)
                //    .ThenInclude(c => c.Subject)
                .Where(t => t.TeacherId == id)
                .FirstOrDefaultAsync();

            mapper.Map<TeacherResource, Teachers>(resource, teacher);

            updateDetails();
            return Ok(mapper.Map<Teachers, TeacherResource>(teacher));
        }

        public async Task<int> updateDetails()
        {
            return await db.SaveChangesAsync();
        }

        [HttpGet("course")]
        public async Task<IEnumerable<CourseSubjectResource>> getCourses()
        {
            var data = await db.CourseSubject.Include(cs => cs.Courses).Where(cs => cs.SubjectId != 0).ToListAsync();
            return mapper.Map<List<CourseSubject>, List<CourseSubjectResource>>(data);
        }


        [HttpPost("getStudent")]
        public async Task<List<TeacherStudentResource>> getStudents(TeacherSearch req)
        {
            var hod = new KeyValuePairResource();
            int tempCourseid = 0;
            var res =await db.TeacherStudent
                        .Include(ts => ts.Student)
                        .Include(ts=>ts.Teacher)
                            //.Where(ts => ts.TeacherId == req.TeacherID)
                            .OrderBy(ts=>ts.StudentId).ToListAsync();

            if (req.myStudents == true)
            {
                res = res.Where(ts => ts.TeacherId == req.TeacherID).ToList();
            }



            var final = mapper.Map<List<TeacherStudent>, List<TeacherStudentResource>>(res);
            foreach (var item in final)
            {
                if (tempCourseid != Convert.ToInt32(item.Teacher.CourseId))
                {
                    hod = await getHOD(Convert.ToInt32(item.Teacher.CourseId));
                    tempCourseid = Convert.ToInt32(item.Teacher.CourseId);
                }
                item.Teacher.HOD = hod;

            }

            return final;
        }

        [HttpGet("search/{id}")]
        public List<TeacherStudentResource> searcher(int id)
        {
            var query = db.TeacherStudent
                        .Include(ts => ts.Student)
                        .Include(ts => ts.Teacher)
                        .Where(ts => ts.StudentId.ToString().StartsWith(id.ToString())).ToList();
            return mapper.Map<List<TeacherStudent>, List<TeacherStudentResource>>(query);
        }
        [HttpPost("setRegisterStudent")]
        public async Task<IActionResult> setRegStudent(RegisterResource register)
        {
            var res = await db.Students.Where(s => s.StudentId == register.id).FirstOrDefaultAsync();
            res.IsReg = register.isReg;
            //Add track on who registered student
            int x = await updateDetails();

            return Ok(x);

        }
    }
}