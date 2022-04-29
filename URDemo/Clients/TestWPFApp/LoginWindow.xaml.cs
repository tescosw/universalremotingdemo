using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TescoSW.OW.Remoting;

namespace URDemo.TestWPFApp
{
    /// <summary>
    /// Interakční logika pro LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IBLAuthentication blAuthentication;
        private readonly IServiceProvider serviceProvider;

        public LoginWindow(IBLAuthentication blAuthentication, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.blAuthentication = blAuthentication;
            this.serviceProvider = serviceProvider;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blAuthentication.Login(userNameBox.Text, passwordBox.Password, "CZ");
                var mainWindow = serviceProvider.GetRequiredService<InvoiceListWindow>();
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                errorMessage.Content = ex.Message;
                errorMessage.Visibility = Visibility.Visible;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
