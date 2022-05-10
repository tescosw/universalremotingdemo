using System;
using System.Runtime.Serialization;
using TescoSW.OW;
using TescoSW.OW.LocalDataCache;

namespace URDemo.Shared
{
#pragma warning disable CS1591

    [SyncStandardOWObjects("Last_modif_cas")]
    [DataContract(Name = "CMerna_Jednotka")]
    public class MeasurementUnit
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember(Name = "Nazev")]
        public string? Name { get; set; }
        [DataMember(Name = "Kod")]
        public string? Code { get; set; }
        [DataMember(Name = "Druh_MJ.Nazev"), IgnoreChanges]
        public string? TypeName { get; set; }
        [DataMember(Name = "Druh_MJ.ID")]
        public long TypeID { get; set; }
        [DataMember(Name = "Plati_DO")]
        public DateTime? ValidTill { get; set; }
        [DataMember(Name = "Create_cas"), SyncCreateTime, IgnoreChanges]
        public DateTime? CreateTime { get; set; }
        [DataMember(Name = "Koeficient_prepoctu")]
        public double ConversionFactor { get; set; }
        public override string ToString()
        {
            return $"{ID}, {Code ?? "",-15}, {Name ?? "",-20}, {TypeName,-20}, {CreateTime, 19}, {ValidTill}, {ConversionFactor}";
        }
    }

#pragma warning restore CS1591
}
