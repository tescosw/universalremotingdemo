using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TescoSW.OW;

namespace URDemo.Shared
{
#pragma warning disable CS1591

    [DataContract(Name = "CRadek_Faktury")]
    public class InvoiceLine
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember(Name = "Faktura.ID")]
        public long InvoiceID { get; set; }
        [DataMember(Name = "Mnozstvi")]
        public decimal? Quantity { get; set; }
        [DataMember(Name = "Jednotka.Kod"), IgnoreChanges]
        public string? MeasurementUnit { get; set; }
        [DataMember(Name = "Popis")]
        public string? Description { get; set; }
        [DataMember(Name = "Cena_za_jednotku")]
        public decimal? PricePerUnit { get; set; }
        [DataMember(Name = "DPH")]
        public decimal? VAT { get; set; }
        [DataMember(Name = "Cena_celkem")]
        public decimal? TotalPrice { get; set; }
        [DataMember(Name = "Cena_celkem_s_DPH")]
        public decimal? TotalPriceWithVAT { get; set; }
        public override string ToString()
        {
            return $"{ID}, {InvoiceID}, {Quantity}, {MeasurementUnit}, {Description}, {PricePerUnit}, {VAT}, {TotalPrice}, {TotalPriceWithVAT}";
        }
    }

#pragma warning restore CS1591
}
