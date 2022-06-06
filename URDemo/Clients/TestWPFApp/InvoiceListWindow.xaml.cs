using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using TescoSW.OW.Remoting;

namespace URDemo.TestWPFApp
{
    /// <summary>
    /// Interaction logic for InvoiceListWindow.xaml
    /// </summary>
    public partial class InvoiceListWindow : Window
    {
        private readonly IBLManager blManager;
        private readonly IServiceProvider serviceProvider;

        public InvoiceListWindow(IBLManager blManager, IServiceProvider serviceProvider)
        {
            this.blManager = blManager;
            this.serviceProvider = serviceProvider;
            InitializeComponent();            
        }

        public async Task LoadData()
        {
            progressBar.Visibility = Visibility.Visible;
            var condition = string.IsNullOrEmpty(filterBox.Text) ? "ID <> 0" : $":Linguistic.Like(Cislo_faktury, '%{filterBox.Text}%') or :Linguistic.Like(Odberatel.Nazev, '%{filterBox.Text}%')";
            var source = await blManager.GetOnlineDataRows("CFaktura", new string[] { "Datum_vystaveni", "Cislo_faktury", "Odberatel.Nazev", "Suma_celkem", "Suma_s_DPH", "Datum_zaplaceni" }, condition, new DataRowsParams { OrderBy = new string[] { "Cislo_faktury" } });
            dataGrid.ItemsSource = source;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private async void newInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            var windows = ActivatorUtilities.CreateInstance<InvoiceEditWindow>(serviceProvider, new object[] { -1L, });
            if (windows.ShowDialog() == true)
                await LoadData();
        }

        private async void showInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var windows = ActivatorUtilities.CreateInstance<InvoiceEditWindow>(serviceProvider, new object[] { ((dynamic)dataGrid.SelectedItem).Id, });
                if (windows.ShowDialog() == true)
                    await LoadData();
            }
        }

        private async void markAsPaidButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                await using var context = await blManager.CreateContextAsync();
                await using IBLEntity obj = await context.GetAsync(((dynamic)dataGrid.SelectedItem).Id, "CFaktura");
                await obj.ExecuteMethodAsync("Oznacit_jako_zaplacenou");
                await LoadData();
            }
        }

        private async void filterBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            await LoadData();
        }
    }
}
