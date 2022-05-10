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
using URDemo.Shared;
using TescoSW.Linq.Extensions;
using TescoSW.Global;

namespace URDemo.TestWPFApp
{
    /// <summary>
    /// Interakční logika pro CustomersWindow.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {
        private readonly IBLManager blManager;
        private readonly IServiceProvider serviceProvider;

        public CustomersWindow(IBLManager blManager, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.blManager = blManager;
            this.serviceProvider = serviceProvider;
        }

        public async Task LoadData()
        {
            progressBar.Visibility = Visibility.Visible;
            var customerQuery = blManager.Query<URDemo.Shared.Customer>();
            var query = (
                            string.IsNullOrEmpty(filterBox.Text) ?
                            customerQuery.Where(c => c.ID != 0) :
                            customerQuery.Where(c =>    c.Name.Contains(filterBox.Text, StringComparisonMode.IgnoreCaseAndAccent) ||
                                                        c.IC.Contains(filterBox.Text, StringComparisonMode.IgnoreCaseAndAccent) ||
                                                        c.VatId.Contains(filterBox.Text, StringComparisonMode.IgnoreCaseAndAccent))
                         ).OrderBy(c => c.Name);

            dataGrid.ItemsSource = await query.ToArrayAsync();
            progressBar.Visibility = Visibility.Collapsed;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        public long Result { get; private set; }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is URDemo.Shared.Customer c && c.ID > 0)
            {
                Result = c.ID;
                DialogResult = true;
            }
            else
                DialogResult = false;
        }

        private async void filterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            await LoadData();
        }
    }
}
