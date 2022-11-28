namespace Service.DTO;

public class UserUpdateModel
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string PhotoSrc { get; set; }
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}