namespace NexusTix.Domain.Entities.Common
{
    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; } = default!;
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
