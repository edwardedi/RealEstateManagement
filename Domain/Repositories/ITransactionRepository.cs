using Domain.Common;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<Result<IEnumerable<Transaction>>> GetAllTransactionsAsync();
        Task<Result<Transaction>> GetTransactionByIdAsync(Guid id);
        Task<Result<Transaction>> GetTransactionByPropertyIdAsync(Guid id);
        Task<Result<IEnumerable<Transaction>>> GetTransactionsByBuyerId(Guid userId);
        Task<Result<IEnumerable<Transaction>>> GetTransactionsBySellerId(Guid userId);
        Task<Result<Guid>> AddTransactionAsync(Transaction transaction);
        Task<Result<Guid>> UpdateTransactionAsync(Transaction transaction);
        Task<Result<Guid>> DeleteTransactionAsync(Guid id);
    }
}
