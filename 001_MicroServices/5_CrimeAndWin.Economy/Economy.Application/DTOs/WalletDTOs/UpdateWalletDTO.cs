namespace Economy.Application.DTOs.WalletDTOs
{
    public class UpdateWalletDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }
    }
}
