namespace Client.Data;

public class ExpedienteViewModel
{
    public int IdExpediente { get; set; }

    public UsuarioViewModel Usuario { get; set; }

    public ReciboViewModel Recibo { get; set; }

    public TipoTomaViewModel TipoToma { get; set; }

    public string? Contrato { get; set; }

    public string? Tarjeta { get; set; }

    public string? NoSolicitud { get; set; }
}
