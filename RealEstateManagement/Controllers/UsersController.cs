﻿using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Users.Commands;
using Application.Use_Cases.Users.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RealEstateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Result<Unit>>> UpdateUser(Guid id, UpdateUserCommand command)
        {
            if (!IsUserAuthorized(command.UserId))
            {
                return Forbid();
            }
            if (id != command.UserId)
            {
                return BadRequest();
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

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (!IsUserAuthorized(id))
            {
                return Forbid();
            }
            var result = await mediator.Send(new DeleteUserCommand { UserId = id });
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
        public async Task<ActionResult<Result<List<TransactionDto>>>> GetAllUsers()
        {
            var result = await mediator.Send(new GetAllUsersQuery());
            if (result.IsSuccess)
            {
                if (result.Data == null) return NotFound("No users found");
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var result = await mediator.Send(new GetUserByIdQuery { UserId = id });
            if (result.IsSuccess)
            {
                if (result.Data == null) return NotFound($"User with ID : {id} not found");
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        private bool IsUserAuthorized(Guid userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return currentUserId != null && currentUserId == userId.ToString();
        }
    }
}
