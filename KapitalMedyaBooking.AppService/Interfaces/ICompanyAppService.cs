using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.AppServiceModels.Response;
using PagedList;
using System.Collections.Generic;

namespace KapitalMedyaBooking.AppService.Interfaces
{
    public interface ICompanyAppService
    {
        public ServiceResponse<IPagedList<CompanyListDto>> Search(CompanySearchRequest request);
        public ServiceResponse<CompanyDto> Get(int companyID);
        public ServiceResponse<bool> Delete(int companyID);
        public ServiceResponse<CompanyDto> CreateOrEdit(CompanyDto company);
    }
}
