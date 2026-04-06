using System;

namespace Shared.Domain.Constants
{
    /// <summary>
    /// Tüm microservice'lerde ortaklaşa kullanılan, deterministik ve değişmez seed veri sabitleri.
    /// GUID formatı: {category_prefix}-0000-0000-{category_prefix}-{sequence}
    /// Tüm karakterler geçerli hex (0-9, a-f) aralığındadır.
    /// </summary>
    public static class SeedDataConstants
    {
        // ---------------------------------------------------------
        // SEED DATE — Migration'larda değişmez, deterministik tarih.
        // DateTime.UtcNow KULLANILMAZ; her migration'da farklı değer üretir.
        // ---------------------------------------------------------
        public static readonly DateTime SeedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // ---------------------------------------------------------
        // ROLES
        // Prefix: 10000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid AdminRoleId     = Guid.Parse("10000000-0000-0000-0000-000000000001");
        public static readonly Guid PlayerRoleId    = Guid.Parse("10000000-0000-0000-0000-000000000002");
        public static readonly Guid ModeratorRoleId = Guid.Parse("10000000-0000-0000-0000-000000000003");

        // ---------------------------------------------------------
        // IDENTITY USERS (AppUser Table)
        // Prefix: 20000000-0000-0000-0000-00000000000x
        // AdminUserId: Gerçek üretim ID'si korunuyor
        // ---------------------------------------------------------
        public static readonly Guid AdminUserId = Guid.Parse("b279261d-72e7-4952-949e-4a476ad07a2a");
        public static readonly Guid UserAlphaId = Guid.Parse("20000000-0000-0000-0000-000000000001");
        public static readonly Guid UserBetaId  = Guid.Parse("20000000-0000-0000-0000-000000000002");

        // ---------------------------------------------------------
        // USER ROLES (UserRole junction table)
        // Prefix: 30000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid UserRoleAdminId  = Guid.Parse("30000000-0000-0000-0000-000000000001");
        public static readonly Guid UserRoleAlphaId  = Guid.Parse("30000000-0000-0000-0000-000000000002");
        public static readonly Guid UserRoleBetaId   = Guid.Parse("30000000-0000-0000-0000-000000000003");

        // ---------------------------------------------------------
        // PLAYERS (PlayerProfile Table — AppUserId üzerinden bağlı)
        // Prefix: 40000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid PlayerAlphaId = Guid.Parse("40000000-0000-0000-0000-000000000001");
        public static readonly Guid PlayerBetaId  = Guid.Parse("40000000-0000-0000-0000-000000000002");

        // ---------------------------------------------------------
        // GANGS
        // Prefix: 50000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid GangBloodlineId = Guid.Parse("50000000-0000-0000-0000-000000000001");
        public static readonly Guid GangSiliconId   = Guid.Parse("50000000-0000-0000-0000-000000000002");

        // ---------------------------------------------------------
        // WALLETS (Player Wallets — Economy Service)
        // Prefix: 60000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid WalletAlphaId = Guid.Parse("60000000-0000-0000-0000-000000000001");
        public static readonly Guid WalletBetaId  = Guid.Parse("60000000-0000-0000-0000-000000000002");

        // ---------------------------------------------------------
        // GANG WALLETS (Gang Treasury — Economy Service)
        // Prefix: 70000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid GangWalletBloodlineId = Guid.Parse("70000000-0000-0000-0000-000000000001");
        public static readonly Guid GangWalletSiliconId   = Guid.Parse("70000000-0000-0000-0000-000000000002");

        // ---------------------------------------------------------
        // TRANSACTIONS (Economy Service)
        // Prefix: 80000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid TransactionAlpha1Id = Guid.Parse("80000000-0000-0000-0000-000000000001");
        public static readonly Guid TransactionAlpha2Id = Guid.Parse("80000000-0000-0000-0000-000000000002");
        public static readonly Guid TransactionBeta1Id  = Guid.Parse("80000000-0000-0000-0000-000000000003");

        // ---------------------------------------------------------
        // INVENTORIES (Inventory Service)
        // Prefix: 90000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid InventoryAlphaId = Guid.Parse("90000000-0000-0000-0000-000000000001");
        public static readonly Guid InventoryBetaId  = Guid.Parse("90000000-0000-0000-0000-000000000002");

        // ---------------------------------------------------------
        // ITEMS (Inventory Service — Oyunculara ait envanter kalemleri)
        // Prefix: a0000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid ItemDesertEagleId  = Guid.Parse("a0000000-0000-0000-0000-000000000001");
        public static readonly Guid ItemKevlarVestId   = Guid.Parse("a0000000-0000-0000-0000-000000000002");
        public static readonly Guid ItemAdrenalineId   = Guid.Parse("a0000000-0000-0000-0000-000000000003");

        // ---------------------------------------------------------
        // REGIONS (GameWorld Service)
        // Prefix: b0000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid RegionAlcatrazId = Guid.Parse("b0000000-0000-0000-0000-000000000001");
        public static readonly Guid RegionDowntownId = Guid.Parse("b0000000-0000-0000-0000-000000000002");

        // ---------------------------------------------------------
        // GAME WORLDS & SEASONS (GameWorld Service)
        // Prefix: c0000000-0000-0000-0000-00000000000x
        // ---------------------------------------------------------
        public static readonly Guid MainlandWorldId = Guid.Parse("c0000000-0000-0000-0000-000000000001");
        public static readonly Guid SeasonOneId      = Guid.Parse("c0000000-0000-0000-0000-000000000002");
    }
}

