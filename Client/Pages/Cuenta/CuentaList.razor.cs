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
    protected List<ExpedienteViewModel> _expediente = new();
    private readonly int[] _pageSizeOptions = { 10, 20, 30, 40, 50 };
    private readonly string _infoFormat = "{first_item}-{last_item} de {all_items}";
    private bool _checkbox;
    private string? _searchString;

    private async Task<TableData<ExpedienteViewModel>> GetCuentaAsync(TableState tableState)
    {
        try
        {
            var parametrosPaginacion = new Dictionary<string, object?>
            {
                { "Buscar", _searchString },
                { "CurrentPage", tableState.Page + 1 },
                { "PageSize", tableState.PageSize }
            };

            var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<ExpedienteViewModel>>
                (Tool.GenerateQueryString(parametrosPaginacion!, _url + "CUENTA")) ?? new();
            _expediente = apiResponse.Items;
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

    private void OnSearch(string text)
    {
        _searchString = text;
        _cuentaTable.ReloadServerData();
    }

    private void NavigateToExpedientePage(ExpedienteViewModel expediente)
    {
        Navigator.NavigateTo($"/Expediente/{expediente.IdExpediente}");
    }

    private void NavigateToReciboPage(ExpedienteViewModel expediente)
    {
        Navigator.NavigateTo($"/Recibo/{expediente.IdCuenta}");
    }

    private void NavigateToEstadoCuentaPage(ExpedienteViewModel expediente)
    {
        Navigator.NavigateTo($"/EstadoCuenta/{expediente.Cuenta.EstadoCuenta.IdEstadoCuenta}");
    }
}
