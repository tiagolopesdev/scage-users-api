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
                        RequestResponse.Error(TypeAction.Obter, "Parametro id inválido")
                     );

                var response = await _userService.GetUserById(id);

                return response == null ?
                    BadRequest(RequestResponse.Error(TypeAction.Obter, "Usuário não encontrado")) :
                    Ok(RequestResponse.New("Usuário obtido com sucesso", response));
            }
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(TypeAction.Obter, ex.Message));
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
                return BadRequest(RequestResponse.Error(TypeAction.Criar, ex.Message));
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
                return BadRequest(RequestResponse.Error(TypeAction.Atualizar, ex.Message));
            }
        }

    }
}
