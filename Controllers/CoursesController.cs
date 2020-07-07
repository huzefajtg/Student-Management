using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentProject.Controllers.Resources;
using StudentProject.Extension.Interface;
using StudentProject.Models;

namespace StudentProject.Controllers
{
    [Route("api/courses")]
    public class CoursesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IManager ICourse;
        public CoursesController(IMapper map, IManager t)
        {
            StudentContext context = new StudentContext();

            this.ICourse = t;
            this.mapper = map;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseSubjectResource>> getCourses()
        {
            var data = await ICourse.getCourses();
            return mapper.Map<List<CourseSubject>, List<CourseSubjectResource>>(data);
        }
    }
}