using FluentValidation;

namespace Client.Data.Validacion;

public class ExpedienteValidator : AbstractValidator<ExpedienteViewModel>
{
    public ExpedienteValidator()
    {
        RuleFor(e => e.Usuario)
           .NotEmpty().WithMessage("El campo Usuario es requerido");

        RuleFor(e => e.TipoToma.Toma)
           .NotEmpty().WithMessage("El campo Toma es requerido");

        RuleFor(e => e.TipoToma.NoToma)
           .NotEmpty().WithMessage("El campo No Toma es requerido");

        RuleFor(e => e.Cuenta.Tarifa.IdTarifa)
           .NotEmpty().WithMessage("El campo Tarifa es requerido");

        RuleFor(e => e.Contrato)
           .NotEmpty().WithMessage("El campo Contrato es requerido");

        RuleFor(e => e.Tarjeta)
           .NotEmpty().WithMessage("El campo Tarjeta es requerido");

        RuleFor(e => e.NoSolicitud)
           .NotEmpty().WithMessage("El campo No Solicitud es requerido");

        RuleFor(e => e.Cuenta.Total)
           .NotEmpty().WithMessage("El campo Total es requerido");

        RuleFor(e => e.Cuenta.EstadoCuenta.Anio)
           .NotEmpty().WithMessage("El campo Año es requerido");

        RuleFor(e => e.Cuenta.EstadoCuenta.Meses)
           .NotEmpty().WithMessage("El campo Meses es requerido");

        RuleFor(e => e.Cuenta.EstadoCuenta.TotalMeses)
           .NotEmpty().WithMessage("El campo Total Meses es requerido");
    }
}
