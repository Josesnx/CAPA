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
    private List<TarifaViewModel> _listTarifa = new();
    private DateTime? _fecha = DateTime.Now;

    protected override async Task OnInitializedAsync()
    {
        _model.Cuenta = new CuentaViewModel()
        {
            Usuario = new UsuarioViewModel(),
            Tarifa = new TarifaViewModel()
        };

        var apiResponseU = await Http!.GetFromJsonAsync<ApiResponseViewModel<UsuarioViewModel>>(_url + "USUARIOS") ?? new();
        _listUsuario = apiResponseU.Items;
        var apiResponseT = await Http!.GetFromJsonAsync<ApiResponseViewModel<TarifaViewModel>>(_url + "TARIFA") ?? new();
        _listTarifa = apiResponseT.Items;
    }

    protected async Task SavePagoAsync()
    {
        _model.Fecha = _fecha!.Value;
        var parametroPago = new Dictionary<string, object?>
        {
            { "Nombre", _model.Cuenta.IdCuenta },
            { "Nombre", _model.NoRecibo },
            { "ApellidoPaterno", _model.Fecha },
            { "ApellidoMaterno", _model.Cantidad }
        };

        var response = await Http!.PostAsJsonAsync(Tool.GenerateQueryString(parametroPago!, _url + "RECIBO"), _model) ?? new();

        if (response.IsSuccessStatusCode)
        {
            SnackBar.Add("Registrado exitosamente", Severity.Success);
            return;
        }
        SnackBar.Add("Ocurrió un error al realizar el pago", Severity.Error);
    }

    private async Task<IEnumerable<UsuarioViewModel>> SearchUsuarios(string valor)
    {

        if (string.IsNullOrEmpty(valor))
            return _listUsuario;

        if (int.TryParse(valor, out int idUsuario))
        {
            //return _listUsuario.Where(u => u.IdUsuario == userId || u.NombreCompleto!.Contains(valor, StringComparison.InvariantCultureIgnoreCase));
            var isNull = _listUsuario.Where(u => u.IdUsuario == idUsuario || u.NombreCompleto!.Contains(valor, StringComparison.InvariantCultureIgnoreCase));
            return isNull.Any() ? isNull : _listUsuario;
        }
        else
        {
            //return _listUsuario.Where(u => u.NombreCompleto!.Contains(valor, StringComparison.InvariantCultureIgnoreCase));
            var isNull = _listUsuario.Where(u => u.NombreCompleto!.Contains(valor, StringComparison.InvariantCultureIgnoreCase));
            return isNull.Any() ? isNull : _listUsuario;
        }
    }

    private async Task CalcularCantidad()
    {
        var apiResponseT = await Http!.GetFromJsonAsync<ApiResponseViewModel<TarifaViewModel>>(_url + $"TARIFA?IdTarifa={_model.Cuenta.Tarifa.IdTarifa}") ?? new();
        _tarifa = apiResponseT.Items.FirstOrDefault()!;

        _model.Cantidad = _tarifa.Precio;
    }
}
