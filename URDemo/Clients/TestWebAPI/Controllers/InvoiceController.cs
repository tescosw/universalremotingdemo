using Microsoft.AspNetCore.Mvc;
using TescoSW.Linq.Extensions;
using TescoSW.OW.Remoting;
using System.Linq;
using URDemo.Shared;

namespace URDemo.TestWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IBLManager blManager;
        private readonly IBLAuthentication blAuthentication;

        public InvoiceController(ILogger<InvoiceController> logger, IBLManager blManager, IBLAuthentication blAuthentication)
        {
            _logger = logger;
            this.blManager = blManager;
            this.blAuthentication = blAuthentication;
        }

        [HttpGet(Name = "GetInvoices")]
        public async Task<IEnumerable<InvoiceDTO>> Get([FromQuery] int year=2022)
        {
            await blAuthentication.Login("Demo", "Demo", "");

            var invoices = await (
                    from i in blManager.Query<Invoice>()
                    where i.IssueDate!.Value.Year == year
                    orderby i.InvoiceNumber
                    select new InvoiceDTO
                    {
                        ID = i.ID,
                        InvoiceNumber = i.InvoiceNumber,
                        IssueDate = i.IssueDate,
                        CustomerName = i.CustomerName,
                        TotalAmount = i.TotalAmount,
                        TotalAmountIncludingVat = i.TotalAmountIncludingVat
                    }
                 ).ToArrayAsync();

            var invoiceLines = await (
                       from il in blManager.Query<InvoiceLine>()
                       where blManager.Query<Invoice>().Any(i => il.InvoiceID == i.ID && i.IssueDate!.Value.Year == year) 
                       orderby il.ID
                       select new InvoiceLineDTO
                       {
                           ID = il.ID,
                           InvoiceID = il.InvoiceID,
                           Quantity = il.Quantity,
                           MeasurementUnit = il.MeasurementUnit,
                           Description = il.Description,
                           PricePerUnit = il.PricePerUnit,
                           VAT = il.VAT,
                           TotalPrice = il.TotalPrice,
                           TotalPriceWithVAT = il.TotalPriceWithVAT
                       }
                ).ToArrayAsync();

            foreach (var invoice in invoices)
                invoice.InvoiceLines = invoiceLines.Where(il=>il.InvoiceID == invoice.ID).ToArray();

            await blAuthentication.Logout();

            return invoices;
        }
    }
}