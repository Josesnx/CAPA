using FluentValidation;

namespace Client.Data.Validacion;

public class DireccionValidator : AbstractValidator<DireccionViewModel>
{
    public DireccionValidator()
    {
        RuleFor(d => d.Calle)
            .NotEmpty().WithMessage("El campo Calle es requerido")
            .MaximumLength(50).WithMessage("El campo Calle no debe de tener más de 50 caracteres");

        RuleFor(d => d.NumeroInterior)
            .MaximumLength(20).WithMessage("El campo Numero Interior no debe de tener más de 20 caracteres");

        RuleFor(d => d.NumeroExterior)
            .NotEmpty().WithMessage("El campo Numero Exterior es requerido")
            .MaximumLength(20).WithMessage("El campo Numero Exterior no debe de tener más de 20 caracteres");

        RuleFor(d => d.Calle1)
            .NotEmpty().WithMessage("El campo Calle 1 es requerido")
            .MaximumLength(50).WithMessage("El campo Calle 1 no debe de tener más de 50 caracteres");

        RuleFor(d => d.Calle2)
            .NotEmpty().WithMessage("El campo Calle 2 es requerido")
            .MaximumLength(50).WithMessage("El campo Calle 2 no debe de tener más de 50 caracteres");
    }
}
