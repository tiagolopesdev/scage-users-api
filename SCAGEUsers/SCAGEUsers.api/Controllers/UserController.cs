using Microsoft.AspNetCore.Mvc;
using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.ServiceSide;
using System.Net;
using SCAGEUsers.Application.Utils;
using SCAGEUsers.Application.VO;

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

        [HttpGet("filters")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RequestResponse>> GetUsersByFilters([FromQuery] string name, [FromQuery] Sex? sex)
        {
            try
            {
                var response = await _userService.GetUsersByFilters(name, sex);

                return Ok(RequestResponse.New("Usuários obtidos com sucesso", response));
            }
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RequestResponse>> GetAllUsers()
        {
            try
            {
                var response = await _userService.GetAllUsers();

                return response == null ?
                    BadRequest(RequestResponse.Error("Usuários não encontrados")) :
                    Ok(RequestResponse.New("Usuários obtidos com sucesso", response));
            }
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<RequestResponse>> GetUserById(Guid id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString())) return BadRequest(
                        RequestResponse.Error("Parametro id inválido")
                     );

                var response = await _userService.GetUserById(id);

                return response == null ?
                    NotFound(RequestResponse.Error("Usuário não encontrado")) :
                    Ok(RequestResponse.New("Usuário obtido com sucesso", response));
            }
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
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
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }

        [HttpPut("updateUser")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RequestResponse>> UpdateUser([FromBody] UserUpdateDto request)
        {
            try
            {
                var response = await _userService.UpdateUser(request);

                return response != Guid.Empty ?
                    Ok(RequestResponse.New("Usuário foi criado", response)) :
                    BadRequest(RequestResponse.New("Usuário não foi criado", response));
            }
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }

    }
}
