using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Usuario;

public partial class UsuarioList : ComponentBase
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
    private MudTable<DireccionViewModel> _usuarioTable = null!;
    protected List<DireccionViewModel> _direccion = new();
    private readonly int[] _pageSizeOptions = { 10, 20, 30, 40, 50 };
    private readonly string _infoFormat = "{first_item}-{last_item} de {all_items}";
    private bool _checkbox;
    private string? _searchString;

    private async Task<TableData<DireccionViewModel>> GetUsuarioAsync(TableState tableState)
    {
        try
        {
            var parametrosPaginacion = new Dictionary<string, object?>
            {
                { "Buscar", _searchString },
                { "CurrentPage", tableState.Page + 1 },
                { "PageSize", tableState.PageSize }
            };

            var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<DireccionViewModel>>
                (Tool.GenerateQueryString(parametrosPaginacion!, _url + "USUARIOS")) ?? new();
            _direccion = apiResponse.Items;

            return new TableData<DireccionViewModel>
            {
                Items = _direccion,
                TotalItems = apiResponse.Count
            };
        }
        catch (Exception)
        {
            SnackBar.Add("Error al obtener los Usuarios", Severity.Error);
        }

        return new TableData<DireccionViewModel>
        {
            Items = Enumerable.Empty<DireccionViewModel>(),
            TotalItems = 0
        };
    }

    private void NavigateToAddUsuarioPage()
    {
        Navigator.NavigateTo("/Usuario/Add");
    }

    private void NavigateToEditUsuarioPage(DireccionViewModel model)
    {
        Navigator.NavigateTo($"/Usuario/Edit/{model.Usuario.IdUsuario}");
    }

    private void OnSearch(string text)
    {
        _searchString = text;
        _usuarioTable.ReloadServerData();
    }
}
