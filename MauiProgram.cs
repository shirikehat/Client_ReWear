﻿using Client_ReWear.ViewModels;
using Client_ReWear.Views;
using Microsoft.Extensions.Logging;

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
                });

            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<Register>();
            builder.Services.AddSingleton<RegisterViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
