using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using TescoSW.OW.Remoting;

namespace URDemo.TestAvaloniaApp
{
    public partial class LoginWindow : Window
    {
        private readonly IBLAuthentication? blAuthentication;
        private readonly IServiceProvider? serviceProvider;

        public LoginWindow() : this(null, null) { }

        public LoginWindow(IBLAuthentication blAuthentication, IServiceProvider serviceProvider)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            if (serviceProvider is null) return;

            var okButton = this.Find<Button>("okButton");
            okButton.Click += okButton_Click;
            var cancelButton = this.Find<Button>("cancelButton");
            cancelButton.Click += cancelButton_Click;
            this.blAuthentication = blAuthentication;
            this.serviceProvider = serviceProvider;
        }

        private void cancelButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Close();
        }

        private void okButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                var userNameBox = this.Find<TextBox>("userNameBox");
                var passwordBox = this.Find<TextBox>("passwordBox");
                blAuthentication?.Login(userNameBox.Text, passwordBox.Text, "CZ");
                var mainWindow = serviceProvider!.GetRequiredService<InvoiceListWindow>();
                //var mainWindow = ActivatorUtilities.CreateInstance<InvoiceListWindow>(serviceProvider!);
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                errorMessage.Content = ex.Message;
                errorMessage.IsVisible = true;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
