using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TescoSW.Tests.TestEFServer.Models;

namespace URDemo.Server.Models
{
    public class CDruh_MJ : BaseBlEntity
    {
        [MaxLength(255)]
        public string? Nazev { get; set; }
        [MaxLength(32)]
        public string? Kod { get; set; }
        public string? Popis { get; set; }

        public DateTime? Plati_DO { get; set; }
    }
}
