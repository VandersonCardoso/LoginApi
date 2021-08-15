using LoginApi.Core.Domain.Models;
using LoginApi.Core.Domain.Responses;
using LoginApi.Core.Interfaces;
using LoginApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace LoginApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        #region UsuarioController
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        #endregion

        #region GetConsultarUsuario
        /// <summary alignment="right">
        /// Endpoint para consulta de um usuário
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como objetivo retornar as informações de um usuário cadastrado, através do seu ID.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint ConsultarUsuario</returns>
        /// <response code="200">Código de retorno caso o endpoint retorne as informações do usuário.</response>
        /// <response code="400">Código de retorno caso ocorra um erro ao obter o usuário informado ou caso o id informado seja inválido.</response>
        [HttpGet("/ConsultarUsuario")]
        [ProducesResponseType(typeof(Usuarios), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Usuarios), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetConsultarUsuario(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _usuarioService.ConsultarUsuario(id);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion

        #region PostIncluirUsuario
        /// <summary alignment="right">
        /// Endpoint para inclusão de um usuário
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como incluir um usuário no cadastro de usuários.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint IncluirUsuario</returns>
        /// <response code="200">Código de retorno caso o endpoint inclua o usuário corretamente.</response>
        /// <response code="400">Código de retorno caso ocorra um erro ao incluir a usuário informado ou caso o conteúdo da requisição esteja divergente da especificação.</response>
        [HttpPost("/IncluirUsuario")]
        [ProducesResponseType(typeof(IncluirUsuarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IncluirUsuarioResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostIncluirUsuario([FromBody] Usuario item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _usuarioService.IncluirUsuario(item);

            if (response.UsuarioIncluido)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion

        #region PutAtualizarUsuario
        /// <summary alignment="right">
        /// Endpoint para atualização de um usuário
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como atualizar as informações de um usuário no seu respectivo cadastro.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint AtualizarrUsuario</returns>
        /// <response code="200">Código de retorno caso o endpoint atualize as informações do usuário corretamente.</response>
        /// <response code="400">Código de retorno caso ocorra um erro ao atualizar as informações do usuário informado ou caso o conteúdo da requisição esteja divergente da especificação.</response>
        [HttpPut("/AtualizarUsuario")]
        [ProducesResponseType(typeof(AtualizarUsuarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AtualizarUsuarioResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAtualizarUsuario(int id, [FromBody] Usuario item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _usuarioService.AtualizarUsuario(id, item);

            if (response.UsuarioAtualizado)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion

        #region DeleteExcluirUsuario
        /// <summary alignment="right">
        /// Endpoint para exclusão de um usuário
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como excluir um usuário do cadastro de usuários.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint ExcluirUsuario</returns>
        /// <response code="200">Código de retorno caso o endpoint exclua o usuário corretamente.</response>
        /// <response code="400">Código de retorno caso ocorra um erro ao excluir o usuário informado ou caso o conteúdo da requisição esteja divergente da especificação.</response>
        [HttpDelete("/ExcluirUsuario")]
        [ProducesResponseType(typeof(ExcluirUsuarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExcluirUsuarioResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteExcluirUsuario(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _usuarioService.ExcluirUsuario(id);

            if (response.UsuarioExcluido)
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
