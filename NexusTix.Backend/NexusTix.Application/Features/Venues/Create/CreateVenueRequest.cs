namespace NexusTix.Application.Features.Venues.Create;

public record CreateVenueRequest(string Name, int Capacity, double Latitude, double Longitude, int DistrictId);
