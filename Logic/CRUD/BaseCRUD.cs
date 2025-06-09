using Microsoft.EntityFrameworkCore;
using SQLSanitizorNator.Data;
using SQLSanitizorNator.Data.Interfaces;
using SQLSanitizorNator.Data.Models;

namespace SQLSanitizorNator.Logic.CRUD;

public interface IBaseCRUD<T, TKey> where T : EntityBase<TKey> where TKey : IEquatable<TKey>
{
    virtual Task<List<T>> GetAll(CancellationToken token = default) => throw new NotImplementedException();
    virtual Task<T?> GetById(TKey id, CancellationToken token = default) => throw new NotImplementedException();
    virtual Task<T> Create(T entity, CancellationToken token = default) => throw new NotImplementedException();
    virtual Task<T> Update(T entity, CancellationToken token = default) => throw new NotImplementedException();
    virtual Task<T> Delete(T entity, CancellationToken token = default) => throw new NotImplementedException();
    virtual Task<T> DeleteById(TKey id, CancellationToken token = default) => throw new NotImplementedException();
}

public class BaseCrud<T, TKey> : IBaseCRUD<T, TKey> where T : EntityBase<TKey> where TKey : IEquatable<TKey>
{
    protected readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public BaseCrud(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    public virtual async Task<ICollection<T>> GetAll(CancellationToken token = default)
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.Set<T>().ToListAsync(token);
    }

    public virtual async Task<T?> GetById(TKey id, CancellationToken token = default)
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.Set<T>().FindAsync(id, token);
    }

    public virtual async Task<T> Create(T entity, CancellationToken token = default)
    {
        using var context = _dbContextFactory.CreateDbContext();
        if (await context.Set<T>().AnyAsync(e => e.Id.Equals(entity.Id), token))
            throw new InvalidOperationException("Entity already exists.");

        var newEntity = context.Set<T>().Add(entity);
        await context.SaveChangesAsync(token);
        return newEntity.Entity;
    }
    public virtual async Task<T> Update(T entity, CancellationToken token = default)
    {
        using var context = _dbContextFactory.CreateDbContext();
        var updatedEntity = context.Set<T>().Update(entity);
        await context.SaveChangesAsync(token);
        return updatedEntity.Entity;
    }

    public virtual async Task<T> Delete(T entity, CancellationToken token = default)
    {
        using var context = _dbContextFactory.CreateDbContext();
        var deleted = context.Set<T>().Remove(entity);
        await context.SaveChangesAsync(token);
        return deleted.Entity;
    }

    public virtual async Task<T> DeleteById(TKey id, CancellationToken token = default)
    {
        using var context = _dbContextFactory.CreateDbContext();
        var entity = await context.Set<T>().FindAsync(id, token) ?? throw new KeyNotFoundException("Entity with specified Id not found");
        //if (entity is null) return null;

        var deleted = context.Set<T>().Remove(entity);
        await context.SaveChangesAsync(token);
        return deleted.Entity;
    }
}
