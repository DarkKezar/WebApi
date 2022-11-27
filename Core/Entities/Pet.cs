using System.ComponentModel.DataAnnotations;

namespace Core.Entities;
//body, eye, nose and mouth


public class Pet
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Farm Farm { get; set; }
    public PetStats Stats { get; set; }
    
    public BodyEnum BodyType { get; set; }
    public EyeEnum EyeType { get; set; }
    public NoseEnum NoseType { get; set; }
    public MouthEnum MouthType { get; set; }
    
}