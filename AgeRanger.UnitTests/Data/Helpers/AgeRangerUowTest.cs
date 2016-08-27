namespace AgeRanger.UnitTests.Data.Helpers
{
  using AgeRanger.Data;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Helpers;
  using AgeRanger.Data.Models;
  using AgeRanger.Data.Repositories;
  using Moq;
  using NUnit.Framework;
  using Should;

  [TestFixture]
  public class AgeRangerUowTest
  {
    private Mock<IRepositoryProvider> repositoryProvider = new Mock<IRepositoryProvider>();
    private AgeRangerUow uow;
    private Mock<AgeRangerDbContext> dbContext = new Mock<AgeRangerDbContext>();

   [SetUp]
    public void Setup()
    {
     var agRepository = new AgRepository<Person>(dbContext.Object);
     var agGroupRepository = new AgRepository<AgeGroup>(dbContext.Object);
     var personRepository = new AgRepository<PersonsRepository>(dbContext.Object);
     var ageGroupRepository = new AgRepository<AgeGroupsRepository>(dbContext.Object);

     this.repositoryProvider.Setup(s => s.GetRepositoryForEntityType<Person>()).Returns(agRepository);
     this.repositoryProvider.Setup(s => s.GetRepositoryForEntityType<AgeGroup>()).Returns(agGroupRepository);
     this.repositoryProvider.Setup(s => s.GetRepositoryForEntityType<PersonsRepository>()).Returns(personRepository);
     this.repositoryProvider.Setup(s => s.GetRepositoryForEntityType<AgeGroupsRepository>()).Returns(ageGroupRepository);

     this.uow = new AgeRangerUow(this.repositoryProvider.Object);

    }

    [Test]
    public void RequestToPersonsPropertyShouldReturnAgRepositoryOfPerson()
    {
      var repo = this.uow.Persons;

      repo.ShouldBeType(typeof(AgRepository<Person>));

    }

    [Test]
    public void RequestToAgeGroupsPropertyShouldReturnAgRepositoryOfAgeGroup()
    {
      var repo = this.uow.AgeGroups;

      repo.ShouldBeType(typeof(AgRepository<AgeGroup>));

    }
  }
}
