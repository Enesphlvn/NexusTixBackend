# 🎫 NexusTix - Backend API

![.NET](https://img.shields.io/badge/.NET_8.0-512BD4?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-68217A?style=for-the-badge&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Clean_Architecture-blue?style=for-the-badge)

**NexusTix**, kullanıcıların etkinlikleri keşfedip bilet alabildiği, yöneticilerin ise tüm sistemi kolayca yönetebildiği bir Full-Stack web uygulamasıdır. Bu repo, sistemin **Backend (Sunucu Tarafı)** mimarisini barındırır. Proje, **Clean Architecture** ve **SOLID** prensiplerine sadık kalınarak geliştirilmiştir.

> **Frontend Reposu:** [NexusTix Frontend](https://github.com/Enesphlvn/NexusTix.Frontend)

---

## 🏗️ Mimari ve Tasarım

Proje, hem Backend hem de Frontend tarafında endüstri standartlarına uygun, sürdürülebilir ve test edilebilir mimari prensipleriyle geliştirilmiştir.

### 1. Katmanlar ve Sorumlulukları

| Katman | Açıklama |
| :--- | :--- |
| **NexusTix.Domain** | **(Çekirdek Katman)**. Veritabanı tablolarının karşılığı olan `Entity` sınıfları ve temel arayüzler buradadır. Dış bağımlılığı yoktur. |
| **NexusTix.Persistence** | **(Veritabanı Erişim Katmanı)**. `AppDbContext`, `Migrations` ve SQL sorgularını içeren `Repository` sınıfları bulunur. **Soft Delete** ve **Eager Loading** burada uygulanmıştır. Ayrıca proje ayağa kalktığında çalışan **Data Seeding** mekanizması da buradadır. |
| **NexusTix.Application** | **(İş Mantığı Katmanı)**. `Services`, `DTOs`, `Validators` ve `Mappings` burada yer alır. **CQRS** prensibiyle optimize edilmiş ve karmaşık iş kuralları (`Rules`) merkezi olarak yönetilmiştir. |
| **NexusTix.WebAPI** | **(Sunum Katmanı)**. HTTP isteklerini karşılayan `Controller` sınıfları buradadır. Sadece yönlendirme yapar, iş mantığı içermez. |

### 2. Bağımlılık Enjeksiyonu (DI)
Katmanlar arasındaki bağımlılığı azaltmak (Loose Coupling) için .NET'in yerleşik DI mekanizması kullanılmıştır. Servisler ve Repository'ler, `Extensions` sınıfları aracılığıyla `AddScoped` yaşam döngüsü ile sisteme kaydedilmiştir.

---

## 🧩 Kullanılan Tasarım Desenleri (Design Patterns)

1.  **Generic Repository Pattern:** Kod tekrarını önlemek ve CRUD işlemlerini standartlaştırmak için.
2.  **Unit of Work Pattern:** Veritabanı işlemlerini tek bir `Transaction` altında toplayarak veri bütünlüğünü korumak için.
3.  **Result Pattern (ServiceResult):** API dönüşlerini standart bir yapıya (Data, Status, Error Message) oturtmak için.
4.  **Validation (FluentValidation):** Veri doğrulama kurallarını Controller'lardan çıkarıp merkezi ve temiz bir yapıda yönetmek için.
5.  **DTO & Mapping (AutoMapper):** Domain varlıklarını doğrudan dışarı açmamak ve sadece ihtiyaç duyulan veriyi taşımak için.
6.  **Asynchronous Programming (Async/Await):** Sistemin performansını artırmak ve sunucu kaynaklarını verimli kullanmak için tüm I/O işlemleri asenkron tasarlanmıştır.

---

## 🔐 Güvenlik (Security)

* **JWT (JSON Web Token):** Kimlik doğrulama için kullanılmıştır.
* **Role-Based Authorization:** Admin ve Manager rolleri için özel erişim kontrolleri sağlanmıştır.
* **Security Stamp:** Kritik işlemlerde (Şifre/Email değişimi) eski token'ların anında geçersiz kılınması ve oturum güvenliğinin sağlanması için kullanılmıştır.

---

## 🛠️ Teknoloji Yığını (Tech Stack)

* **Framework:** ASP.NET Core Web API (.NET 8)
* **ORM:** Entity Framework Core 8 (Code-First)
* **Veritabanı:** Microsoft SQL Server
* **Kimlik Yönetimi:** ASP.NET Core Identity
* **Güvenlik:** JWT Bearer Authentication
* **Validasyon:** FluentValidation
* **Mapping:** AutoMapper
* **Dokümantasyon:** Swagger

---

## 🗝️ Temel Özellikler ve Endpoint Grupları

Backend API, aşağıdaki temel işlevsellikleri sağlar:

* **🔐 Kimlik Doğrulama (Auth):** Kayıt, Giriş, E-posta ve Şifre yenileme işlemleri.
* **📅 Etkinlik Yönetimi:** Gelişmiş filtreleme (Tarih, Kategori, Sanatçı, Şehir, İlçe), detay ve konum bilgisi.
* **🎫 Biletleme:** Stok kontrolü, Satın Alma ve İptal işlemleri.
* **📊 Admin Dashboard:** Aylık gelir analizleri ve sistem istatistikleri.
* **📱 QR Check-In:** Etkinlik girişinde bilet doğrulama.

---

## ⚙️ Kurulum ve Çalıştırma

Projeyi yerel ortamınızda çalıştırmak için aşağıdaki adımları izleyin.

### Gereksinimler
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Microsoft SQL Server
* Eğer yüklü değilse EF Core araçlarını yükleyin:
  
  ```bash
  dotnet tool install --global dotnet-ef
  ```
  
### Adımlar

1.  **Repoyu Klonlayın:**
    ```bash
    git clone [https://github.com/Enesphlvn/NexusTixBackend](https://github.com/Enesphlvn/NexusTixBackend)
    cd NexusTixBackend
    ```

2.  **Veritabanı Ayarları:**
    `NexusTix.WebAPI/appsettings.json` dosyasındaki `ConnectionStrings` bölümünü kendi SQL Server bilgilerinize göre güncelleyin.

3.  **Veritabanını Oluşturun:**
    Terminali açın ve proje kök dizininde şu komutu çalıştırın. Bu işlem veritabanını oluşturacak ve proje ilk ayağa kalktığında **Seed Data** (Örnek veriler ve Admin kullanıcısı) otomatik olarak yüklenecektir.
    ```bash
    cd NexusTix.WebAPI
    dotnet ef database update --project ../NexusTix.Persistence
    ```

4.  **Projeyi Başlatın:**
    ```bash
    dotnet run
    ```
    Tarayıcınızda `https://localhost:7258/swagger` adresine giderek API'yi test edebilirsiniz.

---

## 📞 İletişim

Geliştirici: **[Enes PEHLİVAN]**
* GitHub: [github.com/Enesphlvn](https://github.com/Enesphlvn)
* LinkedIn: [linkedin.com/in/enespehlivan](https://www.linkedin.com/in/enespehlivan/)
