using System.Text.Json.Serialization;

namespace Client.Data;

public class MunicipioViewModel
{
    [JsonPropertyName("idmunicipio")]
    public int IdMunicipio { get; set; }

    public EstadoViewModel Estado { get; set; }

    [JsonPropertyName("nombre")]
    public string? Nombre { get; set; }
}
