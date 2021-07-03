using FluentValidation;

namespace Craftable.Core.validators
{
    public static class ValidatorFactory<TObject, TValidator>
        where TObject : class
        where TValidator : AbstractValidator<TObject>, new()
    {
        public static TValidator Create() => new TValidator();
    }
}