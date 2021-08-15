using LoginApi.Core.Domain.Requests;
using LoginApi.Core.Domain.Responses;
using LoginApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace LoginApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        #region LoginController
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        #endregion

        #region GetLogin
        /// <summary alignment="right">
        /// Endpoint para realização do login
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como objetivo retornar se as credenciais informada estão corretas e consequentemente autenticar o usuário para utilização da plataforma de e-Commerce.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint Login</returns>
        /// <response code="200">Código de retorno caso o endpoint autentique as credenciais de acesso.</response>
        /// <response code="400">Código de retorno caso ocorra um erro autenticar as credenciais informadas ou caso a requisição esteja divergente da especificação.</response>
        [HttpGet("/Login")]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLogin(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _loginService.Login(request);

            if (response.UsuarioAutenticado)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion
    }
}
