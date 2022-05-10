using Microsoft.EntityFrameworkCore;
using TescoSW.Tests.TestEFServer;
using URDemo.Server.Models;

namespace URDemo.Server.Data
{

    //EntityFrameworkCore\Add-Migration InitialCreationConfiguration -OutputDir Migrations\Application -Context DataContext
    //EntityFrameworkCore\Remove-Migration -Context DataContext
    //Script-Migration -Context DataContext

    public class DataContext : SqliteDataContext
    {
        public DataContext(DbContextOptions<DataContext> options, IConfiguration config) : base(options, config.GetConnectionString("DefaultConnection")) { }

        public DbSet<CMerna_Jednotka>? CMerna_Jednotka { get; set; }
        public DbSet<CDruh_MJ>? CDruh_MJ { get; set; }

        public DbSet<COdberatel>? COdberatel { get; set; }
        public DbSet<CFaktura>? CFaktura { get; set; }
        public DbSet<CRadek_Faktury>? CRadek_Faktury { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //https://docs.microsoft.com/cs-cz/ef/core/logging-events-diagnostics/
            optionsBuilder.LogTo(toLog => System.Diagnostics.Debug.WriteLine(toLog));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var mernaJednotka = modelBuilder.Entity<CMerna_Jednotka>();
            var druhMj = modelBuilder.Entity<CDruh_MJ>();
            var odberatel = modelBuilder.Entity<COdberatel>();
            var faktura = modelBuilder.Entity<CFaktura>();
            var radekFaktury = modelBuilder.Entity<CRadek_Faktury>();

            druhMj.HasData(
                new CDruh_MJ { ID = 1, Nazev = "Délkové", Kod = "DLK" },
                new CDruh_MJ { ID = 1000000000000000002, Nazev = "Plošné", Kod = "PLC" },
                new CDruh_MJ { ID = 1000000000000000003, Nazev = "Objemové", Kod = "OBJ" },
                new CDruh_MJ { ID = 4, Nazev = "Napěťové", Kod = "NAP" },
                new CDruh_MJ { ID = 1000000000000000005, Nazev = "Hmotnostní", Kod = "HMO" },
                new CDruh_MJ { ID = 6, Nazev = "Rychlostní", Kod = "RYC" },
                new CDruh_MJ { ID = 7, Nazev = "Frekvenční", Kod = "FRK" },
                new CDruh_MJ { ID = 8, Nazev = "Množstevní", Kod = "MNO" }
            );

            mernaJednotka.HasData(
                new CMerna_Jednotka { ID = 1, Nazev = "Metr", Kod = "m", Druh_MJID = 1, Koeficient_prepoctu = 1 },
                new CMerna_Jednotka { ID = 2, Nazev = "Kilometr", Kod = "km", Druh_MJID = 1, Koeficient_prepoctu = 1000 },
                new CMerna_Jednotka { ID = 3, Nazev = "Metr čtvereční", Kod = "m2", Druh_MJID = 1000000000000000002, Koeficient_prepoctu = 1 },
                new CMerna_Jednotka { ID = 4, Nazev = "Kilometr za hodinu", Kod = "km/h", Druh_MJID = 6, Koeficient_prepoctu = 0.2777777777777777 },
                new CMerna_Jednotka { ID = 5, Nazev = "Hertz", Kod = "hz", Druh_MJID = 7, Koeficient_prepoctu = 1 },
                new CMerna_Jednotka { ID = 6, Nazev = "Gram", Kod = "g", Druh_MJID = 1000000000000000005, Koeficient_prepoctu = 1 },
                new CMerna_Jednotka { ID = 7, Nazev = "Tuna", Kod = "t", Druh_MJID = 1000000000000000005, Koeficient_prepoctu = 1000000 },
                new CMerna_Jednotka { ID = 8, Nazev = "Kilogram", Kod = "kg", Druh_MJID = 1000000000000000005, Koeficient_prepoctu = 1000 },
                new CMerna_Jednotka { ID = 9, Nazev = "Kus", Kod = "ks", Druh_MJID = 8 }
            );

            odberatel.HasData(
                new COdberatel { ID = 1, Nazev = "Albert Česká republika, s.r.o.", IC = "44012373", DIC = "CZ44012373" },
                new COdberatel { ID = 2, Nazev = "Kaufland Česká republika v.o.s.", IC = "25110161", DIC = "CZ25110161" },
                new COdberatel { ID = 3, Nazev = "Lidl Česká republika v.o.s.", IC = "26178541", DIC = "CZ26178541" },
                new COdberatel { ID = 4, Nazev = "Tesco Stores ČR a.s.", IC = "45308314", DIC = "CZ45308314" },
                new COdberatel { ID = 5, Nazev = "Alza.cz a.s.", IC = "27082440", DIC = "CZ27082440" },
                new COdberatel { ID = 6, Nazev = "Internet Mall, a.s.", IC = "26204967", DIC = "CZ26204967" }
            );

            faktura.HasData(
                new CFaktura { ID = 1, Cislo_faktury="202200001", Datum_vystaveni=new DateTime(2022,1,1, 9,0,0), OdberatelID=1, Suma_s_DPH= 58600, Suma_celkem= 67390 },
                new CFaktura { ID = 2, Cislo_faktury = "202200002", Datum_vystaveni = new DateTime(2022, 1, 2, 10,0,0), OdberatelID = 1, Suma_s_DPH = 56400, Suma_celkem = 64860 },
                new CFaktura { ID = 3, Cislo_faktury = "202200003", Datum_vystaveni = new DateTime(2022, 1, 3, 9,30,0), OdberatelID = 4, Suma_s_DPH = 59400, Suma_celkem = 68310 }
            );

            radekFaktury.HasData(
                new CRadek_Faktury { ID = 1, FakturaID = 1, Mnozstvi = 500, JednotkaID = 9, Popis = "1kg bedýnka jahod", Cena_za_jednotku = 70, Cena_celkem = 35000, DPH = 15, Cena_celkem_s_DPH = 35000 * 1.15M },
                new CRadek_Faktury { ID = 2, FakturaID = 1, Mnozstvi = 2000, JednotkaID = 8, Popis = "Brambory", Cena_za_jednotku = 10, Cena_celkem = 20000, DPH = 15, Cena_celkem_s_DPH = 20000 * 1.15M },
                new CRadek_Faktury { ID = 3, FakturaID = 1, Mnozstvi = 200, JednotkaID = 9, Popis = "1kg balení pomerančů", Cena_za_jednotku = 18, Cena_celkem = 3600, DPH = 15, Cena_celkem_s_DPH = 3600 * 1.15M },
                new CRadek_Faktury { ID = 4, FakturaID = 2, Mnozstvi = 3000, JednotkaID = 8, Popis = "Brambory", Cena_za_jednotku = 10, Cena_celkem = 30000, DPH = 15, Cena_celkem_s_DPH = 30000 * 1.15M },
                new CRadek_Faktury { ID = 5, FakturaID = 2, Mnozstvi = 300, JednotkaID = 9, Popis = "1kg bedýnka jahod", Cena_za_jednotku = 70, Cena_celkem = 21000, DPH = 15, Cena_celkem_s_DPH = 21000 * 1.15M },
                new CRadek_Faktury { ID = 6, FakturaID = 2, Mnozstvi = 300, JednotkaID = 9, Popis = "1kg balení pomerančů", Cena_za_jednotku = 18, Cena_celkem = 5400, DPH = 15, Cena_celkem_s_DPH = 5400 * 1.15M },
                new CRadek_Faktury { ID = 7, FakturaID = 3, Mnozstvi = 300, JednotkaID = 9, Popis = "1kg balení pomerančů", Cena_za_jednotku = 18, Cena_celkem = 5400, DPH = 15, Cena_celkem_s_DPH = 5400 * 1.15M },
                new CRadek_Faktury { ID = 8, FakturaID = 3, Mnozstvi = 2, JednotkaID = 7, Popis = "Jablka", Cena_za_jednotku = 12000, Cena_celkem = 24000, DPH = 15, Cena_celkem_s_DPH = 24000 * 1.15M },
                new CRadek_Faktury { ID = 9, FakturaID = 3, Mnozstvi = 3000, JednotkaID = 8, Popis = "Brambory", Cena_za_jednotku = 10, Cena_celkem = 30000, DPH = 15, Cena_celkem_s_DPH = 30000 * 1.15M }
            );


        }
    }
}
