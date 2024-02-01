using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Usuario;

public partial class UsuarioEdit : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    [Parameter]
    public int IdUsuario { get; set; }

    private const string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private UsuarioViewModel _model = new();
    private DireccionViewModel _modelDireccion = new();
    private List<EstadoViewModel> _listEstado = new();
    private List<MunicipioViewModel> _listMunicipio = new();
    private List<ColoniaViewModel> _listColonia = new();
    private List<TipoVialidadViewModel> _listVialidad = new();

    protected override async Task OnInitializedAsync()
    {
        _modelDireccion.TipoVialidad = new TipoVialidadViewModel();
        _modelDireccion.Colonia = new ColoniaViewModel
        {
            Municipio = new MunicipioViewModel()
            {
                Estado = new EstadoViewModel()
            }
        };

        var apiResponseU = await Http!.GetFromJsonAsync<ApiResponseViewModel<UsuarioViewModel>>(_url + $"USUARIOS?IdUsuario={IdUsuario}") ?? new();
        _model = apiResponseU.Items.FirstOrDefault()!;
        var apiResponseD = await Http!.GetFromJsonAsync<ApiResponseViewModel<DireccionViewModel>>(_url + $"USUARIOS?IdUsuario={IdUsuario}") ?? new();
        _modelDireccion = apiResponseD.Items.FirstOrDefault()!;
        var apiResponseT = await Http!.GetFromJsonAsync<ApiResponseViewModel<TipoVialidadViewModel>>(_url + $"USUARIOS?IdUsuario={IdUsuario}") ?? new();
        _modelDireccion.TipoVialidad = apiResponseT.Items.FirstOrDefault()!;
        var apiResponseC = await Http!.GetFromJsonAsync<ApiResponseViewModel<ColoniaViewModel>>(_url + $"USUARIOS?IdUsuario={IdUsuario}") ?? new();
        _modelDireccion.Colonia = apiResponseC.Items.FirstOrDefault()!;
        var apiResponseM = await Http!.GetFromJsonAsync<ApiResponseViewModel<MunicipioViewModel>>(_url + $"USUARIOS?IdUsuario={IdUsuario}") ?? new();
        _modelDireccion.Colonia.Municipio = apiResponseM.Items.FirstOrDefault()!;
        var apiResponseEs = await Http!.GetFromJsonAsync<ApiResponseViewModel<EstadoViewModel>>(_url + $"USUARIOS?IdUsuario={IdUsuario}") ?? new();
        _modelDireccion.Colonia.Municipio.Estado = apiResponseEs.Items.FirstOrDefault()!;

        await GetMunicipioXEstado();

        await GetColoniaXMunicipio();

        var apiResponseE = await Http!.GetFromJsonAsync<ApiResponseViewModel<EstadoViewModel>>(_url + "CAT_E") ?? new();
        _listEstado = apiResponseE.Items;

        var apiResponseTv = await Http!.GetFromJsonAsync<ApiResponseViewModel<TipoVialidadViewModel>>(_url + "CAT_TV") ?? new();
        _listVialidad = apiResponseTv.Items;
    }

    private void NavigateToUsuarioPage()
    {
        Navigator.NavigateTo("/Usuario");
    }

    protected async Task EditUsuarioAsync()
    {
        var parametrosUsuario = new Dictionary<string, object?>
        {
            { "IdUsuario", _model.IdUsuario },
            { "Nombre", _model.Nombre },
            { "ApellidoPaterno", _model.ApellidoPaterno },
            { "ApellidoMaterno", _model.ApellidoMaterno },
            { "CURP", _model.Curp },
            { "RFC", _model.Rfc },
            { "Telefono", _model.Telefono },
            { "TelefonoFijo", _model.TelefonoFijo },
            { "Correo", _model.Correo },
            { "IdDireccion", _modelDireccion.IdDireccion },
            { "IdColonia", _modelDireccion.Colonia.IdColonia },
            { "IdVialidad", _modelDireccion.TipoVialidad.IdTipoVialidad },
            { "Calle", _modelDireccion.Calle },
            { "NumeroInterior", _modelDireccion.NumeroInterior },
            { "NumeroExterior", _modelDireccion.NumeroExterior },
            { "Calle1", _modelDireccion.Calle1 },
            { "Calle2", _modelDireccion.Calle2}
        };

        var response = await Http!.PutAsJsonAsync(Tool.GenerateQueryString(parametrosUsuario!, _url + "USUARIOS"), _model) ?? new();

        if (response.IsSuccessStatusCode)
        {
            SnackBar.Add("Se actualizo exitosamente", Severity.Success);
            Navigator.NavigateTo("/Usuario");
            return;
        }
        SnackBar.Add("Ocurrió un error al actualizar el registro", Severity.Error);
    }

    protected async Task GetMunicipioXEstado()
    {
        var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<MunicipioViewModel>>(_url + $"CAT_M?IdEstado={_modelDireccion.Colonia.Municipio.Estado.IdEstado}") ?? new();
        _listMunicipio = apiResponse.Items;
    }

    protected async Task GetColoniaXMunicipio()
    {
        var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<ColoniaViewModel>>(_url + $"CAT_C?IdMunicipio={_modelDireccion.Colonia.Municipio.IdMunicipio}") ?? new();
        _listColonia = apiResponse.Items;
    }
}
