using LoginApi.Api.Controllers;
using LoginApi.Core.Domain.Requests;
using LoginApi.Core.Domain.Responses;
using LoginApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LoginApi.Tests
{
    [TestFixture]
    public class LoginTests
    {
        private Mock<ILoginService> mockLoginService;

        #region Setup
        [SetUp]
        public void Setup()
        {
            mockLoginService = new Mock<ILoginService>();
        }
        #endregion

        #region GetLogin_ModelStateInvalid
        [Test]
        public async Task GetLogin_ModelStateInvalid()
        {
            LoginController loginController = new LoginController(mockLoginService.Object);
            loginController.ModelState.AddModelError("Key", "errorMessage");
            var result = await loginController.GetLogin(new LoginRequest());
            Assert.IsFalse(loginController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetLogin_BadRequest
        [Test]
        public async Task GetLogin_BadRequest()
        {
            mockLoginService.Setup(l => l.Login(It.IsAny<LoginRequest>())).ReturnsAsync(new LoginResponse { Mensagem = "Credenciais inválidas.", UsuarioAutenticado = false });
            LoginController loginController = new LoginController(mockLoginService.Object);
            var result = await loginController.GetLogin(new LoginRequest());
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetLogin_Ok
        [Test]
        public async Task GetLogin_Ok()
        {
            mockLoginService.Setup(l => l.Login(It.IsAny<LoginRequest>())).ReturnsAsync(new LoginResponse { Mensagem = "Usuário autenticado.", UsuarioAutenticado = true });
            LoginController loginController = new LoginController(mockLoginService.Object);
            var result = await loginController.GetLogin(new LoginRequest());
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion
    }
}