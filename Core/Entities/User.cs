using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string PhotoSrc { get; set; }
    public Farm MyFarm { get; set; }
    public List<Farm> Collaborations { get; set; }
}