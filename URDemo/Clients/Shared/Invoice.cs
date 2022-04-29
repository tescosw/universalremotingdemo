using System.Runtime.Serialization;
using TescoSW.OW;

namespace URDemo.Shared
{
#pragma warning disable CS1591

    [DataContract(Name = "CFaktura")]
    public class Invoice
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember(Name = "Cislo_faktury")]
        public string? InvoiceNumber { get; set; }
        [DataMember(Name = "Datum_vystaveni")]
        public DateTime? IssueDate { get; set; }
        [DataMember(Name = "Odberatel.Nazev"), IgnoreChanges]
        public string? CustomerName { get; set; }
        [DataMember(Name = "Suma_celkem")]
        public decimal? TotalAmount { get; set; }
        [DataMember(Name = "Suma_s_DPH")]
        public decimal? TotalAmountIncludingVat { get; set; }
        public override string ToString()
        {
            return $"{ID}, {InvoiceNumber}, {CustomerName}, {IssueDate}, {TotalAmount}, {TotalAmountIncludingVat}";
        }
    }

#pragma warning restore CS1591
}
