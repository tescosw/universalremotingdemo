using System;
using System.Runtime.Serialization;
using TescoSW.OW;
using TescoSW.OW.LocalDataCache;

namespace URDemo.Shared
{
#pragma warning disable CS1591

    [SyncStandardOWObjects("Last_modif_cas")]
    [DataContract(Name = "CDruh_MJ")]
    public class MeasurementUnitType
    {
        [DataMember]
        public long ID;
        [DataMember(Name = "Nazev")]
        public string? Name;
        [DataMember(Name = "Kod")]
        public string? Code;
        [DataMember(Name = "Plati_DO")]
        public DateTime? ValidTill;
        [DataMember(Name = "Create_cas"), SyncCreateTime, IgnoreChanges]
        public DateTime? CreateTime;
        public override string ToString()
        {
            return $"{ID}, {Code ?? "",-15}, {Name ?? "",-20}, {CreateTime, 19}, {ValidTill}";
        }
    }

#pragma warning restore CS1591
}
