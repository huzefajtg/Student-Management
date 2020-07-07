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
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IManager IStudent;

        private readonly IMapper mapper;

        public StudentController(IMapper map, IManager t)
        {
            StudentContext context = new StudentContext();
            this.mapper = map;
            this.IStudent = t;
        }

        [HttpGet("{id}")]
        public async Task<StudentResource> getSDetails(int id)
        {

            var res = await IStudent.getTeacherStudent(id, true);

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
            var result = await IStudent.getHOD(courseID);
            return mapper.Map<Teachers, KeyValuePairResource>(result);
        }

        public string getUsername(int id)
        {
            return IStudent.getUsername(id);
        }

        public async Task<TeacherResource> GetTeacher(int id)
        {

            var teacher = await IStudent.getTeacher(id);


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

            var teachers = await IStudent.getTeachers(st_course.otherId);   //using courseId

            int[] teachercount = new int[teachers.Count];
            //to find teacher with lowest students
            foreach (var t in teachers)
            {
                tlow = teachers.IndexOf(t);
                //teachercount[tlow] = db.TeacherStudent.Where(ts => ts.TeacherId == t.TeacherId).Count();

                teachercount[tlow] = IStudent.getTeacherStudentCount(t.TeacherId, false);

                if (teachercount[lowest] > teachercount[tlow] && lowest != tlow)
                {
                    lowest = tlow;
                }
            }

            //teachers[lowest].TeacherId; is teacherId. lowest is index of the teacherId to select
            var res = new TeacherStudent();
            res.StudentId = st_course.studentId;
            res.TeacherId = teachers[lowest].TeacherId;
            IStudent.AdderAsync(res);
            TeacherNotification(teachers[lowest].TeacherId, st_course.studentId, "S", 0);

            return mapper.Map<TeacherStudent, TeacherStudentResource>(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateStudent(int id, [FromBody]StudentResource resource)
        {

            var student = await IStudent.getStudent(id);

            mapper.Map<StudentResource, Students>(resource, student);

            IStudent.Saver();

            return Ok(mapper.Map<Students, StudentResource>(student));
        }


        [HttpPost("deleteCourse")]
        public async Task<StudentResource> deleteCourse([FromBody]AddCourseStudent courseStudent)
        {
            //here otherId is TeacherId

            var res = await IStudent.getTeacherStudent(courseStudent, true);
            if (res != null)
                await IStudent.Remover(res);
            TeacherNotification(courseStudent.otherId, courseStudent.studentId, "S", 1);
            IStudent.Saver();
            return await getSDetails(courseStudent.studentId);
        }


        [HttpGet("getStudents")]
        public async Task<List<StudentResource>> getStudents()
        {
            var res = await IStudent.getStudents();
            return mapper.Map<List<Students>, List<StudentResource>>(res);

        }

        [HttpGet("getTeachers")]
        public async Task<List<TeacherResource>> getTeachers()
        {
            var teachers = await IStudent.getTeachers(true);
            var res = mapper.Map<List<Teachers>, List<TeacherResource>>(teachers);
            foreach (var i in res)
                i.HOD = await getHOD(Convert.ToInt32(i.Course.CourseId.ToString()));

            return res;
        }



        //===========================Notifications==================================
        public void TeacherNotification(int teacherId, int otherId, string ortherType, int process)
        {
            var Noti = new TeacherNotification();
            Noti.TeacherId = teacherId;
            Noti.OtherId = otherId;
            Noti.OtherType = ortherType;
            Noti.Viwed = false;
            Noti.NotiDate = DateTime.Now.ToString();
            if (process == 0)   //add course
                Noti.NotiMessage = "Student of Id= " + otherId + " has been added to your class ";
            //delete course
            else
                Noti.NotiMessage = "Student of Id= " + otherId + " has been removed from your class ";
            IStudent.AdderAsync(Noti);
            IStudent.Saver();
        }
    }
}