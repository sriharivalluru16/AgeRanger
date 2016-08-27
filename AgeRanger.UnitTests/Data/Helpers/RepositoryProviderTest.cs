namespace AgeRanger.UnitTests.Data.Helpers
{
  using System;
  using System.Data.Entity;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Helpers;
  using AgeRanger.Data.Models;
  using AgeRanger.Data.Repositories;
  using NUnit.Framework;
  using Should;
  using Should.Core.Exceptions;

  [TestFixture]
  public class RepositoryProviderTest : RepositoryFactoriesTest
  {
    private RepositoryProvider provider;
    [SetUp]
    public void RepositoryProviderTestSetup()
    {
      base.Setup();
      this.provider = new RepositoryProvider(this.Factories);
      this.provider.SetRepository<IPersonsRepository>(new PersonsRepository(this.DbContext.Object));
      this.provider.DbContext = this.DbContext.Object;
    }

    [Test]
    public void RequestForIPersonRepositoryShouldReurnPersonRepository()
    {
      var actual = this.provider.GetRepository<IPersonsRepository>();

      actual.ShouldBeType(typeof(PersonsRepository));
    }

    [Test]
    public void RequestForIAgeGroupsRepositoryShouldReurnAgeGroupRepository()
    {
      var actual = this.provider.GetRepository<IAgeGroupsRepository>();

      actual.ShouldBeType(typeof(AgeGroupsRepository));
    }
     
    [Test]
    public void RequestForRepositoryThatDoesNotExistReturnException()
    {
      var result = Assert.Throws<NotImplementedException>(delegate
      {
        this.provider.GetRepository<IUser>();
      });

      Assert.That(result.Message, Is.EqualTo("No factory for repository type, " + typeof(IUser).FullName));
    }

    [Test]
    public void RequestForAgeGroupEntityTypeRepositoryShouldReturnDefaultRepoForAgeGroup()
    {
      var actual = this.provider.GetRepositoryForEntityType<AgeGroup>();

      actual.ShouldBeType(typeof(AgRepository<AgeGroup>));
    }

    [Test]
    public void RequestForPersonEntityTypeRepositoryShouldReturnDefaultRepoForPerson()
    {
      var actual = this.provider.GetRepositoryForEntityType<Person>();

      actual.ShouldBeType(typeof(AgRepository<Person>));
    }

    [Test]
    public void RequestForIPersonRepositoryFromFactoryShouldReurnPersonRepository()
    {
      var actual = this.Factories.GetRepositoryFactory<IPersonsRepository>();

      actual.ShouldEqual(this.PersonRespository);
    }

    [Test]
    public void RequestForIAgeGroupsRepositoryFromFactoryShouldReurnAgeGroupRepository()
    {
      var actual = this.Factories.GetRepositoryFactory<IAgeGroupsRepository>();

      actual.ShouldEqual(this.AgeGroupRepository);
    }

    [Test]
    public void RequestForUnKnownRepositoryFromFactoryShouldReurnNull()
    {
      var actual = this.Factories.GetRepositoryFactory<IUser>();

      actual.ShouldBeNull();
    }

    [Test]
    public void RequestForIAgeGroupsRepositoryFromEntityFactoryShouldReurnAgeGroupRepository()
    {
      var actual = this.Factories.GetRepositoryFactoryForEntityType<IAgeGroupsRepository>();

      actual.ShouldEqual(this.AgeGroupRepository);
    }

    [Test]
    public void RequestForRepositoryOfPersonFromEntityFactoryShouldReurnAgRepositoryOfPerson()
    {
      var repository = this.Factories.GetRepositoryFactoryForEntityType<Person>();
      var actual = repository(this.DbContext.Object);
      actual.ShouldBeType(typeof(AgRepository<Person>));
    }

    [Test]
    public void RequestForRepositoryOfAgeGroupFromEntityFactoryShouldReurnAgRepositoryOfAgeGroup()
    {
      var repository = this.Factories.GetRepositoryFactoryForEntityType<AgeGroup>();
      var actual = repository(this.DbContext.Object);
      actual.ShouldBeType(typeof(AgRepository<AgeGroup>));
    }
  }
}
