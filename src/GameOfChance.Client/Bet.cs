using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChance.Client
{
    public class Bet
    {
        public Guid PlayerId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int BetPoints { get; set; }


        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Number { get; set; }

        public bool Status { get; set; }

        public int AdditionalBalance { get; set; }

        public Player Player { get; set; }
    }
}
