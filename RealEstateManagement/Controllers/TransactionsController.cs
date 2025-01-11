using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Use_Cases.Transactions.Queries;
using Domain.Common;
using Application.Utils;
using Application.Use_Cases.Transactions.Commands;
using Application.Use_Cases.Commands;
using AutoMapper;
using RealEstateManagement.Application.Transactions.Commands;
using System.Security.Claims;

namespace RealEstateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IPropertyListingRepository propertyListingRepository;
        private readonly IMapper mapper;

        public TransactionsController(IMediator mediator, ITransactionRepository transactionRepository, IPropertyListingRepository propertyListingRepository, IMapper mapper)
        {
            this.mediator = mediator;
            this.propertyListingRepository = propertyListingRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddTransaction([FromBody] CreateTransactionCommand command)
        {
            if (!IsUserAuthorized(command.BuyerId))
            {
                return Forbid();
            }

            var propertyListing = await propertyListingRepository.GetListingByIdAsync(command.PropertyId);
            if (!propertyListing.IsSuccess)
            {
                return BadRequest(propertyListing.ErrorMessage);
            }

            if(propertyListing.Data.UserID != command.SellerId)
            {
                return BadRequest("Property does not belong to seller");
            }

            var updatePropertyListingCommand = mapper.Map<UpdatePropertyListingCommand>(propertyListing.Data);
            updatePropertyListingCommand.Status = "sold";
            await mediator.Send(updatePropertyListingCommand);

            var transactionId = await mediator.Send(command);
            return CreatedAtAction(nameof(AddTransaction), new { id = transactionId }, transactionId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, UpdateTransactionCommand command)
        {
            if (id != command.TransactionId)
            {
                return BadRequest("The provided id doesn't match the TransactionId");
            }
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var result = await mediator.Send(new DeleteTransactionCommand { TransactionId= id });
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<TransactionDto>>>> GetAllTransactions()
        {
            var result = await mediator.Send(new GetAllTransactionsQuery());
            if (result.IsSuccess)
            {
                if (result.Data == null)
                {
                    return NotFound("No transactions found");
                }
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
                if (result.Data == null)
                {
                    return NotFound($"Transaction with ID {id} not found");
                }
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
                if (result.Data == null)
                {
                    return NotFound($"Transaction with Property ID {id} not found");
                }
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
                if (result.Data == null)
                {
                    return NotFound($"Transaction with Buyer ID {id} not found");
                }
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
                if (result.Data == null)
                {
                    return NotFound($"Transaction with Seller ID {id} not found");
                }
                return Ok(result.Data);
            }
            else
            {
                return NotFound();
            }
        }

        private bool IsUserAuthorized(Guid userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return currentUserId != null && currentUserId == userId.ToString();
        }
    }
}