using FluentValidation;

namespace Client.Data.Validacion;

public class TarifaValidator : AbstractValidator<TarifaViewModel>
{
    public TarifaValidator()
    {
        RuleFor(t => t.Tipo)
            .NotEmpty().WithMessage("El campo Tipo es requerido");

        RuleFor(t => t.Anio)
            .NotEmpty().WithMessage("El campo Año es requerido")
            .Must(t => t >= DateTime.Now.Year).WithMessage("El campo Año no puede ser menor que el año actual");

        RuleFor(t => t.Precio)
            .NotEmpty().WithMessage("El campo Precio es requerido");
    }
}
