using Economy.Application.Features.Transactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Economy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet("{playerId}/transactions")]
        //public async Task<IActionResult> GetTransactions(Guid playerId)
        //{
        //    var result = await _mediator.Send(new GetTransactionsQuery { PlayerId = playerId });
        //    return Ok(result);
        //}

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetByWalletByPlayerId(Guid playerId)
        {
            var result = await _mediator.Send(new GetTransactionsByPlayerIdQuery { PlayerId = playerId });
            return Ok(result);
        }
    }
}
