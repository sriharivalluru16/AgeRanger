namespace AgeRanger.Data.Repositories
{
  using System.Data.Entity;
  using AgeRanger.Data.Models;

  public class AgeGroupsRepository : AgRepository<AgeGroup>
  {
    public AgeGroupsRepository(DbContext context)
      : base(context)
    {
    }
  }
}