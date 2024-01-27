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
    protected List<UsuarioViewModel> _usuario = new();
    protected List<ColoniaViewModel> _colonia = new();
    protected List<MunicipioViewModel> _municipio = new();
    protected List<EstadoViewModel> _estado = new();
    private readonly int[] _pageSizeOptions = { 10, 20, 30, 40, 50 };
    private readonly string _infoFormat = "{first_item}-{last_item} de {all_items}";
    private bool _checkbox;

    private async Task<TableData<DireccionViewModel>> GetUsuarioAsync(TableState tableState)
    {
        try
        {
            var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<DireccionViewModel>>(_url + "USUARIOS") ?? new();
            var apiResponseU = await Http!.GetFromJsonAsync<ApiResponseViewModel<UsuarioViewModel>>(_url + "USUARIOS") ?? new();
            var apiResponseC = await Http!.GetFromJsonAsync<ApiResponseViewModel<ColoniaViewModel>>(_url + "USUARIOS") ?? new();
            var apiResponseM = await Http!.GetFromJsonAsync<ApiResponseViewModel<MunicipioViewModel>>(_url + "USUARIOS") ?? new();
            var apiResponseE = await Http!.GetFromJsonAsync<ApiResponseViewModel<EstadoViewModel>>(_url + "USUARIOS") ?? new();
            _direccion = apiResponse.Items;
            _usuario = apiResponseU.Items;
            _colonia = apiResponseC.Items;
            _municipio = apiResponseM.Items;
            _estado = apiResponseE.Items;

            foreach (var direccion in _direccion)
            {
                direccion.Usuario = _usuario.Find(u => u.IdUsuario == direccion.IdUsuario)!;
                direccion.Colonia = _colonia.Find(c => c.IdColonia == direccion.IdColonia)!;

                if (direccion.Colonia != null)
                {
                    direccion.Colonia.Municipio = _municipio.Find(m => m.IdMunicipio == direccion.Colonia.IdMunicipio)!;

                    if (direccion.Colonia.Municipio != null)
                    {
                        direccion.Colonia.Municipio.Estado = _estado.Find(e => e.IdEstado == direccion.Colonia.Municipio.IdEstado)!;
                    }
                }
            }

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

    private async void NavigateToEditUsuarioPage(DireccionViewModel model)
    {
        Navigator.NavigateTo($"/Usuario/Edit/{model.Usuario.IdUsuario}");
    }
}
