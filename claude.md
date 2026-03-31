# CrimeAndWin Projesi (claude.md)
*AI ve geliştirici işbirliğini desteklemek için oluşturulmuş proje özeti.*

## 1. Projenin adı ve kısa açıklaması nedir?
**Projenin Adı:** CrimeAndWin
**Kısa Açıklaması:** Mafya/Suç konseptli (muhtemelen tarayıcı veya mobil tabanlı) bir multiplayer oyunun backend altyapısıdır. Proje .NET (Core) altyapısı ve *Microservice* mimarisi üzerine kurgulanmış olup; geniş kapsamlı bir RPG/strateji oyununun tüm modüllerini (oyuncu profili, dünya, ekonomi, suç aksiyonları vb.) ayrı ayrı modüler servisler üzerinden yönetmeyi hedefler.

## 2. Microservisler arası iletişim nasıl? — REST mi, mesaj kuyruğu mu?
Uygulamada **RabbitMQ** kullanılarak mesaj kuyruğu tabanlı *Event-Driven* mimari de uygulanmaktadır. Servislerin asenkron haberleşmesi ve side-effect işlemlerin (Örneğin: Bir aksiyonun ardından bildirim gönderilmesi veya ekonominin değişmesi vb.) yönetimi RabbitMQ Publisher ve Subscriber yapısı kullanılarak kurgulanmıştır.

## 3. Her microservisin portu / sorumluluğu hakkında kısa bir liste
- **1_CrimeAndWin.Identity (6001 / 6101):** Kullanıcı doğrulama (Authentication), kayıt işlemlerini yürütür. Kendi Identity tabanlı veritabanına sahiptir.
- **2_CrimeAndWin.PlayerProfile (6002 / 6102):** Oyuncunun genel bilgileri, statları (Health, Energy, EXP), seviye yapısı gibi karakteristik özelliklerini tek bir serviste toplar.
- **3_CrimeAndWin.GameWorld (6003 / 6103):** Dünyanın fiziksel veya mantıksal çevre durumunu (mekanlar, haritalar vb.) saklar ve sunar.
- **4_CrimeAndWin.Action (6004 / 6104):** Oyuncunun yaptığı temel eylemleri (işlenen suçlar vb.) simüle eden, başarı/başarısızlık durumlarını ve cooldown sürelerini tutan mantıksal merkezdir.
- **5_CrimeAndWin.Economy (6005 / 6105):** Para işlemleri, mağaza alım-satımları, pazar ekonomisi, elmas/kredi transferleri gibi finansal verileri kontrol eder.
- **6_CrimeAndWin.Inventory (6006 / 6106):** Oyuncu çantasıdır. Zırhlar, silahlar, eşyalar tarzı tüm ürünlerin oyuncular nezdinde sahipliğini izler.
- **7_CrimeAndWin.Leadership (6007 / 6107):** Klan, çete, ekip yapılanmaları ile global ya da bölgesel lider tablosu (Rank) operasyonlarını üstlenir.
- **8_CrimeAndWin.Notification (6008 / 6108):** RabbitMQ'dan aldığı dinlemeler neticesinde oyunculara uygulama içi (anlık mesaj-SignalR) bildirim göndermek için ayrılmış servistir.
- **9_CrimeAndWin.Moderation (6009 / 6109):** Düzen, şikayet takipleri, hile ihtimallerini kontrol ve oyun yöneticilerine admin engelleme ekranlarını sağlar.

## 4. Şu an en çok ne eksik — hangi feature veya servis bir sonraki adım olmalı?
Projenin temel mimari dosyaları hazır duruyor, ancak mevcut yapıdaki net başlıca iki teknik eksik/bir sonraki adım şudur:
1. **API Gateway (Ocelot veya YARP):** İstemci olan Frontends bileşenlerinin `001_MicroServices` katmanındaki bunca farklı porta ayrı ayrı bağlanması güvenlik ve yük yönetimi açısından zor olacağı için, araya bir API Gateway entegre edilmelidir.
2. **Saga Pattern / Orchestrator:** Dağıtılmış işlemler için (Örneğin bir suç/Action işlendikten sonra ödül olarak Envantere eşya ve Ekonomiye para dağıtılacaktır, eğer biri fail olursa Rollback atılmalıdır) RabbitMQ üzerine oturtulmuş MassTransit gibi sağlam bir Saga state makinası senaryoları tasarlanmalıdır. Sistemdeki eksikliğini bu tür karmaşık işlemler gösterebilir.

## 5. Database yapısı — her servisin kendi DB'si mi var (true microservice), yoksa shared mı?
**True Microservice** yapısı izlenmiştir. Ortak bir (Shared) monolit DB modeli **yoktur.**
Uygulama kodları ve `appsettings.json` ayarları incelendiğinde her microservisin **tamamen kendi ConnectionString'ine sahip olduğu** ve bağımsız olarak kendi isminde db'ye çıktığı görülmektedir (`CrimeAndWin_IdentityDB`, `CrimeAndWin_ActionDB`, `CrimeAndWin_EconomyDB` vb.). Bu da gevşek bağımlı (loosely coupled) bir veri mimarisine sahip olunduğunu gösterir.
