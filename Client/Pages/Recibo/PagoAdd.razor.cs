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
    private List<UsuarioViewModel> _listUsuario = new();
    private List<CuentaViewModel> _listCuenta = new();
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

        var apiResponseC = await Http!.GetFromJsonAsync<ApiResponseViewModel<CuentaViewModel>>(_url + "CUENTA") ?? new();
        var apiResponseU = await Http!.GetFromJsonAsync<ApiResponseViewModel<UsuarioViewModel>>(_url + "CUENTA") ?? new();
        _listUsuario = apiResponseU.Items;
        _listCuenta = apiResponseC.Items;
        foreach (var cuenta in _listCuenta)
        {
            cuenta.Usuario = _listUsuario.Find(u => u.IdUsuario == cuenta.IdUsuario)!;
        }
        var apiResponseT = await Http!.GetFromJsonAsync<ApiResponseViewModel<TarifaViewModel>>(_url + "TARIFA") ?? new();
        _listTarifa = apiResponseT.Items;
    }

    protected async Task SavePagoAsync()
    {
        var cuenta = _listCuenta.Find(c => _listUsuario.Exists(u => u.IdUsuario == c.IdUsuario));
        if (cuenta != null)
        {
            _model.Cuenta.IdCuenta = cuenta.IdCuenta;
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
            return _listUsuario;

        if (int.TryParse(valor, out int idUsuario))
        {
            var isNull = _listUsuario.Where(u => u.IdUsuario == idUsuario || u.NombreCompleto!.Contains(valor, StringComparison.InvariantCultureIgnoreCase));
            return isNull.Any() ? isNull : _listUsuario;
        }
        else
        {
            var isNull = _listUsuario.Where(u => u.NombreCompleto!.Contains(valor, StringComparison.InvariantCultureIgnoreCase));
            return isNull.Any() ? isNull : _listUsuario;
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
