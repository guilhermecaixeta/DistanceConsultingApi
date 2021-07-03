using Craftable.Core.extensions;
using Craftable.Core.validators;
using Craftable.SharedKernel;
using FluentValidation;
using System;

namespace Craftable.Core.valueObjects
{
    public abstract class ValueObjectValidator<T, V> : ValueObject
        where T : class
        where V : AbstractValidator<T>, new()
    {
        protected void Validate(T data)
        {
            var validator = ValidatorFactory<T, V>.Create();
            var notificator = validator.Validate(data).ToNotificator();
            if (!notificator.IsValid)
            {
                var message = string.Concat(" ", notificator.Errors);
                throw new Exception(message);
            }
        }
    }
}