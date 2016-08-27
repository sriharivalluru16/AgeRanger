namespace AgeRanger.Data.Repositories
{
  using System.Data.Entity;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Models;

  public class PersonsRepository : AgRepository<Person>, IPersonsRepository
  {
    public PersonsRepository(DbContext context) : base(context)
    {
    }
  }
}