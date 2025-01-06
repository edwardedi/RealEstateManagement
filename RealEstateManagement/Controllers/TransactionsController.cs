using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.DTOs;
using Application.Use_Cases.Transactions.Queries;
using Domain.Common;
using Application.Utils;
using Application.Use_Cases.Transactions.Commands;
using Application.Use_Cases.Commands;
using AutoMapper;

namespace RealEstateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ITransactionRepository transactionRepository;
        private readonly IPropertyListingRepository propertyListingRepository;
        private readonly IMapper mapper;

        public TransactionsController(IMediator mediator, ITransactionRepository transactionRepository, IPropertyListingRepository propertyListingRepository, IMapper mapper)
        {
            this.mediator = mediator;
            this.transactionRepository = transactionRepository;
            this.propertyListingRepository = propertyListingRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddTransaction([FromBody] CreateTransactionCommand command)
        {
            var propertyListing = await propertyListingRepository.GetListingByIdAsync(command.PropertyId);
            if (!propertyListing.IsSuccess)
            {
                return BadRequest(propertyListing.ErrorMessage);
            }

            var updatePropertyListingCommand = mapper.Map<UpdatePropertyListingCommand>(propertyListing.Data);
            updatePropertyListingCommand.Status = "sold";
            await mediator.Send(updatePropertyListingCommand);

            var transactionId = await mediator.Send(command);
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
                await transactionRepository.UpdateTransactionAsync(transaction);
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
                await transactionRepository.DeleteTransactionAsync(id);
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
            var result = await mediator.Send(new GetAllTransactionsQuery());
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
            var result = await mediator.Send(new GetTransactionByIdQuery { TransactionId = id });
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
            var result = await mediator.Send(new GetTransactionByPropertyIdQuery { PropertyId = id });
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
            var result = await mediator.Send(new GetTransactionsByBuyerIdQuery { BuyerId = id, Page = page, PageSize = pageSize });
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
            var result = await mediator.Send(new GetTransactionsBySellerIdQuery { SellerId = id, Page = page, PageSize = pageSize });
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