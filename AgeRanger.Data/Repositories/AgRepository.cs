namespace AgeRanger.Data.Repositories
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Infrastructure;
  using System.Linq;
  using AgeRanger.Data.Contracts;

  public class AgRepository<T> : IRepository<T> where T : class
  {
    protected DbContext DbContext;

    public AgRepository(DbContext dbContext)
    {
      if(dbContext == null)
        throw new ArgumentNullException("dbContext");
      this.DbContext = dbContext;
      this.DbSet = this.DbContext.Set<T>();
    }

    protected DbSet<T> DbSet { get; set; }
    public virtual IQueryable<T> GetAll()
    {
      return this.DbSet;
    }

    public virtual T GetById(long id)
    {
      return this.DbSet.Find(id);
    }

    public virtual void Add(T entity)
    {
      DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
      if (dbEntityEntry.State != EntityState.Detached)
      {
        dbEntityEntry.State = EntityState.Added;
      }
      else
      {
        this.DbSet.Add(entity);
      }
    }

    public virtual void Update(T entity)
    {
      DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
      if (dbEntityEntry.State == EntityState.Detached)
      {
        this.DbSet.Attach(entity);
      }
      dbEntityEntry.State = EntityState.Modified;
     
    }
    public virtual void Delete(T entity)
    {
      DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
      if (dbEntityEntry.State != EntityState.Deleted)
      {
        dbEntityEntry.State = EntityState.Deleted;
      }
      else
      {
        this.DbSet.Attach(entity);
        this.DbSet.Remove(entity);
      }

    }

    public virtual void Delete(long id)
    {
      var entity = this.GetById(id);
      if (entity == null) return;
      this.Delete(entity);
    }
  }
}