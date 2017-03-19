using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseBookingApi.Controllers;
using Moq;
using CourseBookingApi.Models;
using System.Web.Http;
using System.Net;

namespace CourseBookingApi.Tests
{
    [TestClass]
    public class TestCourseController_Moq
    {
        //[TestMethod]
        //public void GetAllCourses_ShouldReturnAllCourses_Moq()
        //{
        //    // Arrange
        //    var testCourses = _inMemoryRep.GetAll() as List<Course>;
        //    var controller = new CourseController(_inMemoryRep);

        //    // Act
        //    var result = controller.GetAllCourses() as List<Course>;

        //    // Assert
        //    Assert.AreEqual(testCourses.Count, result.Count);
        //}

        //[TestMethod]
        //public async Task GetAllCoursesAsync_ShouldReturnAllCourses_Moq()
        //{
        //    var testCourses = _inMemoryRep.GetAll() as List<Course>;
        //    var controller = new CourseController(_inMemoryRep);

        //    var result = await controller.GetAllCoursesAsync() as List<Course>;
        //    Assert.AreEqual(testCourses.Count, result.Count);
        //}

        //[TestMethod]
        //public void GetCourse_ShouldReturnCorrectCourse_Moq()
        //{
        //    var testCourses = _inMemoryRep.GetAll() as List<Course>;
        //    var controller = new CourseController(_inMemoryRep);

        //    var result = controller.GetCourse(4) as OkNegotiatedContentResult<Course>;
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(testCourses[3].Title, result.Content.Title);
        //}

        //[TestMethod]
        //public async Task GetCourseAsync_ShouldReturnCorrectCourse_Moq()
        //{
        //    var testCourses = _inMemoryRep.GetAll() as List<Course>;
        //    var controller = new CourseController(_inMemoryRep);

        //    var result = await controller.GetCourseAsync(4) as OkNegotiatedContentResult<Course>;
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(testCourses[3].Title, result.Content.Title);
        //}

        //[TestMethod]
        //public void GetCourse_ShouldNotFindCourse_Moq()
        //{
        //    var controller = new CourseController(_inMemoryRep);

        //    var result = controller.GetCourse(999);
        //    Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //}

        [TestMethod]
        public void GetReturnsCourseWithSameId_Moq()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(x => x.GetByID(11))
                .Returns(new Course { Id = 11 });

            var controller = new CourseController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetCourse(11);
            var contentResult = actionResult as OkNegotiatedContentResult<Course>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(11, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetReturnsNotFound_Moq()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            var controller = new CourseController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetCourse(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsOk_Moq()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            var controller = new CourseController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(8);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void CreateMethodSetsLocationHeader_Moq()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            var controller = new CourseController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Create(new Course { Id = 11, Title = "ASP .Net Web API In Memo Testing...", Description = "ASP .Net Web API Development with Project work", Reference = "ASPNETB010", Duration = 18, Fees = 2500, Status = "open", Location = "Net Hall 1", TotalPlaces = 100, AvailablePlaces = 0 });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Course>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(11, createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void UpdateReturnsContentResult_Moq()
        {
            // Arrange
            var mockRepository = new Mock<ICourseRepository>();
            var controller = new CourseController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Update(new Course { Id = 10, Title = "ASP .Net Web API In Memo Testing...", Description = "ASP .Net Web API Development with Project work", Reference = "ASPNETB010", Duration = 18, Fees = 2500, Status = "open", Location = "Net Hall 1", TotalPlaces = 100, AvailablePlaces = 0 });
            var contentResult = actionResult as NegotiatedContentResult<Course>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(10, contentResult.Content.Id);
        }
    }
}
