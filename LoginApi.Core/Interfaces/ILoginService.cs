using LoginApi.Core.Domain.Requests;
using LoginApi.Core.Domain.Responses;
using System.Threading.Tasks;

namespace LoginApi.Core.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
