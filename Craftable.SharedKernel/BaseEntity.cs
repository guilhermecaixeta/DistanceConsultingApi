using System;

namespace Craftable.SharedKernel
{
    public abstract class BaseEntity

    {
        public Guid Id { get; private set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}