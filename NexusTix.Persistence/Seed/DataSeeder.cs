using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!await context.Cities.AnyAsync())
            {
                var cities = GetCities();
                await context.Cities.AddRangeAsync(cities);
                await context.SaveChangesAsync();
            }

            if (!await context.Districts.AnyAsync())
            {
                var citiesFromDb = await context.Cities.AsNoTracking().ToListAsync();

                var districts = GetDistricts(citiesFromDb);
                await context.Districts.AddRangeAsync(districts);
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<City> GetCities()
        {
            return
            [
                new City { Name = "ADANA" },
                new City { Name = "ADIYAMAN" },
                new City { Name = "AFYONKARAHİSAR" },
                new City { Name = "AĞRI" },
                new City { Name = "AMASYA" },
                new City { Name = "ANKARA" },
                new City { Name = "ANTALYA" },
                new City { Name = "ARTVİN" },
                new City { Name = "AYDIN" },
                new City { Name = "BALIKESİR" },
                new City { Name = "BİLECİK" },
                new City { Name = "BİNGÖL" },
                new City { Name = "BİTLİS" },
                new City { Name = "BOLU" },
                new City { Name = "BURDUR" },
                new City { Name = "BURSA" },
                new City { Name = "ÇANAKKALE" },
                new City { Name = "ÇANKIRI" },
                new City { Name = "ÇORUM" },
                new City { Name = "DENİZLİ" },
                new City { Name = "DİYARBAKIR" },
                new City { Name = "EDİRNE" },
                new City { Name = "ELAZIĞ" },
                new City { Name = "ERZİNCAN" },
                new City { Name = "ERZURUM" },
                new City { Name = "ESKİŞEHİR" },
                new City { Name = "GAZİANTEP" },
                new City { Name = "GİRESUN" },
                new City { Name = "GÜMÜŞHANE" },
                new City { Name = "HAKKARİ" },
                new City { Name = "HATAY" },
                new City { Name = "ISPARTA" },
                new City { Name = "MERSİN" },
                new City { Name = "İSTANBUL" },
                new City { Name = "İZMİR" },
                new City { Name = "KARS" },
                new City { Name = "KASTAMONU" },
                new City { Name = "KAYSERİ" },
                new City { Name = "KIRKLARELİ" },
                new City { Name = "KIRŞEHİR" },
                new City { Name = "KOCAELİ" },
                new City { Name = "KONYA" },
                new City { Name = "KÜTAHYA" },
                new City { Name = "MALATYA" },
                new City { Name = "MANİSA" },
                new City { Name = "KAHRAMANMARAŞ" },
                new City { Name = "MARDİN" },
                new City { Name = "MUĞLA" },
                new City { Name = "MUŞ" },
                new City { Name = "NEVŞEHİR" },
                new City { Name = "NİĞDE" },
                new City { Name = "ORDU" },
                new City { Name = "RİZE" },
                new City { Name = "SAKARYA" },
                new City { Name = "SAMSUN" },
                new City { Name = "SİİRT" },
                new City { Name = "SİNOP" },
                new City { Name = "SİVAS" },
                new City { Name = "TEKİRDAĞ" },
                new City { Name = "TOKAT" },
                new City { Name = "TRABZON" },
                new City { Name = "TUNCELİ" },
                new City { Name = "ŞANLIURFA" },
                new City { Name = "UŞAK" },
                new City { Name = "VAN" },
                new City { Name = "YOZGAT" },
                new City { Name = "ZONGULDAK" },
                new City { Name = "AKSARAY" },
                new City { Name = "BAYBURT" },
                new City { Name = "KARAMAN" },
                new City { Name = "KIRIKKALE" },
                new City { Name = "BATMAN" },
                new City { Name = "ŞIRNAK" },
                new City { Name = "BARTIN" },
                new City { Name = "ARDAHAN" },
                new City { Name = "IĞDIR" },
                new City { Name = "YALOVA" },
                new City { Name = "KARABÜK" },
                new City { Name = "KİLİS" },
                new City { Name = "OSMANİYE" },
                new City { Name = "DÜZCE" }
            ];
        }

        private static IEnumerable<District> GetDistricts(List<City> cities)
        {
            var cityMap = cities.ToDictionary(c => c.Name, c => c.Id);

            return
            [

            ];
        }
    }
}
