namespace Client_ReWear.Models;

public class LoginInfo : ContentPage
{
    public string Username { get; set; }
    public string Password { get; set; }
    public LoginInfo() { }

    public LoginInfo(Models.LoginInfo loginInfo)
    {
        this.Username = loginInfo.Username;
        this.Password = loginInfo.Password;
    }
}