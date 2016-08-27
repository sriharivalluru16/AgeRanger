namespace AgeRanger.UnitTests.Api.Controllers
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Net.Http;
  using System.Web.Http;
  using System.Web.Http.Results;
  using System.Web.Http.Routing;
  using AgeRanger.Api.Controllers;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Models;
  using Moq;
  using NUnit.Framework;
  using Should;

  [TestFixture]
  public class PersonControllerTest
  {
    private Mock<IAgeRangerUow> uow = new Mock<IAgeRangerUow>();
    private Mock<IRule<Person>> personRule= new Mock<IRule<Person>>();
    private Mock<IRepository<Person>> personRepo = new Mock<IRepository<Person>>();
    private Mock<IRuleResolver<Person>> ageGroupRuleResolver = new Mock<IRuleResolver<Person>>();
    private PersonController controller ;
    private Person person;

    [SetUp]
    public void Setup()
    {
      var persons = new List<Person>();
      List<IRule<Person>> rules = new List<IRule<Person>>();
      personRule.Name = "Toddler";
      rules.Add(this.personRule.Object);
     this.person = new Person()
      {
        Id=1, FirstName = "Age", LastName = "Ranger", Age=2
      };
      persons.Add(person);
      this.personRepo.Setup(s => s.GetAll()).Returns(persons.AsQueryable());
      this.personRepo.Setup(s => s.GetById(1)).Returns(person);
      this.personRepo.Setup(s => s.Add(person));
      this.personRepo.Setup(s => s.Update(person));
      this.uow.Setup(s => s.Persons).Returns(this.personRepo.Object);

      this.ageGroupRuleResolver.Setup(s => s.MatchingRules(person)).Returns(rules);
      this.controller = new PersonController(this.uow.Object, this.ageGroupRuleResolver.Object);
      this.controller.Request= new HttpRequestMessage();
      this.controller.Request.SetConfiguration(new HttpConfiguration());
    }

    [Test]
    public void GetPersons()
    {
      var actual = this.controller.Get();
      actual.Count().ShouldEqual(1);
      actual.First().Id.ShouldEqual(1);
    }

    [Test]
    public void GetPerson()
    {
      var person = this.controller.Get(1);
      var controllerResponse = person as OkNegotiatedContentResult<Person>;
      controllerResponse.Content.Id.ShouldEqual(1);
    }

    [Test]
    public void GetPersonWhoDoesNotExist()
    {
      var person = this.controller.Get(2);
      var controllerResponse = person as OkNegotiatedContentResult<Person>;
      controllerResponse.ShouldBeNull();
    }

    [Test]
    public void UpdatePerson()
    {
      this.person.FirstName = "Updted";
      var response = this.controller.Post(this.person);
      Person updatedeperson;
      response.TryGetContentValue(out updatedeperson);
      updatedeperson.FirstName.ShouldEqual("Updted");
    }

    [Test]
    public void SavePerson()
    {
      var newperson = new Person()
      {
        Id = 0, FirstName = "test"
      };
      var response = this.controller.Post(newperson);
      Person updatedeperson;
      response.TryGetContentValue(out updatedeperson);
      HttpStatusCode.Created.ShouldEqual(response.StatusCode);
    }
  }
}
