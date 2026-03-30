namespace Economy.Application.DTOs.WalletDTOs.Admin
{
    public sealed class AdminUpdateWalletDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }
    }

}
