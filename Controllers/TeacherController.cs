using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Controllers.Resources;
using StudentProject.Controllers.Resources.ResourceModels;
using StudentProject.Extension.Interface;
using StudentProject.Models;

namespace StudentProject.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly IMapper mapper;
        private readonly IManager ITeacher;
        public TeacherController(IMapper map, IManager t)
        {
            StudentContext context = new StudentContext();

            this.ITeacher = t;
            this.mapper = map;
        }

        public async Task<KeyValuePairResource> getHOD(int courseID)
        {
            return mapper.Map<Teachers, KeyValuePairResource>
                (await ITeacher.getHOD(courseID));
        }
        public string getUsername(int id) { return ITeacher.getUsername(id); }
        public int updateDetails() { return ITeacher.Saver(); }


        [HttpGet("{id}")]
        public async Task<TeacherResource> GetTeacher(int id)
        {
            //Teacher has=>Personal,Subject->course->subject
            //(from a in db.Teachers join b in db.Courses on a.CourseId equals b.CourseId select new { a, b })

            var teacher = await ITeacher.getTeacher(id);

            var res= mapper.Map<Teachers, TeacherResource>(teacher);
            res.HOD =await getHOD(Convert.ToInt32(res.Course.CourseId.ToString()));
            //res.username = getUsername(id);
            return res;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateTeacher(int id, [FromBody]TeacherResource resource)
        {
            var teacher = await ITeacher.getTeacher(id);

            mapper.Map<TeacherResource, Teachers>(resource, teacher);
            updateDetails();
            return Ok(mapper.Map<Teachers, TeacherResource>(teacher));
        }


        //--------------------Notification For Teachers-----------------------
        //Go to teacher Notification

        //------------------------Students API-------------------



        //Not Used Anywhere
        //For server side sorting/Searching
        //[HttpGet("search/{id}")]
        //public List<TeacherStudentResource> searchStudent(int id)
        //{
        //    var query = db.TeacherStudent
        //                .Include(ts => ts.Student)
        //                .Include(ts => ts.Teacher)
        //                .Where(ts => ts.StudentId.ToString().StartsWith(id.ToString())).ToList();
        //    return mapper.Map<List<TeacherStudent>, List<TeacherStudentResource>>(query);
        //}

        [HttpPost("setRegisterStudent")]
        public async Task<IActionResult> setRegStudent(RegisterResource register)
        {

            var res = await ITeacher.getStudent(register.id);
            res.IsReg = register.isReg;
            int x = updateDetails();
#warning Add track on who registered student TeacherController=>serRegStudent()

            //============notification=============
            var noti = new StudentNotification();
            noti.StudentId = register.id;
            noti.OtherId = register.otherId;
            noti.OtherType = "T";
            noti.Viwed = false;
            noti.NotiDate = DateTime.Now.ToString();
            if(register.isReg==true)
                noti.NotiMessage = "You have been registered";
            else
                noti.NotiMessage = "You have been un-registered";

            ITeacher.AdderAsync(noti);

            ITeacher.Saver();

            return Ok(x);
        }

        

        [HttpGet("getSInfo/{id}")]
        public async Task<StudentResource> getSInfo(int id)   //studentid
        {

            var res = await ITeacher.getTeacherStudent(id,true);
           
            var final = new StudentResource();

            var temp = mapper.Map<List<TeacherStudent>, List<TeacherStudentResource>>(res);
            /*Add *NULL Teacher* for newly added students with no Courses*/

            final.PersonalInfo = temp[0].Student.PersonalInfo;
            final.StudentId = Convert.ToInt32(temp[0].StudentId);

            foreach (var i in temp)
                final.teacherInfo.Add(await GetTeacher(Convert.ToInt32(i.TeacherId)));

            return final;
        }

        [HttpPost("getStudents")]
        public async Task<List<StudentResource>> getStudents(TeacherSearch req)
        {
            if (req.myStudents == false)
            {
                var val = await ITeacher.getStudents();
                return mapper.Map<List<Students>, List<StudentResource>>(val);
            }

            //for MyStudents==TRUE
            var res = new List<TeacherStudent>();

            if (req.myStudents == true)
                res = await ITeacher.getStudentsOf_Teachers(req.TeacherID);

            return mapper.Map<List<TeacherStudent>, List<StudentResource>>(res);
        }
    }
}

