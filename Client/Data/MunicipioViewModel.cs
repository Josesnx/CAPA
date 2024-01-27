using System.Text.Json.Serialization;

namespace Client.Data;

public class MunicipioViewModel
{
    public int IdMunicipio { get; set; }

    public int IdEstado { get; set; }

    public EstadoViewModel Estado { get; set; }

    [JsonPropertyName("nombremunicipio")]
    public string? Nombre { get; set; }
}
