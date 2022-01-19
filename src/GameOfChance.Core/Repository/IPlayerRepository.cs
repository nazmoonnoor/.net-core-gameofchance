using GameOfChance.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfChance.Core.Repository
{
    public interface IPlayerRepository
    {
        Task<Player> CreateAsync(Player player, CancellationToken cancellationToken = default);
        Task<Player> UpdateAsync(Player player, CancellationToken cancellation = default);
        Task<bool> DeleteAsync(Player player, CancellationToken cancellation = default);
        Task<Player> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
        Task<Player> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IList<Player>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
