using Craftable.Core.queries;
using FluentValidation;

namespace Craftable.Core.validators
{
    public class PostalCodeQueryValidator : AbstractValidator<PostalCodeQuery>
    {
        public PostalCodeQueryValidator()
        {
            RuleFor(postalCode => postalCode).NotNull();
            RuleFor(postalCode => postalCode.Code).NotEmpty().NotNull();
        }
    }
}