using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace Client.Data;

public class CuentaViewModel
{
    public int IdCuenta { get; set; }

    public int IdUsuario { get; set; }

    public int IdEstadoCuenta { get; set; }

    public int IdTarifa { get; set; }

    public UsuarioViewModel Usuario { get; set; }

    public EstadoCuentaViewModel EstadoCuenta { get; set; }

    public TarifaViewModel Tarifa { get; set; }

    public decimal Total { get; set; }

    [JsonPropertyName("estatuscta")]
    public int Estatus { get; set; }

    public string EstatusString
    {
        get
        {
            return (Estatus == 1) ? "Activo" : "Inactivo";
        }
    }
}
