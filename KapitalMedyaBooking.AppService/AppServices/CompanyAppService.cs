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
    public class CompanyAppService : BaseAppService, ICompanyAppService
    {
        public CompanyAppService(KapitalDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public ServiceResponse<CompanyDto> CreateOrEdit(CompanyDto company)
        {
            var serviceResult = new ServiceResponse<CompanyDto>();

            try
            {
                var companyMap = Mapper.Map<Company>(company);

                if (companyMap.ID >= 0)
                {
                    DB.Companies.Update(companyMap);
                    DB.SaveChanges();
                    serviceResult.Message = "Successfully Updated";
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = company;
                }
                else
                {
                    DB.Companies.Add(companyMap);
                    DB.SaveChanges();
                    serviceResult.Message = "Successfully Created";
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = company;
                }
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
                serviceResult.MessageType = MessageTypeEnum.Error;
            }
            return serviceResult;
        }

        public ServiceResponse<bool> Delete(int companyID)
        {
            var serviceResult = new ServiceResponse<bool>();

            try
            {
                var company = DB.Appartments.Find(companyID);

                if (company != null)
                {
                    DB.Remove(company);
                    DB.SaveChanges();
                    serviceResult.Message = string.Empty;
                    serviceResult.MessageType = MessageTypeEnum.Success;
                    serviceResult.ReturnObject = true;
                }
                else
                {
                    serviceResult.Message = "Company not found";
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

        public ServiceResponse<CompanyDto> Get(int companyID)
        {
            var serviceResult = new ServiceResponse<CompanyDto>();

            try
            {
                serviceResult.ReturnObject = Mapper
                                                .Map<CompanyDto>
                                                    (DB.Appartments
                                                        .Find(companyID));
            }
            catch (Exception ex)
            {
                serviceResult.Message = ex.Message;
            }
            return serviceResult;
        }

        public ServiceResponse<IPagedList<CompanyListDto>> Search(CompanySearchRequest request)
        {
            var serviceResult = new ServiceResponse<IPagedList<CompanyListDto>>();

            try
            {
                var results = (from u in DB.Companies
                               where string.IsNullOrEmpty(request.Name) || u.Name.Contains(request.Name)
                               select new CompanyListDto
                               {
                                   ID = u.ID,
                                   Address = u.Address,
                                   Age = u.Age,
                                   Name = u.Name
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
