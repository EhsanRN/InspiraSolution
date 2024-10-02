using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SubmissionsProcessor.API.Controllers;
using SubmissionsProcessor.API.Models;
using SubmissionsProcessor.API.Repositories;
namespace SubmissionsProcessor.API.Tests
{
    public class SubmissionControllerTests
    {
        private Mock<SubmissionController> _controllerMock = null;
        private Mock<ISubmissionRepository> _SubmissionRepositoryMock = null;
        private Mock<ISubmissionContext> _submissionContextMock = null;
        private Mock<ILogger<SubmissionController>> _loggerMock = null;

        public SubmissionControllerTests()
        {
            _SubmissionRepositoryMock = new Mock<ISubmissionRepository>();
            _submissionContextMock = new Mock<ISubmissionContext>();
            _loggerMock = new Mock<ILogger<SubmissionController>>();
            _controllerMock = new Mock<SubmissionController>();

        }

        [Fact]
        public void PostTest_Return_ShouldBe_OK()
        {
            _loggerMock = new Mock<ILogger<SubmissionController>>();
            var SubmissionRequestMock = new Mock<SubmissionRequest>();
            _SubmissionRepositoryMock.Setup(repo => repo.GetSubmissionResponse(SubmissionRequestMock.Object, _submissionContextMock.Object.SubmissionId))
                                  .Returns(Task.FromResult(new SubmissionResponse { result = "true", contactId = 0 }));

            var sut = new SubmissionController(_SubmissionRepositoryMock.Object, _submissionContextMock.Object, _loggerMock.Object);

            //act
            var response = sut.GetContactId(SubmissionRequestMock.Object);

            //assert
            response.Should().NotBeNull();
            response.Result.As<ObjectResult>().Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void PostTest_Return_ShouldBe_BadRequest()
        {
            //arrange
            var SubmissionRequestMock = new Mock<SubmissionRequest>();
            _SubmissionRepositoryMock.Setup(repo => repo.GetSubmissionResponse(SubmissionRequestMock.Object, _submissionContextMock.Object.SubmissionId))
                                  .Throws(new BadHttpRequestException("Bad request"));
            var sut = new SubmissionController(_SubmissionRepositoryMock.Object, _submissionContextMock.Object, _loggerMock.Object);

            //act
            var response = sut.GetContactId(SubmissionRequestMock.Object);

            //assert
            response.Should().NotBeNull();
            response.Result.As<ObjectResult>().StatusCode.Should().Be(400);
        }

        [Fact]
        public void PostTest_Return_ShouldBe_500()
        {
            //arrange
            var SubmissionRequestMock = new Mock<SubmissionRequest>();
            _SubmissionRepositoryMock.Setup(repo => repo.GetSubmissionResponse(SubmissionRequestMock.Object, _submissionContextMock.Object.SubmissionId))
                                  .Throws(new Exception());

            var sut = new SubmissionController(_SubmissionRepositoryMock.Object, _submissionContextMock.Object, _loggerMock.Object);

            //act
            var response = sut.GetContactId(SubmissionRequestMock.Object);

            //assert
            response.Should().NotBeNull();
            //response.Result.As<ObjectResult>().Value.Should().Be("");
            response.Result.As<ObjectResult>().StatusCode.Should().Be(500);
        }
    }
}