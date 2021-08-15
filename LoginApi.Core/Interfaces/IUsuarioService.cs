using LoginApi.Core.Domain.Models;
using LoginApi.Core.Domain.Responses;
using LoginApi.Data.Repositories;
using System.Threading.Tasks;

namespace LoginApi.Core.Interfaces
{
    public interface IUsuarioService
    {
        Usuarios ConsultarUsuario(int id);
        Task<IncluirUsuarioResponse> IncluirUsuario(Usuario item);
        Task<AtualizarUsuarioResponse> AtualizarUsuario(int id, Usuario item);
        Task<ExcluirUsuarioResponse> ExcluirUsuario(int id);
    }
}
