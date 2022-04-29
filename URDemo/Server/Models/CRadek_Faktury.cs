using System;
using TescoSW.Tests.TestEFServer;
using TescoSW.Tests.TestEFServer.Models;

namespace URDemo.Server.Models
{
    public class CRadek_Faktury : BaseBlEntity
    {
        public long FakturaID { get; set; }
        public CFaktura? Faktura { get; set; }
        public decimal? Mnozstvi { get; set; }

        public long JednotkaID { get; set; }
        public CMerna_Jednotka? Jednotka { get; set; }
        public string? Popis { get; set; }
        public decimal? Cena_za_jednotku { get; set; }
        public decimal? DPH { get; set; }
        public decimal? Cena_celkem { get; set; }
        public decimal? Cena_celkem_s_DPH { get; set; }

        protected override void OnAttributeDataChanged(IOWObject blObject, IBlManager blManager, string attribut_name)
        {
            if (string.Equals(attribut_name, nameof(Cena_za_jednotku), StringComparison.OrdinalIgnoreCase))
            {
                if (Cena_za_jednotku is null)
                {
                    blObject.SetValue(nameof(Cena_celkem_s_DPH), null);
                    blObject.SetValue(nameof(Cena_celkem), null);
                }
                else
                {
                    blObject.SetValue(nameof(Cena_celkem), (Mnozstvi ?? 1) * Cena_za_jednotku);
                    blObject.SetValue(nameof(Cena_celkem_s_DPH), Cena_celkem * (DPH is null ? 1 : (DPH / 100) + 1));
                }
                return;
            }
            if (string.Equals(attribut_name, nameof(DPH), StringComparison.OrdinalIgnoreCase))
            {
                if (Cena_za_jednotku is not null)
                {
                    blObject.SetValue(nameof(Cena_celkem), (Mnozstvi ?? 1) * Cena_za_jednotku);
                    blObject.SetValue(nameof(Cena_celkem_s_DPH), Cena_celkem * (DPH is null ? 1 : (DPH / 100) + 1));
                    return;
                }
                if (Cena_celkem is not null)
                {
                    blObject.SetValue(nameof(Cena_celkem_s_DPH), Cena_celkem * (DPH is null ? 1 : (DPH / 100) + 1));
                    return;
                }
                if (Cena_celkem_s_DPH is not null)
                {
                    blObject.SetValue(nameof(Cena_celkem), Cena_celkem_s_DPH / (DPH is null ? 1 : (DPH / 100) + 1));
                    return;
                }
            }
            if (string.Equals(attribut_name, nameof(Cena_celkem_s_DPH), StringComparison.OrdinalIgnoreCase) && Cena_za_jednotku is null)
            {
                blObject.SetValue(nameof(Cena_celkem), Cena_celkem_s_DPH / (DPH is null ? 1 : (DPH / 100) + 1));
                blObject.SetValue(nameof(Cena_za_jednotku), Cena_celkem / (Mnozstvi ?? 1));
                return;
            }
            base.OnAttributeDataChanged(blObject, blManager, attribut_name);
        }
    }
}
