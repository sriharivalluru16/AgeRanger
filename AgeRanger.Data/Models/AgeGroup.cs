namespace AgeRanger.Data.Models
{
  using System;

  public class AgeGroup
  {
    private long? maxAge;
    private long? minAge;
    public long Id { get; set; }
    public long? MinAge
    {
      get
      {
        return this.minAge;
      }
      set
      {
        object x = value;
        this.minAge = x != DBNull.Value ? value : null;
      }
    }
    public long? MaxAge
    {
      get
      {
        return this.maxAge;
      }
      set
      {
        object x = value;
        this.maxAge = x != DBNull.Value ? value : null;
      }
    }
    public string Description { get; set; }
  }
}
