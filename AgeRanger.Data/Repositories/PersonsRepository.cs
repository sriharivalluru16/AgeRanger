namespace AgeRanger.Data.Repositories
{
  using System.Data.Entity;
  using AgeRanger.Data.Models;

  public class PersonsRepository : AgRepository<Person>
  {
    public PersonsRepository(DbContext context) : base(context)
    {
    }
  }
}