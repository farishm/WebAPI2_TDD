using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CourseBookingApi.Models;

namespace CourseBookingApi.Controllers
{
    public class CourseController : ApiController
    {
        private ICourseRepository courseRepo;

        public CourseController() {
            courseRepo=new InMemoryCourseRepository();
        }

        public CourseController(ICourseRepository _courseRepo)
        {
            courseRepo = _courseRepo;
        }
        

        public IEnumerable<Course> GetAllCourses()
        {
            return courseRepo.GetAll();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await Task.FromResult(GetAllCourses());
        }

        public IHttpActionResult GetCourse(int id)
        {
            var course = courseRepo.GetByID(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        public async Task<IHttpActionResult> GetCourseAsync(int id)
        {
            return await Task.FromResult(GetCourse(id));
        }

        public IHttpActionResult Create(Course _course)
        {
            courseRepo.Add(_course);
            return CreatedAtRoute("DefaultApi", new { id = _course.Id }, _course);
        }

        public IHttpActionResult Delete(int id)
        {
            courseRepo.Delete(id);
            return Ok();
        }

        public IHttpActionResult Update(Course _course)
        {
            courseRepo.Update(_course);
            return Content(HttpStatusCode.Accepted, _course);
        }

    }
}
