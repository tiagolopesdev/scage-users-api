using Microsoft.AspNetCore.Mvc;
using SCAGEUsers.Application.DTO;

namespace SCAGEUsers.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<UsersDto>> GetAllUsers()
        {
            return Ok("Tudo ok");
        }
    }
}
