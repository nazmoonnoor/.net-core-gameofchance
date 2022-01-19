using GameOfChance.Core.Domain;
using GameOfChance.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChance.Core
{
    public interface IGame
    {
        Player Player { get; set; }
        Bet Bet { get; set; }
        Task Play(int points, int number);
    }

    public class Game: IGame
    {
        public Player Player { get; set; }
        public Bet Bet { get; set; }
        public IRandomGenerator RandomGenerator { get; }

        public Game(IRandomGenerator randomGenerator)
        {
            RandomGenerator = randomGenerator;
        }

        public Task Play(int points, int number)
        {
            if (points < 0)
                throw new ArgumentOutOfRangeException(nameof(points));

            if (number > 9 || number < 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            int random = RandomGenerator.Generate(9);

            if (number == random)
                Add(points);
            else
                Remove(points);

            return Task.CompletedTask;
        }

        private void Add(int points)
        {
            if (points < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(points));
            }

            Bet.AdditionalBalance = points * 9;
            Bet.Status = true;
            Player.AccountBalance += Bet.AdditionalBalance;
        }

        private void Remove(int points)
        {
            if (points < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(points));
            }

            Bet.AdditionalBalance = -points;
            Bet.Status = false;
            Player.AccountBalance += Bet.AdditionalBalance;
        }
    }
}
