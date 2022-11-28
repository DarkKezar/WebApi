using Core.Entities;

namespace Service.DTO;

public class PetCreationModel
{
    public string Name { get; set; }
    public BodyEnum BodyType { get; set; }
    public EyeEnum EyeType { get; set; }
    public NoseEnum NoseType { get; set; }
    public MouthEnum MouthType { get; set; }
}