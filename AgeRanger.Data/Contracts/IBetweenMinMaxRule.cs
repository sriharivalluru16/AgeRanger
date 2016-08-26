namespace AgeRanger.Data.Contracts
{
  public interface IBetweenMinMaxRule<T> : IRule<T> where T : IUser
  {
    long Min { get; set; }
    long Max { get; set; }
  }
}
