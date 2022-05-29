using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TescoSW.OW.Remoting;
using URDemo.Shared;

namespace URDemo.TestAvaloniaApp
{
    public partial class InvoiceListWindow : Window
    {
        private readonly IBLManager? blManager;
        private readonly IServiceProvider? serviceProvider;

        public InvoiceListWindow() : this(null, null) { }

        public InvoiceListWindow(IBLManager? blManager, IServiceProvider? serviceProvider)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            if (serviceProvider is null) return;

            var filterBox = this.Find<TextBox>("filterBox");
            filterBox.TextInput += filterBox_TextInput;
            var newInvoiceButton = this.Find<Button>("newInvoiceButton");
            newInvoiceButton.Click += newInvoiceButton_Click;
            var showInvoiceButton = this.Find<Button>("showInvoiceButton");
            showInvoiceButton.Click += showInvoiceButton_Click;
            var markAsPaidButton = this.Find<Button>("markAsPaidButton");
            markAsPaidButton.Click += markAsPaidButton_Click;
            this.blManager = blManager;
            this.serviceProvider = serviceProvider;
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
            LoadData();
        }

        public async Task LoadData()
        {
            if (blManager is null) return;            
            var progressBar = this.Find<ProgressBar>("progressBar");
            var filterBox = this.Find<TextBox>("filterBox");
            progressBar.IsVisible = true;
            var condition = string.IsNullOrEmpty(filterBox.Text) ? "ID <> 0" : $":Linguistic.Like(Cislo_faktury, '%{filterBox.Text}%') or :Linguistic.Like(Odberatel.Nazev, '%{filterBox.Text}%')";
            var source = await blManager.GetOnlineDataRows<Invoice>(condition, new DataRowsParams { OrderBy = new string[] { "Cislo_faktury" } });
            var dataGrid = this.Find<DataGrid>("dataGrid");
            dataGrid.Items = source;
            progressBar.IsVisible = false;
        }

        private void markAsPaidButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private async void showInvoiceButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var dataGrid = this.Find<DataGrid>("dataGrid");
            if (dataGrid.SelectedItem != null)
            {
                long id = ((Invoice)dataGrid.SelectedItem).ID;
                var windows = ActivatorUtilities.CreateInstance<InvoiceEditWindow>(serviceProvider, new object[] { id, });
                if (await windows.ShowDialog<bool>(this) == true)
                    await LoadData();
            }
        }

        private void newInvoiceButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void filterBox_TextInput(object? sender, Avalonia.Input.TextInputEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
