using System.Text.Json.Serialization;

namespace Client.Data;

public class ColoniaViewModel
{
    [JsonPropertyName("idcolonia")]
    public int IdColonia { get; set; }

    public MunicipioViewModel Municipio { get; set; }

    [JsonPropertyName("nombre")]
    public string? Nombre { get; set; }

    [JsonPropertyName("codipostal")]
    public string? CodigoPostal { get; set; }
}
