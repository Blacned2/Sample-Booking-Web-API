using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.AppServiceModels.Response;
using KapitalMedyaBooking.Data.Models;
using PagedList;
using System.Collections.Generic;

namespace KapitalMedyaBooking.AppService.Interfaces
{
    public interface IAppartmentAppService
    {
        public ServiceResponse<IPagedList<AppartmentListDto>> Search(AppartmentSearchRequest request);
        public ServiceResponse<AppartmentDto> Get(int appartmentID);
        public ServiceResponse<bool> Delete(int appartmentID);
        public ServiceResponse<AppartmentDto> CreateOrEdit(AppartmentDto appartment);
    }
}
