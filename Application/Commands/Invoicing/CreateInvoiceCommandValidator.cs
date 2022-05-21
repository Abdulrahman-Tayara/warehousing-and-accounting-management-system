using FluentValidation;

namespace Application.Commands.Invoicing;

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(command => command.Items)
            .NotEmpty().WithMessage("Can't create empty invoice");
    }
}