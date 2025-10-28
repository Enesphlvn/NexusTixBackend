namespace NexusTix.Application.Common.Rules
{
    public interface IPagingBusinessRules
    {
        void CheckIfPagingParametersAreValid(int pageNumber, int pageSize);
    }
}
