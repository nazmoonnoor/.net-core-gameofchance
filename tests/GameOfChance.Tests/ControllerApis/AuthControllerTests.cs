using GameOfChance.Api;
using GameOfChance.Client;
using GameOfChance.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChance.Tests.ControllerApis
{
    [TestFixture]
    public class AuthControllerTests
    {
        private APIWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new APIWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task WhenRegistrationPayloadIsPosted_ThenTheResultIsOk()
        {
            // Arrange
            var mockRepo = new Mock<IUserManager>();
            mockRepo.Setup(repo => repo.RegisterUserAsync(It.IsAny<RegisterModel>()))
                .Returns(Task.FromResult(new UserManagerResponse
                {
                    IsSuccess = true
                }));

            var controller = new AuthController(mockRepo.Object);
            var payload = new RegisterModel
            {
                Email = "smith@gmail.com",
                Password = "Aa@1234",
                ConfirmPassword = "Aa@1234",
                Name = "Smith"
            };

            // Act
            var result = await controller.RegisterAsync(payload);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task WhenRegistrationPayloadIsNotOkay_ThenTheResultIsBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IUserManager>();
            mockRepo.Setup(repo => repo.RegisterUserAsync(It.IsAny<RegisterModel>()))
                .Returns(Task.FromResult(new UserManagerResponse
                {
                }));

            var controller = new AuthController(mockRepo.Object);
            
            // Act + Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(await controller.RegisterAsync(new RegisterModel
            {
                Password = "Aa@1234",
                ConfirmPassword = "Aa@1234",
                Name = "Smith"
            }));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
