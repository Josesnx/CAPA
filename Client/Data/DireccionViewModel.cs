using System.Text.Json.Serialization;

namespace Client.Data;

public class DireccionViewModel
{
    [JsonPropertyName("iddireccion")]
    public int IdDireccion { get; set; }

    public UsuarioViewModel Usuario { get; set; }

    public ColoniaViewModel Colonia { get; set; }

    public TipoVialidadViewModel TipoVialidad { get; set; }

    [JsonPropertyName("calle")]
    public string? Calle { get; set; }

    [JsonPropertyName("numerointerior")]
    public string? NumeroInterior { get; set; }

    [JsonPropertyName("numeroexterior")]
    public string? NumeroExterior { get; set; }

    [JsonPropertyName("calle1")]
    public string? Calle1 { get; set; }

    [JsonPropertyName("calle2")]
    public string? Calle2 { get; set; }
}
