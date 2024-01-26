using System.Text.Json.Serialization;

namespace Client.Data;

public class UsuarioViewModel
{
    [JsonPropertyName("idusuario")]
    public int IdUsuario { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; }

    [JsonPropertyName("apellidopaterno")]
    public string ApellidoPaterno { get; set; }

    [JsonPropertyName("apellidomaterno")]
    public string ApellidoMaterno { get; set; }

    [JsonPropertyName("curp")]
    public string Curp { get; set; }

    [JsonPropertyName("rfc")]
    public string Rfc { get; set; }

    [JsonPropertyName("telefono")]
    public string Telefono { get; set; }

    [JsonPropertyName("telefonofijo")]
    public string TelefonoFijo { get; set; }

    [JsonPropertyName("correo")]
    public string Correo { get; set; }
}
