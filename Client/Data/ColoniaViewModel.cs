using System.Text.Json.Serialization;

namespace Client.Data;

public class ColoniaViewModel
{
    public int IdColonia { get; set; }

    public int IdMunicipio { get; set; }

    public MunicipioViewModel Municipio { get; set; }

    [JsonPropertyName("nombrecolonia")]
    public string? Nombre { get; set; }

    public string? CodigoPostal { get; set; }
}
