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
using StudentProject.Extension.Interface;
using StudentProject.Models;

namespace StudentProject.Extension.Definition
{
    public class Manager : IManager
    {
        private readonly StudentContext db;
        private readonly IMapper mapper;

        public Manager(IMapper map)
        {
            StudentContext context = new StudentContext();
            this.db = context;

            this.mapper = map;
        }


        //================================Students=============================
        public async Task<Students> getStudent(int id)
        {
            return await db.Students
                .Where(t => t.StudentId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Students>> getStudents()
        {
            return await db.Students.OrderBy(s=>s.StudentId).ToListAsync();
        }



        //================================Teachers=============================

        //================================================TeacherStudent=======
            public int getTeacherStudentCount(int id, bool isStudent)
            {
                if (isStudent == true)
                    return db.TeacherStudent.Where(ts => ts.StudentId == id).Count(); //StudentId
                else
                    return db.TeacherStudent.Where(ts => ts.TeacherId == id).Count(); //TeacherId
            }


            public async Task<List<TeacherStudent>> getTeacherStudent(int id,bool isStudent)
            {
            if (isStudent == true)
                return await db.TeacherStudent
                           .Include(ts => ts.Student)
                           .Include(ts => ts.Teacher)
                               .ThenInclude(t => t.Course)
                               .Where(ts => ts.StudentId == id)
                               .OrderBy(ts => ts.Teacher.CourseId).ToListAsync(); //StudentId
            else
                return await db.TeacherStudent
                           .Include(ts => ts.Student)
                           .Include(ts => ts.Teacher)
                               .ThenInclude(t => t.Course)
                               .Where(ts => ts.TeacherId == id)
                               .OrderBy(ts => ts.Teacher.CourseId).ToListAsync(); //TeacherId
            }

            public async Task<TeacherStudent> getTeacherStudent(AddCourseStudent courseStudent,bool isTeacher)
            {
                //if(isTeacher==true)
                return await db.TeacherStudent
                    .Where(ts => ts.StudentId == courseStudent.studentId && ts.TeacherId == courseStudent.otherId)  //here otherId is TeacherId
                    .FirstOrDefaultAsync();
            }

            public async Task<List<TeacherStudent>> getStudentsOf_Teachers(int id)
            {
                return await db.TeacherStudent
                         .Include(ts => ts.Student)
                         .Include(ts => ts.Teacher)
                             .Where(ts => ts.TeacherId == id)
                             .OrderBy(ts => ts.StudentId).ToListAsync();
        }
            



        public async Task<Teachers> getTeacher(int id)
        {
            return await db.Teachers
                            .Include(t => t.Course)
                                .ThenInclude(c => c.Subject)
                            .Where(t => t.TeacherId == id) .FirstOrDefaultAsync();
        }

        public async Task<List<Teachers>> getTeachers(bool Subs)
        {
            if (Subs == true)
                return await db.Teachers
                    .Include(t => t.Course)
                        .ThenInclude(c => c.Subject)
                        .OrderBy(ts => ts.TeacherId).ToListAsync();
            else
                return await db.Teachers.OrderBy(t => t.TeacherId).ToListAsync();
        }

        public async Task<List<Teachers>> getTeachers(int courseId)
        {
            return await db.Teachers.Where(t => t.CourseId == courseId).ToListAsync();
        }


        //===============================Courses==============================
        public async Task<List<CourseSubject>> getCourses()
        {
            return await db.CourseSubject.Include(cs => cs.Courses).Where(cs => cs.SubjectId != 0).ToListAsync();
        }



        //================================Support==============================

        public async Task<Teachers> getHOD(int courseID)
        {
            return await db.Teachers.Where(t => t.CourseId == courseID && t.IsHod == true).FirstOrDefaultAsync();
        }
        public string getUsername(int id)
        {
            return db.LoginInfo.Where(l => (l.Id == id && l.UserType == "T")).FirstOrDefault().UserName.ToString();
        }


        public int AdderAsync(TeacherStudent ob)
        {
            db.AddAsync(ob);
            return 1;
        }

        public int AdderAsync(TeacherNotification ob)
        {
            db.AddAsync(ob);
            return 1;
        }
        public int AdderAsync(StudentNotification ob)
        {
            db.AddAsync(ob);
            return 1;
        }

        public async Task<int> Remover(TeacherStudent ob)
        {
            db.Remove(ob);
            return 1;
        }

        public int Saver()
        {
            return db.SaveChanges();
        }

        //================================Notification==============================
        public async Task<List<TeacherNotification>> GetTeacherNotification(int id)
        {
            return await db.TeacherNotification.Where(tn => tn.TeacherId == id).OrderByDescending(tn => tn.NotiDate).ToListAsync();
        }

        public async Task<List<StudentNotification>> GetStudentNotification(int id)
        {
            return await db.StudentNotification.Where(tn => tn.StudentId == id).OrderByDescending(tn => tn.NotiDate).ToListAsync();
        }

        public TeacherNotification retNotificationT(int notificationId)
        {
            return db.TeacherNotification.Where(tn => tn.NotificationId == notificationId).FirstOrDefault();
        }

        public StudentNotification retNotificationS(int notificationId)
        {
            return db.StudentNotification.Where(tn => tn.NotificationId == notificationId).FirstOrDefault();
        }



    }
}
