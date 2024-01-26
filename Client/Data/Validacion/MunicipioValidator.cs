using FluentValidation;

namespace Client.Data.Validacion;

public class MunicipioValidator : AbstractValidator<MunicipioViewModel>
{
    public MunicipioValidator()
    {
        RuleFor(m => m.IdMunicipio)
            .NotEmpty().WithMessage("El campo Municipio es requerido");
    }
}
