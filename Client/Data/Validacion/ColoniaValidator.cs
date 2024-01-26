using FluentValidation;

namespace Client.Data.Validacion;

public class ColoniaValidator : AbstractValidator<ColoniaViewModel>
{
    public ColoniaValidator()
    {
        RuleFor(c => c.IdColonia)
            .NotEmpty().WithMessage("El campo Colonia es requerido");
    }
}
