using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TescoSW.DependencyInjection;

namespace URDemo.TestWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                /*
                // pro debugování přes fidler
                .AddInMemoryCollection(new Dictionary<string, string>()
                {                
                    ["HttpCommunicator:ServiceUrl"] = $"http://{TescoSW.Net.NetUtils.GetLocalIPv4Address()}:7181/",  
                    //pokud chci používat fiddler, tak nelze použít localhost
                    //viz. https://stackoverflow.com/questions/36277311/how-to-use-fiddler-with-httpclient 
                })
                */
                ;

            var configuration = builder.Build();

            services
                .AddSingleton<InvoiceListWindow>()
                .AddSingleton<LoginWindow>()
                .AddTransient<InvoiceEditWindow>()
                .AddTransient<CustomersWindow>()
                .AddTransient<InvoiceLineWindow>()
                .AddTransient<MeasurementUnitWindow>()
                .AddLogging()
                .AddSingleUserCommunicator(configuration.GetSection("HttpCommunicator"))
                .AddBLEntities()
                .AddOWManager()
                .AddOWAuthentication();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetRequiredService<LoginWindow>();
            mainWindow.Show();            
        }
    }
}
