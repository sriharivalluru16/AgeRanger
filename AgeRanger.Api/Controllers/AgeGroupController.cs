namespace AgeRanger.Api.Controllers
{
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Models;
  using System.Collections.Generic;
  using System.Web.Http;

  public class AgeGroupController : BaseApiController
  {
    public AgeGroupController(IAgeRangerUow uow, IRuleResolver<Person> ageGroupRuleResolver)
      : base(uow, ageGroupRuleResolver)
    {
    }

    [ActionName("fetch")]
    [HttpPost]
    public IEnumerable<IRule<Person>> GetAgeGroupsOfPerson(Person person)
    {
      return this.AgeGroupRuleResolver.MatchingRules(person);
    }
  }
}
