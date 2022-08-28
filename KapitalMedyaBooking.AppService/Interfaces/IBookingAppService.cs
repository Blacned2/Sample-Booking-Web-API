using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.AppServiceModels.Response;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KapitalMedyaBooking.AppService.Interfaces
{
    public interface IBookingAppService
    {
        public ServiceResponse<IPagedList<BookingListDto>> Search(BookingSearchRequest request);
        public ServiceResponse<BookingDto> Get(int bookingID);
        public ServiceResponse<bool> Delete(int bookingID);
        public ServiceResponse<BookingDto> CreateOrEdit(BookingDto booking);
    }
}
