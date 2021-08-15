using LoginApi.Core.Domain.Models;
using LoginApi.Core.Domain.Responses;
using LoginApi.Core.Interfaces;
using LoginApi.Data.Contexts;
using LoginApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly LoginDbContext _loginDbContext;

        #region UsuarioService
        public UsuarioService(LoginDbContext loginDbContext)
        {
            _loginDbContext = loginDbContext;
        }
        #endregion

        #region ConsultarUsuario
        public Usuarios ConsultarUsuario(int id)
        {
            return _loginDbContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }
        #endregion

        #region IncluirUsuario
        public async Task<IncluirUsuarioResponse> IncluirUsuario(Usuario item)
        {
            try
            {
                var maxId = await _loginDbContext.Usuarios.MaxAsync(x => (int?)x.Id);

                var usuario = new Usuarios
                {
                    Id = maxId.HasValue ? maxId.Value + 1 : 1,
                    Nome = item.Nome,
                    Email = item.Email,
                    Senha = item.Senha
                };

                _loginDbContext.Add(usuario);
                _loginDbContext.SaveChanges();

                return new IncluirUsuarioResponse { Mensagem = "Usuário incluído com sucesso.", UsuarioIncluido = true };
            }
            catch (Exception ex)
            {
                return new IncluirUsuarioResponse { Mensagem = string.Format("Ocorreu um erro ao incluir o usuário. Erro: {0}", ex.Message), UsuarioIncluido = false };
            }
        }
        #endregion

        #region AtualizarUsuario
        public async Task<AtualizarUsuarioResponse> AtualizarUsuario(int id, Usuario item)
        {
            try
            {
                var usuario = await _loginDbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                if (usuario != null)
                {
                    if (!string.IsNullOrEmpty(item.Nome))
                    {
                        usuario.Nome = item.Nome;
                    }

                    if (!string.IsNullOrEmpty(item.Email))
                    {
                        usuario.Email = item.Email;
                    }

                    if (!string.IsNullOrEmpty(item.Senha))
                    {
                        usuario.Senha = item.Senha;
                    }

                    _loginDbContext.Update(usuario);
                    _loginDbContext.SaveChanges();

                    return new AtualizarUsuarioResponse { Mensagem = "Usuário atualizado com sucesso.", UsuarioAtualizado = true };
                }
                else
                {
                    return new AtualizarUsuarioResponse { Mensagem = string.Format("Usuário de ID [{0}] não localizado", id), UsuarioAtualizado = false };
                }
            }
            catch (Exception ex)
            {
                return new AtualizarUsuarioResponse { Mensagem = string.Format("Ocorreu um erro ao atualizar as informações do usuário. Erro: {0}", ex.Message), UsuarioAtualizado = false };
            }
        }
        #endregion

        #region ExcluirUsuario
        public async Task<ExcluirUsuarioResponse> ExcluirUsuario(int id)
        {
            try
            {
                var usuario = await _loginDbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                if (usuario != null)
                {
                    _loginDbContext.Remove(usuario);
                    _loginDbContext.SaveChanges();

                    return new ExcluirUsuarioResponse { Mensagem = "Usuário excluído com sucesso.", UsuarioExcluido = true };
                }
                else
                {
                    return new ExcluirUsuarioResponse { Mensagem = string.Format("Usuário de ID [{0}] não localizado", id), UsuarioExcluido = false };
                }
            }
            catch (Exception ex)
            {
                return new ExcluirUsuarioResponse { Mensagem = string.Format("Ocorreu um erro ao excluir o usuário. Erro: {0}", ex.Message), UsuarioExcluido = false };
            }
        }
        #endregion
    }
}
