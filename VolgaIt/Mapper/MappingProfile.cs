using AutoMapper;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.AccountCommands.Models;
using VolgaIt.MediatR.AdminRentCommands.Update;
using VolgaIt.MediatR.AdminTransportCommands.Create;
using VolgaIt.MediatR.AdminTransportCommands.Update;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.MediatR.TransportCommands.Create;
using VolgaIt.MediatR.TransportCommands.Models;
using VolgaIt.MediatR.TransportCommands.Update;

namespace VolgaIt.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTransportRequest, Transport>();
            CreateMap<CreateAdminTransportRequest, Transport>();
            CreateMap<UpdateTransportRequest, Transport>();
            CreateMap<UpdateAdminTransportRequest, Transport>();
            CreateMap<UpdateAdminRentRequest, Rent>();
            CreateMap<Transport, TransportViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<Rent, RentViewModel>();

        }
    }
}
