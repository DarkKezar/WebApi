using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Farm
{
    [Key]
    public string Name { get; set; }
    public List<Pet> Pets { get; set; }
}