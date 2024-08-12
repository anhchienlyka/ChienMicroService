using Contracts.Commons.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Infrastructure.Common;

public class RepositoryBaseAsync<T, K, TContext> : RepositoryQueryBaseAsync<T, K, TContext>,
    IRepositoryBaseAsync<T, K, TContext>
    where T : EntityBase<K>
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly IUnitOfWork<TContext> _unitOfWork;

    public RepositoryBaseAsync(TContext context, IUnitOfWork<TContext> unitOfWork) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(_context));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork));
    }

    public async Task<K> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        await SaveChangesAsync();
        return entities.Select(x => x.Id).ToList();
    }

    public async Task UpdateAsync(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Unchanged)
            return;
        T exist = _context.Set<T>().Find(entity.Id);
        _context.Entry(exist).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateListAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteListAsync(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
        await SaveChangesAsync();
    }

    public Task<int> SaveChangesAsync()
    {
        return _unitOfWork.CommitAsync();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public async Task EndTransactionAsync()
    {
        await SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public K Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity.Id;
    }

    public IList<K> CreateList(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        return entities.Select(x => x.Id).ToList();
    }

    public void Update(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Unchanged) return;
        T exist = _context.Set<T>().Find(entity.Id);
        _context.Entry(exist).CurrentValues.SetValues(entity);
    }

    public void UpdateList(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void DeleteList(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(K id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> where)
    {
        return await _context.Set<T>().Where(where).ToListAsync();
    }

    public async Task<bool> IsExist(K id)
    {
        return await _context.Set<T>().AnyAsync(x => x.Id.Equals(id));
    }
}

public class RepositoryQueryBaseAsync<T, K, TContext> : IRepositoryQueryBase<T, K, TContext>
where T : EntityBase<K>
where TContext : DbContext
{
    private readonly TContext _context;

    public RepositoryQueryBaseAsync(TContext dbContext)
    {
        _context = dbContext ?? throw new ArgumentException(nameof(dbContext));
    }

    public IQueryable<T> FindAll(bool trackChanges = false)
    {
        return !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
    }

    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindAll(trackChanges);

        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
    {
        return !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindByCondition(expression, trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public async Task<T?> GetByIdAsync(K id)
    {
        return await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
    {
        return await FindByCondition(x => x.Id.Equals(id), false, includeProperties).FirstOrDefaultAsync();
    }
}