using System.Text.Json.Serialization;

namespace Client.Data;

public class TarifaViewModel
{
    public int IdTarifa { get; set; }

    public string? Tipo { get; set; }

    [JsonPropertyName("añotarifa")]
    public int Anio { get; set; }

    public decimal Precio { get; set; }

    public short Estatus { get; set; }
}
