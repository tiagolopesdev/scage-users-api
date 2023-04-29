using Microsoft.AspNetCore.Mvc;
using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.ServiceSide;
using System.Net;
using SCAGEUsers.Application.Utils;

namespace SCAGEUsers.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<UsersDto>> GetAllUsers()
        {
            return Ok("Tudo ok");
        }

        [HttpPost("createUser")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RequestResponse>> CreateUser([FromBody] UserCreateDto request)
        {
            try
            {
                var response = await _userService.CreateUser(request);

                return response != Guid.Empty ?
                    Ok(RequestResponse.New("Usuário foi criado", response)) : 
                    BadRequest(RequestResponse.New("Usuário não foi criado", response));
            }
            catch(Exception ex) 
            {
                return BadRequest(ex);
            }
        }

    }
}
