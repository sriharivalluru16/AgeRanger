namespace AgeRanger.UnitTests.Data.Helpers
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using AgeRanger.Data;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Helpers;
  using AgeRanger.Data.Repositories;
  using Moq; 
  using NUnit.Framework;


  public class RepositoryFactoriesTest
  { 
    protected Mock<IDictionary<Type, Func<DbContext, object>>> Dbs= new Mock<IDictionary<Type, Func<DbContext, object>>>();
    protected RepositoryFactories Factories;
    protected Func<DbContext, object> PersonRespository = dbContext => new PersonsRepository(dbContext);
    protected Func<DbContext, object> AgeGroupRepository = dbContext => new AgeGroupsRepository(dbContext);
    protected Mock<AgeRangerDbContext> DbContext = new Mock<AgeRangerDbContext>();

    [SetUp]
    public void Setup()
    {
       this.Factories = new RepositoryFactories(this.Dbs.Object);
       this.Dbs.Object.Add(new KeyValuePair<Type, Func<DbContext, object>>(typeof(IPersonsRepository),
            dbContext => new PersonsRepository(dbContext)));
        
         this.Dbs.Object.Add( new KeyValuePair<Type, Func<DbContext, object>> (typeof(IAgeGroupsRepository),
           dbContext => new AgeGroupsRepository(dbContext)));

       this.Dbs.Setup(s => s.TryGetValue(typeof(IPersonsRepository), out this.PersonRespository));
       this.Dbs.Setup(s => s.TryGetValue(typeof(IAgeGroupsRepository), out this.AgeGroupRepository));
    }

  }
}
