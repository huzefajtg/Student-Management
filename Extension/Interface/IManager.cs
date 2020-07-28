using StudentProject.Controllers.Resources;
using StudentProject.Controllers.Resources.ResourceModels;
using StudentProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentProject.Extension.Interface
{
    public interface IManager
    {
        //==========================Student============================
        Task<Students> getStudent(int id);
        Task<List<Students>> getStudents();
        Task<List<Students>> getStudents(FilterResource f);

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

        //=========================Login====================
        Task<bool> isValidUser(string username);
        Task<LoginInfo> GetLoginInfos(UsernamePasswordResource ob);
        //Task<List<LoginInfo>> GetLoginInfos(UsernamePasswordResource ob);
        Task<LoginInfo> GetLoginInfos(string username);

        //==========================Suport======================
        Task<Teachers> getHOD(int courseID);
        string getUsername(int id);
        string getEmail(int id,bool isStudent);

        int Remover(TeacherStudent ob);
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
