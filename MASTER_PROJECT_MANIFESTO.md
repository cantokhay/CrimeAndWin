# CrimeAndWin: Master Project Manifesto

Bu doküman, "CrimeAndWin" projesinin stratejik büyüme vizyonunu, teknik yol haritasını ve operasyonel standartlarını tanımlayan ana kaynaktır. Proje boyunca yapılacak tüm geliştirmeler bu manifesto ışığında yürütülecek ve her adım bu liste üzerinden takip edilecektir.

---

## 1. Vizyon ve Kapsam (Vision & Scope)

CrimeAndWin'in temel amacı; basit bir tıklama (clicker) oyunundan, derinlikli bir **"Suç İmparatorluğu Simülasyonu"**na dönüşmektir. Oyunun yeni vizyonu dört ana sütun üzerine kuruludur:
1.  **Ekonomik Risk Yönetimi:** "Kara Para" ve "Temiz Para" arasındaki denge.
2.  **Sistemik Tehdit:** Oyuncuyu sürekli baskı altında tutan "Isı" (Heat) ve "Baskın" (Raid) mekanikleri.
3.  **Bölgesel Hakimiyet:** Şehrin farklı bölgelerinde (Downtown, Industrial vb.) kurulan ticari ve suç odaklı hegemonyalar.
4.  **Sosyal ve Rekabetçi Ekosistem:** Çeteler, İhale Sistemi ve Rüşvet ağları. (Yeni Faz)

---

## 2. İlerleme Takip Listesi (Task Progress)

| ID | Görev Adı | İlgili Modül | Durum |
| :--- | :--- | :--- | :--- |
| **TSK-001** | **Cüzdan Modeli Güncelleme & Migration** | Economy Service | ✅ Tamamlandı |
| **TSK-002** | **Isı & Saygınlık Modeli Güncelleme** | PlayerProfile Service| ✅ Tamamlandı |
| **TSK-003** | **Aksiyon Tanımlarının Genişletilmesi** | Action Service | ✅ Tamamlandı |
| **TSK-004** | **Pasif Isı Düşürme Arka Plan Servisi** | Action/Profile | ✅ Tamamlandı |
| **TSK-005** | **Suç Aksiyonu Saga Entegrasyonu** | Action Saga | ✅ Tamamlandı |
| **TSK-006** | **Para Aklama Verimlilik Sagası** | Economy Saga | ✅ Tamamlandı |
| **TSK-007** | **Baskın Etkinliği & Ceza Sistemi** | Action/Profile Saga| ✅ Tamamlandı |
| **TSK-008** | **Bölge Sahipliği & Vergi Mantığı** | GameWorld/Leadership| ✅ Tamamlandı |
| **TSK-009** | **Golden Theme UI Metrik Entegrasyonu** | Admin Dashboard | ✅ Tamamlandı |
| **TSK-010** | **Çete/Kulüp Entity & Üyelik Sistemi** | PlayerProfile/Identity| ⏳ Beklemede |
| **TSK-011** | **Çete Deposu & Ortak Cüzdan** | Economy/Inventory | ⏳ Beklemede |
| **TSK-012** | **İhale Motoru: Gerçek Zamanlı Teklif Verme** | GameWorld/Auction | ⏳ Beklemede |
| **TSK-013** | **Gelişmiş Rüşvet Başarı Algoritması** | Action Service | ⏳ Beklemede |
| **TSK-014** | **Admin: Çete & İhale Yönetim Arayüzü** | Admin Dashboard | ⏳ Beklemede |

---

## 3. Uygulama Planı (Faz 4)

### Faz 4.1: Sosyal Yapı (Clubs/Gangs)
Oyuncuların bir araya gelip "Aile" kurmalarını sağlayacak olan `Gang` entity'lerinin inşası, çete liderlerine yetki atama ve ortak kaynak yönetimi (Shared Wallet).

### Faz 4.2: Rekabet (Auctions)
Sistem tarafından periyodik olarak açılan "Liman", "Havalimanı" veya "Büyük Kumarhane" ihaleleri. En yüksek nakit (Cash) veren oyuncunun o mülkü ve getirisini alması.

### Faz 4.3: Yolsuzluk (Bribing)
Isı seviyesine göre dinamik olarak değişen rüşvet maliyetleri. Rüşvet teklifinin polis tarafından reddedilme ihtimali (Bribe Fraud).

---

## 4. Standart Operasyon Prosedürleri (SOP)

**SOP-DEV-001: Kodlama ve Onay Süreci**
1.  Manifesto üzerindeki bir görev (Task) seçilir.
2.  İlgili kodlama değişikliği yapılır.
3.  Dokümantasyon güncellenir.
4.  Kullanıcıdan "Devam Et" onayı alınmadan bir sonraki göreve geçilmez.

---
*Doküman Son Güncelleme: 2026-04-06*
*Durum: Yayında. Faz 4 Başlatılıyor.*
