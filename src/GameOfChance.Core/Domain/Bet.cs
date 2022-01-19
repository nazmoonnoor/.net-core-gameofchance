using GameOfChance.SharedKernel;
using GameOfChance.SharedKernel.Interfaces;
using System;

namespace GameOfChance.Core.Domain
{
    public class Bet: EntityBase, IAggregateRoot
    {
        public Guid? PlayerId { get; set; }
        public int BetPoints { get; set; }
        public int Number { get; set; }
        public bool Status { get; set; }
        public int AdditionalBalance { get; set; }
        public Player Player { get; set; }
    }
}
