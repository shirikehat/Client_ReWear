using System.Text;
using System.Text.Json;
using Client_ReWear.Models;

namespace Client_ReWear.Services;

public class ReWearWebAPI
{

    #region with tunnel
    //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
    private static string serverIP = "jfzd1hz5-5094.euw.devtunnels.ms";
    //מנהל תכונות מתקדמות של בקשות HTTP
    //cookies כמו תמיכה
    private HttpClient client;

    // JSON משתנה זה יכיל את ההגדרות שייקבעו בהמשך כיצד לעבד ולהמיר נתוני
    // בעת שליחת וקבלת בקשות מהשרת
    private JsonSerializerOptions jsonSerializerOptions;

    // אובייקט של מחלקת השירות שמכיל את כתובת הבסיס לשרת
    private string baseUrl;

    // כתובת הבסיס לכתובת השרת מותאמת לפי פלטפורמות ההרצה
    public static string BaseAddress = "https://jfzd1hz5-5094.euw.devtunnels.ms/api/";
    public static string ImageBaseAddress = "https://jfzd1hz5-5094.euw.devtunnels.ms";

    #endregion

    //מאפיין זה מחזיק את פרטי המשתמש לאחר התחברות מוצלחת.
    //ניתן להשתמש בו לצורך בדיקה או שליפה של מידע על המשתמש המחובר
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
    public string GetDefaultProductPhotoUrl()
    {
        return $"{ReWearWebAPI.ImageBaseAddress}/productImages/default.png";
    }

    public async Task<User> Login(LoginInfo info)
    {
        // Set the URL for the specific login API function
        string url = $"{this.baseUrl}login";
        try
        {
            // Serialize the login info into a JSON string using the configured options
            string json = JsonSerializer.Serialize(info);

            // Create the content to send in the POST request with proper encoding and content type
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send the POST request to the server API
            HttpResponseMessage response = await client.PostAsync(url, content);
            //Check status
            if (response.IsSuccessStatusCode)
            {
                //Extract the content as string
                string resContent = await response.Content.ReadAsStringAsync();

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

    public async Task<User> GetUser(int userId)
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}GetUser?userId={userId}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
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
                User result = JsonSerializer.Deserialize<User>(resContent, options);
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



    public async Task<User?> UploadProfileImage(string imagePath)
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}uploadprofileimage";
        try
        {
            //Create the form data
            MultipartFormDataContent form = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
            form.Add(fileContent, "file", imagePath);
            //Call the server API
            HttpResponseMessage response = await client.PostAsync(url, form);
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


    public async Task<Product?> UploadProductImage(string imagePath)
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}uploadproductimage";
        try
        {
            //Create the form data
            MultipartFormDataContent form = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
            form.Add(fileContent, "file", imagePath);
            //Call the server API
            HttpResponseMessage response = await client.PostAsync(url, form);
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
                Product? result = JsonSerializer.Deserialize<Product>(resContent, options);
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


    public async Task<bool> UpdateUser(User user)
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}updateuser";
        try
        {
            //Call the server API
            string json = JsonSerializer.Serialize(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            //Check status
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public async Task<List<Product>?> GetProducts()
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}GetProducts";
        try
        {
            //Call the server API
            HttpResponseMessage response = await client.GetAsync(url);
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
                List<Product>? result = JsonSerializer.Deserialize<List<Product>>(resContent, options);
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




    public async Task<List<Product>?> GetAllProducts()
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}GetAllProducts";
        try
        {
            //Call the server API
            HttpResponseMessage response = await client.GetAsync(url);
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
                List<Product>? result = JsonSerializer.Deserialize<List<Product>>(resContent, options);
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



    public async Task<Product> GetProduct(int productCode)
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}GetProduct?ProductCode={productCode}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
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
                Product result = JsonSerializer.Deserialize<Product>(resContent, options);
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

    public async Task<PrType> GetType(int typeId)
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}GetType?typeId={typeId}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
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
                PrType result = JsonSerializer.Deserialize<PrType>(resContent, options);
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

    public async Task<Status> GetStatus(int statusId)
    {
        //Set URI to the specific function API
        string url = $"{this.baseUrl}GetStatus?statusId={statusId}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
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
                Status result = JsonSerializer.Deserialize<Status>(resContent, options);
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
