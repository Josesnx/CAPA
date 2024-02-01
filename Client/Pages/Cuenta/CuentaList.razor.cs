using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Cuenta;

public partial class CuentaList : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    private const string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private MudTable<ExpedienteViewModel> _cuentaTable = null!;
    protected List<CuentaViewModel> _cuenta = new();
    protected List<UsuarioViewModel> _usuario = new();
    protected List<ExpedienteViewModel> _expediente = new();
    protected List<TipoTomaViewModel> _tipoToma = new();
    protected List<TarifaViewModel> _tarifa = new();
    protected List<EstadoCuentaViewModel> _estadoCuenta = new();
    private readonly int[] _pageSizeOptions = { 10, 20, 30, 40, 50 };
    private readonly string _infoFormat = "{first_item}-{last_item} de {all_items}";
    private bool _checkbox;

    private async Task<TableData<ExpedienteViewModel>> GetCuentaAsync(TableState tableState)
    {
        try
        {
            var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<CuentaViewModel>>(_url + "CUENTA") ?? new();
            var apiResponseU = await Http!.GetFromJsonAsync<ApiResponseViewModel<UsuarioViewModel>>(_url + "CUENTA") ?? new();
            var apiResponseE = await Http!.GetFromJsonAsync<ApiResponseViewModel<ExpedienteViewModel>>(_url + "CUENTA") ?? new();
            var apiResponseTt = await Http!.GetFromJsonAsync<ApiResponseViewModel<TipoTomaViewModel>>(_url + "CUENTA") ?? new();
            var apiResponseTa = await Http!.GetFromJsonAsync<ApiResponseViewModel<TarifaViewModel>>(_url + "CUENTA") ?? new();
            var apiResponseEc = await Http!.GetFromJsonAsync<ApiResponseViewModel<EstadoCuentaViewModel>>(_url + "CUENTA") ?? new();
            _cuenta = apiResponse.Items;
            _usuario = apiResponseU.Items;
            _expediente = apiResponseE.Items;
            _tipoToma = apiResponseTt.Items;
            _tarifa = apiResponseTa.Items;
            _estadoCuenta = apiResponseEc.Items;

            foreach (var expediente in _expediente)
            {
                expediente.Usuario = _usuario.Find(u => u.IdUsuario == expediente.IdUsuario)!;
                expediente.Cuenta = _cuenta.Find(c => c.IdCuenta == expediente.IdCuenta)!;
                expediente.TipoToma = _tipoToma.Find(tt => tt.IdTipoToma == expediente.IdTipoToma)!;

                if (expediente.Cuenta != null)
                {
                    expediente.Cuenta.Tarifa = _tarifa.Find(t => t.IdTarifa == expediente.Cuenta.IdTarifa)!;
                    expediente.Cuenta.EstadoCuenta = _estadoCuenta.Find(ec => ec.IdEstadoCuenta == expediente.Cuenta.IdEstadoCuenta)!;
                }
            }
            return new TableData<ExpedienteViewModel>
            {
                Items = _expediente,
                TotalItems = apiResponse.Count
            };
        }
        catch (Exception)
        {
            SnackBar.Add("Error al obtener las Cuentas", Severity.Error);
        }

        return new TableData<ExpedienteViewModel>
        {
            Items = Enumerable.Empty<ExpedienteViewModel>(),
            TotalItems = 0
        };
    }

    private void NavigateToAddCuentaPage()
    {
        Navigator.NavigateTo("/Cuenta/Add");
    }
    
    private void NavigateToExpedientePage(ExpedienteViewModel expediente)
    {
        Navigator.NavigateTo($"/Expediente/{expediente.IdExpediente}");
    }
    
    private void NavigateToReciboPage(ExpedienteViewModel expediente)
    {
        Navigator.NavigateTo($"/Recibo/{expediente.IdCuenta}");
    }

    private async void NavigateToEstadoCuentaPage(ExpedienteViewModel expediente)
    {
        Navigator.NavigateTo($"/EstadoCuenta/{expediente.Cuenta.EstadoCuenta.IdEstadoCuenta}");
    }
}
