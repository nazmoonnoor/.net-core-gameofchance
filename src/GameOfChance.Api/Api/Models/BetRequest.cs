using System;

namespace GameOfChance.Api.Api.Models
{
    public class BetRequest
    {
        public int Points { get; set; }
        public int Number { get; set; }

        public Client.Bet ToBet()
        {
            return new Client.Bet
            {
                BetPoints = this.Points,
                Number = this.Number
            };
        }
    }
}
