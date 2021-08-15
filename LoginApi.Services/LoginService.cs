using LoginApi.Core.Domain.Requests;
using LoginApi.Core.Domain.Responses;
using LoginApi.Core.Interfaces;
using LoginApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LoginApi.Services
{
    public class LoginService : ILoginService
    {
        private readonly LoginDbContext _loginDbContext;

        #region LoginService
        public LoginService(LoginDbContext loginDbContext)
        {
            _loginDbContext = loginDbContext;
        }
        #endregion

        #region Login
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                var usuario = await _loginDbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);

                if (usuario != null)
                {
                    if (usuario.Senha == request.Senha)
                    {
                        return new LoginResponse { Mensagem = "Login realizado com sucesso.", UsuarioAutenticado = true };
                    }
                    else
                    {
                        return new LoginResponse { Mensagem = "A senha informada está incorreta.", UsuarioAutenticado = false };
                    }
                }
                else
                {
                    return new LoginResponse { Mensagem = "E-mail informado não localizado ou inválido.", UsuarioAutenticado = false };
                }
            }
            catch(Exception ex)
            {
                return new LoginResponse { Mensagem = string.Format("Ocorreu um erro ao realizar o login do usuário. Erro: {0}", ex.Message), UsuarioAutenticado = false };
            }
        }
        #endregion
    }
}
