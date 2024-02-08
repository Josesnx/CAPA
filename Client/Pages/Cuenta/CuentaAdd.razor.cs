using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Cuenta;

public partial class CuentaAdd : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    private readonly string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private readonly ExpedienteViewModel _expediente = new();
    private UsuarioViewModel _usuario = new();
    private List<UsuarioViewModel> _listUsuario = new();
    private List<TipoTomaViewModel> _listTipoToma = new();
    private List<TarifaViewModel> _listTarifa = new();
    private bool _nuevaCuenta = true;

    protected override async Task OnInitializedAsync()
    {
        _expediente.TipoToma = new TipoTomaViewModel();
        _expediente.Usuario = new UsuarioViewModel();
        _expediente.Cuenta = new CuentaViewModel()
        {
            Tarifa = new TarifaViewModel(),
            EstadoCuenta = new EstadoCuentaViewModel()
        };

        var apiResponseU = await Http!.GetFromJsonAsync<ApiResponseViewModel<UsuarioViewModel>>(_url + "USUARIO_SIN_C") ?? new();
        _listUsuario = apiResponseU.Items;
        var apiResponseTt = await Http!.GetFromJsonAsync<ApiResponseViewModel<TipoTomaViewModel>>(_url + "TIPO_TOMA") ?? new();
        _listTipoToma = apiResponseTt.Items;
        var apiResponseT = await Http!.GetFromJsonAsync<ApiResponseViewModel<TarifaViewModel>>(_url + "TARIFA") ?? new();
        _listTarifa = apiResponseT.Items;
    }

    private void NavigateToCuentaPage()
    {
        Navigator.NavigateTo("/Cuenta");
    }

    protected async Task SaveCuentaAsync()
    {
        var parametroCuenta = new Dictionary<string, object?>
        {
            { "IdUsuario", _expediente.Usuario.IdUsuario },
            { "Toma", _expediente.TipoToma.Toma},
            { "NoToma", _expediente.TipoToma.NoToma},
            { "IdTarifa", _expediente.Cuenta.Tarifa.IdTarifa},
            { "Contrato", _expediente.Contrato },
            { "Tarjeta", _expediente.Tarjeta },
            { "NoSolicitud", _expediente.NoSolicitud },
            { "Total", _expediente.Cuenta.Total },
            { "Anio", _expediente.Cuenta.EstadoCuenta.Anio },
            { "Meses", _expediente.Cuenta.EstadoCuenta.Meses },
            { "TotalMeses", _expediente.Cuenta.EstadoCuenta.TotalMeses }
        };

        var response = await Http!.PostAsJsonAsync(Tool.GenerateQueryString(parametroCuenta!, _url + "CUENTA"), _expediente) ?? new();

        if (response.IsSuccessStatusCode)
        {
            SnackBar.Add("Agregado exitosamente", Severity.Success);
            Navigator.NavigateTo("/Cuenta");
            return;
        }
        SnackBar.Add("Ocurrió un error al agregar el registro", Severity.Error);
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
}
