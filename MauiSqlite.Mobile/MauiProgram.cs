﻿using MauiSqlite.Mobile.Repositories;
using MauiSqlite.Mobile.DataAccess;
using MauiSqlite.Mobile.ViewModels;
using MauiSqlite.Mobile.Views;
using Microsoft.Extensions.Logging;

namespace MauiSqlite.Mobile
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
                });


            var dbContext = new EAfiliadoDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddDbContext<EAfiliadoDbContext>();

            builder.Services.AddSingleton<IRepository, Repository>();

            builder.Services.AddTransient<InicioView>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<LoadingView>();
            builder.Services.AddTransient<AfiliadoView>();
            builder.Services.AddTransient<AddAfiliadoView>();
            builder.Services.AddTransient<FlyoutHeaderControl>();


            //View Models
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoadingViewModel>();
            builder.Services.AddTransient<AfiliadoViewModel>();
            builder.Services.AddTransient<AddAfiliadoViewModel>();
            builder.Services.AddTransient<FlyoutHeaderControlModel>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
