using System.Collections.Generic;

namespace Craftable.Core.validators
{
    public class Notificator
    {
        public Notificator(bool isValid, IReadOnlyList<string> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }

        public bool IsValid { get; private set; }

        public IReadOnlyList<string> Errors { get; private set; }

        public override string ToString()
        {
            var message = string.Join(' ', Errors);
            return message;
        }
    }
}