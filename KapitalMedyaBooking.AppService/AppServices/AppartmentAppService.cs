using AutoMapper;
using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Enum;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.AppServiceModels.Response;
using KapitalMedyaBooking.AppService.Interfaces;
using KapitalMedyaBooking.Data.Context;
using KapitalMedyaBooking.Data.Models;
using PagedList;
using System;
using System.Linq;

namespace KapitalMedyaBooking.AppService.AppServices
{
    public class AppartmentAppService : BaseAppService, IAppartmentAppService
    {
        public AppartmentAppService(KapitalDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public ServiceResponse<AppartmentDto> CreateOrEdit(AppartmentDto appartment)
        {
            var serviceResult = new ServiceResponse<AppartmentDto>();

            try
            {
                var appartmentMap = Mapper.Map<Appartment>(appartment);

                if (appartmentMap.ID >= 0)
                {
                    DB.Appartments.Update(appartmentMap);
                    DB.SaveChanges();
                    serviceResult.Message = "Successfully Updated";
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = appartment;
                }
                else
                {
                    DB.Appartments.Add(appartmentMap);
                    DB.SaveChanges();
                    serviceResult.Message = "Successfully Created";
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = appartment;
                }
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
                serviceResult.MessageType = MessageTypeEnum.Error;
            }
            return serviceResult;
        }

        public ServiceResponse<bool> Delete(int appartmentID)
        {
            var serviceResult = new ServiceResponse<bool>();

            try
            {
                var appartment = DB.Appartments.Find(appartmentID);

                if (appartment != null)
                {
                    DB.Remove(appartment);
                    DB.SaveChanges();
                    serviceResult.Message = string.Empty;
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = true;
                }
                else
                {
                    serviceResult.Message = "Appartment not found";
                    serviceResult.MessageType = MessageTypeEnum.Warning;
                    serviceResult.ReturnObject = false;
                }
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
                serviceResult.MessageType = MessageTypeEnum.Error;
                serviceResult.ReturnObject = false;
            }
            return serviceResult;
        }

        public ServiceResponse<AppartmentDto> Get(int appartmentID)
        {
            var serviceResult = new ServiceResponse<AppartmentDto>();

            try
            {
                serviceResult.ReturnObject = Mapper
                                                .Map<AppartmentDto>
                                                    (DB.Appartments
                                                        .Find(appartmentID));
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
            }
            return serviceResult;
        }

        public ServiceResponse<IPagedList<AppartmentListDto>> Search(AppartmentSearchRequest request)
        {
            var serviceResult = new ServiceResponse<IPagedList<AppartmentListDto>>();

            try
            {
                var results = (from u in DB.Appartments
                               where
                               1 == 1
                               && (string.IsNullOrEmpty(request.Name) || u.Name.Contains(request.Name))
                               && (string.IsNullOrEmpty(request.Country) || u.Country.Contains(request.Country))
                               && (string.IsNullOrEmpty(request.City) || u.City.Contains(request.City))
                               && (string.IsNullOrEmpty(request.Address) || u.Address.Contains(request.Address))
                               && (string.IsNullOrEmpty(request.Address) || u.Address2.Contains(request.Address))
                               && (string.IsNullOrEmpty(request.ZipCode) || u.ZipCode == request.ZipCode)
                               select new AppartmentListDto
                               { 
                                    ID = u.ID,
                                    Name = u.Name,
                                    Address = u.Address,
                                    Address2 = u.Address2,
                                    Booked = u.Booked,
                                    City = u.City,
                                    Country = u.Country,
                                    Direction = u.Direction,
                                    Latitude = u.Latitude,
                                    Longitude = u.Longitude,
                                    ZipCode = u.ZipCode
                               }).ToPagedList(request.Page, request.PageSize);

                if (results.Any())
                {
                    serviceResult.Message = string.Empty;
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = results;
                }
                else
                {
                    serviceResult.Message = "Not Found!";
                    serviceResult.MessageType = MessageTypeEnum.Warning;
                }
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
                serviceResult.MessageType = MessageTypeEnum.Error;
            }

            return serviceResult;

        }
    }
}
