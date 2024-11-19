using System.Text;
using System.Text.Json;
using Client_ReWear.Models;

namespace Client_ReWear.Services;

public class ReWearWebAPI
{

    #region with tunnel
    //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
    private static string serverIP = "3zr245ps-5110.euw.devtunnels.ms";
    //���� ������ ������� �� ����� HTTP
    //cookies ��� �����
    private HttpClient client;

    // JSON ����� �� ���� �� ������� ������� ����� ���� ���� ������ �����
    // ��� ����� ����� ����� �����
    private JsonSerializerOptions jsonSerializerOptions;

    // ������� �� ����� ������ ����� �� ����� ����� ����
    private string baseUrl;

    // ����� ����� ������ ���� ������ ��� ��������� �����
    public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://jfzd1hz5-5094.euw.devtunnels.ms/api/" : "http://localhost:5021/api/";
    public static string ImageBaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://jfzd1hz5-5094.euw.devtunnels.ms/images/" : "http://localhost:5021/images/";

    #endregion

    //������ �� ����� �� ���� ������ ���� ������� ������.
    //���� ������ �� ����� ����� �� ����� �� ���� �� ������ ������
    public User LoggedInUser { get; set; }


    public ReWearWebAPI()
    {
        // Set up the HTTP client handler to support cookies
        HttpClientHandler handler = new HttpClientHandler();
        handler.CookieContainer = new System.Net.CookieContainer(); // Initialize a container to store cookies

        // Create a new HttpClient with the handler and enable automatic disposal of the handler
        this.client = new HttpClient(handler, true);

        // Set the base URL for API requests
        this.baseUrl = BaseAddress;

        // Configure JSON serializer options to make JSON formatting more readable and case insensitive
        jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true, // Makes the JSON output more readable (with indents)
            PropertyNameCaseInsensitive = true // Allows deserialization to ignore property name case differences
        };
    }


    public string GetImagesBaseAddress()
    {
        return ReWearWebAPI.ImageBaseAddress;
    }

    public string GetDefaultProfilePhotoUrl()
    {
        return $"{ReWearWebAPI.ImageBaseAddress}/profileImages/default.png";
    }

    public async Task<User> Login(LoginInfo info)
    {
        // Set the URL for the specific login API function
        string url = $"{this.baseUrl}login";
        try
        {
            // Serialize the login info into a JSON string using the configured options
            string json = JsonSerializer.Serialize(info, jsonSerializerOptions);

            // Create the content to send in the POST request with proper encoding and content type
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send the POST request to the server API
            HttpResponseMessage response = await client.PostAsync(url, content);
            //Check status
            if (response.IsSuccessStatusCode)
            {
                //Extract the content as string
                string resContent = await response.Content.ReadAsStringAsync();

                User result = JsonSerializer.Deserialize<User>(resContent, jsonSerializerOptions);

                // Store the logged-in user details for further use
                this.LoggedInUser = result;

                // Show a success message to the user
                await Application.Current.MainPage.DisplayAlert("Login", "Login Succeced", "ok");
                // Return the logged-in user details
                return result;
            }
            else
            {
                // ���� ������ ����� ������ ������ ����� ������� ������ ������
                // ����� ������ ������� ������ ����� �������
                await Application.Current.MainPage.DisplayAlert("Login", "Login Faild!", "ok");
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<User?> Register(User user)
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}register";
        try
        {
            //Call the server API
            string json = JsonSerializer.Serialize(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            //Check status
            if (response.IsSuccessStatusCode)
            {
                //Extract the content as string
                string resContent = await response.Content.ReadAsStringAsync();
                //Desrialize result
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                User? result = JsonSerializer.Deserialize<User>(resContent, options);
                return result;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }





}
