namespace Economy.Application.DTOs.WalletDTOs.Admin
{
    public sealed class AdminCreateWalletDTO
    {
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }
    }
}
