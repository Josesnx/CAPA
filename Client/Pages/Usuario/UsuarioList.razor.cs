using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

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
    private MudTable<UsuarioViewModel> _usuarioTable = null!;
    protected List<UsuarioViewModel> _usuario = new();
    private readonly int[] _pageSizeOptions = { 10, 20, 30, 40, 50 };
    private readonly string _infoFormat = "{first_item}-{last_item} de {all_items}";
    private bool _checkbox;

    private async Task<TableData<UsuarioViewModel>> GetUsuarioAsync(TableState tableState)
    {
        try
        {
            ApiResponseViewModel apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel>(_url + "USUARIOS") ?? new();
            _usuario = apiResponse.Items
                .Select(item => item is JsonElement jsonElement ? JsonSerializer.Deserialize<UsuarioViewModel>(jsonElement.GetRawText()) : null)
                .Where(usuario => usuario != null).ToList()!;
            return new TableData<UsuarioViewModel>
            {
                Items = _usuario,
                TotalItems = apiResponse.Count
            };
        }
        catch (Exception)
        {
            SnackBar.Add("Error al obtener los Usuarios", Severity.Error);
        }

        return new TableData<UsuarioViewModel>
        {
            Items = Enumerable.Empty<UsuarioViewModel>(),
            TotalItems = 0
        };
    }

    private void NavigateToAddUsuarioPage()
    {
        Navigator.NavigateTo("/Usuario/Add");
    }

    private async void NavigateToEditUsuarioPage(UsuarioViewModel model)
    {
        Navigator.NavigateTo($"/Usuario/Edit/{model.IdUsuario}");
    }
}
