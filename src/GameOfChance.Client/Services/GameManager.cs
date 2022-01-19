using GameOfChance.Core;
using GameOfChance.Core.Repository;
using GameOfChance.SharedKernel;
using GameOfChance.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChance.Client.Services
{
    public class GameManager : IGameManager
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IGame game;

        public IBetRepository betRepository { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        public GameManager(IHttpContextAccessor httpContextAccessor, IBetRepository betRepository, 
            IPlayerRepository playerRepository,
            IGame game)
        {
            HttpContextAccessor = httpContextAccessor;
            this.betRepository = betRepository;
            this.playerRepository = playerRepository;
            this.game = game;
        }


        public async Task<Core.Domain.Bet> CreateAsync(Core.Domain.Bet bet)
        {
            if(bet == null)
                throw new ArgumentNullException("Request cannot be empty");
            
            if (bet.Number < 0)
                throw new ArgumentException("Number cannot be less than 0");

            if (bet.Number > 9)
                throw new ArgumentException("Number cannot be greater than 9");

            if(bet.BetPoints <= 0)
                throw new ArgumentException("Points should be greater than 0");

            try
            {
                // Get player
                var user = HttpContextAccessor.HttpContext.User;
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                string email = user.FindFirst(ClaimTypes.Email).Value;

                var player = await playerRepository.GetByEmailAsync(email);
                bet.PlayerId = player.Id;
                bet.Id = Guid.NewGuid();
                bet.Created = DateTime.Now;

                // Play
                game.Player = player;
                game.Bet = bet;

                //var game = new Game(player, bet, randomGenerator);
                await game.Play(bet.BetPoints, bet.Number);

                // Update player account balance
                await betRepository.CreateAsync(game.Bet);

                // Return bet
                return game.Bet;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IList<Core.Domain.Bet>> GetByPlayerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Identifier cannot be empty");
            
            Guid playerId = Guid.Empty;
            if (!Guid.TryParse(id, out playerId))
                throw new ArgumentNullException("Identifier is invalid");
            try 
            {
                var bets = await betRepository.GetAllByPlayerAsync(playerId);
                return bets;
            }
            catch
            {
                throw;
            }
        }
    }
}
