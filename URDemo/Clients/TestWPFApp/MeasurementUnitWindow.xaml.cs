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
using TescoSW.Global;
using TescoSW.OW.Remoting;
using URDemo.Shared;
using TescoSW.Linq.Extensions;

namespace URDemo.TestWPFApp
{
    /// <summary>
    /// Interakční logika pro MeasurementUnitWindow.xaml
    /// </summary>
    public partial class MeasurementUnitWindow : Window
    {
        private readonly IBLManager blManager;
        private readonly IServiceProvider serviceProvider;

        public MeasurementUnitWindow(IBLManager blManager, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.blManager = blManager;
            this.serviceProvider = serviceProvider;
        }

        public async Task LoadData()
        {
            progressBar.Visibility = Visibility.Visible;
            var customerQuery = blManager.Query<MeasurementUnit>();
            var query = (
                            string.IsNullOrEmpty(filterBox.Text) ?
                            customerQuery.Where(mu =>   mu.ID != 0) :
                            customerQuery.Where(mu =>   mu.Name.Contains(filterBox.Text, StringComparisonMode.IgnoreCaseAndAccent) ||
                                                        mu.Code.Contains(filterBox.Text, StringComparisonMode.IgnoreCaseAndAccent) || 
                                                        mu.TypeName.Contains(filterBox.Text, StringComparisonMode.IgnoreCaseAndAccent))
                         ).OrderBy(c => c.Name).Select( mu => new {mu.ID, mu.Name, mu.Code, mu.TypeName});

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
            if (dataGrid.SelectedItem is not null)
            {
                Result = ((dynamic)dataGrid.SelectedItem).ID;
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
