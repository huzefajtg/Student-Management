using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentProject.Controllers.Resources;
using StudentProject.Models;

namespace StudentProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Models to Resources


            CreateMap<LoginInfo, LoginIdTypeResource>();
            CreateMap<CourseSubject, CourseSubjectResource>();
            CreateMap<Courses, CourseResource>();
            CreateMap<NotiFicationTeachers, NotificationTeachersResource>();
            CreateMap<TeacherNotification, TeacherNotificationResource>();

            CreateMap<LoginInfo, LoginInfoResource>();
            CreateMap<LoginInfo, LoginIdTypeResource>()
                .ForMember(logi => logi.Id, opt => opt.MapFrom(log => log.Id))
                .ForMember(logi => logi.UserType, opt => opt.MapFrom(log => log.UserType))
                .ForMember(logi => logi.Loginid, opt => opt.MapFrom(log => log.Loginid));
            CreateMap<Teachers, KeyValuePairResource>()
                .ForMember(k => k.id, opt => opt.MapFrom(t => t.TeacherId))
                .ForMember(k => k.name, opt => opt.MapFrom(t => t.FirstName + " " + t.LastName));

            CreateMap<Teachers, TeacherResource>()
                .ForMember(tr => tr.PersonalInfo, opt => opt.MapFrom(t =>
                    new PersonalResource { FirstName = t.FirstName,
                        SecondName = t.SecondName,
                        LastName = t.LastName,

                        Gender = t.Gender,
                        EmailId = t.EmailId,
                        ContactNumber = t.ContactNumber,
                        ContactAddress = t.ContactAddress,
                        Dob = t.Dob,
                        isReg = Convert.ToBoolean(t.IsReg.ToString()),
                        Type = "T"
                    }
                ))
                //.ForMember(tr=>tr.userinfo.username,opt=>opt.MapFrom(t=>t.)) NO Need
                .ForMember(tr => tr.SubjectInfo, opt => opt.MapFrom(t => new KeyValuePairResource{
                    name = t.Course.Subject.SubjectName,
                    id = Convert.ToInt32(t.Course.SubjectId.ToString())
                }));

            


            CreateMap<Students, StudentResource>()
                .ForMember(tr => tr.PersonalInfo, opt => opt.MapFrom(t =>
                    new PersonalResource
                    {
                        FirstName = t.FirstName,
                        SecondName = t.SecondName,
                        LastName = t.LastName,

                        Gender = t.Gender,
                        EmailId = t.EmailId,
                        ContactNumber = t.ContactNumber,
                        ContactAddress = t.ContactAddress,
                        Dob = t.Dob,
                        isReg = Convert.ToBoolean(t.IsReg.ToString()),
                        Type = "S"
                    }
                ));


            CreateMap<TeacherStudent,StudentResource>()
                .ForMember(tr => tr.PersonalInfo, opt => opt.MapFrom(t =>
                    new PersonalResource
                    {
                        FirstName = t.Student.FirstName,
                        SecondName = t.Student.SecondName,
                        LastName = t.Student.LastName,

                        Gender= t.Student.Gender,
                        EmailId = t.Student.EmailId,
                        ContactNumber = t.Student.ContactNumber,
                        ContactAddress = t.Student.ContactAddress,
                        Dob = t.Student.Dob,
                        isReg = Convert.ToBoolean(t.Student.IsReg.ToString()),
                        Type = "S"
                    }
                ));


            //teacher-student mapping
            CreateMap<TeacherStudent, TeacherStudentResource>();





            //CreateMap<TeacherStudent,TeacherResource>()
            //    .ForMember(tr=>tr.PersonalInfo, opt => opt.MapFrom(t =>
            //        new PersonalResource
            //        {
            //            FirstName = t.Student.FirstName,
            //            SecondName = t.Student.SecondName,
            //            LastName = t.Student.LastName,

            //            Gender = t.Student.Gender,
            //            EmailId = t.Student.EmailId,
            //            ContactNumber = t.Student.ContactNumber,
            //            ContactAddress = t.Student.ContactAddress,
            //            Dob = t.Student.Dob,
            //            isReg = Convert.ToBoolean(t.Student.IsReg.ToString()),
            //            Type = "S"
            //        }
            //    ))
            //    .ForMember(tr => tr.SubjectInfo, opt => opt.MapFrom(t =>
            //          new KeyValuePairResource
            //          {

            //          }))


            //Resource to Model
            //CreateMap<TeacherResource, Teachers>()
            //  .ForMember(t => t.TeacherId, op => op.Ignore())
            //  .ForMember(t => t.IsHod, op => op.Ignore())
            //  .ForMember(t => t.CourseId, opt => opt.MapFrom(tr => tr.Course.CourseId))
            //  .ForMember(t => t.FirstName, opt => opt.MapFrom(tr => tr.PersonalInfo.FirstName))
            //  .ForMember(t => t.SecondName, opt => opt.MapFrom(tr => tr.PersonalInfo.SecondName))
            //  .ForMember(t => t.LastName, opt => opt.MapFrom(tr => tr.PersonalInfo.LastName))
            //  .ForMember(t => t.IsReg, opt => opt.MapFrom(tr => Convert.ToBoolean(tr.PersonalInfo.isReg)))

            //  .ForMember(t => t.Gender, opt => opt.MapFrom(tr => tr.PersonalInfo.Gender))
            //  .ForMember(t => t.EmailId, opt => opt.MapFrom(tr => tr.PersonalInfo.EmailId))
            //  .ForMember(t => t.ContactAddress, opt => opt.MapFrom(tr => tr.PersonalInfo.ContactAddress))
            //  .ForMember(t => t.ContactNumber, opt => opt.MapFrom(tr => tr.PersonalInfo.ContactNumber))
            //  .ForMember(t => t.Dob, opt => opt.MapFrom(tr => tr.PersonalInfo.Dob));


            CreateMap<TeacherResource, Teachers>()
                .ForMember(t => t.IsReg, opt => opt.Ignore())
                .ForMember(t => t.TeacherId, op => op.Ignore())
                .ForMember(t => t.CourseId, opt => opt.MapFrom(tr => tr.Course.CourseId))
                .ForMember(t => t.FirstName, opt => opt.MapFrom(tr => tr.PersonalInfo.FirstName))
                .ForMember(t => t.SecondName, opt => opt.MapFrom(tr => tr.PersonalInfo.SecondName))
                .ForMember(t => t.LastName, opt => opt.MapFrom(tr => tr.PersonalInfo.LastName))

                .ForMember(t => t.Gender, opt => opt.MapFrom(tr => tr.PersonalInfo.Gender))
                .ForMember(t => t.EmailId, opt => opt.MapFrom(tr => tr.PersonalInfo.EmailId))
                .ForMember(t => t.ContactAddress, opt => opt.MapFrom(tr => tr.PersonalInfo.ContactAddress))
                .ForMember(t => t.ContactNumber, opt => opt.MapFrom(tr => tr.PersonalInfo.ContactNumber))
                .ForMember(t => t.Dob, opt => opt.MapFrom(tr => tr.PersonalInfo.Dob));

            //used to avoid error while updating teacher details
            CreateMap<CourseResource, Courses>()
                .ForMember(c => c.CourseId, op => op.Ignore());

            CreateMap<StudentResource, Students>()
              .ForMember(t => t.IsReg, opt => opt.Ignore())
              .ForMember(t => t.StudentId, op => op.Ignore())
              .ForMember(t => t.FirstName, opt => opt.MapFrom(tr => tr.PersonalInfo.FirstName))
              .ForMember(t => t.SecondName, opt => opt.MapFrom(tr => tr.PersonalInfo.SecondName))
              .ForMember(t => t.LastName, opt => opt.MapFrom(tr => tr.PersonalInfo.LastName))

              .ForMember(t => t.Gender, opt => opt.MapFrom(tr => tr.PersonalInfo.Gender))
              .ForMember(t => t.EmailId, opt => opt.MapFrom(tr => tr.PersonalInfo.EmailId))
              .ForMember(t => t.ContactAddress, opt => opt.MapFrom(tr => tr.PersonalInfo.ContactAddress))
              .ForMember(t => t.ContactNumber, opt => opt.MapFrom(tr => tr.PersonalInfo.ContactNumber))
              .ForMember(t => t.Dob, opt => opt.MapFrom(tr => tr.PersonalInfo.Dob));



            CreateMap<TeacherStudentResource, TeacherStudent>()
                .ForMember(ts => ts.TableId, op => op.Ignore())
                .ForMember(ts => ts.Teacher, op => op.Ignore())
                .ForMember(ts => ts.Student, op => op.Ignore());



        }
    }
}
