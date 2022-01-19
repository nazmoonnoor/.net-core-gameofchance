using System;
using NUnit.Framework;
using GameOfChance.Core.Domain;
using GameOfChance.SharedKernel.Interfaces;
using Moq;
using GameOfChance.Core;

namespace GameOfChance.Tests
{
    public class GameOfChanceTests
    {
        private Player player;
        private Bet bet;
        private Game game;

        /// <summary>
        /// Setup the Game logic
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var randomGenerator = new Mock<IRandomGenerator>();
            
            randomGenerator
                .Setup(c => c.Generate(It.IsAny<int>()))
                .Returns(11);

            // ARRANGE
            player = new Player()
            {
                Id = Guid.NewGuid(),
                Email = "testplayer@test.com",
                FullName = "Test Player",
                Created = DateTime.Now
            };

            bet = new Bet();
            bet.Player = player;

        }

        /// <summary>
        /// Playing game should update the balance
        /// </summary>
        [Test]
        public void PlayingGameUpdatesBalance()
        {
            // ARRANGE
            var randomGenerator = new Mock<IRandomGenerator>();

            randomGenerator
                .Setup(c => c.Generate(It.IsAny<int>()))
                .Returns(3);

            game = new Game(randomGenerator.Object)
            {
                Player = player,
                Bet = bet
            };

            int initialBalance = player.AccountBalance;

            // ACT
            game.Play(500, 2);

            // ASSERT
            Assert.AreNotEqual(player.AccountBalance, initialBalance);
        }

        /// <summary>
        /// Playing a negative point or out of range number should throws
        /// </summary>
        [Test]
        public void PlayingNegativePointsOrOutOfRangeNumberThrows()
        {
            // ARRANGE
            var randomGenerator = new Mock<IRandomGenerator>();

            randomGenerator
                .Setup(c => c.Generate(It.IsAny<int>()))
                .Returns(10);

            game = new Game(randomGenerator.Object)
            {
                Player = player,
                Bet = bet
            };

            // ACT + ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => game.Play(-500, 2));

            Assert.Throws<ArgumentOutOfRangeException>(() => game.Play(500, -2));

            Assert.Throws<ArgumentOutOfRangeException>(() => game.Play(500, 11));
        }

        /// <summary>
        /// When bet is successful updates the balance nine times
        /// </summary>
        [Test]
        public void WhenBetIsSuccessfulUpdatesBalanceNineTimes()
        {
            // ARRANGE
            var randomGenerator = new Mock<IRandomGenerator>();

            randomGenerator
                .Setup(c => c.Generate(It.IsAny<int>()))
                .Returns(3);

            game = new Game(randomGenerator.Object)
            {
                Player = player,
                Bet = bet
            };

            int initialBalance = player.AccountBalance;

            // ACT
            game.Play(500, 3);

            // ASSERT
            Assert.AreEqual(bet.AdditionalBalance, 500 * 9);
            Assert.AreEqual(player.AccountBalance, initialBalance + (500 * 9));
        }

        /// <summary>
        /// When bet is unsuccessful, points gets deduct from the balance
        /// </summary>
        [Test]
        public void WhenBetIsUnsuccessfullPointsGetDeductFromBalance()
        {
            // ARRANGE
            var randomGenerator = new Mock<IRandomGenerator>();

            randomGenerator
                .Setup(c => c.Generate(It.IsAny<int>()))
                .Returns(4);

            game = new Game(randomGenerator.Object)
            {
                Player = player,
                Bet = bet
            };

            int initialBalance = player.AccountBalance;

            // ACT
            game.Play(500, 3);

            // ASSERT
            Assert.AreEqual(bet.AdditionalBalance, -500);
            Assert.AreEqual(player.AccountBalance, initialBalance - 500);
        }
    }
}