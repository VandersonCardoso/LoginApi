using LoginApi.Api.Controllers;
using LoginApi.Core.Domain.Models;
using LoginApi.Core.Domain.Responses;
using LoginApi.Core.Interfaces;
using LoginApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LoginApi.Tests
{
    [TestFixture]
    public class UsuarioTests
    {
        private Mock<IUsuarioService> mockUsuarioService;

        #region Setup
        [SetUp]
        public void Setup()
        {
            mockUsuarioService = new Mock<IUsuarioService>();
        }
        #endregion

        #region GetConsultarUsuario_ModelStateInvalid
        [TestCase(1)]
        public async Task GetConsultarUsuario_ModelStateInvalid(int id)
        {
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            usuarioController.ModelState.AddModelError("Key", "errorMessage");
            var result = usuarioController.GetConsultarUsuario(id);
            Assert.IsFalse(usuarioController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetConsultarUsuario_BadRequest
        [TestCase(999)]
        public async Task GetConsultarUsuario_BadRequest(int id)
        {
            mockUsuarioService.Setup(usu => usu.ConsultarUsuario(It.IsAny<int>())).Returns((Usuarios)null);
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            var result = usuarioController.GetConsultarUsuario(id);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetConsultarUsuario_Ok
        [TestCase(1)]
        public async Task GetConsultarUsuario_Ok(int id)
        {
            mockUsuarioService.Setup(usu => usu.ConsultarUsuario(It.IsAny<int>())).Returns(new Usuarios
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@teste.com",
                Senha = "12345"                
            });
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            var result = usuarioController.GetConsultarUsuario(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion

        #region PostIncluirUsuario_ModelStateInvalid
        [Test]
        public async Task PostIncluirUsuario_ModelStateInvalid()
        {
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            usuarioController.ModelState.AddModelError("Key", "errorMessage");
            var result = await usuarioController.PostIncluirUsuario(new Usuario());
            Assert.IsFalse(usuarioController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region PostIncluirUsuario_BadRequest
        [Test]
        public async Task PostIncluirUsuario_BadRequest()
        {
            mockUsuarioService.Setup(usu => usu.IncluirUsuario(It.IsAny<Usuario>())).ReturnsAsync(new IncluirUsuarioResponse { Mensagem = "Erro ao incluir usuário.", UsuarioIncluido = false });
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            var result = await usuarioController.PostIncluirUsuario(new Usuario());
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region PostIncluirUsuario_Ok
        [Test]
        public async Task PostIncluirUsuario_Ok()
        {
            mockUsuarioService.Setup(usu => usu.IncluirUsuario(It.IsAny<Usuario>())).ReturnsAsync(new IncluirUsuarioResponse { Mensagem = "Usuário incluído com sucesso.", UsuarioIncluido = true });
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            var result = await usuarioController.PostIncluirUsuario(new Usuario());
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion

        #region PutAtualizarUsuario_ModelStateInvalid
        [TestCase(1)]
        public async Task PutAtualizarUsuario_ModelStateInvalid(int id)
        {
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            usuarioController.ModelState.AddModelError("Key", "errorMessage");
            var result = await usuarioController.PutAtualizarUsuario(id, new Usuario());
            Assert.IsFalse(usuarioController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region PutAtualizarUsuario_BadRequest
        [TestCase(999)]
        public async Task PutAtualizarUsuario_BadRequest(int id)
        {
            mockUsuarioService.Setup(usu => usu.AtualizarUsuario(It.IsAny<int>(), It.IsAny<Usuario>())).ReturnsAsync(new AtualizarUsuarioResponse { Mensagem = "Erro ao atualizar usuário.", UsuarioAtualizado = false });
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            var result = await usuarioController.PutAtualizarUsuario(id, new Usuario());
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region PutAtualizarUsuario_Ok
        [TestCase(1)]
        public async Task PutAtualizarUsuario_Ok(int id)
        {
            mockUsuarioService.Setup(usu => usu.AtualizarUsuario(It.IsAny<int>(), It.IsAny<Usuario>())).ReturnsAsync(new AtualizarUsuarioResponse { Mensagem = "Usuário atualizado com sucesso.", UsuarioAtualizado = true });
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            var result = await usuarioController.PutAtualizarUsuario(id, new Usuario());
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion

        #region DeleteExcluirUsuario_ModelStateInvalid
        [TestCase(1)]
        public async Task DeleteExcluirUsuario_ModelStateInvalid(int id)
        {
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            usuarioController.ModelState.AddModelError("Key", "errorMessage");
            var result = await usuarioController.DeleteExcluirUsuario(id);
            Assert.IsFalse(usuarioController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region DeleteExcluirUsuario_BadRequest
        [TestCase(999)]
        public async Task DeleteExcluirUsuario_BadRequest(int id)
        {
            mockUsuarioService.Setup(usu => usu.ExcluirUsuario(It.IsAny<int>())).ReturnsAsync(new ExcluirUsuarioResponse { Mensagem = "Erro ao excluir usuário.", UsuarioExcluido = false });
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            var result = await usuarioController.DeleteExcluirUsuario(id);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region DeleteExcluirUsuario_Ok
        [TestCase(1)]
        public async Task DeleteExcluirUsuario_Ok(int id)
        {
            mockUsuarioService.Setup(usu => usu.ExcluirUsuario(It.IsAny<int>())).ReturnsAsync(new ExcluirUsuarioResponse { Mensagem = "Usuário excluído com sucesso.", UsuarioExcluido = true });
            UsuarioController usuarioController = new UsuarioController(mockUsuarioService.Object);
            var result = await usuarioController.DeleteExcluirUsuario(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion
    }
}
