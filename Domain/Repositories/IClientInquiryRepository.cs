using Domain.Common;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IClientInquiryRepository
    {
        Task<Result<IEnumerable<ClientInquiry>>> GetAllInquiriesAsync();
        Task<Result<ClientInquiry>> GetInquiryByIdAsync(Guid id);
        Task<Result<IEnumerable<ClientInquiry>>> GetInquiriesByClientId(Guid clientId);
        Task<Result<Guid>> AddInquiryAsync(ClientInquiry inquiry);
        Task<Result<Guid>> UpdateInquiryAsync(ClientInquiry inquiry);
        Task<Result<Guid>> DeleteInquiryAsync(Guid id);
    }
}
