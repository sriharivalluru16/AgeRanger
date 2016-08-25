namespace AgeRanger.Data.Models
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using AgeRanger.Data.Contracts;
  using System.Collections.Generic;

  public class Person :IUser
  {
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false,ErrorMessage ="First name is required." )]
    [StringLength(120,ErrorMessage = "First name length should not exceed 120 chars.")]
    public string FirstName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
    [StringLength(120, ErrorMessage = "Last name length should not exceed 120 chars.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Age is required.")]
    [RegularExpression(pattern:"^(\\d+)$",ErrorMessage = "Age must be a number.")]
    public int Age { get; set; }

    [NotMapped]
    public IEnumerable<string> AgeGroups { get; set; }
 
  }
}
