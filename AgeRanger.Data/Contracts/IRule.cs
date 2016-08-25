namespace AgeRanger.Data.Contracts
{ 
  public interface IRule<T> where T : IUser
  {
    string Name { get; set; }
    bool IsSatisfied(T type);
  }
}
