using FinanceTracker.Application.DTOs;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Interfaces;

namespace FinanceTracker.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Select(t => new TransactionDto
            {
                Id = t.Id, 
                Type = t.Type,
                Amount = t.Amount,
                Date = t.Date,
                Category = t.Category
            });
        }

        public async Task<TransactionDto?> GetByIdAsync(Guid id)
        {
            var t = await _transactionRepository.GetByIdAsync(id);
            if (t == null) return null;
            return new TransactionDto
            {
                Id = t.Id,
                Type = t.Type,
                Amount = t.Amount,
                Date = t.Date,
                Category = t.Category
            };
        }

        public async Task AddAsync(TransactionDto transactionDto)
        {
            var transaction = new Domain.Entities.Transaction
            {
                Id = transactionDto.Id,
                Type = transactionDto.Type,
                Amount = transactionDto.Amount,
                Date = transactionDto.Date,
                Category = transactionDto.Category
            };

            await _transactionRepository.AddAsync(transaction);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _transactionRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(TransactionDto transactionDto)
        {
            var transaction = new Domain.Entities.Transaction
            {
                Id = transactionDto.Id,
                Type = transactionDto.Type,
                Amount = transactionDto.Amount,
                Date = transactionDto.Date,
                Category = transactionDto.Category
            };
            await _transactionRepository.UpdateAsync(transaction);
        }
    }
}
