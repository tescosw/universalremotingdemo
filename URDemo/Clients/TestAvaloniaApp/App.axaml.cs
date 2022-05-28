using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using TescoSW.DependencyInjection;

namespace URDemo.TestAvaloniaApp
{
    public partial class App : Application
    {
#pragma warning disable CS8618
        private ServiceProvider serviceProvider;
#pragma warning restore CS8618

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        
        private void ConfigureServices(ServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                ;

            var configuration = builder.Build();

            services
                .AddSingleton<InvoiceListWindow>()
                .AddSingleton<LoginWindow>()
                //.AddTransient<InvoiceEditWindow>()
                //.AddTransient<CustomersWindow>()
                //.AddTransient<InvoiceLineWindow>()
                //.AddTransient<MeasurementUnitWindow>()                
                .AddLogging()
                .AddSingleUserCommunicator(configuration.GetSection("HttpCommunicator"))
                .AddBLEntities()
                .AddOWManager()
                .AddOWAuthentication();
        }
        
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = serviceProvider.GetRequiredService<LoginWindow>();
                //desktop.MainWindow = ActivatorUtilities.CreateInstance<LoginWindow>(serviceProvider);
                //desktop.MainWindow = new LoginWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}


