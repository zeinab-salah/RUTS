using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LiveChartsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using RUTS.View;
using RUTS.View.Layouts;
using RUTS.ViewModel.Accounts;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace RUTS;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }
    public static User AuthenticatedUser { get; set; }

    private readonly string _connectionString = RepositoryBase.GetConnectionString();

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<Login>();
                services.AddTransient<View.Home>();
                services.AddTransient<View.Accounts.Index>();
                services.AddTransient<View.Accounts.Create>();
                services.AddTransient<View.Correspondents.Index>();
                services.AddTransient<View.Correspondents.Create>();
                services.AddTransient<View.Currencies.Index>();
                services.AddTransient<View.Currencies.Create>();
                services.AddTransient<View.Beneficaries.Index>();
                services.AddTransient<View.Beneficaries.Create>();
                services.AddTransient<View.Transactions.Index>();
                services.AddTransient<View.Transactions.Create>();
                services.AddTransient<View.ResourceItems.Index>();
                services.AddTransient<View.ResourceItems.Create>();
                services.AddTransient<View.Banks.Index>();
                services.AddTransient<View.Banks.Create>();
                services.AddTransient<AccountRepository>();
                services.AddTransient<IUnitofwork, Unitofwork>();
                services.AddTransient<RUTSDesignTimeDbContextFactory>();
                //services.AddTransient<View.Layouts.Layout> ();

                //services.AddScoped<IAccountRepository, AccountRepository>();
            }
            ).Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var loginView = AppHost.Services.GetRequiredService<Login>();
        loginView.IsVisibleChanged += (s, e) =>
        {
            if (loginView.IsVisible == false && loginView.IsLoaded)
            {
                var mainView = new Home();
                mainView.Show();
                try
                {
                    loginView.Close();
                }
                catch (Exception ex) { }
            }
        };
        loginView.Show();

        base.OnStartup(e);


        LiveCharts.Configure(config =>
            config
            // registers SkiaSharp as the library backend
            // REQUIRED unless you build your own
            .AddSkiaSharp()

            // adds the default supported types
            // OPTIONAL but highly recommend
            .AddDefaultMappers()

            // select a theme, default is Light
            // OPTIONAL
            //.AddDarkTheme()
            .AddLightTheme()

            // finally register your own mappers
            // you can learn more about mappers at:
            // ToDo add website link...
            //.HasMap<City>((city, point) =>
            //{
            //    point.PrimaryValue = city.Population;
            //    point.SecondaryValue = point.Context.Index;
            //})
        // .HasMap<Foo>( .... )
        // .HasMap<Bar>( .... )
        );
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        base.OnExit(e);
    }

}
