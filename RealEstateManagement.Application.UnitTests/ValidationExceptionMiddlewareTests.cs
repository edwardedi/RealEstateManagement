using Application;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Net;
using System.Text.Json;

namespace RealEstateManagement.Application.UnitTests
{
    public class ValidationExceptionMiddlewareTests
    {
        private readonly RequestDelegate _next;
        private readonly ValidationExceptionMiddleware _middleware;

        public ValidationExceptionMiddlewareTests()
        {
            _next = Substitute.For<RequestDelegate>();
            _middleware = new ValidationExceptionMiddleware(_next);
        }

        [Fact]
        public async Task InvokeAsync_Should_Call_Next_Delegate_When_No_Exception()
        {
            // Arrange
            var context = new DefaultHttpContext();

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            await _next.Received(1).Invoke(context);
        }

        [Fact]
        public async Task InvokeAsync_Should_Handle_ValidationException()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Property1", "Error1"),
                new ValidationFailure("Property2", "Error2")
            };
            var validationException = new ValidationException(validationFailures);

            _next.When(x => x.Invoke(context)).Do(x => { throw validationException; });

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            context.Response.ContentType.Should().Be("application/json");

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBodyStream).ReadToEndAsync();

            var expectedResponse = JsonSerializer.Serialize(new
            {
                errors = validationFailures.GroupBy(e => e.PropertyName)
                                           .ToDictionary(g => g.Key, g => g.First().ErrorMessage)
            });

            responseText.Should().Be(expectedResponse);
        }
    }
}
