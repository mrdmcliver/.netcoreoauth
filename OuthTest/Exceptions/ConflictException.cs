using OAuthTest.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace OAuthTest.Exceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string message) : base(message)
        {
        }

        public override IActionResult GetResponse()
        {
            return new ConflictObjectResult(new BaseResponseDto(Message));
        }
    }
}
