
namespace AgeRanger.Data.RuleResolvers
{
  using System.Collections.Generic;
  using System.Linq; 
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Rules;

  public class AgeGroupResolver<T> : IRuleResolver<T> where T : IUser
  {
    private readonly List<BetweenMinMaxRule<T>> rules = new List<BetweenMinMaxRule<T>>();
    private IAgeRangerUow Uow { get; set; }

    public AgeGroupResolver(IAgeRangerUow uow)
    {
      this.Uow = uow;
      this.FetchRules();
    }
   
    private void FetchRules()
    {
      foreach (var rule in this.Uow.AgeGroups.GetAll())
      {
        this.rules.Add(new BetweenMinMaxRule<T>
        {
          Name = rule.Description,
          Min = rule.MinAge ?? 0,
          Max = rule.MaxAge ?? int.MaxValue
        });
      }
    }

    public List<IRule<T>> MatchingRules(T type)
    {
      return new List<IRule<T>>(this.rules.Where(r => r.IsSatisfied(type)).ToList());
    }

  }
}
