using GameOfChance.SharedKernel;
using GameOfChance.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfChance.Core.Domain
{
    public class Player : EntityBase, IAggregateRoot
    {
        public Player(int accountBalance = 10000)
        {
            AccountBalance = accountBalance;
        }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int AccountBalance { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
