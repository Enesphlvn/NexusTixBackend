using AutoMapper;
using NexusTix.Application.Features.Artists.Create;
using NexusTix.Application.Features.Artists.Responses;
using NexusTix.Application.Features.Artists.Update;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.Artists
{
    public class ArtistProfileMapping : Profile
    {
        public ArtistProfileMapping()
        {
            CreateMap<CreateArtistRequest, Artist>();
            CreateMap<UpdateArtistRequest, Artist>();

            CreateMap<Artist, ArtistResponse>();
            CreateMap<Artist, ArtistWithEventsResponse>();
        }
    }
}
