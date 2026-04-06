# CrimeAndWin: Oyun Mekanikleri ve Pazar Analizi

Bu doküman, CrimeAndWin projesinin derinleştirilmesi hedeflenen oyun mekaniklerini, pazar analizlerini ve teknik uygulama önerilerini içermektedir.

---

## 1. Pazar Analizi: Suç ve İş Tycoon Mobil Oyunları

Piyasadaki başarılı örneklerin (Mafia City, Idle Mafia, Grand Mafia, Adventure Capitalist) incelenmesi sonucunda "Hibrid Suç-Ekonomi" modelinin oyuncu tutma (retention) oranlarını en çok artıran yapı olduğu görülmüştür.

### A. Mafia City & Grand Mafia Modeli (Aktif Savaş / 4X)
*   **Odak:** Bölge kontrolü, ordu kurma ve aktif PvP.
*   **Enerji:** "Street Force" (sokak çeteleri) temizleme gibi günlük görevler için kullanılır.
*   **Gelir:** Hem pasif binalardan hem de aktif yağmalarla gelir elde edilir.
*   **Öğreti:** Oyuncuyu sürekli bir "tehdit" ve "büyüme" döngüsünde tutar.

### B. Idle Mafia & Tycoon Modeli (Pasif Yönetim / Menajerlik)
*   **Odak:** İşletmeleri otomatikleştirme, "Capo" (yönetici) toplama.
*   **Enerji:** Genellikle savaş etaplarında kullanılır, ticaret kısımları "zaman" (time-gated) ile yönetilir.
*   **Gelir:** Tamamen pasif, zamanla artan bir grafiğe sahiptir.
*   **Öğreti:** Oyuncuya "hiçbir şey yapmasa bile kazanıyor olma" hissini (idling) verir.

---

## 2. Genişletilmiş Oyun Mekanikleri Önerisi

Mevcut CrimeAndWin altyapısını baz alarak, "Enerjiye Dayalı Suç" ve "Zamana Dayalı Ticaret" ayrımını şu şekilde kurgulayabiliriz:

### A. Suç Mekanikleri (Aktif & Riskli)
Suç aksiyonları, oyuncunun "Enerji" kaynağını tüketerek hızlı ve yüksek kazanç sağladığı kanaldır.

| Parametre | Detay |
| :--- | :--- |
| **Girdi** | Enerji + Ekipman (Inventory) |
| **Başarı Oranı** | `%5` ile `%95` arası (Oyuncu Gücü vs. Zorluk Seviyesi) |
| **Ödül** | Yüksek Miktarda Para + Nadir Eşyalar (Item Drop) |
| **Risk** | Başarısızlık durumunda "Health" kaybı veya "Hapis" (Cooldown) |
| **Örnekler** | Kuyumcu Soygunu, Uyuşturucu Sevkiyatı, Rakip Mekan Yağması |

### B. Ticaret Mekanikleri (Pasif & Güvenli)
Ticaret aksiyonları, üretim odaklıdır ve enerji harcamaz. Daha düşük ama stabil bir kazanç sağlar.

| Parametre | Detay |
| :--- | :--- |
| **Girdi** | Başlangıç Sermayesi + Zaman (Cooldown/Production Time) |
| **Başarı Oranı** | `%100` (Her zaman üretir ve kazandırır) |
| **Ödül** | Düzenli Pasif Gelir + Üretim Materyalleri |
| **Sinerji** | Suçtan gelen "Kirli Parayı" aklamak (Money Laundering) |
| **Örnekler** | Gece Kulübü İşletmek, Lojistik Şirketi, Silah Fabrikası |

---

## 3. Teknik Uygulama ve Akış

### Suç Akışı (Action Service)
1.  Oyuncu bir aksiyon seçer (Örn: Banka Soygunu).
2.  `ActionDefinition` içindeki `SuccessRate` ve `EnergyCost` kontrol edilir.
3.  `Action Service`:
    *   Enerjiyi düşer.
    *   Rastgele sayı (Dice roll) ile başarıyı belirler.
    *   Başarılıysa `Economy Service` ve `Inventory Service`'e sinyal gönderir.
    *   Başarısızsa "Heat" (aranma seviyesi) artırır.

### Ticaret Akışı (Economy/Production Service)
1.  Oyuncu bir tesis kurar (Örn: Kumarhane).
2.  Tesis her 1 saatte X birim temiz para üretir.
3.  Enerjiden bağımsızdır; oyuncunun oyuna girmesi ve "toplaması" (collect) gerekir.
4.  Geliştirme (Upgrade) sistemi ile üretim hızı ve kapasitesi artırılabilir.

---

## 4. Analitik ve Dengeleme (Balancing)

*   **Pity Mechanics:** Oyuncu üst üste 3 suçta başarısız olursa, bir sonraki suçun başarı oranı `%20` bonus kazanmalıdır.
*   **Double Economy:**
    *   **Kirli Para (Black Money):** Sadece suçtan gelir, gelişmiş silahlar/ekipmanlar için kullanılır.
    *   **Temiz Para (Cash):** Ticaretten gelir, tesis upgradeleri ve resmi işlemler için kullanılır.
*   **Risk/Reward:** Suçun getirisi ticaretin saatlik getirisinin en az 5-10 katı olmalı ki oyuncu enerjisini harcamak için motive olsun.

---

## 5. Öneri Özet Tablosu

| Özellik | Suç (Crime) | Ticaret (Trade) |
| :--- | :--- | :--- |
| **Ana Kaynak** | Enerji | Zaman |
| **Risk** | Yüksek (Fail ihtimali) | Sıfır (Garanti kazanç) |
| **Kazanç Hızı** | Anlık (Burst) | Zamana Yayılı (Passive) |
| **Motivasyon** | Heyecan ve Adrenalin | Stratejik Büyüme ve Stabilite |
| **Gereksinim** | Silah, Güç, Seviye | Sermaye, Hammadde, Tesis |

---

## 6. Teknik Yol Haritası ve Mimari Hizalama

Mevcut .NET 8 mikroservis mimariniz (Mediator, Mapperly, Saga, FluentValidation) bu yeni özellikleri barındırmak için oldukça uygundur.

### A. Domain Değişiklikleri (Action.Domain)
*   **ActionDefinition:** `ActionType` enum'u eklenerek Suç ve Ticaret ayrımı yapılmalıdır.
*   **SuccessRate:** `ActionRequirements` veya doğrudan `ActionDefinition` içine `%0-100` bazlı başarı şansı eklenmelidir.
*   **Loot:** `ActionRewards` VO'su genişletilerek `Inventory Service` ile konuşacak bir `ItemId` listesi desteklemelidir.

### B. Yeni Servis Önerisi: Production/Tycoon Service
Ticaret (Trade) aksiyonlarını `Action Service` içinde tutmak yerine, pasif üretim odaklı yeni bir mikroservis kurgulanabilir. Bu servis:
*   Oyuncunun sahip olduğu "Front" işletmeleri takip eder.
*   Zamana dayalı üretim (Production) hesaplar.
*   `Economy Service` ile koordineli olarak üretilen parayı "Kasa"ya ekler.

### C. Saga Akışları (Action.Saga)
Karmaşık aksiyonlar (Örn: Banka Soygunu) bir Saga ile yönetilmelidir:
1.  `EnergyConsumed` event'i (Action Service).
2.  `DiceRolled` (Success Check).
3.  `MoneyAwarded` (Economy Service).
4.  `ItemDropped` (Inventory Service).
5.  `HeatIncreased` (PlayerProfile/Moderation Service).

### D. Admin Paneli Entegrasyonu (Golden Theme)
Metronic "Golden Theme" kullanılarak şunlar eklenmelidir:
*   **Success Rate Analyzer:** Hangi aksiyonların ne kadar kazandırdığını ve başarı oranlarını analiz eden grafikler.
*   **Action Configurator:** UI üzerinden yeni aksiyon tanımlama (Zorluk, Enerji, Ödül, Başarı Oranı).
*   **Loot Management:** Aksiyonlardan düşecek eşyaların drop rate (düşme oranı) yönetimi.

---

Bu yapı, CrimeAndWin'i basit bir tıklama oyunundan, derinliği olan bir "Suç İmparatorluğu Simülasyonu"na dönüştürecektir.
