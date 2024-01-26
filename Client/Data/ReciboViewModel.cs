namespace Client.Data;

public class ReciboViewModel
{
    public int IdRecibo { get; set; }

    public ExpedienteViewModel Expediente { get; set; }

    public CuentaViewModel Cuenta { get; set; }

    public string? NoRecibo { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Cantidad { get; set; }
}
