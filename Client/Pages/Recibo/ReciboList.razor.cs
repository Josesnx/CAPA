using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Recibo;

public partial class ReciboList : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    [Parameter]
    public int IdCuenta { get; set; }

    private const string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private MudTable<ReciboViewModel> _reciboTable = null!;
    protected List<ReciboViewModel> _recibo = new();
    protected List<Abono> _totalAbono = new();
    private readonly int[] _pageSizeOptions = { 10, 20, 30, 40, 50 };
    private readonly string _infoFormat = "{first_item}-{last_item} de {all_items}";
    private decimal _SumCantidad;

    private async Task<TableData<ReciboViewModel>> GetReciboAsync(TableState tableState)
    {
        try
        {
            var parametrosPaginacion = new Dictionary<string, object?>
            {
                { "IdCuenta", IdCuenta },
                { "CurrentPage", tableState.Page + 1 },
                { "PageSize", tableState.PageSize }
            };

            var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<ReciboViewModel>>(Tool.GenerateQueryString(parametrosPaginacion!, _url + "RECIBO")) ?? new();
            _recibo = apiResponse.Items;
            var apiResponseAbono = await Http!.GetFromJsonAsync<ApiResponseViewModel<Abono>>(Tool.GenerateQueryString(parametrosPaginacion!, _url + "RECIBO_ABONO_TOTAL")) ?? new();
            _totalAbono = apiResponseAbono.Items;
            foreach (var abono in _totalAbono)
            {
                _SumCantidad = abono.AbonoTotal;
            }

            return new TableData<ReciboViewModel>
            {
                Items = _recibo.OrderBy(r => r.Fecha).ToList(),
                TotalItems = apiResponse.Count
            };
        }
        catch (Exception)
        {
            SnackBar.Add("Error al obtener los Recibos", Severity.Error);
        }

        return new TableData<ReciboViewModel>
        {
            Items = Enumerable.Empty<ReciboViewModel>(),
            TotalItems = 0
        };
    }

    private void NavigateToCuentaPage()
    {
        Navigator.NavigateTo("/Cuenta");
    }
}
