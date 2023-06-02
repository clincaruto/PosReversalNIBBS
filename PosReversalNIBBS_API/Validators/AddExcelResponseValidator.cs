using FluentValidation;
using PosReversalNIBBS_API.Models.DTO;

namespace PosReversalNIBBS_API.Validators
{
    public class AddExcelResponseValidator : AbstractValidator<AddExcelResponseVM>
    {
        public AddExcelResponseValidator()
        {
            RuleFor(x => x.TERMINAL_ID).NotEmpty().WithMessage("This field is required");
        }
    }
}
