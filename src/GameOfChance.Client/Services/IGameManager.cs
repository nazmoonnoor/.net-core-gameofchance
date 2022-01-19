using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfChance.Client.Services
{
    /// <summary>
    /// Represents Game functionalities
    /// </summary>
    public interface IGameManager
    {
        Task<IList<Core.Domain.Bet>> GetByPlayerAsync(string id);
        Task<Core.Domain.Bet> CreateAsync(Core.Domain.Bet bet);
    }
}
