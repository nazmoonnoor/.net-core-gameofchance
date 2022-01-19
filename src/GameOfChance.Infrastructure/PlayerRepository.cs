using GameOfChance.Core.Domain;
using GameOfChance.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfChance.Infrastructure
{
    // Ef core implementation for IPlayerRepository
    // IPlayerRepository and Player domain is an abstruction layer and has no knowledge of ef infrastructure
    public class PlayerRepository : IPlayerRepository
    {
        public ApplicationDbContext Context { get; }

        public PlayerRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public async Task<Player> CreateAsync(Player player, CancellationToken cancellationToken = default)
        {
            try
            {
                await Context.Set<Player>().AddAsync(player);
                await Context.SaveChangesAsync();

                return player;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Player> UpdateAsync(Player player, CancellationToken cancellation = default)
        {
            try
            {
                Context.Entry(player).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return player;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(Player player, CancellationToken cancellation = default)
        {
            try
            {
                var entity = Context.Set<Player>().Remove(player);
                await Context.SaveChangesAsync();
                return entity.State == EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<Player>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await Context.Set<Player>().ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Player> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentNullException("Identifier is not provided.");

                var response = await Context.Set<Player>().SingleOrDefaultAsync(e => e.Id == id);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Player> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    throw new ArgumentNullException("Email is not provided.");

                var response = await Context.Set<Player>().SingleOrDefaultAsync(e => e.Email == email);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
