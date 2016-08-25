namespace AgeRanger.Data.Helpers
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Repositories;

  public class RepositoryFactories
  {
    private readonly IDictionary<Type, Func<DbContext, object>> _repositoryFactories;

    private IDictionary<Type, Func<DbContext, object>> GetArFactories()
    {
      return new Dictionary<Type, Func<DbContext, object>>
      {
        {
          typeof(IPersonsRepository), dbContext => new PersonsRepository(dbContext)
        },
        {
          typeof(IAgeGroupsRepository), dbContext => new AgeGroupsRepository(dbContext)
        }


      };
    }
    public RepositoryFactories()
    {
      this._repositoryFactories = this.GetArFactories();
    }



   
    public RepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories)
    {
      this._repositoryFactories = factories;
    }

   
    public Func<DbContext, object> GetRepositoryFactory<T>()
    {
      Func<DbContext, object> factory;
      this._repositoryFactories.TryGetValue(typeof(T), out factory);
      return factory;
    }

   
    public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
    {
      return this.GetRepositoryFactory<T>() ?? this.DefaultEntityRepositoryFactory<T>();
    }

    /// <summary>
    /// Default factory for a <see cref="IRepository{T}"/> where T is an entity.
    /// </summary>
    /// <typeparam name="T">Type of the repository's root entity</typeparam>
    protected virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
    {
      return dbContext => new AgRepository<T>(dbContext);
    }
  
  }
}