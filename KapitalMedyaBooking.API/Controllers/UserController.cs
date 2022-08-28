using KapitalMedyaBooking.AppService.AppServiceModels.Dto;
using KapitalMedyaBooking.AppService.AppServiceModels.Request;
using KapitalMedyaBooking.AppService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KapitalMedyaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService UserAppService;
        public UserController(IUserAppService userAppService)
        {
            this.UserAppService = userAppService;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] UserSearchRequest request)
        {
            var result = UserAppService.Search(request);

            return Ok(result);
        }

        [HttpGet("get")]
        public IActionResult Get(int userID)
        {
            var result = UserAppService.Get(userID);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateOrEdit([FromQuery] UserDto request)
        {
            var result = UserAppService.CreateOrEdit(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = UserAppService.Delete(id);

            return Ok(result);
        }
    }
}
