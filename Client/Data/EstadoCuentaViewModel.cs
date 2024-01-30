using System.Text.Json.Serialization;

namespace Client.Data;

public class EstadoCuentaViewModel
{
    public int IdEstadoCuenta { get; set; }

    [JsonPropertyName("años")]
    public int Anio { get; set; }

    public int Meses { get; set; }

    public int TotalMeses { get; set; }

    [JsonPropertyName("estatusecta")]
    public int Estatus { get; set; }
}
