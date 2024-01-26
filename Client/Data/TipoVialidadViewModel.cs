using System.Text.Json.Serialization;

namespace Client.Data;

public class TipoVialidadViewModel
{
    [JsonPropertyName("idtipovialidad")]
    public int IdTipoVialidad { get; set; }

    [JsonPropertyName("vialidad")]
    public string? Vialidad { get; set; }
}
