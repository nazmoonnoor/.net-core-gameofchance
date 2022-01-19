using GameOfChance.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfChance.Core.Repository
{
    public interface IBetRepository
    {
        Task<Bet> CreateAsync(Bet bet, CancellationToken cancellationToken = default);
        Task<Bet> UpdateAsync(Bet bet, CancellationToken cancellation = default);
        Task<bool> DeleteAsync(Bet bet, CancellationToken cancellation = default);
        Task<Bet> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<Bet>> GetAllByPlayerAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
