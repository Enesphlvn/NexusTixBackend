namespace NexusTix.Application.Features.Venues.Update
{
    public record UpdateVenueRequest(int Id, string Name, int Capacity, int DistrictId);
}
