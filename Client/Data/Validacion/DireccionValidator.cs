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

        RuleFor(u => u.Usuario.Nombre)
            .NotEmpty().WithMessage("El campo Nombre es requerido")
            .MaximumLength(50).WithMessage("El campo Nombre no debe de tener más de 50 caracteres");

        RuleFor(u => u.Usuario.ApellidoPaterno)
            .NotEmpty().WithMessage("El campo Apellido Paterno es requerido")
            .MaximumLength(50).WithMessage("El campo Apellido Paterno no debe de tener más de 50 caracteres");

        RuleFor(u => u.Usuario.ApellidoMaterno)
            .NotEmpty().WithMessage("El campo Apellido Materno es requerido")
            .MaximumLength(50).WithMessage("El campo Apellido Materno no debe de tener más de 50 caracteres");

        RuleFor(u => u.Usuario.Curp)
            .NotEmpty().WithMessage("El campo CURP es requerido")
            .MaximumLength(50).WithMessage("El campo CURP no debe de tener más de 50 caracteres")
            .Matches("^[A-Z]{4}\\d{6}[HM][A-Z]{5}[0-9]{2}$").WithMessage("El campo CURP es inválido");

        RuleFor(u => u.Usuario.Rfc)
            .NotEmpty().WithMessage("El campo RFC es requerido")
            .MaximumLength(50).WithMessage("El campo RFC no debe de tener más de 50 caracteres")
            .Matches("^[A-Z&Ññ]{3,4}\\d{6}[A-Za-z0-9]{3}$").WithMessage("El campo RFC es inválido");

        RuleFor(u => u.Usuario.Telefono)
            .NotEmpty().WithMessage("El campo Teléfono es requerido")
            .MaximumLength(10).WithMessage("El campo Teléfono no debe de tener más de 10 caracteres")
            .Matches("^[0-9]*$").WithMessage("El campo Teléfono es inválido");

        RuleFor(u => u.Usuario.TelefonoFijo)
            .MaximumLength(10).WithMessage("El campo Teléfono Fijo no debe de tener más de 10 caracteres")
            .Matches("^[0-9]*$").WithMessage("El campo Teléfono Fijo es inválido");

        RuleFor(u => u.Usuario.Correo)
            .NotEmpty().WithMessage("El campo Correo es requerido")
            .MaximumLength(250).WithMessage("El campo Correo no debe de tener más de 250 caracteres")
            .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$").WithMessage("El campo Correo es inválido");

        RuleFor(e => e.TipoVialidad.IdTipoVialidad)
           .NotEmpty().WithMessage("El campo Tipo Vialidad es requerido");

        RuleFor(e => e.Colonia.IdColonia)
           .NotEmpty().WithMessage("El campo Colonia es requerido");

        RuleFor(e => e.Colonia.Municipio.IdMunicipio)
           .NotEmpty().WithMessage("El campo Municipio es requerido");

        RuleFor(e => e.Colonia.Municipio.Estado.IdEstado)
           .NotEmpty().WithMessage("El campo Estado es requerido");
    }
}
