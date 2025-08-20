namespace Economy.Application.DTOs.WalletDTOs
{
    public class CreateWalletDTO
    {
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }
    }
}
