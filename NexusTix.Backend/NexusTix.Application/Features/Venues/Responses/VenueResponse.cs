namespace NexusTix.Application.Features.Venues.Responses;

public record VenueResponse(int Id, string Name, int Capacity, double Latitude, double Longitude, int DistrictId);