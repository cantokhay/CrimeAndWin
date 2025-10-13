using Economy.Application.DTOs.WalletDTOs;
using Economy.Application.Features.Seed;
using Economy.Application.Features.Wallet.Commands.CreateWallet;
using Economy.Application.Features.Wallet.Commands.DepositMoney;
using Economy.Application.Features.Wallet.Commands.WithdrawMoney;
using Economy.Application.Features.Wallet.Queries;
using Economy.Application.Features.Wallet.Queries.GetAllWallets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Economy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetWallet(Guid playerId)
        {
            var result = await _mediator.Send(new GetWalletByPlayerIdQuery { PlayerId = playerId });
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("{playerId}/deposit")]
        public async Task<IActionResult> Deposit(Guid playerId, [FromBody] DepositMoneyCommand? command)
        {
            if (command is null) return BadRequest("Request body is required.");
            command.PlayerId = playerId;
            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest("Deposit failed.");
        }

        [HttpPost("{playerId}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid playerId, [FromBody] WithdrawMoneyCommand? command)
        {
            if (command is null) return BadRequest("Request body is required.");
            command.PlayerId = playerId;
            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest("Withdraw failed.");
        }

        [HttpPost("{playerId}/createWallet")]
        public async Task<IActionResult> CreateWallet(Guid playerId)
        {
            var command = new CreateWalletCommand { PlayerId = playerId };
            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest("Wallet creation failed.");
        }

        /// <summary>
        /// Tüm Wallet kayıtlarını ve Transactions detaylarını listeler.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ResultWalletDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllWalletsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Rastgele Wallet ve Transaction verileri oluşturur.
        /// </summary>
        [HttpPost("SeedRun")]
        public async Task<IActionResult> SeedRun([FromQuery] int count = 10)
        {
            await _mediator.Send(new RunEconomySeedCommand(count));
            return Ok(new { message = $"{count} adet Wallet ve Transaction başarıyla seed edildi." });
        }
    }
}
