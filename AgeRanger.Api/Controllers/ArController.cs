using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AgeRanger.Api.Controllers
{
  using System.Web.Http.Cors;
  using AgeRanger.Data;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Helpers;
  using AgeRanger.Data.Models;

  [EnableCors("http://localhost:54565","*","*")]
  public class ArController : ApiController
  {
    private IAgeRangerUow Uow { get; set; }
    public ArController(IAgeRangerUow uow)
    {
      this.Uow = uow;
    }

    //[ActionName("agegroups")]
    public IEnumerable<AgeGroup> GetAgeGroups()
    {
      return Uow.AgeGroups.GetAll();
    }

    //[ActionName("persons")]
    public IEnumerable<Person> GetPersons()
    {
      return Uow.Persons.GetAll();
    }

    //[ActionName("agegroup")]
    public IHttpActionResult GetAgeGroup(int id)
    {
      var ageGroup = Uow.AgeGroups.GetById(id);
      if (ageGroup == null)
      {
        return NotFound();
      }
      return Ok(ageGroup);
    }

    //[ActionName("person")]
    public IHttpActionResult GetPerson(int id)
    {
      var person = Uow.Persons.GetById(id);
      if (person == null)
      {
        return NotFound();
      }
      return Ok(person);
    }

    //[ActionName("newperson")]
    [HttpPut]
    [HttpPost]
    public IHttpActionResult AddPerson(Person person)
    {
     Uow.Persons.Add(person);
     Uow.Commit();

     return Ok(person);
    }
    //[ActionName("updateperson")]
    [HttpPut]
    [HttpPost]
    public IHttpActionResult UppdatePerson(Person person)
    {
      Uow.Persons.Update(person);
        Uow.Commit();
      return Ok(person);
    }

  }
}
