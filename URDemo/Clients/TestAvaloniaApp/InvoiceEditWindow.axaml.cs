using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using TescoSW.OW.Remoting;

namespace URDemo.TestAvaloniaApp
{
    public partial class InvoiceEditWindow : Window
    {
        private readonly long id;
        private readonly IBLManager blManager;
        private readonly IServiceProvider serviceProvider;

        public InvoiceEditWindow() : this(default, default, default) { }

        public InvoiceEditWindow(long id, IBLManager blManager, IServiceProvider serviceProvider)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
            this.id = id;
            this.blManager = blManager;
            this.serviceProvider = serviceProvider;
#endif

            /*
             TextChanged="filterBox_TextChanged"
            Click="newLineButton_Click"
            Click="editLineButton_Click" 
            Click="deleteLineButton_Click" 

            Click="selectCustomerButton_Click"

            Click="okButton_Click"
            Click="cancelButton_Click"
             */
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
