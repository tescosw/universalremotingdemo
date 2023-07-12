using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using TescoSW.OW.Remoting;
using TescoSW.OW.Remoting.Reports;

namespace URDemo.TestWPFApp
{
    /// <summary>
    /// Interaction logic for InvoiceListWindow.xaml
    /// </summary>
    public partial class InvoiceListWindow : Window
    {
        private readonly IBLManager blManager;
        private readonly IServiceProvider serviceProvider;
        private readonly IReports reports;

        public InvoiceListWindow(IBLManager blManager, IServiceProvider serviceProvider, IReports reports)
        {
            this.blManager = blManager;
            this.serviceProvider = serviceProvider;
            this.reports = reports;
            InitializeComponent();
        }

        public async Task LoadData()
        {
            progressBar.Visibility = Visibility.Visible;
            var condition = getCondition();
            var source = await blManager.GetOnlineDataRows("CFaktura", new string[] { "Datum_vystaveni", "Cislo_faktury", "Odberatel.Nazev", "Suma_celkem", "Suma_s_DPH", "Datum_zaplaceni" }, condition, new DataRowsParams { OrderBy = new string[] { "Cislo_faktury" } });
            dataGrid.ItemsSource = source;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private string getCondition()
        {
            return string.IsNullOrEmpty(filterBox.Text) ? "ID <> 0" : $":Linguistic.Like(Cislo_faktury, '%{filterBox.Text}%') or :Linguistic.Like(Odberatel.Nazev, '%{filterBox.Text}%')";
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

        private async void printButton_Click(object sender, RoutedEventArgs e)
        {
            var @params = new TableExportParams()
            {
                ExportType = ReportOutputTypes.DOCX, // desired type of the report file - PDF, DOCX, XLSX for reports
                EntityTypeName = "CFaktura",
                Condition = getCondition(), // condition for data selection
                OrderBy = new[] { "Cislo_faktury" },
                Attributes = new TableExportAttrParams[] // attributes to select from the entity (analogous to GetOnlineDataRows)
                {
                        new() { AttributeName = "Datum_vystaveni", },
                        new() { AttributeName = "Cislo_faktury", },
                        new() { AttributeName = "Odberatel.Nazev", },
                        new() { AttributeName = "Suma_celkem", },
                        new() { AttributeName = "Suma_s_DPH", },
                        new() { AttributeName = "Datum_zaplaceni", },
                }
            };

            using var report = await reports.GetFrameReport(@params); // get list/frame report
            var dialog = new SaveFileDialog()
            {
                RestoreDirectory = true,
                Filter = "DOCX file|*.docx",
                DefaultExt = "docx",
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