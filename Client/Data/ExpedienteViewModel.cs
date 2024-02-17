using Client.Data.Herramienta;
using System.Text.Json.Serialization;

namespace Client.Data;

public class ExpedienteViewModel
{
    public int IdExpediente { get; set; }

    public int IdUsuario { get; set; }

    public int IdCuenta { get; set; }

    public int IdTipoToma { get; set; }

    [JsonPropertyName("usuario")]
    [JsonConverter(typeof(ConvertirJson<UsuarioViewModel>))]
    public UsuarioViewModel Usuario { get; set; }

    [JsonPropertyName("cuenta")]
    [JsonConverter(typeof(ConvertirJson<CuentaViewModel>))]
    public CuentaViewModel Cuenta { get; set; }

    [JsonPropertyName("tipotoma")]
    [JsonConverter(typeof(ConvertirJson<TipoTomaViewModel>))]
    public TipoTomaViewModel TipoToma { get; set; }

    public string? Contrato { get; set; }

    public string? Tarjeta { get; set; }

    public string? NoSolicitud { get; set; }
}
