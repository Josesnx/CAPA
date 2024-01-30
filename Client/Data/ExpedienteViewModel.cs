namespace Client.Data;

public class ExpedienteViewModel
{
    public int IdExpediente { get; set; }

    public int IdUsuario { get; set; }

    public int IdCuenta { get; set; }

    public int IdTipoToma { get; set; }

    public UsuarioViewModel Usuario { get; set; }

    public CuentaViewModel Cuenta { get; set; }

    public TipoTomaViewModel TipoToma { get; set; }

    public string? Contrato { get; set; }

    public string? Tarjeta { get; set; }

    public string? NoSolicitud { get; set; }
}
