namespace NexusTix.Application.Common.BaseRules
{
    public interface IBaseBusinessRules
    {
        void CheckIfPagingParametersAreValid(int pageNumber, int pageSize);
    }
}
