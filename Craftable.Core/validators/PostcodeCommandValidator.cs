using Craftable.Core.commands;
using FluentValidation;

namespace Craftable.Core.validators
{
    public class PostcodeCommandValidator : AbstractValidator<PostcodeCommand>
    {
        public PostcodeCommandValidator()
        {
            RuleFor(address => address.Postcode).NotNull().NotEmpty();
            RuleFor(address => address).NotNull();
        }
    }
}