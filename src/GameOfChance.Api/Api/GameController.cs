using AutoMapper;
using GameOfChance.Api.Api.Models;
using GameOfChance.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameOfChance.Api
{
    [Route("api/game")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGameManager _gameManager;
        public GameController(ILogger<GameController> logger, IMapper mapper, IGameManager gameManager)
        {
            Logger = logger;
            _mapper = mapper;
            _gameManager = gameManager;
        }

        public ILogger<GameController> Logger { get; }

        /// <summary>
        /// Retrieve game bet by id
        /// </summary>
        /// <param name="id">Game bet identifier</param>
        /// <returns>If exist, it returns the bet</returns>
        [HttpGet("{id}")]
        //[Route("{id:String}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IList<Client.Bet>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var bets = await _gameManager.GetByPlayerAsync(id);

            if (bets is null || bets.Count <= 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IList<Client.Bet>>(bets));
        }

        /// <summary>
        /// Creates a new stack 
        /// </summary>
        /// <param name="new game">Payload</param>
        /// <returns>If created, return the new game</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Client.Bet), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(BetRequest request)
        {
            if (request == null)
                return BadRequest();
            
            Client.Bet requestBet = request.ToBet();

            var bet = _mapper.Map<Core.Domain.Bet>(requestBet);

            bet = await _gameManager.CreateAsync(bet);

            if (bet is null)
            {
                return BadRequest(bet);
            }

            var betModel = _mapper.Map<Client.Bet>(bet);
            
            Logger.Log(LogLevel.Information, "Bet request is successful");

            return CreatedAtAction(nameof(Get), new { id = bet.Id },
                betModel.ToBetResponse());
        }
    }
}
