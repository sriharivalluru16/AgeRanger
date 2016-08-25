namespace AgeRanger.Data.Rules
{
  using System;
  using System.Runtime.Serialization;
  using AgeRanger.Data.Contracts;

  [KnownType(typeof(BetweenMinMaxRule<>))]
  public class BetweenMinMaxRule<T> : IBetweenMinMaxRule<T> where T : IUser
  {
    private readonly Func<int, int, int, bool> betweenMinMaxRule = (min, max, input) => input >= min && input < max;
    public string Name { get; set; }

    public bool IsSatisfied(T type)
    {
      return this.betweenMinMaxRule(this.Min, this.Max, type.Age);
    }
    public int Min { get; set; }
    public int Max { get; set; }
  }
}
