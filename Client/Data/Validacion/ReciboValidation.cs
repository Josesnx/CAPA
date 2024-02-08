using FluentValidation;

namespace Client.Data.Validacion;

public class ReciboValidation : AbstractValidator<ReciboViewModel>
{
    public ReciboValidation()
    {
        RuleFor(r => r.NoRecibo)
            .NotEmpty().WithMessage("El campo No Recibo es requerido");

        RuleFor(r => r.Fecha)
            .NotEmpty().WithMessage("El campo Fecha es requerido");

        RuleFor(r => r.Cantidad)
            .NotEmpty().WithMessage("El campo Cantidad es requerido");

        RuleFor(r => r.Cuenta.EstadoCuenta.Meses)
            .NotEmpty().WithMessage("El campo Meses es requerido")
            .NotEqual(0).WithMessage("El campo Meses no puede ser cero");

        RuleFor(r => r.Cuenta.Usuario)
            .NotEmpty().NotNull().WithMessage("El campo Cuenta es requerido");
    }
}
