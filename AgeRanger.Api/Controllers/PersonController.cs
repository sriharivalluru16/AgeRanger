namespace AgeRanger.Api.Controllers
{
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Models;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Net.Http;
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
      if (id == 0)
      {
        return this.Ok(new Person());
      }

      var person = Uow.Persons.GetById(id);

      if (person == null)
      {
        return this.NotFound();
      }
     
      return this.Ok(person);
    }

    // POST api/person
    public HttpResponseMessage Post([FromBody] Person person)
    {
      if (person.Id != 0)
      {
        this.Uow.Persons.Update(person);
        this.Uow.Commit();
        return this.Request.CreateResponse(HttpStatusCode.OK, person);
      }
      else
      {
        this.Uow.Persons.Add(person);
        this.Uow.Commit();
        return this.Request.CreateResponse(HttpStatusCode.Created, person);
      }
    }

  }
}
