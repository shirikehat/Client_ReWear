using Client_ReWear.Models;
using Client_ReWear.Services;

namespace Client_ReWear.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    private string name;
    private int? userId { get; set; }
    private string? user_error;
    private string email;
    private string password;
    private string? password_error;

    private readonly IServiceProvider serviceProvider;


    private ReWearWebAPI proxy;
    public RegisterViewModel(ReWearWebAPI proxy)
	{
        this.proxy = proxy;
        RegisterCommand = new Command(OnRegister);
        CancelCommand = new Command(OnCancel);
        ShowPasswordCommand = new Command(OnShowPassword);
        IsPassword = true;
        NameError = "Name is required";
        EmailError = "Email is required";
        PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
    }


    //Defiine properties for each field in the registration form including error messages and validation logic
    #region Name
    private bool showNameError;

    public bool ShowNameError
    {
        get => showNameError;
        set
        {
            showNameError = value;
            OnPropertyChanged("ShowNameError");
        }
    }

    public string Name
    {
        get => name;
        set
        {
            name = value;
            ValidateName();
            OnPropertyChanged("Name");
        }
    }

    private string nameError;

    public string NameError
    {
        get => nameError;
        set
        {
            nameError = value;
            OnPropertyChanged("NameError");
        }
    }

    private void ValidateName()
    {
        this.ShowNameError = string.IsNullOrEmpty(Name);
    }
    #endregion


    #region Email
    private bool showEmailError;

    public bool ShowEmailError
    {
        get => showEmailError;
        set
        {
            showEmailError = value;
            OnPropertyChanged("ShowEmailError");
        }
    }

    public string Email
    {
        get => email;
        set
        {
            email = value;
            ValidateEmail();
            OnPropertyChanged("Email");
        }
    }

    private string emailError;

    public string EmailError
    {
        get => emailError;
        set
        {
            emailError = value;
            OnPropertyChanged("EmailError");
        }
    }

    private void ValidateEmail()
    {
        this.ShowEmailError = string.IsNullOrEmpty(Email);
        if (!ShowEmailError)
        {
            //check if email is in the correct format using regular expression
            if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                EmailError = "Email is not valid";
                ShowEmailError = true;
            }
            else
            {
                EmailError = "";
                ShowEmailError = false;
            }
        }
        else
        {
            EmailError = "Email is required";
        }
    }
    #endregion

    #region Password
    private bool showPasswordError;

    public bool ShowPasswordError
    {
        get => showPasswordError;
        set
        {
            showPasswordError = value;
            OnPropertyChanged("ShowPasswordError");
        }
    }


    public string Password
    {
        get => password;
        set
        {
            password = value;
            ValidatePassword();
            OnPropertyChanged("Password");
        }
    }

    private string passwordError;

    public string PasswordError
    {
        get => passwordError;
        set
        {
            passwordError = value;
            OnPropertyChanged("PasswordError");
        }
    }

    private void ValidatePassword()
    {
        //Password must include characters and numbers and be longer than 4 characters
        if (string.IsNullOrEmpty(password) ||
            password.Length < 4 ||
            !password.Any(char.IsDigit) ||
            !password.Any(char.IsLetter))
        {
            this.ShowPasswordError = true;
        }
        else
            this.ShowPasswordError = false;
    }

    //This property will indicate if the password entry is a password
    private bool isPassword = true;
    public bool IsPassword
    {
        get => isPassword;
        set
        {
            isPassword = value;
            OnPropertyChanged("IsPassword");
        }
    }
    //This command will trigger on pressing the password eye icon
    public Command ShowPasswordCommand { get; }
    //This method will be called when the password eye icon is pressed
    public void OnShowPassword()
    {
        //Toggle the password visibility
        IsPassword = !IsPassword;
    }
    #endregion

    //Define a command for the register button
    public Command RegisterCommand { get; }
    public Command CancelCommand { get; }

    //Define a method that will be called when the register button is clicked
    public async void OnRegister()
    {
        ValidateName();
        ValidateEmail();
        ValidatePassword();

        if (!ShowNameError &&  !ShowEmailError && !ShowPasswordError)
        {
            //Create a new AppUser object with the data from the registration form
            var newUser = new User
            {
                UserName = Name,
                Email = Email,
                Password = Password,
                IsManager = false
            };

            //Call the Register method on the proxy to register the new user
            InServerCall = true;
            newUser = await proxy.Register(newUser);
            InServerCall = false;

            //If the registration was successful, navigate to the login page
            if (newUser != null)
            {
                
                InServerCall = false;

                ((App)(Application.Current)).MainPage.Navigation.PopAsync();
            }
            else
            {

                //If the registration failed, display an error message
                string errorMsg = "Registration failed. Please try again.";
                await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
            }
        }
    }

    //Define a method that will be called upon pressing the cancel button
    public void OnCancel()
    {
        //Navigate back to the login page
        ((App)(Application.Current)).MainPage.Navigation.PopAsync();
    }

}

