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

            if (!await context.EventTypes.AnyAsync())
            {
                var eventTypes = GetEventTypes();
                await context.EventTypes.AddRangeAsync(eventTypes);
                await context.SaveChangesAsync();
            }

            if (!await context.Venues.AnyAsync())
            {
                var districtsFromDb = await context.Districts.Include(d => d.City).AsNoTracking().ToListAsync();

                if (districtsFromDb.Any())
                {
                    var venues = GetVenues(districtsFromDb);
                    await context.Venues.AddRangeAsync(venues);
                    await context.SaveChangesAsync();
                }
            }

            if (!await context.Events.AnyAsync())
            {
                var allEventTypes = await context.EventTypes.AsNoTracking().ToListAsync();
                var allVenues = await context.Venues.AsNoTracking().ToListAsync();

                if (allEventTypes.Any() && allVenues.Any())
                {
                    var events = GetEvents(allEventTypes, allVenues);
                    await context.Events.AddRangeAsync(events);
                    await context.SaveChangesAsync();
                }
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
                // --- ADANA İLÇELERİ ---
                new District { Name = "ALADAĞ", CityId = cityMap["ADANA"] },
                new District { Name = "CEYHAN", CityId = cityMap["ADANA"] },
                new District { Name = "ÇUKUROVA", CityId = cityMap["ADANA"] },
                new District { Name = "FEKE", CityId = cityMap["ADANA"] },
                new District { Name = "İNCİRLİK", CityId = cityMap["ADANA"] },
                new District { Name = "İMAMOĞLU", CityId = cityMap["ADANA"] },
                new District { Name = "KARAİSALI", CityId = cityMap["ADANA"] },
                new District { Name = "KARATAŞ", CityId = cityMap["ADANA"] },
                new District { Name = "KOZAN", CityId = cityMap["ADANA"] },
                new District { Name = "POZANTI", CityId = cityMap["ADANA"] },
                new District { Name = "SAİMBEYLİ", CityId = cityMap["ADANA"] },
                new District { Name = "SARIÇAM", CityId = cityMap["ADANA"] },
                new District { Name = "SEYHAN", CityId = cityMap["ADANA"] },
                new District { Name = "TUFANBEYLİ", CityId = cityMap["ADANA"] },
                new District { Name = "YUMURTALIK", CityId = cityMap["ADANA"] },
                new District { Name = "YÜREĞİR", CityId = cityMap["ADANA"] },

                // --- ADIYAMAN İLÇELERİ ---
                new District { Name = "BESNİ", CityId = cityMap["ADIYAMAN"] },
                new District { Name = "ÇELİKHAN", CityId = cityMap["ADIYAMAN"] },
                new District { Name = "GERGER", CityId = cityMap["ADIYAMAN"] },
                new District { Name = "GÖLBAŞI", CityId = cityMap["ADIYAMAN"] },
                new District { Name = "KAHTA", CityId = cityMap["ADIYAMAN"] },
                new District { Name = "MERKEZ", CityId = cityMap["ADIYAMAN"] },
                new District { Name = "SAMSAT", CityId = cityMap["ADIYAMAN"] },
                new District { Name = "SİNCİK", CityId = cityMap["ADIYAMAN"] },
                new District { Name = "TUT", CityId = cityMap["ADIYAMAN"] },

                // --- AFYONKARAHİSAR İLÇELERİ ---
                new District { Name = "BAŞMAKÇI", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "BAYAT", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "BOLVADİN", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "ÇAY", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "ÇOBANLAR", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "DAZKIRI", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "DİNAR", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "EMİRDAĞ", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "EVCİLER", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "HOCALAR", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "İHSANİYE", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "İSCEHİSAR", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "KIZILÖREN", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "MERKEZ", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "SANDIKLI", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "SİNANPAŞA", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "SULTANDAĞI", CityId = cityMap["AFYONKARAHİSAR"] },
                new District { Name = "ŞUHUT", CityId = cityMap["AFYONKARAHİSAR"] },

                // --- AĞRI İLÇELERİ ---
                new District { Name = "DİYADİN", CityId = cityMap["AĞRI"] },
                new District { Name = "DOĞUBAYAZIT", CityId = cityMap["AĞRI"] },
                new District { Name = "ELEŞKİRT", CityId = cityMap["AĞRI"] },
                new District { Name = "HAMUR", CityId = cityMap["AĞRI"] },
                new District { Name = "MERKEZ", CityId = cityMap["AĞRI"] },
                new District { Name = "PATNOS", CityId = cityMap["AĞRI"] },
                new District { Name = "TAŞLIÇAY", CityId = cityMap["AĞRI"] },
                new District { Name = "TUTAK", CityId = cityMap["AĞRI"] },

                // --- AMASYA İLÇELERİ ---
                new District { Name = "GÖYNÜCEK", CityId = cityMap["AMASYA"] },
                new District { Name = "GÜMÜŞHACIKÖY", CityId = cityMap["AMASYA"] },
                new District { Name = "HAMAMÖZÜ", CityId = cityMap["AMASYA"] },
                new District { Name = "MERKEZ", CityId = cityMap["AMASYA"] },
                new District { Name = "MERZİFON", CityId = cityMap["AMASYA"] },
                new District { Name = "SULUOVA", CityId = cityMap["AMASYA"] },
                new District { Name = "TAŞOVA", CityId = cityMap["AMASYA"] },

                // --- ANKARA İLÇELERİ ---
                new District { Name = "AKYURT", CityId = cityMap["ANKARA"] },
                new District { Name = "ALTINDAĞ", CityId = cityMap["ANKARA"] },
                new District { Name = "AYAŞ", CityId = cityMap["ANKARA"] },
                new District { Name = "BALA", CityId = cityMap["ANKARA"] },
                new District { Name = "BEYPAZARI", CityId = cityMap["ANKARA"] },
                new District { Name = "ÇAMLIDERE", CityId = cityMap["ANKARA"] },
                new District { Name = "ÇANKAYA", CityId = cityMap["ANKARA"] },
                new District { Name = "ÇUBUK", CityId = cityMap["ANKARA"] },
                new District { Name = "ELMADAĞ", CityId = cityMap["ANKARA"] },
                new District { Name = "ETİMESGUT", CityId = cityMap["ANKARA"] },
                new District { Name = "EVREN", CityId = cityMap["ANKARA"] },
                new District { Name = "GÖLBAŞI", CityId = cityMap["ANKARA"] },
                new District { Name = "GÜDÜL", CityId = cityMap["ANKARA"] },
                new District { Name = "HAYMANA", CityId = cityMap["ANKARA"] },
                new District { Name = "KAHRAMANKAZAN", CityId = cityMap["ANKARA"] },
                new District { Name = "KALECİK", CityId = cityMap["ANKARA"] },
                new District { Name = "KEÇİÖREN", CityId = cityMap["ANKARA"] },
                new District { Name = "KIZILCAHAMAM", CityId = cityMap["ANKARA"] },
                new District { Name = "MAMAK", CityId = cityMap["ANKARA"] },
                new District { Name = "NALLIHAN", CityId = cityMap["ANKARA"] },
                new District { Name = "POLATLI", CityId = cityMap["ANKARA"] },
                new District { Name = "PURSAKLAR", CityId = cityMap["ANKARA"] },
                new District { Name = "SİNCAN", CityId = cityMap["ANKARA"] },
                new District { Name = "ŞEREFLİKOÇHİSAR", CityId = cityMap["ANKARA"] },
                new District { Name = "YENİMAHALLE", CityId = cityMap["ANKARA"] },

                // --- ANTALYA İLÇELERİ ---
                new District { Name = "AKSEKİ", CityId = cityMap["ANTALYA"] },
                new District { Name = "AKSU", CityId = cityMap["ANTALYA"] },
                new District { Name = "ALANYA", CityId = cityMap["ANTALYA"] },
                new District { Name = "DEMRE", CityId = cityMap["ANTALYA"] },
                new District { Name = "DÖŞEMEALTI", CityId = cityMap["ANTALYA"] },
                new District { Name = "ELMALI", CityId = cityMap["ANTALYA"] },
                new District { Name = "FİNİKE", CityId = cityMap["ANTALYA"] },
                new District { Name = "GAZİPAŞA", CityId = cityMap["ANTALYA"] },
                new District { Name = "GÜNDOĞMUŞ", CityId = cityMap["ANTALYA"] },
                new District { Name = "İBRADI", CityId = cityMap["ANTALYA"] },
                new District { Name = "KAŞ", CityId = cityMap["ANTALYA"] },
                new District { Name = "KEMER", CityId = cityMap["ANTALYA"] },
                new District { Name = "KEPEZ", CityId = cityMap["ANTALYA"] },
                new District { Name = "KONYAALTI", CityId = cityMap["ANTALYA"] },
                new District { Name = "KORKUTELİ", CityId = cityMap["ANTALYA"] },
                new District { Name = "KUMLUCA", CityId = cityMap["ANTALYA"] },
                new District { Name = "MANAVGAT", CityId = cityMap["ANTALYA"] },
                new District { Name = "MURATPAŞA", CityId = cityMap["ANTALYA"] },
                new District { Name = "SERİK", CityId = cityMap["ANTALYA"] },

                // --- ARTVİN İLÇELERİ ---
                new District { Name = "ARDANUÇ", CityId = cityMap["ARTVİN"] },
                new District { Name = "ARHAVİ", CityId = cityMap["ARTVİN"] },
                new District { Name = "BORÇKA", CityId = cityMap["ARTVİN"] },
                new District { Name = "HOPA", CityId = cityMap["ARTVİN"] },
                new District { Name = "KEMALPAŞA", CityId = cityMap["ARTVİN"] },
                new District { Name = "MERKEZ", CityId = cityMap["ARTVİN"] },
                new District { Name = "MURGUL", CityId = cityMap["ARTVİN"] },
                new District { Name = "ŞAVŞAT", CityId = cityMap["ARTVİN"] },
                new District { Name = "YUSUFELİ", CityId = cityMap["ARTVİN"] },

                // --- AYDIN İLÇELERİ ---
                new District { Name = "BOZDOĞAN", CityId = cityMap["AYDIN"] },
                new District { Name = "BUHARKENT", CityId = cityMap["AYDIN"] },
                new District { Name = "ÇİNE", CityId = cityMap["AYDIN"] },
                new District { Name = "DİDİM", CityId = cityMap["AYDIN"] },
                new District { Name = "EFELER", CityId = cityMap["AYDIN"] },
                new District { Name = "GERMENCİK", CityId = cityMap["AYDIN"] },
                new District { Name = "İNCİRLİOVA", CityId = cityMap["AYDIN"] },
                new District { Name = "KARACASU", CityId = cityMap["AYDIN"] },
                new District { Name = "KARPUZLU", CityId = cityMap["AYDIN"] },
                new District { Name = "KOÇARLI", CityId = cityMap["AYDIN"] },
                new District { Name = "KÖŞK", CityId = cityMap["AYDIN"] },
                new District { Name = "KUŞADASI", CityId = cityMap["AYDIN"] },
                new District { Name = "KUYUCAK", CityId = cityMap["AYDIN"] },
                new District { Name = "NAZİLLİ", CityId = cityMap["AYDIN"] },
                new District { Name = "SÖKE", CityId = cityMap["AYDIN"] },
                new District { Name = "SULTANHİSAR", CityId = cityMap["AYDIN"] },
                new District { Name = "YENİPAZAR", CityId = cityMap["AYDIN"] },

                // --- BALIKESİR İLÇELERİ ---
                new District { Name = "ALTIEYLÜL", CityId = cityMap["BALIKESİR"] },
                new District { Name = "AYVALIK", CityId = cityMap["BALIKESİR"] },
                new District { Name = "BALYA", CityId = cityMap["BALIKESİR"] },
                new District { Name = "BANDIRMA", CityId = cityMap["BALIKESİR"] },
                new District { Name = "BİGADİÇ", CityId = cityMap["BALIKESİR"] },
                new District { Name = "BURHANİYE", CityId = cityMap["BALIKESİR"] },
                new District { Name = "DURSUNBEY", CityId = cityMap["BALIKESİR"] },
                new District { Name = "EDREMİT", CityId = cityMap["BALIKESİR"] },
                new District { Name = "ERDEK", CityId = cityMap["BALIKESİR"] },
                new District { Name = "GÖMEÇ", CityId = cityMap["BALIKESİR"] },
                new District { Name = "GÖNEN", CityId = cityMap["BALIKESİR"] },
                new District { Name = "HAVRAN", CityId = cityMap["BALIKESİR"] },
                new District { Name = "İVRİNDİ", CityId = cityMap["BALIKESİR"] },
                new District { Name = "KARESİ", CityId = cityMap["BALIKESİR"] },
                new District { Name = "KEPSUT", CityId = cityMap["BALIKESİR"] },
                new District { Name = "MANYAS", CityId = cityMap["BALIKESİR"] },
                new District { Name = "MARMARA", CityId = cityMap["BALIKESİR"] },
                new District { Name = "SAVAŞTEPE", CityId = cityMap["BALIKESİR"] },
                new District { Name = "SINDIRGI", CityId = cityMap["BALIKESİR"] },
                new District { Name = "SUSURLUK", CityId = cityMap["BALIKESİR"] },

                // --- BİLECİK İLÇELERİ ---
                new District { Name = "BOZÜYÜK", CityId = cityMap["BİLECİK"] },
                new District { Name = "GÖLPAZARI", CityId = cityMap["BİLECİK"] },
                new District { Name = "İNHİSAR", CityId = cityMap["BİLECİK"] },
                new District { Name = "MERKEZ", CityId = cityMap["BİLECİK"] },
                new District { Name = "OSMANELİ", CityId = cityMap["BİLECİK"] },
                new District { Name = "PAZARYERİ", CityId = cityMap["BİLECİK"] },
                new District { Name = "SÖĞÜT", CityId = cityMap["BİLECİK"] },
                new District { Name = "YENİPAZAR", CityId = cityMap["BİLECİK"] },

                // --- BİNGÖL İLÇELERİ ---
                new District { Name = "ADAKLI", CityId = cityMap["BİNGÖL"] },
                new District { Name = "GENÇ", CityId = cityMap["BİNGÖL"] },
                new District { Name = "KARLIOVA", CityId = cityMap["BİNGÖL"] },
                new District { Name = "KİĞI", CityId = cityMap["BİNGÖL"] },
                new District { Name = "MERKEZ", CityId = cityMap["BİNGÖL"] },
                new District { Name = "SOLHAN", CityId = cityMap["BİNGÖL"] },
                new District { Name = "YAYLADERE", CityId = cityMap["BİNGÖL"] },
                new District { Name = "YEDİSU", CityId = cityMap["BİNGÖL"] },

                // --- BİTLİS İLÇELERİ ---
                new District { Name = "ADİLCEVAZ", CityId = cityMap["BİTLİS"] },
                new District { Name = "AHLAT", CityId = cityMap["BİTLİS"] },
                new District { Name = "GÜROYMAK", CityId = cityMap["BİTLİS"] },
                new District { Name = "HİZAN", CityId = cityMap["BİTLİS"] },
                new District { Name = "MERKEZ", CityId = cityMap["BİTLİS"] },
                new District { Name = "MUTKİ", CityId = cityMap["BİTLİS"] },
                new District { Name = "TATVAN", CityId = cityMap["BİTLİS"] },

                // --- BOLU İLÇELERİ ---
                new District { Name = "DÖRTDİVAN", CityId = cityMap["BOLU"] },
                new District { Name = "GEREDE", CityId = cityMap["BOLU"] },
                new District { Name = "GÖYNÜK", CityId = cityMap["BOLU"] },
                new District { Name = "KIBRISCIK", CityId = cityMap["BOLU"] },
                new District { Name = "MENGEN", CityId = cityMap["BOLU"] },
                new District { Name = "MERKEZ", CityId = cityMap["BOLU"] },
                new District { Name = "MUDURNU", CityId = cityMap["BOLU"] },
                new District { Name = "SEBEN", CityId = cityMap["BOLU"] },
                new District { Name = "YENİÇAĞA", CityId = cityMap["BOLU"] },

                // --- BURDUR İLÇELERİ ---
                new District { Name = "AĞLASUN", CityId = cityMap["BURDUR"] },
                new District { Name = "ALTINYAYLA", CityId = cityMap["BURDUR"] },
                new District { Name = "BUCAK", CityId = cityMap["BURDUR"] },
                new District { Name = "ÇAVDIR", CityId = cityMap["BURDUR"] },
                new District { Name = "ÇELTİKÇİ", CityId = cityMap["BURDUR"] },
                new District { Name = "GÖLHİSAR", CityId = cityMap["BURDUR"] },
                new District { Name = "KARAMANLI", CityId = cityMap["BURDUR"] },
                new District { Name = "KEMER", CityId = cityMap["BURDUR"] },
                new District { Name = "MERKEZ", CityId = cityMap["BURDUR"] },
                new District { Name = "TEFENNİ", CityId = cityMap["BURDUR"] },
                new District { Name = "YEŞİLOVA", CityId = cityMap["BURDUR"] },

                // --- BURSA İLÇELERİ ---
                new District { Name = "BÜYÜKORHAN", CityId = cityMap["BURSA"] },
                new District { Name = "GEMLİK", CityId = cityMap["BURSA"] },
                new District { Name = "GÜRSU", CityId = cityMap["BURSA"] },
                new District { Name = "HARMANCIK", CityId = cityMap["BURSA"] },
                new District { Name = "İNEGÖL", CityId = cityMap["BURSA"] },
                new District { Name = "İZNİK", CityId = cityMap["BURSA"] },
                new District { Name = "KARACABEY", CityId = cityMap["BURSA"] },
                new District { Name = "KELES", CityId = cityMap["BURSA"] },
                new District { Name = "KESTEL", CityId = cityMap["BURSA"] },
                new District { Name = "MUDANYA", CityId = cityMap["BURSA"] },
                new District { Name = "MUSTAFAKEMALPAŞA", CityId = cityMap["BURSA"] },
                new District { Name = "NİLÜFER", CityId = cityMap["BURSA"] },
                new District { Name = "ORHANELİ", CityId = cityMap["BURSA"] },
                new District { Name = "ORHANGAZİ", CityId = cityMap["BURSA"] },
                new District { Name = "OSMANGAZİ", CityId = cityMap["BURSA"] },
                new District { Name = "YENİŞEHİR", CityId = cityMap["BURSA"] },
                new District { Name = "YILDIRIM", CityId = cityMap["BURSA"] },

                // --- ÇANAKKALE İLÇELERİ ---
                new District { Name = "AYVACIK", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "BAYRAMİÇ", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "BİGA", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "BOZCAADA", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "ÇAN", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "ECEABAT", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "EZİNE", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "GELİBOLU", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "GÖKÇEADA", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "LAPSEKİ", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "MERKEZ", CityId = cityMap["ÇANAKKALE"] },
                new District { Name = "YENİCE", CityId = cityMap["ÇANAKKALE"] },

                // --- ÇANKIRI İLÇELERİ ---
                new District { Name = "ATKARACALAR", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "BAYRAMÖREN", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "ÇERKEŞ", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "ELDİVAN", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "ILGAZ", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "KIZILIRMAK", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "KORGUN", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "KURŞUNLU", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "MERKEZ", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "ORTA", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "ŞABANÖZÜ", CityId = cityMap["ÇANKIRI"] },
                new District { Name = "YAPRAKLI", CityId = cityMap["ÇANKIRI"] },

                // --- ÇORUM İLÇELERİ ---
                new District { Name = "ALACA", CityId = cityMap["ÇORUM"] },
                new District { Name = "BAYAT", CityId = cityMap["ÇORUM"] },
                new District { Name = "BOĞAZKALE", CityId = cityMap["ÇORUM"] },
                new District { Name = "DODURGA", CityId = cityMap["ÇORUM"] },
                new District { Name = "İSKİLİP", CityId = cityMap["ÇORUM"] },
                new District { Name = "KARGI", CityId = cityMap["ÇORUM"] },
                new District { Name = "LAÇİN", CityId = cityMap["ÇORUM"] },
                new District { Name = "MECİTÖZÜ", CityId = cityMap["ÇORUM"] },
                new District { Name = "MERKEZ", CityId = cityMap["ÇORUM"] },
                new District { Name = "OĞUZLAR", CityId = cityMap["ÇORUM"] },
                new District { Name = "ORTAKÖY", CityId = cityMap["ÇORUM"] },
                new District { Name = "OSMANCIK", CityId = cityMap["ÇORUM"] },
                new District { Name = "SUNGURLU", CityId = cityMap["ÇORUM"] },
                new District { Name = "UĞURLUDAĞ", CityId = cityMap["ÇORUM"] },

                // --- DENİZLİ İLÇELERİ ---
                new District { Name = "ACIPAYAM", CityId = cityMap["DENİZLİ"] },
                new District { Name = "BABADAĞ", CityId = cityMap["DENİZLİ"] },
                new District { Name = "BAKLAN", CityId = cityMap["DENİZLİ"] },
                new District { Name = "BEKİLLİ", CityId = cityMap["DENİZLİ"] },
                new District { Name = "BEYAĞAÇ", CityId = cityMap["DENİZLİ"] },
                new District { Name = "BOZKURT", CityId = cityMap["DENİZLİ"] },
                new District { Name = "BULDAN", CityId = cityMap["DENİZLİ"] },
                new District { Name = "ÇAL", CityId = cityMap["DENİZLİ"] },
                new District { Name = "ÇAMELİ", CityId = cityMap["DENİZLİ"] },
                new District { Name = "ÇARDAK", CityId = cityMap["DENİZLİ"] },
                new District { Name = "ÇİVRİL", CityId = cityMap["DENİZLİ"] },
                new District { Name = "GÜNEY", CityId = cityMap["DENİZLİ"] },
                new District { Name = "HONAZ", CityId = cityMap["DENİZLİ"] },
                new District { Name = "KALE", CityId = cityMap["DENİZLİ"] },
                new District { Name = "MERKEZEFENDİ", CityId = cityMap["DENİZLİ"] },
                new District { Name = "PAMUKKALE", CityId = cityMap["DENİZLİ"] },
                new District { Name = "SARAYKÖY", CityId = cityMap["DENİZLİ"] },
                new District { Name = "SERİNHİSAR", CityId = cityMap["DENİZLİ"] },
                new District { Name = "TAVAS", CityId = cityMap["DENİZLİ"] },

                // --- DİYARBAKIR İLÇELERİ ---
                new District { Name = "BAĞLAR", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "BİSMİL", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "ÇERMİK", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "ÇINAR", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "ÇÜNGÜŞ", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "DİCLE", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "EĞİL", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "ERGANİ", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "HANİ", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "HAZRO", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "KAYAPINAR", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "KOCAKÖY", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "KULP", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "LİCE", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "SİLVAN", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "SUR", CityId = cityMap["DİYARBAKIR"] },
                new District { Name = "YENİŞEHİR", CityId = cityMap["DİYARBAKIR"] },

                // --- EDİRNE İLÇELERİ ---
                new District { Name = "ENEZ", CityId = cityMap["EDİRNE"] },
                new District { Name = "HAVSA", CityId = cityMap["EDİRNE"] },
                new District { Name = "İPSALA", CityId = cityMap["EDİRNE"] },
                new District { Name = "KEŞAN", CityId = cityMap["EDİRNE"] },
                new District { Name = "LALAPAŞA", CityId = cityMap["EDİRNE"] },
                new District { Name = "MERİÇ", CityId = cityMap["EDİRNE"] },
                new District { Name = "MERKEZ", CityId = cityMap["EDİRNE"] },
                new District { Name = "SÜLOĞLU", CityId = cityMap["EDİRNE"] },
                new District { Name = "UZUNKÖPRÜ", CityId = cityMap["EDİRNE"] },

                // --- ELAZIĞ İLÇELERİ ---
                new District { Name = "AĞIN", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "ALACAKAYA", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "ARICAK", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "BASKİL", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "KARAKOÇAN", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "KEBAN", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "KOVANCILAR", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "MADEN", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "MERKEZ", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "PALU", CityId = cityMap["ELAZIĞ"] },
                new District { Name = "SİVRİCE", CityId = cityMap["ELAZIĞ"] },

                // --- ERZİNCAN İLÇELERİ ---
                new District { Name = "ÇAYIRLI", CityId = cityMap["ERZİNCAN"] },
                new District { Name = "İLİÇ", CityId = cityMap["ERZİNCAN"] },
                new District { Name = "KEMAH", CityId = cityMap["ERZİNCAN"] },
                new District { Name = "KEMALİYE", CityId = cityMap["ERZİNCAN"] },
                new District { Name = "MERKEZ", CityId = cityMap["ERZİNCAN"] },
                new District { Name = "OTLUKBELİ", CityId = cityMap["ERZİNCAN"] },
                new District { Name = "REFAHİYE", CityId = cityMap["ERZİNCAN"] },
                new District { Name = "TERCAN", CityId = cityMap["ERZİNCAN"] },
                new District { Name = "ÜZÜMLÜ", CityId = cityMap["ERZİNCAN"] },

                // --- ERZURUM İLÇELERİ ---
                new District { Name = "AŞKALE", CityId = cityMap["ERZURUM"] },
                new District { Name = "AZİZİYE", CityId = cityMap["ERZURUM"] },
                new District { Name = "ÇAT", CityId = cityMap["ERZURUM"] },
                new District { Name = "HINIS", CityId = cityMap["ERZURUM"] },
                new District { Name = "HORASAN", CityId = cityMap["ERZURUM"] },
                new District { Name = "İSPİR", CityId = cityMap["ERZURUM"] },
                new District { Name = "KARAÇOBAN", CityId = cityMap["ERZURUM"] },
                new District { Name = "KARAYAZI", CityId = cityMap["ERZURUM"] },
                new District { Name = "KÖPRÜKÖY", CityId = cityMap["ERZURUM"] },
                new District { Name = "NARMAN", CityId = cityMap["ERZURUM"] },
                new District { Name = "OLTU", CityId = cityMap["ERZURUM"] },
                new District { Name = "OLUR", CityId = cityMap["ERZURUM"] },
                new District { Name = "PALANDÖKEN", CityId = cityMap["ERZURUM"] },
                new District { Name = "PASİNLER", CityId = cityMap["ERZURUM"] },
                new District { Name = "PAZARYOLU", CityId = cityMap["ERZURUM"] },
                new District { Name = "ŞENKAYA", CityId = cityMap["ERZURUM"] },
                new District { Name = "TEKMAN", CityId = cityMap["ERZURUM"] },
                new District { Name = "TORTUM", CityId = cityMap["ERZURUM"] },
                new District { Name = "UZUNDERE", CityId = cityMap["ERZURUM"] },
                new District { Name = "YAKUTİYE", CityId = cityMap["ERZURUM"] },

                // --- ESKİŞEHİR İLÇELERİ ---
                new District { Name = "ALPU", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "BEYLİKOVA", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "ÇİFTELER", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "GÜNYÜZÜ", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "HAN", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "İNÖNÜ", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "MAHMUDİYE", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "MİHALGAZİ", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "MİHALIÇÇIK", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "ODUNPAZARI", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "SARICAKAYA", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "SEYİTGAZİ", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "SİVRİHİSAR", CityId = cityMap["ESKİŞEHİR"] },
                new District { Name = "TEPEBAŞI", CityId = cityMap["ESKİŞEHİR"] },

                // --- GAZİANTEP İLÇELERİ ---
                new District { Name = "ARABAN", CityId = cityMap["GAZİANTEP"] },
                new District { Name = "İSLAHİYE", CityId = cityMap["GAZİANTEP"] },
                new District { Name = "KARKAMIŞ", CityId = cityMap["GAZİANTEP"] },
                new District { Name = "NİZİP", CityId = cityMap["GAZİANTEP"] },
                new District { Name = "NURDAĞI", CityId = cityMap["GAZİANTEP"] },
                new District { Name = "OĞUZELİ", CityId = cityMap["GAZİANTEP"] },
                new District { Name = "ŞAHİNBEY", CityId = cityMap["GAZİANTEP"] },
                new District { Name = "ŞEHİTKAMİL", CityId = cityMap["GAZİANTEP"] },
                new District { Name = "YAVUZELİ", CityId = cityMap["GAZİANTEP"] },

                // --- GİRESUN İLÇELERİ ---
                new District { Name = "ALUCRA", CityId = cityMap["GİRESUN"] },
                new District { Name = "BULANCAK", CityId = cityMap["GİRESUN"] },
                new District { Name = "ÇAMOLUK", CityId = cityMap["GİRESUN"] },
                new District { Name = "ÇANAKÇI", CityId = cityMap["GİRESUN"] },
                new District { Name = "DERELİ", CityId = cityMap["GİRESUN"] },
                new District { Name = "DOĞANKENT", CityId = cityMap["GİRESUN"] },
                new District { Name = "ESPİYE", CityId = cityMap["GİRESUN"] },
                new District { Name = "EYNESİL", CityId = cityMap["GİRESUN"] },
                new District { Name = "GÖRELE", CityId = cityMap["GİRESUN"] },
                new District { Name = "GÜCE", CityId = cityMap["GİRESUN"] },
                new District { Name = "KEŞAP", CityId = cityMap["GİRESUN"] },
                new District { Name = "MERKEZ", CityId = cityMap["GİRESUN"] },
                new District { Name = "PİRAZİZ", CityId = cityMap["GİRESUN"] },
                new District { Name = "ŞEBİNKARAHİSAR", CityId = cityMap["GİRESUN"] },
                new District { Name = "TİREBOLU", CityId = cityMap["GİRESUN"] },
                new District { Name = "YAĞLIDERE", CityId = cityMap["GİRESUN"] },

                // --- GÜMÜŞHANE İLÇELERİ ---
                new District { Name = "KELKİT", CityId = cityMap["GÜMÜŞHANE"] },
                new District { Name = "KÖSE", CityId = cityMap["GÜMÜŞHANE"] },
                new District { Name = "KÜRTÜN", CityId = cityMap["GÜMÜŞHANE"] },
                new District { Name = "MERKEZ", CityId = cityMap["GÜMÜŞHANE"] },
                new District { Name = "ŞİRAN", CityId = cityMap["GÜMÜŞHANE"] },
                new District { Name = "TORUL", CityId = cityMap["GÜMÜŞHANE"] },

                // --- HAKKARİ İLÇELERİ ---
                new District { Name = "ÇUKURCA", CityId = cityMap["HAKKARİ"] },
                new District { Name = "DERECİK", CityId = cityMap["HAKKARİ"] },
                new District { Name = "MERKEZ", CityId = cityMap["HAKKARİ"] },
                new District { Name = "ŞEMDİNLİ", CityId = cityMap["HAKKARİ"] },
                new District { Name = "YÜKSEKOVA", CityId = cityMap["HAKKARİ"] },

                // --- HATAY İLÇELERİ ---
                new District { Name = "ALTINÖZÜ", CityId = cityMap["HATAY"] },
                new District { Name = "ANTAKYA", CityId = cityMap["HATAY"] },
                new District { Name = "ARSUZ", CityId = cityMap["HATAY"] },
                new District { Name = "BELEN", CityId = cityMap["HATAY"] },
                new District { Name = "DEFNE", CityId = cityMap["HATAY"] },
                new District { Name = "DÖRTYOL", CityId = cityMap["HATAY"] },
                new District { Name = "ERZİN", CityId = cityMap["HATAY"] },
                new District { Name = "HASSA", CityId = cityMap["HATAY"] },
                new District { Name = "İSKENDERUN", CityId = cityMap["HATAY"] },
                new District { Name = "KIRIKHAN", CityId = cityMap["HATAY"] },
                new District { Name = "KUMLU", CityId = cityMap["HATAY"] },
                new District { Name = "PAYAS", CityId = cityMap["HATAY"] },
                new District { Name = "REYHANLI", CityId = cityMap["HATAY"] },
                new District { Name = "SAMANDAĞ", CityId = cityMap["HATAY"] },
                new District { Name = "YAYLADAĞI", CityId = cityMap["HATAY"] },

                // --- ISPARTA İLÇELERİ ---
                new District { Name = "AKSU", CityId = cityMap["ISPARTA"] },
                new District { Name = "ATABEY", CityId = cityMap["ISPARTA"] },
                new District { Name = "EĞİRDİR", CityId = cityMap["ISPARTA"] },
                new District { Name = "GELENDOST", CityId = cityMap["ISPARTA"] },
                new District { Name = "GÖNEN", CityId = cityMap["ISPARTA"] },
                new District { Name = "KEÇİBORLU", CityId = cityMap["ISPARTA"] },
                new District { Name = "MERKEZ", CityId = cityMap["ISPARTA"] },
                new District { Name = "SENİRKENT", CityId = cityMap["ISPARTA"] },
                new District { Name = "SÜTÇÜLER", CityId = cityMap["ISPARTA"] },
                new District { Name = "ŞARKİKARAAĞAÇ", CityId = cityMap["ISPARTA"] },
                new District { Name = "ULUBORLU", CityId = cityMap["ISPARTA"] },
                new District { Name = "YALVAÇ", CityId = cityMap["ISPARTA"] },
                new District { Name = "YENİŞARBADEMLİ", CityId = cityMap["ISPARTA"] },

                // --- MERSİN İLÇELERİ ---
                new District { Name = "AKDENİZ", CityId = cityMap["MERSİN"] },
                new District { Name = "ANAMUR", CityId = cityMap["MERSİN"] },
                new District { Name = "AYDINCIK", CityId = cityMap["MERSİN"] },
                new District { Name = "BOZYAZI", CityId = cityMap["MERSİN"] },
                new District { Name = "ÇAMLIYAYLA", CityId = cityMap["MERSİN"] },
                new District { Name = "ERDEMLİ", CityId = cityMap["MERSİN"] },
                new District { Name = "GÜLNAR", CityId = cityMap["MERSİN"] },
                new District { Name = "MEZİTLİ", CityId = cityMap["MERSİN"] },
                new District { Name = "MUT", CityId = cityMap["MERSİN"] },
                new District { Name = "SİLİFKE", CityId = cityMap["MERSİN"] },
                new District { Name = "TARSUS", CityId = cityMap["MERSİN"] },
                new District { Name = "TOROSLAR", CityId = cityMap["MERSİN"] },
                new District { Name = "YENİŞEHİR", CityId = cityMap["MERSİN"] },

                // --- İSTANBUL İLÇELERİ ---
                new District { Name = "ADALAR", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ARNAVUTKÖY", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ATAŞEHİR", CityId = cityMap["İSTANBUL"] },
                new District { Name = "AVCILAR", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BAĞCILAR", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BAHÇELİEVLER", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BAKIRKÖY", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BAŞAKŞEHİR", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BAYRAMPAŞA", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BEŞİKTAŞ", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BEYKOZ", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BEYLİKDÜZÜ", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BEYOĞLU", CityId = cityMap["İSTANBUL"] },
                new District { Name = "BÜYÜKÇEKMECE", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ÇATALCA", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ÇEKMEKÖY", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ESENLER", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ESENYURT", CityId = cityMap["İSTANBUL"] },
                new District { Name = "EYÜPSULTAN", CityId = cityMap["İSTANBUL"] },
                new District { Name = "FATİH", CityId = cityMap["İSTANBUL"] },
                new District { Name = "GAZİOSMANPAŞA", CityId = cityMap["İSTANBUL"] },
                new District { Name = "GÜNGÖREN", CityId = cityMap["İSTANBUL"] },
                new District { Name = "KADIKÖY", CityId = cityMap["İSTANBUL"] },
                new District { Name = "KAĞITHANE", CityId = cityMap["İSTANBUL"] },
                new District { Name = "KARTAL", CityId = cityMap["İSTANBUL"] },
                new District { Name = "KÜÇÜKÇEKMECE", CityId = cityMap["İSTANBUL"] },
                new District { Name = "MALTEPE", CityId = cityMap["İSTANBUL"] },
                new District { Name = "PENDİK", CityId = cityMap["İSTANBUL"] },
                new District { Name = "SANCAKTEPE", CityId = cityMap["İSTANBUL"] },
                new District { Name = "SARIYER", CityId = cityMap["İSTANBUL"] },
                new District { Name = "SİLİVRİ", CityId = cityMap["İSTANBUL"] },
                new District { Name = "SULTANBEYLİ", CityId = cityMap["İSTANBUL"] },
                new District { Name = "SULTANGAZİ", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ŞİLE", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ŞİŞLİ", CityId = cityMap["İSTANBUL"] },
                new District { Name = "TUZLA", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ÜMRANİYE", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ÜSKÜDAR", CityId = cityMap["İSTANBUL"] },
                new District { Name = "ZEYTİNBURNU", CityId = cityMap["İSTANBUL"] },

                // --- İZMİR İLÇELERİ ---
                new District { Name = "ALİAĞA", CityId = cityMap["İZMİR"] },
                new District { Name = "BALÇOVA", CityId = cityMap["İZMİR"] },
                new District { Name = "BAYINDIR", CityId = cityMap["İZMİR"] },
                new District { Name = "BAYRAKLI", CityId = cityMap["İZMİR"] },
                new District { Name = "BERGAMA", CityId = cityMap["İZMİR"] },
                new District { Name = "BEYDAĞ", CityId = cityMap["İZMİR"] },
                new District { Name = "BORNOVA", CityId = cityMap["İZMİR"] },
                new District { Name = "BUCA", CityId = cityMap["İZMİR"] },
                new District { Name = "ÇEŞME", CityId = cityMap["İZMİR"] },
                new District { Name = "ÇİĞLİ", CityId = cityMap["İZMİR"] },
                new District { Name = "DİKİLİ", CityId = cityMap["İZMİR"] },
                new District { Name = "FOÇA", CityId = cityMap["İZMİR"] },
                new District { Name = "GAZİEMİR", CityId = cityMap["İZMİR"] },
                new District { Name = "GÜZELBAHÇE", CityId = cityMap["İZMİR"] },
                new District { Name = "KARABAĞLAR", CityId = cityMap["İZMİR"] },
                new District { Name = "KARABURUN", CityId = cityMap["İZMİR"] },
                new District { Name = "KARŞIYAKA", CityId = cityMap["İZMİR"] },
                new District { Name = "KEMALPAŞA", CityId = cityMap["İZMİR"] },
                new District { Name = "KINIK", CityId = cityMap["İZMİR"] },
                new District { Name = "KİRAZ", CityId = cityMap["İZMİR"] },
                new District { Name = "KONAK", CityId = cityMap["İZMİR"] },
                new District { Name = "MENDERES", CityId = cityMap["İZMİR"] },
                new District { Name = "MENEMEN", CityId = cityMap["İZMİR"] },
                new District { Name = "NARLIDERE", CityId = cityMap["İZMİR"] },
                new District { Name = "ÖDEMİŞ", CityId = cityMap["İZMİR"] },
                new District { Name = "SEFERİHİSAR", CityId = cityMap["İZMİR"] },
                new District { Name = "SELÇUK", CityId = cityMap["İZMİR"] },
                new District { Name = "TİRE", CityId = cityMap["İZMİR"] },
                new District { Name = "TORBALI", CityId = cityMap["İZMİR"] },
                new District { Name = "URLA", CityId = cityMap["İZMİR"] },

                // --- KARS İLÇELERİ ---
                new District { Name = "AKYAKA", CityId = cityMap["KARS"] },
                new District { Name = "ARPAÇAY", CityId = cityMap["KARS"] },
                new District { Name = "DİGOR", CityId = cityMap["KARS"] },
                new District { Name = "KAĞIZMAN", CityId = cityMap["KARS"] },
                new District { Name = "MERKEZ", CityId = cityMap["KARS"] },
                new District { Name = "SARIKAMIŞ", CityId = cityMap["KARS"] },
                new District { Name = "SELİM", CityId = cityMap["KARS"] },
                new District { Name = "SUSUZ", CityId = cityMap["KARS"] },

                // --- KASTAMONU İLÇELERİ ---
                new District { Name = "ABANA", CityId = cityMap["KASTAMONU"] },
                new District { Name = "AĞLI", CityId = cityMap["KASTAMONU"] },
                new District { Name = "ARAÇ", CityId = cityMap["KASTAMONU"] },
                new District { Name = "AZDAVAY", CityId = cityMap["KASTAMONU"] },
                new District { Name = "BOZKURT", CityId = cityMap["KASTAMONU"] },
                new District { Name = "CİDE", CityId = cityMap["KASTAMONU"] },
                new District { Name = "ÇATALZEYTİN", CityId = cityMap["KASTAMONU"] },
                new District { Name = "DADAY", CityId = cityMap["KASTAMONU"] },
                new District { Name = "DEVREKANİ", CityId = cityMap["KASTAMONU"] },
                new District { Name = "DOĞANYURT", CityId = cityMap["KASTAMONU"] },
                new District { Name = "HANÖNÜ", CityId = cityMap["KASTAMONU"] },
                new District { Name = "İHSANGAZİ", CityId = cityMap["KASTAMONU"] },
                new District { Name = "İNEBOLU", CityId = cityMap["KASTAMONU"] },
                new District { Name = "KÜRE", CityId = cityMap["KASTAMONU"] },
                new District { Name = "MERKEZ", CityId = cityMap["KASTAMONU"] },
                new District { Name = "PINARBAŞI", CityId = cityMap["KASTAMONU"] },
                new District { Name = "SEYDİLER", CityId = cityMap["KASTAMONU"] },
                new District { Name = "ŞENPAZAR", CityId = cityMap["KASTAMONU"] },
                new District { Name = "TAŞKÖPRÜ", CityId = cityMap["KASTAMONU"] },
                new District { Name = "TOSYA", CityId = cityMap["KASTAMONU"] },

                // --- KAYSERİ İLÇELERİ ---
                new District { Name = "AKKIŞLA", CityId = cityMap["KAYSERİ"] },
                new District { Name = "BÜNYAN", CityId = cityMap["KAYSERİ"] },
                new District { Name = "DEVELİ", CityId = cityMap["KAYSERİ"] },
                new District { Name = "FELAHİYE", CityId = cityMap["KAYSERİ"] },
                new District { Name = "HACILAR", CityId = cityMap["KAYSERİ"] },
                new District { Name = "İNCESU", CityId = cityMap["KAYSERİ"] },
                new District { Name = "KOCASİNAN", CityId = cityMap["KAYSERİ"] },
                new District { Name = "MELİKGAZİ", CityId = cityMap["KAYSERİ"] },
                new District { Name = "ÖZVATAN", CityId = cityMap["KAYSERİ"] },
                new District { Name = "PINARBAŞI", CityId = cityMap["KAYSERİ"] },
                new District { Name = "SARIOĞLAN", CityId = cityMap["KAYSERİ"] },
                new District { Name = "SARIZ", CityId = cityMap["KAYSERİ"] },
                new District { Name = "TALAS", CityId = cityMap["KAYSERİ"] },
                new District { Name = "TOMARZA", CityId = cityMap["KAYSERİ"] },
                new District { Name = "YAHYALI", CityId = cityMap["KAYSERİ"] },
                new District { Name = "YEŞİLHİSAR", CityId = cityMap["KAYSERİ"] },

                // --- KIRKLARELİ İLÇELERİ ---
                new District { Name = "BABAESKİ", CityId = cityMap["KIRKLARELİ"] },
                new District { Name = "DEMİRKÖY", CityId = cityMap["KIRKLARELİ"] },
                new District { Name = "KOFÇAZ", CityId = cityMap["KIRKLARELİ"] },
                new District { Name = "LÜLEBURGAZ", CityId = cityMap["KIRKLARELİ"] },
                new District { Name = "MERKEZ", CityId = cityMap["KIRKLARELİ"] },
                new District { Name = "PEHLİVANKÖY", CityId = cityMap["KIRKLARELİ"] },
                new District { Name = "PINARHİSAR", CityId = cityMap["KIRKLARELİ"] },
                new District { Name = "VİZE", CityId = cityMap["KIRKLARELİ"] },

                // --- KIRŞEHİR İLÇELERİ ---
                new District { Name = "AKÇAKENT", CityId = cityMap["KIRŞEHİR"] },
                new District { Name = "AKPINAR", CityId = cityMap["KIRŞEHİR"] },
                new District { Name = "BOZTEPE", CityId = cityMap["KIRŞEHİR"] },
                new District { Name = "ÇİÇEKDAĞI", CityId = cityMap["KIRŞEHİR"] },
                new District { Name = "KAMAN", CityId = cityMap["KIRŞEHİR"] },
                new District { Name = "MERKEZ", CityId = cityMap["KIRŞEHİR"] },
                new District { Name = "MUCUR", CityId = cityMap["KIRŞEHİR"] },

                // --- KOCAELİ İLÇELERİ ---
                new District { Name = "BAŞİSKELE", CityId = cityMap["KOCAELİ"] },
                new District { Name = "ÇAYIROVA", CityId = cityMap["KOCAELİ"] },
                new District { Name = "DARICA", CityId = cityMap["KOCAELİ"] },
                new District { Name = "DERİNCE", CityId = cityMap["KOCAELİ"] },
                new District { Name = "DİLOVASI", CityId = cityMap["KOCAELİ"] },
                new District { Name = "GEBZE", CityId = cityMap["KOCAELİ"] },
                new District { Name = "GÖLCÜK", CityId = cityMap["KOCAELİ"] },
                new District { Name = "İZMİT", CityId = cityMap["KOCAELİ"] },
                new District { Name = "KANDIRA", CityId = cityMap["KOCAELİ"] },
                new District { Name = "KARAMÜRSEL", CityId = cityMap["KOCAELİ"] },
                new District { Name = "KARTEPE", CityId = cityMap["KOCAELİ"] },
                new District { Name = "KÖRFEZ", CityId = cityMap["KOCAELİ"] },

                // --- KONYA İLÇELERİ ---
                new District { Name = "AHIRLI", CityId = cityMap["KONYA"] },
                new District { Name = "AKÖREN", CityId = cityMap["KONYA"] },
                new District { Name = "AKŞEHİR", CityId = cityMap["KONYA"] },
                new District { Name = "ALTINEKİN", CityId = cityMap["KONYA"] },
                new District { Name = "BEYŞEHİR", CityId = cityMap["KONYA"] },
                new District { Name = "BOZKIR", CityId = cityMap["KONYA"] },
                new District { Name = "CİHANBEYLİ", CityId = cityMap["KONYA"] },
                new District { Name = "ÇELTİK", CityId = cityMap["KONYA"] },
                new District { Name = "ÇUMRA", CityId = cityMap["KONYA"] },
                new District { Name = "DERBENT", CityId = cityMap["KONYA"] },
                new District { Name = "DEREBUCAK", CityId = cityMap["KONYA"] },
                new District { Name = "DOĞANHİSAR", CityId = cityMap["KONYA"] },
                new District { Name = "EMİRGAZİ", CityId = cityMap["KONYA"] },
                new District { Name = "EREĞLİ", CityId = cityMap["KONYA"] },
                new District { Name = "GÜNEYSINIR", CityId = cityMap["KONYA"] },
                new District { Name = "HADİM", CityId = cityMap["KONYA"] },
                new District { Name = "HALKAPINAR", CityId = cityMap["KONYA"] },
                new District { Name = "HÜYÜK", CityId = cityMap["KONYA"] },
                new District { Name = "ILGIN", CityId = cityMap["KONYA"] },
                new District { Name = "KADINHANI", CityId = cityMap["KONYA"] },
                new District { Name = "KARAPINAR", CityId = cityMap["KONYA"] },
                new District { Name = "KARATAY", CityId = cityMap["KONYA"] },
                new District { Name = "KULU", CityId = cityMap["KONYA"] },
                new District { Name = "MERAM", CityId = cityMap["KONYA"] },
                new District { Name = "SARAYÖNÜ", CityId = cityMap["KONYA"] },
                new District { Name = "SELÇUKLU", CityId = cityMap["KONYA"] },
                new District { Name = "SEYDİŞEHİR", CityId = cityMap["KONYA"] },
                new District { Name = "TAŞKENT", CityId = cityMap["KONYA"] },
                new District { Name = "TUZLUKÇU", CityId = cityMap["KONYA"] },
                new District { Name = "YALIHÜYÜK", CityId = cityMap["KONYA"] },
                new District { Name = "YUNAK", CityId = cityMap["KONYA"] },

                // --- KÜTAHYA İLÇELERİ ---
                new District { Name = "ALTINTAŞ", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "ASLANAPA", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "ÇAVDARHİSAR", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "DOMANİÇ", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "DUMLUPINAR", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "EMET", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "GEDİZ", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "HİSARCIK", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "MERKEZ", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "PAZARLAR", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "SİMAV", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "ŞAPHANE", CityId = cityMap["KÜTAHYA"] },
                new District { Name = "TAVŞANLI", CityId = cityMap["KÜTAHYA"] },

                // --- MALATYA İLÇELERİ ---
                new District { Name = "AKÇADAĞ", CityId = cityMap["MALATYA"] },
                new District { Name = "ARAPGİR", CityId = cityMap["MALATYA"] },
                new District { Name = "ARGUVAN", CityId = cityMap["MALATYA"] },
                new District { Name = "BATTALGAZİ", CityId = cityMap["MALATYA"] },
                new District { Name = "DARENDE", CityId = cityMap["MALATYA"] },
                new District { Name = "DOĞANŞEHİR", CityId = cityMap["MALATYA"] },
                new District { Name = "DOĞANYOL", CityId = cityMap["MALATYA"] },
                new District { Name = "HEKİMHAN", CityId = cityMap["MALATYA"] },
                new District { Name = "KALE", CityId = cityMap["MALATYA"] },
                new District { Name = "KULUNCAK", CityId = cityMap["MALATYA"] },
                new District { Name = "PÜTÜRGE", CityId = cityMap["MALATYA"] },
                new District { Name = "YAZIHAN", CityId = cityMap["MALATYA"] },
                new District { Name = "YEŞİLYURT", CityId = cityMap["MALATYA"] },

                // --- MANİSA İLÇELERİ ---
                new District { Name = "AHMETLİ", CityId = cityMap["MANİSA"] },
                new District { Name = "AKHİSAR", CityId = cityMap["MANİSA"] },
                new District { Name = "ALAŞEHİR", CityId = cityMap["MANİSA"] },
                new District { Name = "DEMİRCİ", CityId = cityMap["MANİSA"] },
                new District { Name = "GÖLMARMARA", CityId = cityMap["MANİSA"] },
                new District { Name = "GÖRDES", CityId = cityMap["MANİSA"] },
                new District { Name = "KIRKAĞAÇ", CityId = cityMap["MANİSA"] },
                new District { Name = "KÖPRÜBAŞI", CityId = cityMap["MANİSA"] },
                new District { Name = "KULA", CityId = cityMap["MANİSA"] },
                new District { Name = "SALİHLİ", CityId = cityMap["MANİSA"] },
                new District { Name = "SARIGÖL", CityId = cityMap["MANİSA"] },
                new District { Name = "SARUHANLI", CityId = cityMap["MANİSA"] },
                new District { Name = "SELENDİ", CityId = cityMap["MANİSA"] },
                new District { Name = "SOMA", CityId = cityMap["MANİSA"] },
                new District { Name = "ŞEHZADELER", CityId = cityMap["MANİSA"] },
                new District { Name = "TURGUTLU", CityId = cityMap["MANİSA"] },
                new District { Name = "YUNUSEMRE", CityId = cityMap["MANİSA"] },

                // --- KAHRAMANMARAŞ İLÇELERİ ---
                new District { Name = "AFŞİN", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "ANDIRIN", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "ÇAĞLAYANCERİT", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "DULKADİROĞLU", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "EKİNÖZÜ", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "ELBİSTAN", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "GÖKSUN", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "NURHAK", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "ONİKİŞUBAT", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "PAZARCIK", CityId = cityMap["KAHRAMANMARAŞ"] },
                new District { Name = "TÜRKOĞLU", CityId = cityMap["KAHRAMANMARAŞ"] },

                // --- MARDİN İLÇELERİ ---
                new District { Name = "ARTUKLU", CityId = cityMap["MARDİN"] },
                new District { Name = "DARGEÇİT", CityId = cityMap["MARDİN"] },
                new District { Name = "DERİK", CityId = cityMap["MARDİN"] },
                new District { Name = "KIZILTEPE", CityId = cityMap["MARDİN"] },
                new District { Name = "MAZIDAĞI", CityId = cityMap["MARDİN"] },
                new District { Name = "MİDYAT", CityId = cityMap["MARDİN"] },
                new District { Name = "NUSAYBİN", CityId = cityMap["MARDİN"] },
                new District { Name = "ÖMERLİ", CityId = cityMap["MARDİN"] },
                new District { Name = "SAVUR", CityId = cityMap["MARDİN"] },
                new District { Name = "YEŞİLLİ", CityId = cityMap["MARDİN"] },

                // --- MUĞLA İLÇELERİ ---
                new District { Name = "BODRUM", CityId = cityMap["MUĞLA"] },
                new District { Name = "DALAMAN", CityId = cityMap["MUĞLA"] },
                new District { Name = "DATÇA", CityId = cityMap["MUĞLA"] },
                new District { Name = "FETHİYE", CityId = cityMap["MUĞLA"] },
                new District { Name = "KAVAKLIDERE", CityId = cityMap["MUĞLA"] },
                new District { Name = "KÖYCEĞİZ", CityId = cityMap["MUĞLA"] },
                new District { Name = "MARMARİS", CityId = cityMap["MUĞLA"] },
                new District { Name = "MENTEŞE", CityId = cityMap["MUĞLA"] },
                new District { Name = "MİLAS", CityId = cityMap["MUĞLA"] },
                new District { Name = "ORTACA", CityId = cityMap["MUĞLA"] },
                new District { Name = "SEYDİKEMER", CityId = cityMap["MUĞLA"] },
                new District { Name = "ULA", CityId = cityMap["MUĞLA"] },
                new District { Name = "YATAĞAN", CityId = cityMap["MUĞLA"] },

               // --- MUŞ İLÇELERİ ---
                new District { Name = "BULANIK", CityId = cityMap["MUŞ"] },
                new District { Name = "HASKÖY", CityId = cityMap["MUŞ"] },
                new District { Name = "KORKUT", CityId = cityMap["MUŞ"] },
                new District { Name = "MALAZGİRT", CityId = cityMap["MUŞ"] },
                new District { Name = "MERKEZ", CityId = cityMap["MUŞ"] },
                new District { Name = "VARTO", CityId = cityMap["MUŞ"] },

                // --- NEVŞEHİR İLÇELERİ ---
                new District { Name = "ACIGÖL", CityId = cityMap["NEVŞEHİR"] },
                new District { Name = "AVANOS", CityId = cityMap["NEVŞEHİR"] },
                new District { Name = "DERİNKUYU", CityId = cityMap["NEVŞEHİR"] },
                new District { Name = "GÜLŞEHİR", CityId = cityMap["NEVŞEHİR"] },
                new District { Name = "HACIBEKTAŞ", CityId = cityMap["NEVŞEHİR"] },
                new District { Name = "KOZAKLI", CityId = cityMap["NEVŞEHİR"] },
                new District { Name = "MERKEZ", CityId = cityMap["NEVŞEHİR"] },
                new District { Name = "ÜRGÜP", CityId = cityMap["NEVŞEHİR"] },

                // --- NİĞDE İLÇELERİ ---
                new District { Name = "ALTUNHİSAR", CityId = cityMap["NİĞDE"] },
                new District { Name = "BOR", CityId = cityMap["NİĞDE"] },
                new District { Name = "ÇAMARDI", CityId = cityMap["NİĞDE"] },
                new District { Name = "ÇİFTLİK", CityId = cityMap["NİĞDE"] },
                new District { Name = "MERKEZ", CityId = cityMap["NİĞDE"] },
                new District { Name = "ULUKIŞLA", CityId = cityMap["NİĞDE"] },

                // --- ORDU İLÇELERİ ---
                new District { Name = "AKKUŞ", CityId = cityMap["ORDU"] },
                new District { Name = "ALTINORDU", CityId = cityMap["ORDU"] },
                new District { Name = "AYBASTI", CityId = cityMap["ORDU"] },
                new District { Name = "ÇAMAŞ", CityId = cityMap["ORDU"] },
                new District { Name = "ÇATALPINAR", CityId = cityMap["ORDU"] },
                new District { Name = "ÇAYBAŞI", CityId = cityMap["ORDU"] },
                new District { Name = "FATSA", CityId = cityMap["ORDU"] },
                new District { Name = "GÖLKÖY", CityId = cityMap["ORDU"] },
                new District { Name = "GÜLYALI", CityId = cityMap["ORDU"] },
                new District { Name = "GÜRGENTEPE", CityId = cityMap["ORDU"] },
                new District { Name = "İKİZCE", CityId = cityMap["ORDU"] },
                new District { Name = "KABADÜZ", CityId = cityMap["ORDU"] },
                new District { Name = "KABATAŞ", CityId = cityMap["ORDU"] },
                new District { Name = "KORGAN", CityId = cityMap["ORDU"] },
                new District { Name = "KUMRU", CityId = cityMap["ORDU"] },
                new District { Name = "MESUDİYE", CityId = cityMap["ORDU"] },
                new District { Name = "PERŞEMBE", CityId = cityMap["ORDU"] },
                new District { Name = "ULUBEY", CityId = cityMap["ORDU"] },
                new District { Name = "ÜNYE", CityId = cityMap["ORDU"] },

                // --- RİZE İLÇELERİ ---
                new District { Name = "ARDEŞEN", CityId = cityMap["RİZE"] },
                new District { Name = "ÇAMLIHEMŞİN", CityId = cityMap["RİZE"] },
                new District { Name = "ÇAYELİ", CityId = cityMap["RİZE"] },
                new District { Name = "DEREPAZARI", CityId = cityMap["RİZE"] },
                new District { Name = "FINDIKLI", CityId = cityMap["RİZE"] },
                new District { Name = "GÜNEYSU", CityId = cityMap["RİZE"] },
                new District { Name = "HEMŞİN", CityId = cityMap["RİZE"] },
                new District { Name = "İKİZDERE", CityId = cityMap["RİZE"] },
                new District { Name = "İYİDERE", CityId = cityMap["RİZE"] },
                new District { Name = "KALKANDERE", CityId = cityMap["RİZE"] },
                new District { Name = "MERKEZ", CityId = cityMap["RİZE"] },
                new District { Name = "PAZAR", CityId = cityMap["RİZE"] },

                // --- SAKARYA İLÇELERİ ---
                new District { Name = "ADAPAZARI", CityId = cityMap["SAKARYA"] },
                new District { Name = "AKYAZI", CityId = cityMap["SAKARYA"] },
                new District { Name = "ARİFİYE", CityId = cityMap["SAKARYA"] },
                new District { Name = "ERENLER", CityId = cityMap["SAKARYA"] },
                new District { Name = "FERİZLİ", CityId = cityMap["SAKARYA"] },
                new District { Name = "GEYVE", CityId = cityMap["SAKARYA"] },
                new District { Name = "HENDEK", CityId = cityMap["SAKARYA"] },
                new District { Name = "KARAPÜRÇEK", CityId = cityMap["SAKARYA"] },
                new District { Name = "KARASU", CityId = cityMap["SAKARYA"] },
                new District { Name = "KAYNARCA", CityId = cityMap["SAKARYA"] },
                new District { Name = "KOCAALİ", CityId = cityMap["SAKARYA"] },
                new District { Name = "PAMUKOVA", CityId = cityMap["SAKARYA"] },
                new District { Name = "SAPANCA", CityId = cityMap["SAKARYA"] },
                new District { Name = "SERDİVAN", CityId = cityMap["SAKARYA"] },
                new District { Name = "SÖĞÜTLÜ", CityId = cityMap["SAKARYA"] },
                new District { Name = "TARAKLI", CityId = cityMap["SAKARYA"] },

                // --- SAMSUN İLÇELERİ ---
                new District { Name = "19 MAYIS", CityId = cityMap["SAMSUN"] },
                new District { Name = "ALAÇAM", CityId = cityMap["SAMSUN"] },
                new District { Name = "ASARCIK", CityId = cityMap["SAMSUN"] },
                new District { Name = "ATAKUM", CityId = cityMap["SAMSUN"] },
                new District { Name = "AYVACIK", CityId = cityMap["SAMSUN"] },
                new District { Name = "BAFRA", CityId = cityMap["SAMSUN"] },
                new District { Name = "CANİK", CityId = cityMap["SAMSUN"] },
                new District { Name = "ÇARŞAMBA", CityId = cityMap["SAMSUN"] },
                new District { Name = "HAVZA", CityId = cityMap["SAMSUN"] },
                new District { Name = "İLKADIM", CityId = cityMap["SAMSUN"] },
                new District { Name = "KAVAK", CityId = cityMap["SAMSUN"] },
                new District { Name = "LADİK", CityId = cityMap["SAMSUN"] },
                new District { Name = "SALIPAZARI", CityId = cityMap["SAMSUN"] },
                new District { Name = "TEKKEKÖY", CityId = cityMap["SAMSUN"] },
                new District { Name = "TERME", CityId = cityMap["SAMSUN"] },
                new District { Name = "VEZİRKÖPRÜ", CityId = cityMap["SAMSUN"] },
                new District { Name = "YAKAKENT", CityId = cityMap["SAMSUN"] },

                // --- SİİRT İLÇELERİ ---
                new District { Name = "BAYKAN", CityId = cityMap["SİİRT"] },
                new District { Name = "ERUH", CityId = cityMap["SİİRT"] },
                new District { Name = "KURTALAN", CityId = cityMap["SİİRT"] },
                new District { Name = "MERKEZ", CityId = cityMap["SİİRT"] },
                new District { Name = "PERVARİ", CityId = cityMap["SİİRT"] },
                new District { Name = "ŞİRVAN", CityId = cityMap["SİİRT"] },
                new District { Name = "TİLLO", CityId = cityMap["SİİRT"] },

                // --- SİNOP İLÇELERİ ---
                new District { Name = "AYANCIK", CityId = cityMap["SİNOP"] },
                new District { Name = "BOYABAT", CityId = cityMap["SİNOP"] },
                new District { Name = "DİKMEN", CityId = cityMap["SİNOP"] },
                new District { Name = "DURAĞAN", CityId = cityMap["SİNOP"] },
                new District { Name = "ERFELEK", CityId = cityMap["SİNOP"] },
                new District { Name = "GERZE", CityId = cityMap["SİNOP"] },
                new District { Name = "MERKEZ", CityId = cityMap["SİNOP"] },
                new District { Name = "SARAYDÜZÜ", CityId = cityMap["SİNOP"] },
                new District { Name = "TÜRKELİ", CityId = cityMap["SİNOP"] },

                // --- SİVAS İLÇELERİ ---
                new District { Name = "AKINCILAR", CityId = cityMap["SİVAS"] },
                new District { Name = "ALTINYAYLA", CityId = cityMap["SİVAS"] },
                new District { Name = "DİVRİĞİ", CityId = cityMap["SİVAS"] },
                new District { Name = "DOĞANŞAR", CityId = cityMap["SİVAS"] },
                new District { Name = "GEMEREK", CityId = cityMap["SİVAS"] },
                new District { Name = "GÖLOVA", CityId = cityMap["SİVAS"] },
                new District { Name = "GÜRÜN", CityId = cityMap["SİVAS"] },
                new District { Name = "HAFİK", CityId = cityMap["SİVAS"] },
                new District { Name = "İMRANLI", CityId = cityMap["SİVAS"] },
                new District { Name = "KANGAL", CityId = cityMap["SİVAS"] },
                new District { Name = "KOYULHİSAR", CityId = cityMap["SİVAS"] },
                new District { Name = "MERKEZ", CityId = cityMap["SİVAS"] },
                new District { Name = "SUŞEHRİ", CityId = cityMap["SİVAS"] },
                new District { Name = "ŞARKIŞLA", CityId = cityMap["SİVAS"] },
                new District { Name = "ULAŞ", CityId = cityMap["SİVAS"] },
                new District { Name = "YILDIZELİ", CityId = cityMap["SİVAS"] },
                new District { Name = "ZARA", CityId = cityMap["SİVAS"] },

                // --- TEKİRDAĞ İLÇELERİ ---
                new District { Name = "ÇERKEZKÖY", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "ÇORLU", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "ERGENE", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "HAYRABOLU", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "KAPAKLI", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "MALKARA", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "MARMARAEREĞLİSİ", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "MURATLI", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "SARAY", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "SÜLEYMANPAŞA", CityId = cityMap["TEKİRDAĞ"] },
                new District { Name = "ŞARKÖY", CityId = cityMap["TEKİRDAĞ"] },

                // --- TOKAT İLÇELERİ ---
                new District { Name = "ALMUS", CityId = cityMap["TOKAT"] },
                new District { Name = "ARTOVA", CityId = cityMap["TOKAT"] },
                new District { Name = "BAŞÇİFTLİK", CityId = cityMap["TOKAT"] },
                new District { Name = "ERBAA", CityId = cityMap["TOKAT"] },
                new District { Name = "MERKEZ", CityId = cityMap["TOKAT"] },
                new District { Name = "NİKSAR", CityId = cityMap["TOKAT"] },
                new District { Name = "PAZAR", CityId = cityMap["TOKAT"] },
                new District { Name = "REŞADİYE", CityId = cityMap["TOKAT"] },
                new District { Name = "SULUSARAY", CityId = cityMap["TOKAT"] },
                new District { Name = "TURHAL", CityId = cityMap["TOKAT"] },
                new District { Name = "YEŞİLYURT", CityId = cityMap["TOKAT"] },
                new District { Name = "ZİLE", CityId = cityMap["TOKAT"] },

                // --- TRABZON İLÇELERİ ---
                new District { Name = "AKÇAABAT", CityId = cityMap["TRABZON"] },
                new District { Name = "ARAKLI", CityId = cityMap["TRABZON"] },
                new District { Name = "ARSİN", CityId = cityMap["TRABZON"] },
                new District { Name = "BEŞİKDÜZÜ", CityId = cityMap["TRABZON"] },
                new District { Name = "ÇARŞIBAŞI", CityId = cityMap["TRABZON"] },
                new District { Name = "ÇAYKARA", CityId = cityMap["TRABZON"] },
                new District { Name = "DERNEKPAZARI", CityId = cityMap["TRABZON"] },
                new District { Name = "DÜZKÖY", CityId = cityMap["TRABZON"] },
                new District { Name = "HAYRAT", CityId = cityMap["TRABZON"] },
                new District { Name = "KÖPRÜBAŞI", CityId = cityMap["TRABZON"] },
                new District { Name = "MAÇKA", CityId = cityMap["TRABZON"] },
                new District { Name = "OF", CityId = cityMap["TRABZON"] },
                new District { Name = "ORTAHİSAR", CityId = cityMap["TRABZON"] },
                new District { Name = "SÜRMENE", CityId = cityMap["TRABZON"] },
                new District { Name = "ŞALPAZARI", CityId = cityMap["TRABZON"] },
                new District { Name = "TONYA", CityId = cityMap["TRABZON"] },
                new District { Name = "VAKFIKEBİR", CityId = cityMap["TRABZON"] },
                new District { Name = "YOMRA", CityId = cityMap["TRABZON"] },

                // --- TUNCELİ İLÇELERİ ---
                new District { Name = "ÇEMİŞGEZEK", CityId = cityMap["TUNCELİ"] },
                new District { Name = "HOZAT", CityId = cityMap["TUNCELİ"] },
                new District { Name = "MAZGİRT", CityId = cityMap["TUNCELİ"] },
                new District { Name = "MERKEZ", CityId = cityMap["TUNCELİ"] },
                new District { Name = "NAZIMİYE", CityId = cityMap["TUNCELİ"] },
                new District { Name = "OVACIK", CityId = cityMap["TUNCELİ"] },
                new District { Name = "PERTEK", CityId = cityMap["TUNCELİ"] },
                new District { Name = "PÜLÜMÜR", CityId = cityMap["TUNCELİ"] },

                // --- ŞANLIURFA İLÇELERİ ---
                new District { Name = "AKÇAKALE", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "BİRECİK", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "BOZOVA", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "CEYLANPINAR", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "EYYÜBİYE", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "HALFETİ", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "HALİLİYE", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "HARRAN", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "HİLVAN", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "KARAKÖPRÜ", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "SİVEREK", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "SURUÇ", CityId = cityMap["ŞANLIURFA"] },
                new District { Name = "VİRANŞEHİR", CityId = cityMap["ŞANLIURFA"] },

                // --- UŞAK İLÇELERİ ---
                new District { Name = "BANAZ", CityId = cityMap["UŞAK"] },
                new District { Name = "EŞME", CityId = cityMap["UŞAK"] },
                new District { Name = "KARAHALLI", CityId = cityMap["UŞAK"] },
                new District { Name = "MERKEZ", CityId = cityMap["UŞAK"] },
                new District { Name = "SİVASLI", CityId = cityMap["UŞAK"] },
                new District { Name = "ULUBEY", CityId = cityMap["UŞAK"] },

                // --- VAN İLÇELERİ ---
                new District { Name = "BAHÇESARAY", CityId = cityMap["VAN"] },
                new District { Name = "BAŞKALE", CityId = cityMap["VAN"] },
                new District { Name = "ÇALDIRAN", CityId = cityMap["VAN"] },
                new District { Name = "ÇATAK", CityId = cityMap["VAN"] },
                new District { Name = "EDREMİT", CityId = cityMap["VAN"] },
                new District { Name = "ERCİŞ", CityId = cityMap["VAN"] },
                new District { Name = "GEVAŞ", CityId = cityMap["VAN"] },
                new District { Name = "GÜRPINAR", CityId = cityMap["VAN"] },
                new District { Name = "İPEKYOLU", CityId = cityMap["VAN"] },
                new District { Name = "MURADİYE", CityId = cityMap["VAN"] },
                new District { Name = "ÖZALP", CityId = cityMap["VAN"] },
                new District { Name = "SARAY", CityId = cityMap["VAN"] },
                new District { Name = "TUŞBA", CityId = cityMap["VAN"] },

                // --- YOZGAT İLÇELERİ ---
                new District { Name = "AKDAĞMADENİ", CityId = cityMap["YOZGAT"] },
                new District { Name = "AYDINCIK", CityId = cityMap["YOZGAT"] },
                new District { Name = "BOĞAZLIYAN", CityId = cityMap["YOZGAT"] },
                new District { Name = "ÇANDIR", CityId = cityMap["YOZGAT"] },
                new District { Name = "ÇAYIRALAN", CityId = cityMap["YOZGAT"] },
                new District { Name = "ÇEKEREK", CityId = cityMap["YOZGAT"] },
                new District { Name = "KADIŞEHRİ", CityId = cityMap["YOZGAT"] },
                new District { Name = "MERKEZ", CityId = cityMap["YOZGAT"] },
                new District { Name = "SARAYKENT", CityId = cityMap["YOZGAT"] },
                new District { Name = "SARIKAYA", CityId = cityMap["YOZGAT"] },
                new District { Name = "SORGUN", CityId = cityMap["YOZGAT"] },
                new District { Name = "ŞEFAATLİ", CityId = cityMap["YOZGAT"] },
                new District { Name = "YENİFAKILI", CityId = cityMap["YOZGAT"] },
                new District { Name = "YERKÖY", CityId = cityMap["YOZGAT"] },

                // --- ZONGULDAK İLÇELERİ ---
                new District { Name = "ALAPLI", CityId = cityMap["ZONGULDAK"] },
                new District { Name = "ÇAYCUMA", CityId = cityMap["ZONGULDAK"] },
                new District { Name = "DEVREK", CityId = cityMap["ZONGULDAK"] },
                new District { Name = "EREĞLİ", CityId = cityMap["ZONGULDAK"] },
                new District { Name = "GÖKÇEBEY", CityId = cityMap["ZONGULDAK"] },
                new District { Name = "KİLİMLİ", CityId = cityMap["ZONGULDAK"] },
                new District { Name = "KOZLU", CityId = cityMap["ZONGULDAK"] },
                new District { Name = "MERKEZ", CityId = cityMap["ZONGULDAK"] },

                // --- AKSARAY İLÇELERİ ---
                new District { Name = "AĞAÇÖREN", CityId = cityMap["AKSARAY"] },
                new District { Name = "ESKİL", CityId = cityMap["AKSARAY"] },
                new District { Name = "GÜLAĞAÇ", CityId = cityMap["AKSARAY"] },
                new District { Name = "GÜZELYURT", CityId = cityMap["AKSARAY"] },
                new District { Name = "MERKEZ", CityId = cityMap["AKSARAY"] },
                new District { Name = "ORTAKÖY", CityId = cityMap["AKSARAY"] },
                new District { Name = "SARIYAHŞİ", CityId = cityMap["AKSARAY"] },
                new District { Name = "SULTANHANI", CityId = cityMap["AKSARAY"] },

                // --- BAYBURT İLÇELERİ ---
                new District { Name = "AYDINTEPE", CityId = cityMap["BAYBURT"] },
                new District { Name = "DEMİRÖZÜ", CityId = cityMap["BAYBURT"] },
                new District { Name = "MERKEZ", CityId = cityMap["BAYBURT"] },

                // --- KARAMAN İLÇELERİ ---
                new District { Name = "AYRANCI", CityId = cityMap["KARAMAN"] },
                new District { Name = "BAŞYAYLA", CityId = cityMap["KARAMAN"] },
                new District { Name = "ERMENEK", CityId = cityMap["KARAMAN"] },
                new District { Name = "KAZIMKARABEKİR", CityId = cityMap["KARAMAN"] },
                new District { Name = "MERKEZ", CityId = cityMap["KARAMAN"] },
                new District { Name = "SARIVELİLER", CityId = cityMap["KARAMAN"] },

                // --- KIRIKKALE İLÇELERİ ---
                new District { Name = "BAHŞILI", CityId = cityMap["KIRIKKALE"] },
                new District { Name = "BALIŞEYH", CityId = cityMap["KIRIKKALE"] },
                new District { Name = "ÇELEBİ", CityId = cityMap["KIRIKKALE"] },
                new District { Name = "DELİCE", CityId = cityMap["KIRIKKALE"] },
                new District { Name = "KARAKEÇİLİ", CityId = cityMap["KIRIKKALE"] },
                new District { Name = "KESKİN", CityId = cityMap["KIRIKKALE"] },
                new District { Name = "MERKEZ", CityId = cityMap["KIRIKKALE"] },
                new District { Name = "SULAKYURT", CityId = cityMap["KIRIKKALE"] },
                new District { Name = "YAHŞİHAN", CityId = cityMap["KIRIKKALE"] },

                // --- BATMAN İLÇELERİ ---
                new District { Name = "BEŞİRİ", CityId = cityMap["BATMAN"] },
                new District { Name = "GERCÜŞ", CityId = cityMap["BATMAN"] },
                new District { Name = "HASANKEYF", CityId = cityMap["BATMAN"] },
                new District { Name = "KOZLUK", CityId = cityMap["BATMAN"] },
                new District { Name = "MERKEZ", CityId = cityMap["BATMAN"] },
                new District { Name = "SASON", CityId = cityMap["BATMAN"] },

                // --- ŞIRNAK İLÇELERİ ---
                new District { Name = "BEYTÜŞŞEBAP", CityId = cityMap["ŞIRNAK"] },
                new District { Name = "CİZRE", CityId = cityMap["ŞIRNAK"] },
                new District { Name = "GÜÇLÜKONAK", CityId = cityMap["ŞIRNAK"] },
                new District { Name = "İDİL", CityId = cityMap["ŞIRNAK"] },
                new District { Name = "MERKEZ", CityId = cityMap["ŞIRNAK"] },
                new District { Name = "SİLOPİ", CityId = cityMap["ŞIRNAK"] },
                new District { Name = "ULUDERE", CityId = cityMap["ŞIRNAK"] },

                // --- BARTIN İLÇELERİ ---
                new District { Name = "AMASRA", CityId = cityMap["BARTIN"] },
                new District { Name = "KURUCAŞİLE", CityId = cityMap["BARTIN"] },
                new District { Name = "MERKEZ", CityId = cityMap["BARTIN"] },
                new District { Name = "ULUS", CityId = cityMap["BARTIN"] },

                // --- ARDAHAN İLÇELERİ ---
                new District { Name = "ÇILDIR", CityId = cityMap["ARDAHAN"] },
                new District { Name = "DAMAL", CityId = cityMap["ARDAHAN"] },
                new District { Name = "GÖLE", CityId = cityMap["ARDAHAN"] },
                new District { Name = "HANAK", CityId = cityMap["ARDAHAN"] },
                new District { Name = "MERKEZ", CityId = cityMap["ARDAHAN"] },
                new District { Name = "POSOF", CityId = cityMap["ARDAHAN"] },

                // --- IĞDIR İLÇELERİ ---
                new District { Name = "ARALIK", CityId = cityMap["IĞDIR"] },
                new District { Name = "KARAKOYUNLU", CityId = cityMap["IĞDIR"] },
                new District { Name = "MERKEZ", CityId = cityMap["IĞDIR"] },
                new District { Name = "TUZLUCA", CityId = cityMap["IĞDIR"] },

                // --- YALOVA İLÇELERİ ---
                new District { Name = "ALTINOVA", CityId = cityMap["YALOVA"] },
                new District { Name = "ARMUTLU", CityId = cityMap["YALOVA"] },
                new District { Name = "ÇINARCIK", CityId = cityMap["YALOVA"] },
                new District { Name = "ÇİFTLİKKÖY", CityId = cityMap["YALOVA"] },
                new District { Name = "MERKEZ", CityId = cityMap["YALOVA"] },
                new District { Name = "TERMAL", CityId = cityMap["YALOVA"] },

                // --- KARABÜK İLÇELERİ ---
                new District { Name = "EFLANİ", CityId = cityMap["KARABÜK"] },
                new District { Name = "ESKİPAZAR", CityId = cityMap["KARABÜK"] },
                new District { Name = "MERKEZ", CityId = cityMap["KARABÜK"] },
                new District { Name = "OVACIK", CityId = cityMap["KARABÜK"] },
                new District { Name = "SAFRANBOLU", CityId = cityMap["KARABÜK"] },
                new District { Name = "YENİCE", CityId = cityMap["KARABÜK"] },

                // --- KİLİS İLÇELERİ ---
                new District { Name = "ELBEYLİ", CityId = cityMap["KİLİS"] },
                new District { Name = "MERKEZ", CityId = cityMap["KİLİS"] },
                new District { Name = "MUSABEYLİ", CityId = cityMap["KİLİS"] },
                new District { Name = "POLATELİ", CityId = cityMap["KİLİS"] },

                // --- OSMANİYE İLÇELERİ ---
                new District { Name = "BAHÇE", CityId = cityMap["OSMANİYE"] },
                new District { Name = "DÜZİÇİ", CityId = cityMap["OSMANİYE"] },
                new District { Name = "HASANBEYLİ", CityId = cityMap["OSMANİYE"] },
                new District { Name = "KADİRLİ", CityId = cityMap["OSMANİYE"] },
                new District { Name = "MERKEZ", CityId = cityMap["OSMANİYE"] },
                new District { Name = "SUMBAS", CityId = cityMap["OSMANİYE"] },
                new District { Name = "TOPRAKKALE", CityId = cityMap["OSMANİYE"] },

                // --- DÜZCE İLÇELERİ ---
                new District { Name = "AKÇAKOCA", CityId = cityMap["DÜZCE"] },
                new District { Name = "CUMAYERİ", CityId = cityMap["DÜZCE"] },
                new District { Name = "ÇİLİMLİ", CityId = cityMap["DÜZCE"] },
                new District { Name = "GÖLYAKA", CityId = cityMap["DÜZCE"] },
                new District { Name = "GÜMÜŞOVA", CityId = cityMap["DÜZCE"] },
                new District { Name = "KAYNAŞLI", CityId = cityMap["DÜZCE"] },
                new District { Name = "MERKEZ", CityId = cityMap["DÜZCE"] },
                new District { Name = "YIĞILCA", CityId = cityMap["DÜZCE"] },
            ];
        }

        private static IEnumerable<EventType> GetEventTypes()
        {
            return
            [
                new EventType{  Name= "KONSER" },
                new EventType{  Name= "TİYATRO" },
                new EventType{  Name= "SİNEMA" },
                new EventType{  Name= "OPERA" },
                new EventType{  Name= "KONFERANS" },
            ];
        }

        private static IEnumerable<Venue> GetVenues(List<District> districts)
        {
            var districtMap = districts.ToDictionary(d => $"{d.Name.ToUpper()}-{d.City.Name.ToUpper()}", d => d.Id);

            return
            [
                // --- İSTANBUL Mekanları ---
                new Venue { Name = "ZORLU PSM", Capacity = 2200, DistrictId = districtMap["BEŞİKTAŞ-İSTANBUL"] },
                new Venue { Name = "CADDEBOSTAN KÜLTÜR MERKEZİ", Capacity = 800, DistrictId = districtMap["KADIKÖY-İSTANBUL"] },
                new Venue { Name = "DASDAS SAHNE", Capacity = 600, DistrictId = districtMap["KADIKÖY-İSTANBUL"] }, // Not: Ataşehir'de, ancak Seed datanızda Ataşehir yoksa Kadıköy'e ekleyelim.
                new Venue { Name = "VOLKSWAGEN ARENA", Capacity = 5000, DistrictId = districtMap["SARIYER-İSTANBUL"] },
                new Venue { Name = "HARBİYE CEMİL TOPUZLU AÇIKHAVA TİYATROSU", Capacity = 4500, DistrictId = districtMap["ŞİŞLİ-İSTANBUL"] },
                new Venue { Name = "BABA SAHNE", Capacity = 300, DistrictId = districtMap["KADIKÖY-İSTANBUL"] },

                // --- ANKARA Mekanları ---
                new Venue { Name = "CSO ADA ANKARA", Capacity = 2000, DistrictId = districtMap["ÇANKAYA-ANKARA"] },
                new Venue { Name = "CONGRESIUM ANKARA", Capacity = 3000, DistrictId = districtMap["ÇANKAYA-ANKARA"] },
                new Venue { Name = "MEB ŞURA SALONU", Capacity = 1000, DistrictId = districtMap["ÇANKAYA-ANKARA"] },
                new Venue { Name = "ODTÜ KKM", Capacity = 1200, DistrictId = districtMap["ÇANKAYA-ANKARA"] },

                // --- İZMİR Mekanları ---
                new Venue { Name = "İZMİR ARENA", Capacity = 3000, DistrictId = districtMap["BAYRAKLI-İZMİR"] },
                new Venue { Name = "BOSTANLI SUAT TAŞER TİYATROSU", Capacity = 700, DistrictId = districtMap["KARŞIYAKA-İZMİR"] },
                new Venue { Name = "KÜLTÜRPARK AÇIKHAVA TİYATROSU", Capacity = 2500, DistrictId = districtMap["KONAK-İZMİR"] },
                new Venue { Name = "AHMET ADNAN SAYGUN SANAT MERKEZİ", Capacity = 1100, DistrictId = districtMap["KONAK-İZMİR"] },

                // --- BURSA Mekanları ---
                new Venue { Name = "MERİNOS AKKM", Capacity = 1700, DistrictId = districtMap["OSMANGAZİ-BURSA"] },
                new Venue { Name = "TAYYARE KÜLTÜR MERKEZİ", Capacity = 650, DistrictId = districtMap["OSMANGAZİ-BURSA"] },

                // --- ANTALYA Mekanları ---
                new Venue { Name = "ANTALYA AÇIKHAVA TİYATROSU", Capacity = 3000, DistrictId = districtMap["KONYAALTI-ANTALYA"] },
                new Venue { Name = "EXPO 2016 KONGRE MERKEZİ", Capacity = 5000, DistrictId = districtMap["AKSU-ANTALYA"] }
            ];
        }

        private static IEnumerable<Event> GetEvents(List<EventType> eventTypes, List<Venue> venues)
        {
            var eventTypeMap = eventTypes.ToDictionary(x => x.Name.ToUpper(), x => x.Id);
            var venueMap = venues.ToDictionary(x => x.Name.ToUpper(), x => x.Id);

            return
            [
                // 1. İstanbul Konseri (Zorlu)
                new Event
                {
                    Name = "Büyük Ev Ablukada Konseri",
                    Date = DateTimeOffset.UtcNow.AddDays(20).AddHours(21),
                    Price = 750.00m,
                    Description = "Büyük Ev Ablukada 'Defansif Dizayn' albümüyle Zorlu PSM'de.",
                    Capacity = 2200,
                    EventTypeId = eventTypeMap["KONSER"],
                    VenueId = venueMap["ZORLU PSM"]
                },
                
                // 2. Ankara Tiyatrosu (CSO)
                new Event
                {
                    Name = "Hamlet Oyunu",
                    Date = DateTimeOffset.UtcNow.AddDays(30).AddHours(20),
                    Price = 400.00m,
                    Description = "Shakespeare klasiği CSO ADA'da.",
                    Capacity = 2000,
                    EventTypeId = eventTypeMap["TİYATRO"],
                    VenueId = venueMap["CSO ADA ANKARA"]
                },

                // 3. İstanbul Tiyatrosu (DasDas)
                new Event
                {
                    Name = "Cyrano de Bergerac",
                    Date = DateTimeOffset.UtcNow.AddDays(15).AddHours(20),
                    Price = 300.00m,
                    Description = "DasDas Sahne'de klasik bir eser.",
                    Capacity = 600,
                    EventTypeId = eventTypeMap["TİYATRO"],
                    VenueId = venueMap["DASDAS SAHNE"]
                },

                // 4. Ankara Konferansı (Congresium)
                new Event
                {
                    Name = "Geleceğin Teknolojileri Zirvesi",
                    Date = DateTimeOffset.UtcNow.AddDays(45).AddHours(10),
                    Price = 1500.00m,
                    Description = "Yapay zeka ve blockchain üzerine konuşmalar.",
                    Capacity = 3000,
                    EventTypeId = eventTypeMap["KONFERANS"],
                    VenueId = venueMap["CONGRESIUM ANKARA"]
                },
                
                // 5. İzmir Konseri (Arena)
                new Event
                {
                    Name = "Duman Konseri",
                    Date = DateTimeOffset.UtcNow.AddDays(10).AddHours(21),
                    Price = 600.00m,
                    Description = "Duman, İzmir Arena'da.",
                    Capacity = 3000,
                    EventTypeId = eventTypeMap["KONSER"],
                    VenueId = venueMap["İZMİR ARENA"]
                },

                // 6. İstanbul Sinema (CKM)
                new Event
                {
                    Name = "Bağımsız Filmler Festivali - Açılış",
                    Date = DateTimeOffset.UtcNow.AddDays(5).AddHours(19),
                    Price = 150.00m,
                    Description = "Bağımsız yönetmenlerin filmleri CKM'de.",
                    Capacity = 800,
                    EventTypeId = eventTypeMap["SİNEMA"],
                    VenueId = venueMap["CADDEBOSTAN KÜLTÜR MERKEZİ"]
                },

                // 7. İstanbul Opera (Zorlu)
                new Event
                {
                    Name = "La Traviata Operası",
                    Date = DateTimeOffset.UtcNow.AddDays(50).AddHours(20),
                    Price = 900.00m,
                    Description = "Devlet Opera ve Balesi sunar.",
                    Capacity = 2200,
                    EventTypeId = eventTypeMap["OPERA"],
                    VenueId = venueMap["ZORLU PSM"]
                },

                // 8. Bursa Tiyatrosu (TKM)
                new Event
                {
                    Name = "Bir Delinin Hatıra Defteri",
                    Date = DateTimeOffset.UtcNow.AddDays(25).AddHours(20),
                    Price = 250.00m,
                    Description = "Genco Erkal yorumuyla.",
                    Capacity = 650,
                    EventTypeId = eventTypeMap["TİYATRO"],
                    VenueId = venueMap["TAYYARE KÜLTÜR MERKEZİ"]
                },

                // 9. İstanbul Konseri (VW Arena)
                new Event
                {
                    Name = "Teoman Akustik",
                    Date = DateTimeOffset.UtcNow.AddDays(18).AddHours(21),
                    Price = 800.00m,
                    Description = "Teoman'ın akustik performansı.",
                    Capacity = 5000,
                    EventTypeId = eventTypeMap["KONSER"],
                    VenueId = venueMap["VOLKSWAGEN ARENA"]
                },
                
                // 10. İstanbbul Tiyatrosu (Baba Sahne)
                new Event
                {
                    Name = "Karagöz ve Hacivat Tiyatro Oyunu",
                    Date = DateTimeOffset.UtcNow.AddDays(25),
                    Price = 100.00m,
                    Description = "Sevilen ve özlenen Hacivat ve Karagöz oyunu.",
                    Capacity = 300,
                    EventTypeId = eventTypeMap["TİYATRO"],
                    VenueId = venueMap["BABA SAHNE"]
                }
            ];
        }
    }
}