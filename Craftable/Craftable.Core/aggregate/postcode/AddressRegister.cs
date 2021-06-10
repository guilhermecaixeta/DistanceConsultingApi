using Craftable.Core.extensions;
using Craftable.Core.validators;
using Craftable.Core.valueObjects;
using Craftable.SharedKernel;
using System;

namespace Craftable.Core.aggregate.postcode
{
    public class AddressRegister : BaseEntity
    {
        public AddressRegister()
        {
        }

        public AddressRegister(string postcode, string country, Coordinates coordinates, Distance distance)
        {
            Postcode = postcode;
            Country = country;
            Coordinates = coordinates;
            Distance = distance;
            IsValid();
            Date = DateTime.Now;
        }

        private void IsValid()
        {
            var validator = ValidatorFactory<AddressRegister, AddressRangedValidator>.Create();
            var result = validator.Validate(this).ToNotificator();
            if (!result.IsValid)
            {
                throw new Exception(result.ToString());
            }
        }

        public string Postcode { get; private set; }
        public string Country { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public Distance Distance { get; private set; }
        public DateTime Date { get; private set; }
    }
}