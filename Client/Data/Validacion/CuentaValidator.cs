using FluentValidation;

namespace Client.Data.Validacion;

public class CuentaValidator : AbstractValidator<CuentaViewModel>
{
    public CuentaValidator()
    {
        RuleFor(c => c.EstadoCuenta.Anio)
           .NotEmpty().WithMessage("El campo Año es requerido");

        RuleFor(c => c.EstadoCuenta.Meses)
           .NotEmpty().WithMessage("El campo Meses es requerido");

        RuleFor(c => c.EstadoCuenta.TotalMeses)
           .NotEmpty().WithMessage("El campo Total Meses es requerido");

        RuleFor(c => c.Total)
           .NotEmpty().WithMessage("El campo Total es requerido");
    }
}
