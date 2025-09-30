using FinanceTracker.Application.DTOs;
using FinanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {


        private readonly ITransactionService _transactionService;
        private readonly FluentValidation.IValidator<TransactionDto> _validator;

        public TransactionsController(ITransactionService transactionService, FluentValidation.IValidator<TransactionDto> validator)
        {
            _transactionService = transactionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionDto dto)
        {
            var result = _validator.Validate(dto);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            await _transactionService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TransactionDto dto)
        {
            dto.Id = id;
            var result = _validator.Validate(dto);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            await _transactionService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _transactionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
