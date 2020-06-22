using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            res.HOD =await getHOD(Convert.ToInt32(res.Course.CourseId.ToString()));
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

            await updateDetails();
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







        //------------------------Students API-------------------



        [HttpGet("search/{id}")]
        public List<TeacherStudentResource> seachStudent(int id)
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
        

        [HttpGet("getSInfo/{id}")]
        public async Task<StudentResource> getSInfo(int id)   //studentid
        {
            var res = await db.TeacherStudent
                        .Include(ts => ts.Student)
                        .Include(ts => ts.Teacher)
                            .ThenInclude(t=>t.Course)
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


        [HttpPost("getStudents")]
        public async Task<List<StudentResource>> getStudents(TeacherSearch req)
        {
            if (req.myStudents == false)
            {
                var val = await db.Students.OrderBy(s => s.StudentId).ToListAsync();
                return mapper.Map<List<Students>, List<StudentResource>>(val);
            }
            var res = await db.TeacherStudent
                         .Include(ts => ts.Student)
                         .Include(ts => ts.Teacher)
                             //.Where(ts => ts.TeacherId == req.TeacherID)
                             .OrderBy(ts => ts.StudentId).ToListAsync();

            if (req.myStudents == true)
            {
                res = res.Where(ts => ts.TeacherId == req.TeacherID).ToList();
            }

            return mapper.Map<List<TeacherStudent>, List<StudentResource>>(res);

        }
    }
}

