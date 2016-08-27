namespace AgeRanger.Data.Helpers
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using AgeRanger.Data.Contracts;
  using System.Linq;

  public class RepositoryProvider : IRepositoryProvider
  {
    private RepositoryFactories _repositoryFactories; 

    protected Dictionary<Type, object> Repositories { get; private set; }

    public RepositoryProvider(RepositoryFactories repositoryFactories)
    {
      this._repositoryFactories = repositoryFactories;
      this.Repositories = new Dictionary<Type, object>();
    }

    public DbContext DbContext { get; set; }

    public IRepository<T> GetRepositoryForEntityType<T>() where T : class
    {
      return this.GetRepository<IRepository<T>>(
        this._repositoryFactories.GetRepositoryFactoryForEntityType<T>());
    }

    
    public virtual T GetRepository<T>(Func<DbContext, object> factory = null) where T : class
    {
      // Look for T dictionary cache under typeof(T).
      object repoObj;
      this.Repositories.TryGetValue(typeof(T), out repoObj);
      if (repoObj != null)
      {
        return (T)repoObj;
      }

      // Not found or null; make one, add to dictionary cache, and return it.
      return this.MakeRepository<T>(factory, this.DbContext);
    }
      
    protected virtual T MakeRepository<T>(Func<DbContext, object> factory, DbContext dbContext)
    {
      var f = factory ?? this._repositoryFactories.GetRepositoryFactory<T>();
      if (f == null)
      {
        throw new NotImplementedException("No factory for repository type, " + typeof(T).FullName);
      }
      var repo = (T)f(dbContext);
      this.Repositories[typeof(T)] = repo;
      return repo;
    }

    
    public void SetRepository<T>(T repository)
    {
      this.Repositories[typeof(T)] = repository;
    }

    
  }
}