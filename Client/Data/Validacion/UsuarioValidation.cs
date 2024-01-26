using FluentValidation;

namespace Client.Data.Validacion;

public class UsuarioValidation : AbstractValidator<UsuarioViewModel>
{
    public UsuarioValidation()
    {
        RuleFor(u => u.Nombre)
            .NotEmpty().WithMessage("El campo Nombre es requerido")
            .MaximumLength(50).WithMessage("El campo Nombre no debe de tener más de 50 caracteres");

        RuleFor(u => u.ApellidoPaterno)
            .NotEmpty().WithMessage("El campo Apellido Paterno es requerido")
            .MaximumLength(50).WithMessage("El campo Apellido Paterno no debe de tener más de 50 caracteres");

        RuleFor(u => u.ApellidoMaterno)
            .NotEmpty().WithMessage("El campo Apellido Materno es requerido")
            .MaximumLength(50).WithMessage("El campo Apellido Materno no debe de tener más de 50 caracteres");

        RuleFor(u => u.Curp)
            .NotEmpty().WithMessage("El campo CURP es requerido")
            .MaximumLength(50).WithMessage("El campo CURP no debe de tener más de 50 caracteres")
            .Matches("^[A-Z]{4}\\d{6}[HM][A-Z]{5}[0-9]{2}$").WithMessage("El campo CURP es inválido");

        RuleFor(u => u.Rfc)
            .NotEmpty().WithMessage("El campo RFC es requerido")
            .MaximumLength(50).WithMessage("El campo RFC no debe de tener más de 50 caracteres")
            .Matches("^[A-Z&Ññ]{3,4}\\d{6}[A-Za-z0-9]{3}$").WithMessage("El campo RFC es inválido");

        RuleFor(u => u.Telefono)
            .NotEmpty().WithMessage("El campo Teléfono es requerido")
            .MaximumLength(10).WithMessage("El campo Teléfono no debe de tener más de 10 caracteres")
            .Matches("^[0-9]*$").WithMessage("El campo Teléfono es inválido");

        RuleFor(u => u.TelefonoFijo)
            .MaximumLength(10).WithMessage("El campo Teléfono Fijo no debe de tener más de 10 caracteres")
            .Matches("^[0-9]*$").WithMessage("El campo Teléfono Fijo es inválido");

        RuleFor(u => u.Correo)
            .NotEmpty().WithMessage("El campo Correo es requerido")
            .MaximumLength(250).WithMessage("El campo Correo no debe de tener más de 250 caracteres")
            .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$").WithMessage("El campo Correo es inválido");
    }
}
