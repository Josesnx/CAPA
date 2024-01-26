using FluentValidation;

namespace Client.Data.Validacion;

public class EstadoValidator : AbstractValidator<EstadoViewModel>
{
    public EstadoValidator()
    {
        RuleFor(e => e.IdEstado)
            .NotEmpty().WithMessage("El campo Estado es requerido");
    }
}
