using FluentValidation;
using PosReversalNIBBS_API.Models.DTO;

namespace PosReversalNIBBS_API.Validators
{
    public class LoginRequestValidator: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
