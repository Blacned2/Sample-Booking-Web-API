using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KapitalMedyaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingAppService BookingAppService;
        public BookingController(IBookingAppService bookingAppService)
        {
            this.BookingAppService = bookingAppService;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] BookingSearchRequest request)
        {
            var result = BookingAppService.Search(request);

            return Ok(result);
        }

        [HttpGet("get")]
        public IActionResult Get(int bookingID)
        {
            var result = BookingAppService.Get(bookingID);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateOrEdit([FromQuery] BookingDto request)
        {
            var result = BookingAppService.CreateOrEdit(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = BookingAppService.Delete(id);

            return Ok(result);
        }
    }
}
