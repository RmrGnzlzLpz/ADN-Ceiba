using AutoMapper;
using Estacionamiento.Application.Parking.Queries;
using Estacionamiento.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Estacionamiento.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Dtos.TakeOutVehicleDto, Application.Vehicle.Commands.TakeOutVehicle.TakeOutVehicleDto>();

            CreateMap<Domain.Entities.Vehicle, Application.Vehicle.Commands.ParkVehicle.ParkVehicleDto>();

            CreateMap<Domain.Ports.IParkingConfiguration, Application.Parking.Queries.ParkingDto>();

            CreateMap<KeyValuePair<VehicleType, double>, ParkingProperty<double>>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<KeyValuePair<VehicleType, uint>, ParkingProperty<uint>>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<KeyValuePair<VehicleType, ushort>, ParkingProperty<ushort>>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<Domain.Entities.VehicleHistory, Application.Vehicle.Queries.VehicleParkingDto>();

            CreateMap<Domain.Entities.Vehicle, Application.Vehicle.Queries.HistoryOfVehicleDto>();

            CreateMap<Domain.Entities.Vehicle, Application.Vehicle.Queries.GetVehicleDto>();

            CreateMap<Domain.Dtos.TakeOutVehicleDto, Application.Vehicle.Commands.TakeOutVehicle.TakeOutVehicleDto>();

            CreateMap<Domain.Dtos.TakeOutVehicleDto, Application.Vehicle.Queries.CalculateParkingCostDto>();
        }
    }
}
