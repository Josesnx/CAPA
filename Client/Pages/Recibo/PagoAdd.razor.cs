using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Recibo;

public partial class PagoAdd : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    private readonly string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private readonly ReciboViewModel _model = new();
    private TarifaViewModel _tarifa = new();
    private List<ExpedienteViewModel> _listExpediente = new();
    private List<TarifaViewModel> _listTarifa = new();

    protected override async Task OnInitializedAsync()
    {
        _model.Fecha = DateTime.Now;
        _model.Cuenta = new CuentaViewModel()
        {
            Usuario = new UsuarioViewModel(),
            Tarifa = new TarifaViewModel(),
            EstadoCuenta = new EstadoCuentaViewModel() { Meses = 1 }
        };

        var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<ExpedienteViewModel>>(_url + "CUENTA") ?? new();
        _listExpediente = apiResponse.Items;
        var apiResponseT = await Http!.GetFromJsonAsync<ApiResponseViewModel<TarifaViewModel>>(_url + "TARIFA?Estatus=1") ?? new();
        _listTarifa = apiResponseT.Items;
    }

    protected async Task SavePagoAsync()
    {
        var expediente = _listExpediente.Find(e => e.Usuario.IdUsuario == _model.Cuenta.Usuario.IdUsuario);
        if (expediente != null)
        {
            _model.Cuenta.IdCuenta = expediente.Cuenta.IdCuenta;
        }

        var parametroPago = new Dictionary<string, object?>
        {
            { "IdCuenta", _model.Cuenta.IdCuenta },
            { "NoRecibo", _model.NoRecibo },
            { "Fecha", _model.Fecha!.Value.ToString("dd-MM-yyyy") },
            { "Cantidad", _model.Cantidad },
            { "Meses", _model.Cuenta.EstadoCuenta.Meses }
        };

        var response = await Http!.PostAsJsonAsync(Tool.GenerateQueryString(parametroPago!, _url + "RECIBO"), _tarifa) ?? new();
        if (response.IsSuccessStatusCode)
        {
            SnackBar.Add("Registrado exitosamente", Severity.Success);
            return;
        }
        SnackBar.Add("Ocurrió un error al realizar el pago", Severity.Error);
    }

    private async Task<IEnumerable<UsuarioViewModel>> SearchCuentas(string valor)
    {
        if (string.IsNullOrEmpty(valor))
            return _listExpediente.Select(u => u.Usuario);

        if (int.TryParse(valor, out int idUsuario))
        {
            var isNull = _listExpediente.Where(u => u.Usuario.IdUsuario == idUsuario || u.Usuario.NombreCompleto!.Contains(valor, StringComparison.InvariantCultureIgnoreCase)).Select(u => u.Usuario);
            return isNull.Any() ? isNull : _listExpediente.Select(u => u.Usuario);
        }
        else
        {
            var isNull = _listExpediente.Where(u => u.Usuario.NombreCompleto!.Contains(valor, StringComparison.InvariantCultureIgnoreCase)).Select(u => u.Usuario);
            return isNull.Any() ? isNull : _listExpediente.Select(u => u.Usuario);
        }
    }

    private async Task CalcularCantidad()
    {
        var apiResponseT = await Http!.GetFromJsonAsync<ApiResponseViewModel<TarifaViewModel>>(_url + $"TARIFA?IdTarifa={_model.Cuenta.Tarifa.IdTarifa}") ?? new();
        _tarifa = apiResponseT.Items.FirstOrDefault()!;

        _model.Cantidad = _tarifa.Precio;

        CalcularCantidadNMeses();
    }

    private void CalcularCantidadNMeses()
    {
        _model.Cantidad = _tarifa.Precio * _model.Cuenta.EstadoCuenta.Meses;
    }
}
