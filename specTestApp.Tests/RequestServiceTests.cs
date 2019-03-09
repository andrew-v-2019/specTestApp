using InMemoryDbSet;
using Moq;
using NUnit.Framework;
using specTestApp.Data;
using specTestApp.Data.Entities;
using specTestApp.Services.Services;
using specTestApp.Web.Models;
using System;
using System.Linq;

namespace specTestApp.Tests
{
    public class RequestServiceTests
    {
        private RequestsService _requestService;
        private Mock<ApplicationDbContext> _dbContextMock;

        [SetUp]
        public void SetUp()
        {
            _requestService = new RequestsService();
            _dbContextMock = new Mock<ApplicationDbContext>();
            _dbContextMock.Setup(x => x.Requests).Returns(new InMemoryDbSet<Request>());
        }




        [Test]
        public async System.Threading.Tasks.Task CreateRequest_CorrectData_ItemCreatedAsync()
        {
            var caption = Guid.NewGuid().ToString();
            var message = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            FileSaveResult file = new FileSaveResult()
            {
                FileName = Guid.NewGuid().ToString(),
                OrigFileName = Guid.NewGuid().ToString()
            };
            CreateRequestViewModel model = new CreateRequestViewModel()
            {
                Caption = caption,
                Message = message,
            };

            await _requestService.CreateRequestAsync(model, file, userId);
            var actualResult = _dbContextMock.Object.Requests.FirstOrDefault(x => x.Message.Equals(message));

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(actualResult.Caption, caption);
            Assert.AreEqual(actualResult.Message, message);
            Assert.AreEqual(actualResult.CreatedBy, userId);
            Assert.AreEqual(actualResult.FileName, file.FileName);
            Assert.AreEqual(actualResult.OriginalFileName, file.OrigFileName);
        }
    }
}
