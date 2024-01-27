using System.Text.Json.Serialization;

namespace Client.Data;

public class EstadoViewModel
{
    public int IdEstado { get; set; }

    [JsonPropertyName("nombreestado")]
    public string? Nombre { get; set; }
}
