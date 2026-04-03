using Economy.Application.DTOs.TransactionDTOs;
using Economy.Application.DTOs.TransactionDTOs.Admin;
using Economy.Application.DTOs.WalletDTOs;
using Economy.Application.DTOs.WalletDTOs.Admin;
using Economy.Application.Features.Wallet.Commands.DepositMoney;
using Economy.Application.Features.Wallet.Commands.WithdrawMoney;
using Economy.Domain.Entities;
using Economy.Domain.VOs;
using Riok.Mapperly.Abstractions;

namespace Economy.Application.Mapping;

[Mapper]
public partial class EconomyMapper
{
    // Wallet
    public partial CreateWalletDTO ToCreateDto(Wallet entity);
    public partial Wallet ToEntity(CreateWalletDTO dto);

    public partial UpdateWalletDTO ToUpdateDto(Wallet entity);
    public partial Wallet ToEntity(UpdateWalletDTO dto);

    public partial ResultWalletDTO ToResultDto(Wallet entity);
    public partial Wallet ToEntity(ResultWalletDTO dto);

    // Transaction
    [MapProperty(nameof(Transaction.Money.Amount), nameof(CreateTransactionDTO.Amount))]
    [MapProperty(nameof(Transaction.Money.CurrencyType), nameof(CreateTransactionDTO.CurrencyType))]
    [MapProperty(nameof(Transaction.Reason.ReasonCode), nameof(CreateTransactionDTO.ReasonCode))]
    [MapProperty(nameof(Transaction.Reason.Description), nameof(CreateTransactionDTO.Description))]
    public partial CreateTransactionDTO ToCreateDto(Transaction entity);

    [MapProperty(nameof(CreateTransactionDTO.Amount), "Money.Amount")]
    [MapProperty(nameof(CreateTransactionDTO.CurrencyType), "Money.CurrencyType")]
    [MapProperty(nameof(CreateTransactionDTO.ReasonCode), "Reason.ReasonCode")]
    [MapProperty(nameof(CreateTransactionDTO.Description), "Reason.Description")]
    public partial Transaction ToEntity(CreateTransactionDTO dto);

    [MapProperty(nameof(Transaction.Money.Amount), nameof(UpdateTransactionDTO.Amount))]
    [MapProperty(nameof(Transaction.Money.CurrencyType), nameof(UpdateTransactionDTO.CurrencyType))]
    [MapProperty(nameof(Transaction.Reason.ReasonCode), nameof(UpdateTransactionDTO.ReasonCode))]
    [MapProperty(nameof(Transaction.Reason.Description), nameof(UpdateTransactionDTO.Description))]
    public partial UpdateTransactionDTO ToUpdateDto(Transaction entity);

    [MapProperty(nameof(UpdateTransactionDTO.Amount), "Money.Amount")]
    [MapProperty(nameof(UpdateTransactionDTO.CurrencyType), "Money.CurrencyType")]
    [MapProperty(nameof(UpdateTransactionDTO.ReasonCode), "Reason.ReasonCode")]
    [MapProperty(nameof(UpdateTransactionDTO.Description), "Reason.Description")]
    public partial Transaction ToEntity(UpdateTransactionDTO dto);

    [MapProperty(nameof(Transaction.Money.Amount), nameof(ResultTransactionDTO.Amount))]
    [MapProperty(nameof(Transaction.Money.CurrencyType), nameof(ResultTransactionDTO.Type))]
    [MapProperty(nameof(Transaction.Reason.ReasonCode), nameof(ResultTransactionDTO.ReasonCode))]
    [MapProperty(nameof(Transaction.Reason.Description), nameof(ResultTransactionDTO.Description))]
    public partial ResultTransactionDTO ToResultDto(Transaction entity);
    
    [MapProperty(nameof(ResultTransactionDTO.Amount), "Money.Amount")]
    [MapProperty(nameof(ResultTransactionDTO.Type), "Money.CurrencyType")]
    [MapProperty(nameof(ResultTransactionDTO.ReasonCode), "Reason.ReasonCode")]
    [MapProperty(nameof(ResultTransactionDTO.Description), "Reason.Description")]
    public partial Transaction ToEntity(ResultTransactionDTO dto);

    // Commands -> Transaction
    [MapProperty(nameof(DepositMoneyCommand.Amount), "Money.Amount")]
    [MapProperty(nameof(DepositMoneyCommand.CurrencyType), "Money.CurrencyType")]
    [MapProperty(nameof(DepositMoneyCommand.Reason), "Reason.Description")]
    public partial Transaction ToEntity(DepositMoneyCommand dto);

    [MapProperty(nameof(WithdrawMoneyCommand.Amount), "Money.Amount")]
    [MapProperty(nameof(WithdrawMoneyCommand.CurrencyType), "Money.CurrencyType")]
    [MapProperty(nameof(WithdrawMoneyCommand.Reason), "Reason.Description")]
    public partial Transaction ToEntity(WithdrawMoneyCommand dto);

    // ==========================
    //      ADMIN MAPPINGS
    // ==========================

    public partial AdminResultWalletDTO ToAdminResultDto(Wallet entity);
    public partial Wallet ToEntity(AdminResultWalletDTO dto);

    public partial AdminCreateWalletDTO ToAdminCreateDto(Wallet entity);
    public partial Wallet ToEntity(AdminCreateWalletDTO dto);

    public partial AdminUpdateWalletDTO ToAdminUpdateDto(Wallet entity);
    public partial Wallet ToEntity(AdminUpdateWalletDTO dto);

    [MapProperty(nameof(Transaction.Money.Amount), nameof(AdminResultTransactionDTO.Amount))]
    [MapProperty(nameof(Transaction.Money.CurrencyType), nameof(AdminResultTransactionDTO.CurrencyType))]
    [MapProperty(nameof(Transaction.Reason.ReasonCode), nameof(AdminResultTransactionDTO.ReasonCode))]
    [MapProperty(nameof(Transaction.Reason.Description), nameof(AdminResultTransactionDTO.Description))]
    public partial AdminResultTransactionDTO ToAdminResultDto(Transaction entity);

    [MapProperty(nameof(AdminResultTransactionDTO.Amount), "Money.Amount")]
    [MapProperty(nameof(AdminResultTransactionDTO.CurrencyType), "Money.CurrencyType")]
    [MapProperty(nameof(AdminResultTransactionDTO.ReasonCode), "Reason.ReasonCode")]
    [MapProperty(nameof(AdminResultTransactionDTO.Description), "Reason.Description")]
    public partial Transaction ToEntity(AdminResultTransactionDTO dto);

    [MapProperty(nameof(AdminCreateTransactionDTO.Amount), "Money.Amount")]
    [MapProperty(nameof(AdminCreateTransactionDTO.CurrencyType), "Money.CurrencyType")]
    [MapProperty(nameof(AdminCreateTransactionDTO.ReasonCode), "Reason.ReasonCode")]
    [MapProperty(nameof(AdminCreateTransactionDTO.Description), "Reason.Description")]
    public partial Transaction ToEntity(AdminCreateTransactionDTO dto);

    [MapProperty(nameof(AdminUpdateTransactionDTO.Amount), "Money.Amount")]
    [MapProperty(nameof(AdminUpdateTransactionDTO.CurrencyType), "Money.CurrencyType")]
    [MapProperty(nameof(AdminUpdateTransactionDTO.ReasonCode), "Reason.ReasonCode")]
    [MapProperty(nameof(AdminUpdateTransactionDTO.Description), "Reason.Description")]
    public partial Transaction ToEntity(AdminUpdateTransactionDTO dto);

    // List Mappings
    public partial IEnumerable<ResultWalletDTO> ToResultDtoList(IEnumerable<Wallet> entities);
    public partial IEnumerable<ResultTransactionDTO> ToResultDtoList(IEnumerable<Transaction> entities);
    public partial IEnumerable<AdminResultWalletDTO> ToAdminResultDtoList(IEnumerable<Wallet> entities);
    public partial IEnumerable<AdminResultTransactionDTO> ToAdminResultDtoList(IEnumerable<Transaction> entities);
}


