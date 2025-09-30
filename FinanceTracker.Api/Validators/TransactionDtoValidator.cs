using FinanceTracker.Application.DTOs;
using FluentValidation;

namespace FinanceTracker.Api.Validators
{
    public class TransactionDtoValidator : AbstractValidator<TransactionDto>
    {
        public TransactionDtoValidator()
        {
            RuleFor(t => t.Type)
               .NotEmpty().WithMessage("Transaction type is required.");
            RuleFor(t => t.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(t => t.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(5).WithMessage("Category cannot exceed 100 characters.");
            RuleFor(t => t.Date)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");
        }
    }
}
