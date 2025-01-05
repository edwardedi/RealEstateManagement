using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.DTOs;
using Application.Use_Cases.Queries;
using Microsoft.AspNetCore.Components.Forms;
using Application.Use_Cases.Transactions.Queries;
using Domain.Common;
using Application.Utils;
using Application.Use_Cases.Transactions.Commands;

namespace RealEstateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(IMediator mediator, ITransactionRepository transactionRepository)
        {
            _mediator = mediator;
            _transactionRepository = transactionRepository;
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> AddTransaction([FromBody] CreateTransactionCommand command)
        {
            var transactionId = await _mediator.Send(command);

            return CreatedAtAction(nameof(AddTransaction), new { id = transactionId }, transactionId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            try
            {
                await _transactionRepository.UpdateTransactionAsync(transaction);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            try
            {
                await _transactionRepository.DeleteTransactionAsync(id);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<TransactionDto>>>> GetAllTransactions()
        {
            var result = await _mediator.Send(new GetAllTransactionsQuery());
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransactionById(Guid id)
        {
            var result = await _mediator.Send(new GetTransactionByIdQuery { TransactionId = id });
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("property/{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransactionByPropertyId(Guid id)
        {
            var result = await _mediator.Send(new GetTransactionByPropertyIdQuery { PropertyId = id });
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("buyer/{id}")]
        public async Task<ActionResult<Result<PagedResult<TransactionDto>>>> GetTransactionsByBuyerId(Guid id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetTransactionsByBuyerIdQuery { BuyerId = id, Page = page, PageSize = pageSize });
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("seller/{id}")]
        public async Task<ActionResult<Result<PagedResult<TransactionDto>>>> GetTransactionsBySellerId(Guid id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetTransactionsBySellerIdQuery { SellerId = id, Page = page, PageSize = pageSize });
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound();
            }


        }
    }
}