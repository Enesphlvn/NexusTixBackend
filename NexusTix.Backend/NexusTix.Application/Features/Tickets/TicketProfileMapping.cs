using AutoMapper;
using NexusTix.Application.Features.Tickets.Create;
using NexusTix.Application.Features.Tickets.Responses;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.Tickets
{
    public class TicketProfileMapping : Profile
    {
        public TicketProfileMapping()
        {
            CreateMap<CreateTicketRequest, Ticket>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.QRCodeGuid, opt => opt.Ignore())
                .ForMember(dest => dest.PurchaseDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsUsed, opt => opt.Ignore());

            CreateMap<Ticket, TicketResponse>();

            CreateMap<Ticket, TicketAggregateResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event));

            CreateMap<Ticket, TicketByEventResponse>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event));

            CreateMap<Ticket, TicketByUserResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<Ticket, TicketByDateRangeResponse>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
