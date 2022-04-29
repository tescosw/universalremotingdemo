namespace URDemo.TestWebAPI
{
    public class InvoiceDTO
    {
        public long ID { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? CustomerName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalAmountIncludingVat { get; set; }

        public InvoiceLineDTO[]? InvoiceLines { get; set; }
    }

    public class InvoiceLineDTO
    {
        public long ID { get; set; }
        public long InvoiceID { get; set; }
        public decimal? Quantity { get; set; }
        public string? MeasurementUnit { get; set; }
        public string? Description { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? VAT { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalPriceWithVAT { get; set; }
    }
}