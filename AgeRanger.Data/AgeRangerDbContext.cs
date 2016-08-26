namespace AgeRanger.Data
{
  using System.Data.Entity;
  using System.Data.Entity.ModelConfiguration.Conventions;
  using AgeRanger.Data.BootstrapData;
  using AgeRanger.Data.Models;

  public class AgeRangerDbContext: DbContext
  {
    public AgeRangerDbContext()
      : base(nameOrConnectionString: "AgeRangerEntities")
    {
      //Database.SetInitializer(new AgeGroupDataInitializer());
    }  
    public DbSet<Person> Persons { get; set; }
    public DbSet<AgeGroup> AgeGroups { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      modelBuilder.Entity<Person>();
      modelBuilder.Entity<AgeGroup>();  
    }
  }
}
