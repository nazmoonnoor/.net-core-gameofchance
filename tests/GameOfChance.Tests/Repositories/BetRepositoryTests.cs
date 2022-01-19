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
    public class BetRepositoryTests
    {
        Guid betId = Guid.NewGuid();    
        Guid playerId = Guid.NewGuid();
        public IBetRepository MockBetRepository = null;

        private Player GetPlayer()
        {
            return new Player()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                FullName = "Test Player 4",
                Email = "testplayer4@test.com",
                AccountBalance = 100000
            };
        }
        private Bet GetBet()
        {
            return new Bet()
            {
                Id = betId,
                Created = DateTime.Now,
                PlayerId = playerId,
                BetPoints = 100,
                Number = 1,
                AdditionalBalance = 900,
                Player = GetPlayer()
            };
        }
        /// <summary>
        /// Setup the bet repository
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // ARRANGE
            IList<Bet> bets = new List<Bet>
                {
                    new Bet() { Id = betId, PlayerId = playerId, BetPoints = 100, Number=1, Created = System.DateTime.Now },
                    new Bet()  { Id = Guid.NewGuid(), PlayerId = Guid.NewGuid(), BetPoints = 100, Number=1, Created = System.DateTime.Now },
                    new Bet()  { Id = Guid.NewGuid(), PlayerId = Guid.NewGuid(), BetPoints = 100, Number=1, Created = System.DateTime.Now },

                };

            // Mock the Bet Repository using Moq
            Mock<IBetRepository> mockBetRepository = new Mock<IBetRepository>();

            // Return all the bets
            mockBetRepository.Setup(x => x.GetAllByPlayerAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(bets));

            // return a bet by Id
            mockBetRepository.Setup(mr => mr.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(bets.FirstOrDefault()));

            // Allows us to test saving a bet
            mockBetRepository.Setup(mr => mr.CreateAsync(It.IsAny<Bet>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetBet()));

            // Allows us to test saving a bet
            mockBetRepository.Setup(mr => mr.UpdateAsync(It.IsAny<Bet>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetBet()));

            // Complete the setup
            this.MockBetRepository = mockBetRepository.Object;
        }

        /// <summary>
        /// Gets by id return a bet
        /// </summary>
        [Test]
        public void CanReturnBetById()
        {
            // ACT
            Bet testBet = this.MockBetRepository.GetByIdAsync(betId, It.IsAny<CancellationToken>()).Result;

            // ASSERT
            Assert.IsNotNull(testBet);
            Assert.IsInstanceOf(typeof(Bet), testBet);
            Assert.AreEqual(betId, testBet.Id);
        }

        /// <summary>
        /// Gets all bets
        /// </summary>
        [Test]
        public void CanReturnAllBets()
        {
            // ACT
            var bets = this.MockBetRepository.GetAllByPlayerAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()).Result;

            // ASSERT
            Assert.IsNotNull(bets);
            Assert.IsInstanceOf(typeof(IList<Bet>), bets);
            Assert.True(bets.Count > 0);
        }

        /// <summary>
        /// Can create a bet
        /// </summary>
        [Test]
        public void CanCreateBet()
        {
            // ACT
            var bet = this.MockBetRepository.CreateAsync(GetBet(), It.IsAny<CancellationToken>()).Result;

            // ASSERT
            Assert.IsNotNull(bet);
            Assert.IsInstanceOf(typeof(Bet), bet);
            Assert.False(bet.Id == Guid.Empty);
        }

        /// <summary>
        /// Can update a bet
        /// </summary>
        [Test]
        public void CanUpdateBet()
        {
            // ACT
            var bet = this.MockBetRepository.UpdateAsync(GetBet(), It.IsAny<CancellationToken>()).Result;

            // ASSERT
            Assert.IsNotNull(bet);
            Assert.IsInstanceOf(typeof(Bet), bet);
            Assert.False(bet.Id == Guid.Empty);
        }
    }
}
