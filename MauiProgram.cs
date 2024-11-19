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
            return builder;
        }
    }
}

