namespace AgeRanger.Api.Controllers
{
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Models;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.Http; 

  public class PersonController : BaseApiController
  {
    public PersonController(IAgeRangerUow uow, IRuleResolver<Person> ageGroupRuleResolver)
      : base(uow, ageGroupRuleResolver)
    {
    } 

    // GET api/person
    public IEnumerable<Person> Get()
    {
      var persons = this.Uow.Persons.GetAll().ToList().Select(
        p =>
        {
          p.AgeGroups = this.AgeGroupRuleResolver.MatchingRules(p).Select(r => r.Name).AsEnumerable();
          return p;
        });
      return persons;
    }

    // GET api/person/5
    public IHttpActionResult Get(int id)
    {
      var person = Uow.Persons.GetById(id);

      if (person == null)
      {
        return this.Ok(new Person());
      }
     
      return this.Ok(person);
    }

    // POST api/person
    public void Post([FromBody] Person person)
    {
      if (person.Id != 0)
      {
        this.Uow.Persons.Update(person);
      }
      else
      {
        this.Uow.Persons.Add(person);
      }
      this.Uow.Commit();
    }
  }
}
