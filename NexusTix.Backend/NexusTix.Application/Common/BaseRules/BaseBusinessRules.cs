using NexusTix.Domain.Exceptions;
using System.Net;

namespace NexusTix.Application.Common.BaseRules
{
    public class BaseBusinessRules : IBaseBusinessRules
    {
        public void CheckIfPagingParametersAreValid(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new BusinessException("Sayfa numarası veya boyutu sıfırdan büyük olmalıdır.", HttpStatusCode.BadRequest);
            }
        }
    }
}
