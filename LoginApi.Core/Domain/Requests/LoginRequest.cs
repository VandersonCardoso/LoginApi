using LoginApi.Core.Validations;

namespace LoginApi.Core.Domain.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public bool IsValid()
        {
            var ValidationResult = new LoginRequestSelfValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
