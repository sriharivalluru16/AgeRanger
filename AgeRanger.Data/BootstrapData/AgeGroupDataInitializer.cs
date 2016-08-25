using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Data.BootstrapData
{
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using AgeRanger.Data.Models;

  internal class AgeGroupDataInitializer : DbMigrationsConfiguration  <AgeRangerDbContext>
  {
    public AgeGroupDataInitializer()
    {
      this.AutomaticMigrationsEnabled = false;
    }
    protected override void Seed(AgeRangerDbContext context)
    {
      IList<AgeGroup> defaultAgeGroups = new List<AgeGroup>();

      defaultAgeGroups.Add(new AgeGroup() { Id = 1, MinAge = null, MaxAge = 2, Description = "Toddler"});
      defaultAgeGroups.Add(new AgeGroup() { Id = 2, MinAge = 2, MaxAge = 14, Description = "Child" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 3, MinAge = 14, MaxAge = 20, Description = "Teenager" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 4, MinAge = 20, MaxAge = 25, Description = "Early twenties" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 5, MinAge = 25, MaxAge = 30, Description = "Almost thirty" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 6, MinAge = 30, MaxAge = 50, Description = "Very adult" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 7, MinAge = 50, MaxAge = 70, Description = "Kinda old" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 8, MinAge = 70, MaxAge = 90, Description = "Old" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 9, MinAge = 90, MaxAge = 110, Description = "Very old" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 10, MinAge = 110, MaxAge = 199, Description = "Crazy ancient" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 11, MinAge = 199, MaxAge = 4999, Description = "Vampire" });
      defaultAgeGroups.Add(new AgeGroup() { Id = 12, MinAge = 4999, MaxAge = null, Description = "Kauri tree" });

      foreach (AgeGroup std in defaultAgeGroups)
        context.AgeGroups.AddOrUpdate(std);

     base.Seed(context);
    }
  }
}
