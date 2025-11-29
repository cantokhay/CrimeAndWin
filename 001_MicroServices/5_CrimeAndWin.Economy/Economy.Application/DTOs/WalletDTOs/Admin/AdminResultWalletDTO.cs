namespace Economy.Application.DTOs.WalletDTOs.Admin
{
    public sealed class AdminResultWalletDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
