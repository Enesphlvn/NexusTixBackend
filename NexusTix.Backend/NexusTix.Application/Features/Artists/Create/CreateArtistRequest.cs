namespace NexusTix.Application.Features.Artists.Create;

public record CreateArtistRequest(string Name, string? Bio, string? ImageUrl);