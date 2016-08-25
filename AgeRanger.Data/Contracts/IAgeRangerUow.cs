namespace AgeRanger.Data.Contracts
{
  using AgeRanger.Data.Models;

  public interface IAgeRangerUow
  {
    IRepository<Person> Persons { get; }
    IRepository<AgeGroup> AgeGroups { get; }
    void Commit();
  }
}