using FluentValidation;
using LoginApi.Core.Domain.Models;

namespace LoginApi.Core.Validations
{
    public class UsuarioSelfValidation : AbstractValidator<Usuario>
    {
        public UsuarioSelfValidation()
        {
            RuleFor(r => r.Nome)
                .Length(2, 60).WithMessage("O atributo [Nome] deve ter entre 2 e 60 caracteres.");

            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("O e-mail informado não é válido.");

            RuleFor(r => r.Senha)
                .Length(6, 18).WithMessage("O atributo [Senha] deve ter entre 6 e 18 caracteres.");
        }
    }
}
