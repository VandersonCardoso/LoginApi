using FluentValidation;
using LoginApi.Core.Domain.Requests;

namespace LoginApi.Core.Validations
{
    public class LoginRequestSelfValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestSelfValidation()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("O e-mail informado é inválido.");

            RuleFor(r => r.Senha)
                .NotEmpty()
                .Length(6, 18).WithMessage("A senha informada é inválida.");
        }
    }
}
