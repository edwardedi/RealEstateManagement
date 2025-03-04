﻿using Application.DTOs;
using Application.Use_Cases.ClientInquiries.Queries;
using Application.Use_Cases.ClientInquiries.QueryHandler;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace RealEstateManagement.Application.UnitTests
{
    public class GetAllInquiriesQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IClientInquiryRepository _repository;
        private readonly GetAllInquiriesQueryHandler _handler;

        public GetAllInquiriesQueryHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _repository = Substitute.For<IClientInquiryRepository>();
            _handler = new GetAllInquiriesQueryHandler(_mapper, _repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var inquiries = new List<ClientInquiry>
            {
                new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 100000, MaxPrice = 200000 },
                new ClientInquiry { InquiryId = Guid.NewGuid(), ClientId = Guid.NewGuid(), MinPrice = 200000, MaxPrice = 300000 }
            };
            var result = Result<IEnumerable<ClientInquiry>>.Success(inquiries);
            _repository.GetAllInquiriesAsync().Returns(result);

            var inquiryDtos = inquiries.Select(i => new ClientInquiryDto { InquiryId = i.InquiryId, ClientId = i.ClientId, MinPrice = i.MinPrice, MaxPrice = i.MaxPrice }).ToList();
            _mapper.Map<ClientInquiryDto>(Arg.Any<ClientInquiry>()).Returns(args => new ClientInquiryDto { InquiryId = ((ClientInquiry)args[0]).InquiryId, ClientId = ((ClientInquiry)args[0]).ClientId, MinPrice = ((ClientInquiry)args[0]).MinPrice, MaxPrice = ((ClientInquiry)args[0]).MaxPrice });

            var query = new GetAllInquiriesQuery();

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Should().BeEquivalentTo(inquiryDtos);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenRepositoryReturnsFailure()
        {
            // Arrange
            var errorMessage = "Error occurred";
            var result = Result<IEnumerable<ClientInquiry>>.Failure(errorMessage);
            _repository.GetAllInquiriesAsync().Returns(result);

            var query = new GetAllInquiriesQuery();

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}



