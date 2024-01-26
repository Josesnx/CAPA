using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

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
    private MudTable<CuentaViewModel> _cuentaTable = null!;
    protected List<CuentaViewModel> _cuenta = new();
    protected List<UsuarioViewModel> _usuario = new();
    private readonly int[] _pageSizeOptions = { 10, 20, 30, 40, 50 };
    private readonly string _infoFormat = "{first_item}-{last_item} de {all_items}";

    private async Task<TableData<CuentaViewModel>> GetCuentaAsync(TableState tableState)
    {
        try
        {
            ApiResponseViewModel apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel>(_url + "CUENTA") ?? new();
            _cuenta = apiResponse.Items
                .Select(item => item is JsonElement jsonElement ? JsonSerializer.Deserialize<CuentaViewModel>(jsonElement.GetRawText()) : null)
                .Where(cuenta => cuenta != null).ToList()!;
            _usuario = apiResponse.Items
                .Select(item => item is JsonElement jsonElement ? JsonSerializer.Deserialize<UsuarioViewModel>(jsonElement.GetRawText()) : null)
                .Where(usuario => usuario != null).ToList()!;
            return new TableData<CuentaViewModel>
            {
                Items = _cuenta,
                TotalItems = apiResponse.Count
            };
        }
        catch (Exception)
        {
            SnackBar.Add("Error al obtener las Cuentas", Severity.Error);
        }

        return new TableData<CuentaViewModel>
        {
            Items = Enumerable.Empty<CuentaViewModel>(),
            TotalItems = 0
        };
    }
}
