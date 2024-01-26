using System.Text.Json.Serialization;

namespace Client.Data;

public class EstadoViewModel
{
    [JsonPropertyName("idestado")]
    public int IdEstado { get; set; }

    [JsonPropertyName("nombre")]
    public string? Nombre { get; set; }
}
