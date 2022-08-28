using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.AppServiceModels.Response;
using PagedList;
using System.Collections.Generic;

namespace KapitalMedyaBooking.AppService.Interfaces
{
    public interface IUserAppService
    {
        public ServiceResponse<IPagedList<UserListDto>> Search(UserSearchRequest request);
        public ServiceResponse<UserDto> Get(int userID);
        public ServiceResponse<bool> Delete(int userID);
        public ServiceResponse<UserDto> CreateOrEdit(UserDto user);
    }
}
