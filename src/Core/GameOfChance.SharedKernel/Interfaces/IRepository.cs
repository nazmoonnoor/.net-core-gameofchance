using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfChance.SharedKernel.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(Guid id) where T : EntityBase, IAggregateRoot;
        Task<List<T>> ListAsync<T>() where T : EntityBase, IAggregateRoot;
        Task<T> AddAsync<T>(T entity) where T : EntityBase, IAggregateRoot;
        Task UpdateAsync<T>(T entity) where T : EntityBase, IAggregateRoot;
        Task DeleteAsync<T>(T entity) where T : EntityBase, IAggregateRoot;
    }
 }
