using Refit;

namespace Craftable.Infrastructure.client.postcode.models
{
    public class PostcodeResponse<T>
    {
        [AliasAs("status")]
        public int Status { get; set; }

        [AliasAs("result")]
        public T Result { get; set; }
    }
}