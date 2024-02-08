using FluentValidation;

namespace Client.Data.Validacion;

public class EstadoCuentaValidator : AbstractValidator<EstadoCuentaViewModel>
{
    public EstadoCuentaValidator()
    {
        RuleFor(ec => ec.Anio)
           .NotEmpty().WithMessage("El campo Año es requerido");
    }
}
