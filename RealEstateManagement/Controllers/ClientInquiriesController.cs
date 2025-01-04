using Application.DTOs;
using Application.Use_Cases.ClientInquiries.Commands;
using Application.Use_Cases.ClientInquiries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientInquiriesController : ControllerBase
    {
        private readonly IMediator mediator;
        public ClientInquiriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInquiry(CreateClientInquiryCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetInquiryById(Guid id)
        {
            var result = await mediator.Send(new GetInquiryByIdQuery { InquiryId = id });
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<PropertyListingDto>>> GetAllInquiriesAsync()
        {
            var result = await mediator.Send(new GetAllInquiriesQuery());
            return Ok(result);
        }
        [HttpGet("client/{clientId:guid}")]
        public async Task<ActionResult<List<ClientInquiryDto>>> GetInquiriesByClientId(Guid clientId)
        {
            var result = await mediator.Send(new GetInquiryByClientIdQuery { ClientId = clientId });
            return Ok(result);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateInquiry(Guid id, UpdateClientInquiryCommand command)
        {
            if(id != command.InquiryId)
            {
                return BadRequest("Inquiry ID mismatch.");
            }
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteInquiry(Guid id)
        {
            var result = await mediator.Send(new DeleteClientInquiryCommand { InquiryId = id });
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
