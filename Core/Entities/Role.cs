using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class Role : IdentityRole<Guid>
{
    public Role() : base() { }

    public Role(string name)
        : base(name)
    { }
}