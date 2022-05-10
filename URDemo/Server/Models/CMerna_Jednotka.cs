using System.ComponentModel.DataAnnotations;
using TescoSW.Tests.TestEFServer.Models;

namespace URDemo.Server.Models
{
    public class CMerna_Jednotka : BaseBlEntity
    {
        [MaxLength(255)]
        public string? Nazev { get; set; }
        [MaxLength(32)]
        public string? Kod { get; set; }

        public long Druh_MJID { get; set; }
        public CDruh_MJ? Druh_MJ { get; set; }
        public string? Popis { get; set; }
        public double? Koeficient_prepoctu { get; set; }

        public DateTime? Plati_DO { get; set; }

        public object? NaZakladniJednotky(params object[] values) =>
            Convert.ToDouble(values.FirstOrDefault() ?? throw new ArgumentNullException(nameof(values))) * Koeficient_prepoctu;
    }
}
