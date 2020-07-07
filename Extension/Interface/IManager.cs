using StudentProject.Controllers.Resources.ResourceModels;
using StudentProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Extension.Interface
{
    public interface IManager
    {
        //==========================Student============================
        Task<Students> getStudent(int id);
        Task<List<Students>> getStudents();



        //==========================Teacher============================
        //======TeacherStudent=========================================
            int getTeacherStudentCount(int id, bool isStudent);

            Task<List<TeacherStudent>> getTeacherStudent(int id, bool isStudent);
            Task<TeacherStudent> getTeacherStudent(AddCourseStudent courseStudent, bool isTeacher);

            Task<List<TeacherStudent>> getStudentsOf_Teachers(int id);


        Task<Teachers> getTeacher(int id);
        Task<List<Teachers>> getTeachers(int courseId);
        Task<List<Teachers>> getTeachers(bool Subs);

        //==========================Courses=================
        Task<List<CourseSubject>> getCourses();

        //==========================Suport======================
        Task<Teachers> getHOD(int courseID);
        string getUsername(int id);

        Task<int> Remover(TeacherStudent ob);
        int AdderAsync(TeacherStudent ob);
        int AdderAsync(TeacherNotification ob);
        int AdderAsync(StudentNotification ob);
        int Saver();

        //==========================Notification======================
        Task<List<TeacherNotification>> GetTeacherNotification(int id);
        Task<List<StudentNotification>> GetStudentNotification(int id);

        TeacherNotification retNotificationT(int notificationId);
        StudentNotification retNotificationS(int notificationId);



    }
}
