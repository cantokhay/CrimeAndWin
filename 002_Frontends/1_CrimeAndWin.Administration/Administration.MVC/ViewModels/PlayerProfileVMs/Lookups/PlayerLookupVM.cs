namespace Administration.MVC.ViewModels.PlayerProfileVMs.Lookups
{
    public sealed class PlayerLookupVM
    {
        public Guid Id { get; set; }
        public string? DisplayName { get; set; }
        public Guid AppUserId { get; set; }
    }
}
