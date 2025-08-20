using AutoMapper;
using Economy.Application.DTOs.TransactionDTOs;
using Economy.Application.DTOs.WalletDTOs;
using Economy.Application.Features.Wallet.Commands;
using Economy.Domain.Entities;
using Economy.Domain.VOs;

namespace Economy.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // Wallet -> WalletDTO
            CreateMap<Wallet, CreateWalletDTO>().ReverseMap();
            CreateMap<Wallet, UpdateWalletDTO>().ReverseMap();
            CreateMap<Wallet, ResultWalletDTO>().ReverseMap();

            // Transaction -> TransactionDTO
            CreateMap<Transaction, CreateTransactionDTO>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Money.Amount))
                .ForMember(dest => dest.CurrencyType, opt => opt.MapFrom(src => src.Money.CurrencyType))
                .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => src.Reason.ReasonCode))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Reason.Description))
                .ReverseMap();

            // Transaction -> TransactionDTO
            CreateMap<Transaction, UpdateTransactionDTO>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Money.Amount))
                .ForMember(dest => dest.CurrencyType, opt => opt.MapFrom(src => src.Money.CurrencyType))
                .ForMember(dest => dest.ReasonCode, opt => opt.MapFrom(src => src.Reason.ReasonCode))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Reason.Description))
                .ReverseMap();

            // Transaction -> TransactionDTO
            CreateMap<Transaction, ResultTransactionDTO>()
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Money.Amount))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Money.CurrencyType.ToString()))
                .ForMember(d => d.ReasonCode, o => o.MapFrom(s => s.Reason.ReasonCode))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Reason.Description))
                .ReverseMap();

            // Commands -> Transaction (Deposit / Withdraw)
            CreateMap<DepositMoneyCommand, Transaction>()
                .ForMember(dest => dest.Money,
                    opt => opt.MapFrom(src => new Money(src.Amount, src.CurrencyType)))
                .ForMember(dest => dest.Reason,
                    opt => opt.MapFrom(src => new TransactionReason("DEPOSIT", src.Reason)))
                .ReverseMap();

            CreateMap<WithdrawMoneyCommand, Transaction>()
                .ForMember(dest => dest.Money,
                    opt => opt.MapFrom(src => new Money(src.Amount, src.CurrencyType)))
                .ForMember(dest => dest.Reason,
                    opt => opt.MapFrom(src => new TransactionReason("WITHDRAW", src.Reason)))
                .ReverseMap();
        }
    }
}
