using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KapitalMedyaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyAppService CompanyAppService;
        public CompanyController(ICompanyAppService companyAppService)
        {
            this.CompanyAppService = companyAppService;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] CompanySearchRequest request)
        {
            var result = CompanyAppService.Search(request);

            return Ok(result);
        }

        [HttpGet("get")]
        public IActionResult Get(int companyID)
        {
            var result = CompanyAppService.Get(companyID);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateOrEdit([FromQuery] CompanyDto request)
        {
            var result = CompanyAppService.CreateOrEdit(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = CompanyAppService.Delete(id);

            return Ok(result);
        }
    }
}
