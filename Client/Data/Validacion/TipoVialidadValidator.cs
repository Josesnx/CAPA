using FluentValidation;

namespace Client.Data.Validacion;

public class TipoVialidadValidator : AbstractValidator<TipoVialidadViewModel>
{
    public TipoVialidadValidator()
    {
        RuleFor(tv => tv.IdTipoVialidad)
            .NotEmpty().WithMessage("El campo Tipo Vialidad es requerido");
    }
}
