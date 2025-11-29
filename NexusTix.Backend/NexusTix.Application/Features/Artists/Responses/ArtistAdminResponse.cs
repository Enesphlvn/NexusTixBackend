namespace NexusTix.Application.Features.Artists.Responses
{
    public class ArtistAdminResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Bio { get; init; }
        public string? ImageUrl { get; init; }
        public bool IsActive { get; init; }
        public DateTimeOffset Created { get; init; }
        public DateTimeOffset? Updated { get; init; }
    }
}
