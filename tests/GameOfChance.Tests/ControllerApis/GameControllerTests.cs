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
                .Returns(Task.FromResult(new Core.Domain.Bet{ BetPoints = 100, Number=3, Created=DateTime.Now, AdditionalBalance=100,PlayerId=Guid.NewGuid() }));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Core.Domain.Bet>(It.IsAny<Client.Bet>()))
                .Returns(new Core.Domain.Bet{ BetPoints = 100, Number = 3, Created = DateTime.Now, AdditionalBalance = 100, PlayerId = Guid.NewGuid() });

            mockMapper.Setup(x => x.Map<Client.Bet>(It.IsAny<Core.Domain.Bet>()))
                .Returns(new Client.Bet { BetPoints = 100, Number = 3, AdditionalBalance = 100, PlayerId = Guid.NewGuid() });


            var mockLogger = new Mock<ILogger<GameController>>();
            mockLogger.Setup(x => x.Log(
                            It.IsAny<LogLevel>(),
                            It.IsAny<EventId>(),
                            It.IsAny<It.IsAnyType>(),
                            It.IsAny<Exception>(),
                            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));

            var controller = new GameController(mockLogger.Object, mockMapper.Object, mockRepo.Object);

            // Act
            var result = await controller.Post(new Api.Api.Models.BetRequest
            {
                Number = 1,
                Points = 100
            });

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task WhenPostingWrongBetPoints_ThenTheResultIsBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IGameManager>();
            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Core.Domain.Bet>()))
                .Returns(Task.FromResult<Core.Domain.Bet> (null));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Core.Domain.Bet>(It.IsAny<Client.Bet>()))
                .Returns(new Core.Domain.Bet { BetPoints = 100, Number = -1, Created = DateTime.Now, AdditionalBalance = 100, PlayerId = Guid.NewGuid() });

            mockMapper.Setup(x => x.Map<Client.Bet>(It.IsAny<Core.Domain.Bet>()))
                .Returns(new Client.Bet { BetPoints = 100, Number = -1, AdditionalBalance = 100, PlayerId = Guid.NewGuid() });

            var mockLogger = new Mock<ILogger<GameController>>();
            mockLogger.Setup(x => x.Log(
                            It.IsAny<LogLevel>(),
                            It.IsAny<EventId>(),
                            It.IsAny<It.IsAnyType>(),
                            It.IsAny<Exception>(),
                            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));

            var controller = new GameController(mockLogger.Object, mockMapper.Object, mockRepo.Object);

            // Act
            var result = await controller.Post(new Api.Api.Models.BetRequest
            {
                
            });

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
