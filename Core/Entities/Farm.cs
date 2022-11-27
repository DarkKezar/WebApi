using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Farm
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Pet> Pets { get; set; }
    public User FarmOwner { get; set; }
    public List<Guid> FarmCollabersId { get; set; }
}