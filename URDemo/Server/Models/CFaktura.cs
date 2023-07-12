using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TescoSW.Tests.TestEFServer;
using TescoSW.Tests.TestEFServer.Models;

namespace URDemo.Server.Models
{
    public class CFaktura : BaseBlEntity
    {
        public string? Cislo_faktury { get; set; }
        public DateTime? Datum_vystaveni { get; set; }

        public long OdberatelID { get; set; }
        public COdberatel? Odberatel { get; set; }

        public decimal? Suma_celkem { get; set; }
        public decimal? Suma_s_DPH { get; set; }

        public DateTime? Datum_zaplaceni{ get; set; }

        [NotMapped]
        public bool? JeZaplacena { get; set; }

        public void Oznacit_jako_zaplacenou(IOWObject blObject, IBlManager blManager)
        {
            blObject.SetValue(nameof(Datum_zaplaceni), DateTime.Now);
            blObject.SetValue(nameof(JeZaplacena), true);
            blObject.ApplyUpdates();
        }

        protected override void OnAfterCreate(IOWObject blObject, IBlManager blManager)
        {
            if (blObject.IsNew)
            {
                var count = blManager.GetCount(nameof(CFaktura), $"DBDate.DatePart('year', Datum_vystaveni) = DBDate.DatePart('year', 'SYSDATE')");
                blObject.SetValue(nameof(Datum_vystaveni), DateTime.Now);
                blObject.SetValue(nameof(Cislo_faktury), $"{DateTime.Now.Year}{count + 1:D5}");
            }
            blObject.SetValue(nameof(JeZaplacena), Datum_zaplaceni is not null);
            base.OnAfterCreate(blObject, blManager);
        }

        protected override void OnBeforeApplyUpdates(IOWObject blObject, IBlManager blManager)
        {
            if (ID is not null)
            {
                var data = blManager.GetData(nameof(CRadek_Faktury), new string[] { nameof(CRadek_Faktury.Cena_celkem), nameof(CRadek_Faktury.Cena_celkem_s_DPH) }, $"Faktura.ID = {ID}");
                blObject.SetValue(nameof(Suma_celkem), data.Sum(i => (decimal)(((dynamic)i).Cena_celkem)));
                blObject.SetValue(nameof(Suma_s_DPH), data.Sum(i => (decimal)(((dynamic)i).Cena_celkem_s_DPH)));
            }
            base.OnBeforeApplyUpdates(blObject, blManager);
        }

        public override string ToString()
        {
            return $"{ID}, {Cislo_faktury}, {Odberatel?.Nazev}, {Datum_vystaveni}, {Suma_celkem}, {Suma_s_DPH}";
        }
    }
}
