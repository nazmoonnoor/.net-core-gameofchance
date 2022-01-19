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
    // Ef core implementation for IBetRepository
    // IBettRepository and Bet domain is an abstruction layer and has no knowledge of ef infrastructure
    public class BetRepository : IBetRepository
    {
        private ApplicationDbContext Context;
        public BetRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public async Task<Bet> CreateAsync(Bet bet, CancellationToken cancellationToken = default)
        {
            try
            {
                await Context.Set<Bet>().AddAsync(bet);
                await Context.SaveChangesAsync();

                return bet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Bet> UpdateAsync(Bet bet, CancellationToken cancellation = default)
        {
            try
            {
                Context.Entry(bet).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return bet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(Bet bet, CancellationToken cancellation = default)
        {
            try
            {
                var entity = Context.Set<Bet>().Remove(bet);
                await Context.SaveChangesAsync();
                return entity.State == EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<Bet>> GetAllByPlayerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await Context.Set<Bet>().Where(b=>b.PlayerId == id).ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Bet> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentNullException("Identifier is not provided.");

                var response = await Context.Set<Bet>().SingleOrDefaultAsync(e => e.Id == id);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
