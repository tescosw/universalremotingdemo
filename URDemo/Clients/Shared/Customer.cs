using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TescoSW.OW;
using TescoSW.OW.LocalDataCache;

namespace URDemo.Shared
{
#pragma warning disable CS1591

    [SyncStandardOWObjects("Last_modif_cas")]
    [DataContract(Name = "COdberatel")]
    public class Customer
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember(Name = "Nazev")]
        public string? Name { get; set; }
        [DataMember]
        public string? IC { get; set; }
        [DataMember(Name = "DIC")]
        public string? VatId { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Name ?? "",32}, {IC ?? "",20}, , {VatId ?? "",20}";
        }
    }

#pragma warning restore CS1591
}
