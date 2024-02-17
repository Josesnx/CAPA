using Client.Data.Herramienta;
using System.Text.Json.Serialization;

namespace Client.Data;

public class DireccionViewModel
{
    public int IdDireccion { get; set; }

    public int IdUsuario { get; set; }

    public int IdColonia { get; set; }

    public int IdTipoVialidad { get; set; }

    [JsonPropertyName("usuario")]
    [JsonConverter(typeof(ConvertirJson<UsuarioViewModel>))]
    public UsuarioViewModel Usuario { get; set; }

    [JsonPropertyName("colonia")]
    [JsonConverter(typeof(ConvertirJson<ColoniaViewModel>))]
    public ColoniaViewModel Colonia { get; set; }

    [JsonPropertyName("tipovialidad")]
    [JsonConverter(typeof(ConvertirJson<TipoVialidadViewModel>))]
    public TipoVialidadViewModel TipoVialidad { get; set; }

    public string? Calle { get; set; }

    public string? NumeroInterior { get; set; }

    public string? NumeroExterior { get; set; }

    public string? Calle1 { get; set; }

    public string? Calle2 { get; set; }
}