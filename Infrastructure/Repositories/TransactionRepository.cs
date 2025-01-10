using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext context;

        public TransactionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<IEnumerable<Transaction>>> GetAllTransactionsAsync() 
        {
            try
            {
                var transactions = await context.Transactions.ToListAsync();
                return Result<IEnumerable<Transaction>>.Success(transactions);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Transaction>>.Failure($"An error occurred while retrieving transactions: {ex.Message}");
            }
        }
        public async Task<Result<Transaction>> GetTransactionByIdAsync(Guid id)
        {
            try
            {
                var transaction = await context.Transactions.FindAsync(id);
                return Result<Transaction>.Success(transaction);
            }
            catch (Exception ex)
            {
                return Result<Transaction>.Failure($"An error occurred while retrieving transaction: {ex.Message}");
            }
        }


        public async Task<Result<Guid>> AddTransactionAsync(Transaction transaction)
        {
            try
            {
                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(transaction.TransactionId);
            }
            catch (Exception ex) 
            {
                return Result<Guid>.Failure($"An error occurred while creating transaction: {ex.Message}");
            }
        }

        public async Task<Result<Guid>> UpdateTransactionAsync(Transaction transaction)
        {
            try
            {
                var existingTransaction = await context.Transactions.FindAsync(transaction.TransactionId);
                if (existingTransaction != null)
                {
                    context.Entry(existingTransaction).CurrentValues.SetValues(transaction);
                    await context.SaveChangesAsync();
                    return Result<Guid>.Success(transaction.TransactionId);
                }
                else
                {
                    return Result<Guid>.Failure("Transaction not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure($"An error occurred while updating transaction: {ex.Message}");
            }
        }

        public async Task<Result<Guid>> DeleteTransactionAsync(Guid id)
        {
            try
            {
                var transaction = await context.Transactions.FindAsync(id);
                if (transaction != null)
                {
                    context.Transactions.Remove(transaction);
                    await context.SaveChangesAsync();
                    return Result<Guid>.Success(id);
                }
                else
                {
                    return Result<Guid>.Failure("Transaction not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure($"An error occurred while deleting transaction: {ex.Message}");
            }
        }

        public async Task<Result<Transaction>> GetTransactionByPropertyIdAsync(Guid id)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.PropertyId == id);
                if (transaction != null)
                {
                    return Result<Transaction>.Success(transaction);
                }
                return Result<Transaction>.Failure("Transaction not found.");
            }
            catch (Exception ex)
            {
                return Result<Transaction>.Failure($"An error occurred while retrieving the transaction: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<Transaction>>> GetTransactionsByBuyerId(Guid userId)
        {
            try
            {
                var transactions = await context.Transactions.Where(t => t.BuyerId == userId).ToListAsync();
                return Result<IEnumerable<Transaction>>.Success(transactions);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Transaction>>.Failure($"An error occurred while retrieving transactions: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<Transaction>>> GetTransactionsBySellerId(Guid userId)
        {
            try
            {
                var transactions = await context.Transactions.Where(t => t.SellerId == userId).ToListAsync();
                return Result<IEnumerable<Transaction>>.Success(transactions);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Transaction>>.Failure($"An error occurred while retrieving transactions: {ex.Message}");
            }
        }
    }
}
