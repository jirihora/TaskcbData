using FluentValidation;
using OrderAggregator.Model.Models;

namespace OrderAggregator.Model.Validations
{
    /// <summary>
    /// Validator for <see cref="Order"/> class.
    /// </summary>
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.ProductId).NotEmpty()
                .WithMessage("Product id must not be empty.");
            RuleFor(o => o.Quantity).GreaterThan(0)
                .WithMessage("Quantity must be greater then 0");
        }
    }
}
