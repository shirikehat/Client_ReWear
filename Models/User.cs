namespace Client_ReWear.Models;

public class User : ContentPage
{
	public User() { }
	
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public User(Models.User modeluser)
    {
        this.UserName = modeluser.UserName;
        this.Password = modeluser.Password;
        this.Phone = modeluser.Phone;
        this.Email = modeluser.Email;
    }
}
