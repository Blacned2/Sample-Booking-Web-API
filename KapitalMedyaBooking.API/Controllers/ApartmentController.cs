using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KapitalMedyaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IAppartmentAppService AppartmentAppService;
        public ApartmentController(IAppartmentAppService appartmentAppService)
        {
            this.AppartmentAppService = appartmentAppService;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] AppartmentSearchRequest request)
        {
            var result = AppartmentAppService.Search(request);

            return Ok(result);
        }

        [HttpGet("get")]
        public IActionResult Get(int appartmentID)
        {
            var result = AppartmentAppService.Get(appartmentID);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateOrEdit([FromQuery] AppartmentDto request)
        {
            var result = AppartmentAppService.CreateOrEdit(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = AppartmentAppService.Delete(id);

            return Ok(result);
        }

    }
}
