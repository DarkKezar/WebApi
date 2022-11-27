using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class UserAction
{
    [Key]
    public Guid Id { get; set; }
    public Pet Pet { get; set; }
    public User User { get; set; }
    public ActionEnum Action { get; set; }
}