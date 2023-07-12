using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using TescoSW.OW.Remoting;
using TescoSW.OW.Remoting.Reports;

namespace URDemo.TestWPFApp
{
    /// <summary>
    /// Interakční logika pro InvoiceEditWindow.xaml
    /// </summary>
    public partial class InvoiceEditWindow : Window
    {
        private long id;
        private readonly bool newItem;
        private readonly IBLManager blManager;
        private readonly IServiceProvider serviceProvider;
        private readonly IReports reports;
        private IBLEntitiesContext? context;
        private bool edited = false;

        public InvoiceEditWindow(long id, IBLManager blManager, IServiceProvider serviceProvider, IReports reports)
        {
            InitializeComponent();
            newItem = (this.id = id) == -1;
            if (newItem)
                printButton.Visibility = Visibility.Hidden;

            this.blManager = blManager;
            this.serviceProvider = serviceProvider;
            this.reports = reports;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context = await blManager.CreateContextAsync();
            if (newItem)
            {
                DataContext = await context.NewAsync("CFaktura");
            }
            else
            {
                DataContext = await context.GetAsync(id, "CFaktura");
                printButton.IsEnabled = true;
                await LoadGrid();
            }
        }

        private async Task LoadGrid()
        {
            progressBar.Visibility = Visibility.Visible;
            var condition = string.IsNullOrEmpty(filterBox.Text) ? $"Faktura.ID = {id}" : $"Faktura.ID = {id} and :Linguistic.Like(Popis, '%{filterBox.Text}%')";
            var source = await blManager.GetOnlineDataRows("CRadek_Faktury", new string[] { "Mnozstvi", "Jednotka.Kod", "Popis", "Cena_za_jednotku", "Cena_celkem", "DPH", "Cena_celkem_s_DPH" }, condition, new DataRowsParams { OrderBy = new string[] { "id" } });
            dataGrid.ItemsSource = source;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private async void selectCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var window = serviceProvider.GetRequiredService<CustomersWindow>();
            if (window.ShowDialog() == true)
                await ((IBLEntity)DataContext).SetValueAsync("Odberatel.ID", window.Result);
        }

        private async void newLineButton_Click(object sender, RoutedEventArgs e)
        {
            if (id == -1)
            {
                var obj = ((IBLEntity)DataContext);
                await obj.ApplyUpdatesAsync();
                id = (long)await obj.GetValueAsync("ID");
            }
            var windows = ActivatorUtilities.CreateInstance<InvoiceLineWindow>(serviceProvider, new object[] { -1L, id, context!, });
            if (windows.ShowDialog() == true)
            {
                await LoadGrid();
                edited = true;
            }
        }

        private async void editLineButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var windows = ActivatorUtilities.CreateInstance<InvoiceLineWindow>(serviceProvider, new object[] { ((dynamic)dataGrid.SelectedItem).Id, id, context!, });
                if (windows.ShowDialog() == true)
                {
                    await LoadGrid();
                    edited = true;
                }
            }
        }

        private async void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (newItem)
                await ((IBLEntity)DataContext).DeleteAsync();
        }

        private async void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (edited)
                await ((IBLEntity)DataContext).ApplyUpdatesAsync();
            DialogResult = true;
        }

        private async void filterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            await LoadGrid();
        }

        private async void deleteLineButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                IBLEntity obj = await context!.GetAsync(((dynamic)dataGrid.SelectedItem).Id, "CRadek_faktury");
                await obj.DeleteAsync();
                await LoadGrid();
                edited = true;
            }
        }

        private async void printButton_Click(object sender, RoutedEventArgs e)
        {
            var obj = (IBLEntity)DataContext;
            // get entity detail report
            using var report = await reports.GetDetailReport(obj, "FrameDetail1", "FRM.CFaktura.CFaktura", ReportOutputTypes.PDF);

            var dialog = new SaveFileDialog()
            {
                RestoreDirectory = true,
                Filter = "PDF file|*.pdf",
                DefaultExt = "pdf",
            };
            if (dialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(dialog.FileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dialog.FileName)!);
                using var file = dialog.OpenFile();
                report.CopyTo(file);
            }
        }
    }
}