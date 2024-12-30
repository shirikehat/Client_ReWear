namespace Client_ReWear.Models;

public class User
{
	
    public int Id { get; set; }

    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsManager { get; set; }

    public string ProfileImagePath { get; set; } = "";
    public User() { }
}
