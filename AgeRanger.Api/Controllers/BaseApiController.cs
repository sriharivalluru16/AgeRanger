namespace AgeRanger.Api.Controllers
{
  using System.Web.Http;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Models;   
  
  public class BaseApiController : ApiController
  {
    protected IAgeRangerUow Uow { get; set; }
    protected IRuleResolver<Person> AgeGroupRuleResolver { get; set; }

    public BaseApiController(IAgeRangerUow uow, IRuleResolver<Person> ageGroupRuleResolver)
    {
      this.Uow = uow;
      this.AgeGroupRuleResolver = ageGroupRuleResolver;
    }
  }
}
