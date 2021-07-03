using Craftable.Core.validators;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Craftable.Core.extensions
{
    public static class ValidationResultConverter
    {
        public static Notificator ToNotificator(this ValidationResult validationResult)
        {
            var errorList = validationResult.Errors.Aggregate(new List<string>(), (errors, error) =>
              {
                  errors.Add($"{error.PropertyName} has the error: {error.ErrorMessage}");
                  return errors;
              });

            return new Notificator(validationResult.IsValid, errorList);
        }
    }
}