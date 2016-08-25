namespace AgeRanger.Data.Contracts
{
  public interface IBetweenMinMaxRule<T> : IRule<T> where T : IUser
  {
    int Min { get; set; }
    int Max { get; set; }
  }
}
