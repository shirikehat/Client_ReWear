using Client_ReWear.Services;
using Client_ReWear.ViewModels;
using Client_ReWear.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace Client_ReWear
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Klhamer.otf", "Klhamer");
                    fonts.AddFont("felix007-medium-webfont.ttf", "felix");
                    fonts.AddFont("Library School.otf", "library");
                    fonts.AddFont("Sweet Autumn.otf", "SA");
                    
                })
                .RegisterPages()
                .RegisterViewModels()
                .RegisterDataServices();

            
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<Register>();
            builder.Services.AddTransient<Home>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<AdminPage>();
            builder.Services.AddTransient<Wishlist>();
            builder.Services.AddTransient<Post>();
            builder.Services.AddTransient<Profile>();
            builder.Services.AddTransient<CartView>();
            builder.Services.AddTransient<Orders>();
            builder.Services.AddTransient<ProductPage>();
            builder.Services.AddTransient<EditProfile>();

            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ReWearWebAPI>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<AdminPageViewModel>();
            builder.Services.AddTransient<WishlistViewModel>();
            builder.Services.AddTransient<PostViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<CartViewModel>();
            builder.Services.AddTransient<OrdersViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<ProductPageViewModel>();
            builder.Services.AddTransient<EditProfileViewModel>();
            return builder;
        }
    }
}

