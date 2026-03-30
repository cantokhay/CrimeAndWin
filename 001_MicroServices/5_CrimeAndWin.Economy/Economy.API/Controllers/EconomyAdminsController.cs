using Economy.Application.DTOs.TransactionDTOs.Admin;
using Economy.Application.DTOs.WalletDTOs.Admin;
using Economy.Application.Features.Transactions.Commands.AdminCreateTransaction;
using Economy.Application.Features.Transactions.Commands.AdminDeleteTransaction;
using Economy.Application.Features.Transactions.Commands.AdminUpdateTransaction;
using Economy.Application.Features.Transactions.Queries;
using Economy.Application.Features.Transactions.Queries.GetAllTransactionsAsAdmin;
using Economy.Application.Features.Wallet.Commands.AdminCreateWallet;
using Economy.Application.Features.Wallet.Commands.AdminDeleteWallet;
using Economy.Application.Features.Wallet.Commands.AdminUpdateWallet;
using Economy.Application.Features.Wallet.Queries.GetAllWalletsAsAdmin;
using Economy.Application.Features.Wallet.Queries.GetWalletByIdAsAdmin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Economy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EconomyAdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EconomyAdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ================================
        //           WALLET CRUD
        // ================================

        // GET: api/EconomyAdmins/GetAllWalletsAsAdmin
        [HttpGet("GetAllWalletsAsAdmin")]
        public async Task<IActionResult> GetAllWalletsAsAdmin()
        {
            var result = await _mediator.Send(new GetAllWalletsAsAdminQuery());
            return Ok(result);
        }

        // GET: api/EconomyAdmins/GetWalletByIdAsAdmin/{id}
        [HttpGet("GetWalletByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetWalletByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetWalletByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/EconomyAdmins/CreateWalletAsAdmin
        [HttpPost("CreateWalletAsAdmin")]
        public async Task<IActionResult> CreateWalletAsAdmin([FromBody] AdminCreateWalletDTO dto)
        {
            var command = new AdminCreateWalletCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // PUT: api/EconomyAdmins/UpdateWalletAsAdmin
        [HttpPut("UpdateWalletAsAdmin")]
        public async Task<IActionResult> UpdateWalletAsAdmin([FromBody] AdminUpdateWalletDTO dto)
        {
            var command = new AdminUpdateWalletCommand(dto);
            var ok = await _mediator.Send(command);
            return ok ? Ok("Wallet updated successfully.") : NotFound("Wallet not found.");
        }

        // DELETE: api/EconomyAdmins/DeleteWalletAsAdmin/{id}
        [HttpDelete("DeleteWalletAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteWalletAsAdmin(Guid id)
        {
            var command = new AdminDeleteWalletCommand(id);
            var ok = await _mediator.Send(command);
            return ok ? Ok("Wallet deleted successfully.") : NotFound("Wallet not found.");
        }

        // ================================
        //        TRANSACTION CRUD
        // ================================

        // GET: api/EconomyAdmins/GetAllTransactionsAsAdmin
        [HttpGet("GetAllTransactionsAsAdmin")]
        public async Task<IActionResult> GetAllTransactionsAsAdmin()
        {
            var result = await _mediator.Send(new GetAllTransactionsAsAdminQuery());
            return Ok(result);
        }

        // GET: api/EconomyAdmins/GetTransactionByIdAsAdmin/{id}
        [HttpGet("GetTransactionByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetTransactionByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetTransactionByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/EconomyAdmins/CreateTransactionAsAdmin
        [HttpPost("CreateTransactionAsAdmin")]
        public async Task<IActionResult> CreateTransactionAsAdmin([FromBody] AdminCreateTransactionDTO dto)
        {
            var command = new AdminCreateTransactionCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // PUT: api/EconomyAdmins/UpdateTransactionAsAdmin
        [HttpPut("UpdateTransactionAsAdmin")]
        public async Task<IActionResult> UpdateTransactionAsAdmin([FromBody] AdminUpdateTransactionDTO dto)
        {
            var command = new AdminUpdateTransactionCommand(dto);
            var ok = await _mediator.Send(command);
            return ok ? Ok("Transaction updated successfully.") : NotFound("Transaction not found.");
        }

        // DELETE: api/EconomyAdmins/DeleteTransactionAsAdmin/{id}
        [HttpDelete("DeleteTransactionAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteTransactionAsAdmin(Guid id)
        {
            var command = new AdminDeleteTransactionCommand(id);
            var ok = await _mediator.Send(command);
            return ok ? Ok("Transaction deleted successfully.") : NotFound("Transaction not found.");
        }
    }
}
