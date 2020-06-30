using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Controllers.Resources;
using StudentProject.Models;

namespace StudentProject.Controllers
{
    [Route("api/courses")]
    public class CoursesController : Controller
    {
        private readonly StudentContext db;
        private readonly IMapper mapper;
        public CoursesController(IMapper map)
        {
            StudentContext context = new StudentContext();
            this.db = context;

            this.mapper = map;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseSubjectResource>> getCourses()
        {
            var data = await db.CourseSubject.Include(cs => cs.Courses).Where(cs => cs.SubjectId != 0).ToListAsync();
            return mapper.Map<List<CourseSubject>, List<CourseSubjectResource>>(data);
        }
    }
}