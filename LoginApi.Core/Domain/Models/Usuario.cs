using LoginApi.Core.Validations;

namespace LoginApi.Core.Domain.Models
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public bool IsValid()
        {
            var ValidationResult = new UsuarioSelfValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
