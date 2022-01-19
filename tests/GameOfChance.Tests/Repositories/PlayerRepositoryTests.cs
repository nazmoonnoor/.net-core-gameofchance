using GameOfChance.Core.Domain;
using GameOfChance.Core.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfChance.Tests.Repositories
{
    public class PlayerRepositoryTests
    {
        Guid playerId = Guid.NewGuid();
        public IPlayerRepository MockPlayerRepository = null;
        
        private Player GetPlayer()
        {
            return new Player()
            {
                Id= Guid.NewGuid(),
                Created = DateTime.Now,
                FullName = "Test Player 4",
                Email = "testplayer4@test.com",
                AccountBalance = 100000
            };
        }
        /// <summary>
        /// Setup the player repository
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // ARRANGE
            IList<Player> players = new List<Player>
                {
                    new Player { Id = playerId, FullName = "Test Player 1",
                        Email = "testplayer1@test.com", AccountBalance = 100000, Created = System.DateTime.Now },
                    new Player { Id = Guid.NewGuid(), FullName = "Test Player 2",
                        Email = "testplayer2@test.com", AccountBalance = 100000, Created = System.DateTime.Now },
                    new Player { Id = Guid.NewGuid(), FullName = "Test Player 3",
                        Email = "testplayer3@test.com", AccountBalance = 100000, Created = System.DateTime.Now },

                };

            // Mock the Player Repository using Moq
            Mock<IPlayerRepository> mockPlayerRepository = new Mock<IPlayerRepository>();

            // Return all the players
            mockPlayerRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(players));

            // return a player by Id
            mockPlayerRepository.Setup(mr => mr.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(players.FirstOrDefault()));

            // Allows us to test saving a player
            mockPlayerRepository.Setup(mr => mr.CreateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetPlayer()));

            // Allows us to test saving a player
            mockPlayerRepository.Setup(mr => mr.UpdateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetPlayer()));

            // Complete the setup
            this.MockPlayerRepository = mockPlayerRepository.Object;
        }

        /// <summary>
        /// Gets by id return a player
        /// </summary>
        [Test]
        public void CanReturnPlayerById()
        {
            // ACT
            Player testPlayer = this.MockPlayerRepository.GetByIdAsync(playerId, It.IsAny<CancellationToken>()).Result;

            // ASSERT
            Assert.IsNotNull(testPlayer);
            Assert.IsInstanceOf(typeof(Player), testPlayer);
            Assert.AreEqual("Test Player 1", testPlayer.FullName);
        }

        /// <summary>
        /// Gets all players
        /// </summary>
        [Test]
        public void CanReturnAllPlayers()
        {
            // ACT
            var players = this.MockPlayerRepository.GetAllAsync(It.IsAny<CancellationToken>()).Result;

            // ASSERT
            Assert.IsNotNull(players);
            Assert.IsInstanceOf(typeof(IList<Player>), players);
            Assert.True(players.Count > 0);
        }

        /// <summary>
        /// Can create a player
        /// </summary>
        [Test]
        public void CanCreatePlayer()
        {
            // ACT
            var player = this.MockPlayerRepository.CreateAsync(new Player(), It.IsAny<CancellationToken>()).Result;

            // ASSERT
            Assert.IsNotNull(player);
            Assert.IsInstanceOf(typeof(Player), player);
            Assert.False(player.Id == Guid.Empty);
        }

        /// <summary>
        /// Can update a player
        /// </summary>
        [Test]
        public void CanUpdatePlayer()
        {
            // ACT
            var player = this.MockPlayerRepository.UpdateAsync(new Player(), It.IsAny<CancellationToken>()).Result;

            // ASSERT
            Assert.IsNotNull(player);
            Assert.IsInstanceOf(typeof(Player), player);
            Assert.False(player.Id == Guid.Empty);
        }
    }
}
