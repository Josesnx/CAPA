namespace Client.Data;

public class DireccionViewModel
{
    public int IdDireccion { get; set; }

    public int IdUsuario { get; set; }

    public int IdColonia { get; set; }

    public int IdTipoVialidad { get; set; }

    public UsuarioViewModel Usuario { get; set; }

    public ColoniaViewModel Colonia { get; set; }

    public TipoVialidadViewModel TipoVialidad { get; set; }

    public string? Calle { get; set; }

    public string? NumeroInterior { get; set; }

    public string? NumeroExterior { get; set; }

    public string? Calle1 { get; set; }

    public string? Calle2 { get; set; }
}
