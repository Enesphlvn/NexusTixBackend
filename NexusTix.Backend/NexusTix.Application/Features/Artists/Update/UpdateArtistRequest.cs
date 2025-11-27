namespace NexusTix.Application.Features.Artists.Update;

public record UpdateArtistRequest(int Id, string Name, string? Bio, string? ImageUrl);
