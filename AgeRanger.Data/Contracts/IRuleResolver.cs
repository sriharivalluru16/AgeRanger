
namespace AgeRanger.Data.Contracts
{
  using System.Collections.Generic;

  public interface IRuleResolver<T>  where T:IUser
  {
    List<IRule<T>> MatchingRules(T type);
  }
}
