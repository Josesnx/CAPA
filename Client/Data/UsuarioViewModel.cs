namespace Client.Data;

public class UsuarioViewModel
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Curp { get; set; }

    public string? Rfc { get; set; }

    public string? Telefono { get; set; }

    public string? TelefonoFijo { get; set; }

    public string? Correo { get; set; }

    public string? NombreCompleto
    {
        get
        {
            return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}".Trim();
        }
    }
}
