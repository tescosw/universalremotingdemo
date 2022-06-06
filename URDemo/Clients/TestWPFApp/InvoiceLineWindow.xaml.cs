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
    /// Interakční logika pro InvoiceLineWindow.xaml
    /// </summary>
    public partial class InvoiceLineWindow : Window
    {
        private readonly long id;
        private readonly long invoiceId;
        private readonly IBLEntitiesContext context;
        private readonly IBLManager blManager;
        private readonly IServiceProvider serviceProvider;

        public InvoiceLineWindow(long id, long invoiceId, IBLEntitiesContext context, IBLManager blManager, IServiceProvider serviceProvider)
        {            
            this.id = id;
            this.invoiceId = invoiceId;
            this.context = context;
            this.blManager = blManager;
            this.serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id <= 0)
            {
                var obj = await context.NewAsync("CRadek_Faktury");
                await obj.SetValueAsync("Faktura.ID", invoiceId);
                DataContext = obj;
            }
            else
                DataContext = await context.GetAsync(id, "CRadek_Faktury");
        }

        private async void selectMeasureUnitButton_Click(object sender, RoutedEventArgs e)
        {
            var window = serviceProvider.GetRequiredService<MeasurementUnitWindow>();
            if (window.ShowDialog() == true)
                await ((IBLEntity)DataContext).SetValueAsync("Jednotka.ID", window.Result);
        }

        private async void okButton_Click(object sender, RoutedEventArgs e)
        {
            await ((IBLEntity)DataContext).ApplyUpdatesAsync();
            DialogResult = true;
        }
    }
}
