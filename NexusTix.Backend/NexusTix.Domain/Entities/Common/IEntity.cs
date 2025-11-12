namespace NexusTix.Domain.Entities.Common
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        bool IsActive { get; set; }
    }
}
