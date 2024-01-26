using Client.Data.Herramienta;
using Client.Data;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

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

    private UsuarioViewModel _model = new();
    private DireccionViewModel _modelDireccion = new();
    private List<EstadoViewModel> _listEstado = new();
    private List<MunicipioViewModel> _listMunicipio = new();
    private List<ColoniaViewModel> _listColonia = new();
    private List<TipoVialidadViewModel> _listVialidad = new();
    private const string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";

    protected override async Task OnInitializedAsync()
    {
        ApiResponseViewModel apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel>(_url + $"USUARIOS?IdUsuario={IdUsuario}") ?? new();
        _model = apiResponse.Items?.OfType<JsonElement>().Select(item => JsonSerializer.Deserialize<UsuarioViewModel>(item.GetRawText())).FirstOrDefault() ?? new UsuarioViewModel();
        _modelDireccion = apiResponse.Items?.OfType<JsonElement>().Select(item => JsonSerializer.Deserialize<DireccionViewModel>(item.GetRawText())).FirstOrDefault() ?? new DireccionViewModel();
        _modelDireccion.Colonia.Municipio.Estado = new EstadoViewModel();
        _modelDireccion.Colonia.Municipio.Estado = apiResponse.Items?.OfType<JsonElement>().Select(item => JsonSerializer.Deserialize<EstadoViewModel>(item.GetRawText())).FirstOrDefault() ?? new EstadoViewModel();
        _modelDireccion.Colonia.Municipio = new MunicipioViewModel();
        _modelDireccion.Colonia.Municipio = apiResponse.Items?.OfType<JsonElement>().Select(item => JsonSerializer.Deserialize<MunicipioViewModel>(item.GetRawText())).FirstOrDefault() ?? new MunicipioViewModel();
        _modelDireccion.Colonia = new ColoniaViewModel();
        _modelDireccion.Colonia = apiResponse.Items?.OfType<JsonElement>().Select(item => JsonSerializer.Deserialize<ColoniaViewModel>(item.GetRawText())).FirstOrDefault() ?? new ColoniaViewModel();
        _modelDireccion.TipoVialidad = new TipoVialidadViewModel();
        _modelDireccion.TipoVialidad = apiResponse.Items?.OfType<JsonElement>().Select(item => JsonSerializer.Deserialize<TipoVialidadViewModel>(item.GetRawText())).FirstOrDefault() ?? new TipoVialidadViewModel();

        ApiResponseViewModel apiResponseE = await Http!.GetFromJsonAsync<ApiResponseViewModel>(_url + "CAT_E") ?? new();
        _listEstado = apiResponseE.Items
                .Select(item => item is JsonElement jsonElement ? JsonSerializer.Deserialize<EstadoViewModel>(jsonElement.GetRawText()) : null)
                .Where(estado => estado != null).ToList()!;

        ApiResponseViewModel apiResponseTv = await Http!.GetFromJsonAsync<ApiResponseViewModel>(_url + "CAT_TV") ?? new();
        _listVialidad = apiResponseTv.Items
                .Select(item => item is JsonElement jsonElement ? JsonSerializer.Deserialize<TipoVialidadViewModel>(jsonElement.GetRawText()) : null)
                .Where(vialidad => vialidad != null).ToList()!;
    }

    private void NavigateToUsuarioPage()
    {
        Navigator.NavigateTo("/Usuario");
    }

    protected async Task EditUsuarioAsync()
    {
        var parametrosPaginacion = new Dictionary<string, object?>
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

        var response = await Http!.PutAsJsonAsync(Tool.GenerateQueryString(parametrosPaginacion!, _url + "USUARIOS"), _model) ?? new();

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
        ApiResponseViewModel apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel>(_url + $"CAT_M?IdEstado={_modelDireccion.Colonia.Municipio.Estado.IdEstado}") ?? new();
        _listMunicipio = apiResponse.Items
                .Select(item => item is JsonElement jsonElement ? JsonSerializer.Deserialize<MunicipioViewModel>(jsonElement.GetRawText()) : null)
                .Where(estado => estado != null).ToList()!;
    }

    protected async Task GetColoniaXMunicipio()
    {
        ApiResponseViewModel apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel>(_url + $"CAT_C?IdMunicipio={_modelDireccion.Colonia.Municipio.IdMunicipio}") ?? new();
        _listColonia = apiResponse.Items
                .Select(item => item is JsonElement jsonElement ? JsonSerializer.Deserialize<ColoniaViewModel>(jsonElement.GetRawText()) : null)
                .Where(colonia => colonia != null).ToList()!;
    }
}
