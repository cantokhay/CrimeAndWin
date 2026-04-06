namespace Administration.MVC.ViewModels.AuctionVMs
{
    public class AdminAuctionListVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal BasePrice { get; set; }
        public decimal CurrentBid { get; set; }
        public string HighestBidderName { get; set; }
        public DateTime EndsAt { get; set; }
        public bool IsFinished { get; set; }
        public string StatusLabel => IsFinished ? "Tamamlandi" : (DateTime.UtcNow > EndsAt ? "Bitis Bekliyor" : "Aktif");
    }
}
