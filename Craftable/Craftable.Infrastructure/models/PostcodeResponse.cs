﻿using Refit;

namespace Craftable.Infrastructure.models
{
    public class PostcodeResponse<T>
    {
        [AliasAs("status")]
        public int Status { get; set; }

        [AliasAs("result")]
        public T Result { get; set; }
    }
}