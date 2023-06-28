using FluentValidation;
using PosReversalNIBBS_API.Models.DTO;

namespace PosReversalNIBBS_API.Validators
{
    public class AddExcelResponseValidator : AbstractValidator<AddExcelResponseVM>
    {
        public AddExcelResponseValidator()
        {
            RuleFor(x => x.TERMINAL_ID).NotEmpty().WithMessage("This field is required");
            RuleFor(x => x.MERCHANT_ID).NotEmpty().WithMessage("This field is required");
            RuleFor(x => x.AMOUNT).NotEmpty();
            RuleFor(x => x.STAN).NotEmpty().WithMessage("This field is required");
            RuleFor(x => x.RRN).NotEmpty().WithMessage("This field is required");

        }
    }
}
