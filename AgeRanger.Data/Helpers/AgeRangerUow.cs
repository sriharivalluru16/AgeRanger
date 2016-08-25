namespace AgeRanger.Data.Helpers
{
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Models;

  public class AgeRangerUow : IAgeRangerUow
  {
    private AgeRangerDbContext DbContext { get; set; }

    public AgeRangerUow(IRepositoryProvider repositoryProvider)
    {
      this.CreateDbContext();
      repositoryProvider.DbContext = this.DbContext;
      this.RepositoryProvider = repositoryProvider;
    }

    protected void CreateDbContext()
    {
      this.DbContext = new AgeRangerDbContext();

      // Do NOT enable proxied entities, else serialization fails
      this.DbContext.Configuration.ProxyCreationEnabled = false;

      // Load navigation properties explicitly (avoid serialization trouble)
      this.DbContext.Configuration.LazyLoadingEnabled = false;

      // Because Web API will perform validation, we don't need/want EF to do so
      this.DbContext.Configuration.ValidateOnSaveEnabled = false;
    }

    public IRepository<AgeGroup> AgeGroups
    {
      get
      {
        return this.GetRepo<AgeGroup>();
      }
    }

    public IRepository<Person> Persons
    {
      get
      {
        return this.GetRepo<Person>();
      }
    }

    protected IRepositoryProvider RepositoryProvider { get; set; }

    private IRepository<T> GetRepo<T>() where T : class
    {
      return this.RepositoryProvider.GetRepositoryForEntityType<T>();
    }

    public void Commit()
    {
      this.DbContext.SaveChanges();
    }
  }
}
