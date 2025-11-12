using NexusTix.Domain.Entities;

namespace NexusTix.Application.Common.Security
{
    public interface ITokenService
    {
        (string token, DateTime expiration) GenerateToken(User user, IList<string> roles);
    }
}
