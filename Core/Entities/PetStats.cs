using System.ComponentModel.DataAnnotations;

namespace Core.Entities;



public class PetStats
{
    [Key]
    public Guid Id { get; set; }
    //public Pet Pet { get; set; }
    public HungerLevelEnum HungerLevel { get; set; }
    public ThirstyLevelEnum ThirstyLevel { get; set; }
    public uint HappyDaysCount { get; set; }
}