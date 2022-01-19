using AutoMapper;
using GameOfChance.Api;
using GameOfChance.Client;
using GameOfChance.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class GameControllerTests
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
        public async Task WhenPostingBet_ThenTheResultIsOk()
        {
            // Arrange
            var mockRepo = new Mock<IGameManager>();
            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Core.Domain.Bet>()))
                .Returns(Task.FromResult(new Core.Domain.Bet{ }));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Core.Domain.Bet>(It.IsAny<Client.Bet>()))
                .Returns(new Core.Domain.Bet{ });

            var mockLogger = new Mock<ILogger<GameController>>();
            mockLogger.Setup(x => x.LogInformation(It.IsAny<string>())).Returns("FF");

            var controller = new GameController(mockLogger.Object, mockMapper.Object, mockRepo.Object);

            // Act
            var result = await controller.Post(new Api.Api.Models.BetRequest
            {
                Number = 1,
                Points = 100
            });

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
