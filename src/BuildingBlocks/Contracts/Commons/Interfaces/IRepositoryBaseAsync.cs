﻿using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq.Expressions;

namespace Contracts.Commons.Interfaces
{
    public interface IRepositoryQueryBase<T, K>
       where T : EntityBase<K>
    {
        IQueryable<T> FindAll(bool trackChanges = false);

        IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
            params Expression<Func<T, object>>[] includeProperties);

        Task<T?> GetByIdAsync(K id);

        Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
    }

    public interface IRepositoryBaseAsync<T, K> : IRepositoryQueryBase<T, K>
        where T : EntityBase<K>
    {
        K Create(T entity);

        Task<K> CreateAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetById(K id);

        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> where);

        IList<K> CreateList(IEnumerable<T> entities);

        Task<bool>IsExist(K id);

        Task<IList<K>> CreateListAsync(IEnumerable<T> entities);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void UpdateList(IEnumerable<T> entities);

        Task UpdateListAsync(IEnumerable<T> entities);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        void DeleteList(IEnumerable<T> entities);

        Task DeleteListAsync(IEnumerable<T> entities);

        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task EndTransactionAsync();

        Task RollbackTransactionAsync();

        Task SaveAsync();

        void Save();
    }

    public interface IRepositoryBaseAsync<T, K, TContext>
        : IRepositoryBaseAsync<T, K>
        where T : EntityBase<K>
        where TContext : DbContext
    {
    }

    public interface IRepositoryQueryBase<T, K, TContext>
        : IRepositoryQueryBase<T, K>
        where T : EntityBase<K>
        where TContext : DbContext
    {
    }
}