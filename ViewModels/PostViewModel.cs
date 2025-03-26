using Client_ReWear.Models;
using Client_ReWear.Services;
using System.Collections.ObjectModel;
namespace Client_ReWear.ViewModels;

public class PostViewModel : ViewModelBase
{
    public ReWearWebAPI proxy;


    public ObservableCollection<PrType> PrTypes { get; set; }
    public PostViewModel(ReWearWebAPI proxy)
	{
        this.proxy = proxy;
        this.PrTypes = new ObservableCollection<PrType>(((App)Application.Current).BasicData.PrTypes);
        SelectedType = this.PrTypes[0];
        CreateCommand = new Command(Create);

        UploadPhotoCommand = new Command(OnUploadPhoto);
        PhotoURL = proxy.GetDefaultProductPhotoUrl();
        LocalPhotoPath = "";
        DescriptionError = "Description is required";
        PriceError = "Price is required";
        StoreError = "Store is required";
        SizeError = "Size is required";

    }

    #region Description

    private bool showDescError;
    public bool ShowDescError
    {
        get => showDescError;
        set
        {
            showDescError = value;
            OnPropertyChanged("ShowDescError");
        }
    }
    private string description;

    public string Description
    {
        get => description;
        set
        {
            description = value;
            ValidateDescription();
            OnPropertyChanged("Description");
        }
    }
    private string descriptionError;

    public string DescriptionError
    {
        get => descriptionError;
        set
        {
            descriptionError = value;
            OnPropertyChanged("DescriptionError");
        }
    }
    private void ValidateDescription()
    {
        if (string.IsNullOrEmpty(description) ||
            description.Length < 5)
        {
            this.ShowDescError = true;
        }
        else
            this.ShowDescError = false;
    }
    #endregion


    #region Price

    private bool showPriceError;
    public bool ShowPriceError
    {
        get => showPriceError;
        set
        {
            showPriceError = value;
            OnPropertyChanged("ShowPriceError");
        }
    }
    private int price;

    public int Price
    {
        get => price;
        set
        {
            price = value;
            ValidatePrice();
            OnPropertyChanged("Price");
        }
    }
    private string priceError;

    public string PriceError
    {
        get => priceError;
        set
        {
            priceError = value;
            OnPropertyChanged("PriceError");
        }
    }
    private void ValidatePrice()
    {
        if (price == null)
        {
            this.ShowPriceError = true;
        }
        else
            this.ShowPriceError = false;
    }
    #endregion

    #region Store

    private bool showStoreError;
    public bool ShowStoreError
    {
        get => showStoreError;
        set
        {
            showStoreError = value;
            OnPropertyChanged("ShowStoreError");
        }
    }
    private string store;

    public string Store
    {
        get => store;
        set
        {
            store = value;
            ValidateStore();
            OnPropertyChanged("Store");
        }
    }
    private string storeError;

    public string StoreError
    {
        get => storeError;
        set
        {
            storeError = value;
            OnPropertyChanged("StoreError");
        }
    }
    private void ValidateStore()
    {
        if (string.IsNullOrEmpty(store))
        {
            this.ShowStoreError = true;
        }
        else
            this.ShowStoreError = false;
    }
    #endregion

    #region Size

    private bool showSizeError;
    public bool ShowSizeError
    {
        get => showSizeError;
        set
        {
            showSizeError = value;
            OnPropertyChanged("ShowSizeError");
        }
    }
    private string size;

    public string Size
    {
        get => size;
        set
        {
            size = value;
            ValidateSize();
            OnPropertyChanged("Size");
        }
    }
    private string sizeError;

    public string SizeError
    {
        get => sizeError;
        set
        {
            sizeError = value;
            OnPropertyChanged("SizeError");
        }
    }
    private void ValidateSize()
    {
        if (string.IsNullOrEmpty(size))
        {
            this.ShowStoreError = true;
        }
        else
            this.ShowStoreError = false;
    }
    #endregion


    #region Photo

    private string photoURL;

    public string PhotoURL
    {
        get => photoURL;
        set
        {
            photoURL = value;
            OnPropertyChanged("PhotoURL");
        }
    }

    private string localPhotoPath;

    public string LocalPhotoPath
    {
        get => localPhotoPath;
        set
        {
            localPhotoPath = value;
            OnPropertyChanged("LocalPhotoPath");
        }
    }

    public Command UploadPhotoCommand { get; }
    //This method open the file picker to select a photo
    private async void OnUploadPhoto()
    {
        try
        {
            var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Please select a photo",
            });

            if (result != null)
            {
                // The user picked a file
                this.LocalPhotoPath = result.FullPath;
                this.PhotoURL = result.FullPath;
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void UpdatePhotoURL(string virtualPath)
    {
        Random r = new Random();
        PhotoURL = proxy.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
        LocalPhotoPath = "";
    }

    #endregion


    #region create product
    public Command CreateCommand { get; }

    public async void Create()
    {
        ValidateDescription();
        ValidatePrice();
        ValidateStore();
        ValidateSize();

        if (!ShowDescError && !ShowPriceError && !ShowStoreError && !ShowSizeError)
        {

            var newProduct = new Models.Product()
            {
                TypeId = SelectedType.TypeCode,
                UserId = ((App)Application.Current).LoggedInUser.Id,
                UserName = ((App)Application.Current).LoggedInUser.UserName,
                Description = Description,
                Price = Price,
                Store = Store,
                Size = Size

            };

            //Call the Register method on the proxy to register the new user
            InServerCall = true;
            int? productId = await proxy.PostProduct(newProduct);
            InServerCall = false;

            //If the registration was successful, navigate to the login page
            if (productId != null)
            {

                //UPload profile imae if needed
                if (!string.IsNullOrEmpty(LocalPhotoPath))
                {
                    
                    Product? updatedProduct = await proxy.UploadProductImage(LocalPhotoPath);
                    if (updatedProduct == null)
                    {
                        InServerCall = false;
                        await Application.Current.MainPage.DisplayAlert("Posting product", "Product Data Was Saved BUT image upload failed", "ok");
                    }
                }



                InServerCall = false;
                newProduct.ProductCode = productId.Value;
                ((App)(Application.Current)).MainPage.Navigation.PopAsync();
            }
            else
            {

                //If the posting failed, display an error message
                string errorMsg = "Posting Product failed. Please try again.";
                await Application.Current.MainPage.DisplayAlert("Posting Product", errorMsg, "ok");
            }


        }
        else
        {

            //If the posting failed, display an error message
            string errorMsg = "Posting Product failed. Please try again.";
            await Application.Current.MainPage.DisplayAlert("Posting Product", errorMsg, "ok");
        }
    }


    #endregion

        #region Type
    private bool showTypeError;

    public bool ShowTypeError
    {
        get => showTypeError;
        set
        {
            showTypeError = value;
            OnPropertyChanged("ShowNameError");
        }
    }

    private PrType selectedType;

    public PrType SelectedType
    {
        get => selectedType;
        set
        {
            selectedType = value;
            OnPropertyChanged("SelectedType");
        }
    }

    #endregion

}