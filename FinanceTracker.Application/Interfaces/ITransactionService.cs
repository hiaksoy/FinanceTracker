using FinanceTracker.Application.DTOs;

namespace FinanceTracker.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllAsync();
        Task<TransactionDto?> GetByIdAsync(Guid id);
        Task AddAsync(TransactionDto transactionDto);
        Task UpdateAsync(TransactionDto transactionDto);
        Task DeleteAsync(Guid id);
    }
}
