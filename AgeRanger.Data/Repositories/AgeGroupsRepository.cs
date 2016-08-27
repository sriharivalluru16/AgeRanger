namespace AgeRanger.Data.Repositories
{
  using System.Data.Entity;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Models;

  public class AgeGroupsRepository : AgRepository<AgeGroup>  , IAgeGroupsRepository
  {
    public AgeGroupsRepository(DbContext context)
      : base(context)
    {
    }
  }
}