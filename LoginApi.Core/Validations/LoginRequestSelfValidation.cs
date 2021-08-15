using FluentValidation;
using LoginApi.Core.Domain.Requests;

namespace LoginApi.Core.Validations
{
    public class LoginRequestSelfValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestSelfValidation()
        {
            RuleFor(r => r.Email)
                .Empty()
                .EmailAddress().WithMessage("O e-mail informado é inválido.");

            RuleFor(r => r.Senha)
                .Empty()
                .Length(6, 18).WithMessage("A senha informada é inválida.");
        }
    }
}
