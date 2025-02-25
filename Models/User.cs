using Client_ReWear.Services;

namespace Client_ReWear.Models;

public class User
{
	
    public int Id { get; set; }

    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsManager { get; set; }

    public bool IsBlocked { get; set; }
    public string ProfileImagePath { get; set; } = "";

    public string FullProfileImageUrl 
    { 
        get
        {
            return ReWearWebAPI.ImageBaseAddress + ProfileImagePath;
        }
    }
    public User() { }

    public User(Models.User user)
    {
        this.Id = user.Id;
        this.UserName = user.UserName;
        this.Password = user.Password;
        this.Phone = user.Phone;
        this.Email = user.Email;
        this.IsManager = user.IsManager;

    }
}
