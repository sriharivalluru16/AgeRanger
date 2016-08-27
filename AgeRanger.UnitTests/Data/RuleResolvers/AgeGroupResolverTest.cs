namespace AgeRanger.UnitTests.Data.RuleResolvers
{
  using System.Collections.Generic;
  using System.Linq;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Models;
  using AgeRanger.Data.RuleResolvers;
  using Moq;
  using NUnit.Framework;
  using Should;

  [TestFixture]
  public class AgeGroupResolverTest
  { 
    private Mock<IAgeRangerUow> uow = new Mock<IAgeRangerUow>();

    [SetUp]
    public void Setup()
    {
      var ageGroups = new List<AgeGroup>
      {
          new AgeGroup{MinAge = null, MaxAge = 10,Description = "UpToTen"},
          new AgeGroup{MinAge = 10, MaxAge = 20,Description = "TenToTwenty"},
          new AgeGroup{MinAge = 20, MaxAge = 30,Description = "TwentyToThirty"},
          new AgeGroup{MinAge = 14, MaxAge = 25,Description = "Teenager"}
      };

      this.uow.Setup(x => x.AgeGroups.GetAll()).Returns(ageGroups.AsQueryable());

    }

    [Test]
    public void AllMatchingAgeGroupNamesBeShouldReturnedForPersonWhoFallsInMultipleGroups()
    {
      var resolver = new AgeGroupResolver<Person>(this.uow.Object);
      var person = new Person
      {
        Age = 15
      };
      var expectedRules = new []
      {
        "TenToTwenty", "Teenager"
      };
               
      var rules = resolver.MatchingRules(person);

      rules.ForEach(r => expectedRules.ShouldContain(r.Name));

    }

    [Test]
    public void SingleAgeGroupNamesBeShouldReturnedForPersonWhoFallsInOneGroups()
    {
      var resolver = new AgeGroupResolver<Person>(this.uow.Object);
      var person = new Person
      {
        Age = 25
      };
      var expectedRules = new[]
      {
        "TwentyToThirty"
      };

      var rules = resolver.MatchingRules(person);

      rules.ForEach(r => expectedRules.ShouldContain(r.Name));

    }
  }
}
