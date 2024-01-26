namespace Client.Data;

public class CuentaViewModel
{
    public int IdCuenta { get; set; }

    public UsuarioViewModel Usuario { get; set; }

    public EstadoCuenta EstadoCuenta { get; set; }

    public decimal Total { get; set; }

    public int Estatus { get; set; }
}
