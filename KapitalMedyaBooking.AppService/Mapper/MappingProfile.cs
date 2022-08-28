using AutoMapper;
using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.AppService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppartmentDto, Appartment>();
            CreateMap<Appartment, AppartmentDto>();
            CreateMap<BookingDto, Booking>();
            CreateMap<Booking, BookingDto>();
            CreateMap<CompanyDto, Company>();
            CreateMap<Company, CompanyDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
