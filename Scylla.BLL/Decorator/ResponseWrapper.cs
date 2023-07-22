using System.ComponentModel.DataAnnotations;
using Scylla.BLL.Enums;

namespace Scylla.BLL.Decorator
{
    public class ResponseWrapper<TContent>
    {
        [Required]
        public ResponseStatusCodes StatusCodeEnum { get; set; }
            = ResponseStatusCodes.Success;

        public string? Description { get; set; } = "The operation was successful.";

        public TContent? Content { get; set; } = default(TContent?);


    }
}

