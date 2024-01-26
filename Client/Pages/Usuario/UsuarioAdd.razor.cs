using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.Json;

namespace Client.Pages.Usuario;

public partial class UsuarioAdd : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    private readonly UsuarioViewModel _model = new();
    private readonly DireccionViewModel _modelDireccion = new();
    private List<EstadoViewModel> _listEstado = new();
    private List<MunicipioViewModel> _listMunicipio = new();
    private List<ColoniaViewModel> _listColonia = new();
    private List<TipoVialidadViewModel> _listVialidad = new();
    private readonly string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";

    protected override async Task OnInitializedAsync()
    {
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

    protected async Task SaveUsuarioAsync()
    {
        var parametroUsuario = new Dictionary<string, object?>
        {
            { "Nombre", _model.Nombre },
            { "ApellidoPaterno", _model.ApellidoPaterno },
            { "ApellidoMaterno", _model.ApellidoMaterno },
            { "CURP", _model.Curp },
            { "RFC", _model.Rfc },
            { "Telefono", _model.Telefono },
            { "TelefonoFijo", _model.TelefonoFijo },
            { "Correo", _model.Correo },
            { "IdColonia", _modelDireccion.Colonia.IdColonia },
            { "IdVialidad", _modelDireccion.TipoVialidad.IdTipoVialidad },
            { "Calle", _modelDireccion.Calle },
            { "NumeroInterior", _modelDireccion.NumeroInterior },
            { "NumeroExterior", _modelDireccion.NumeroExterior },
            { "Calle1", _modelDireccion.Calle1 },
            { "Calle2", _modelDireccion.Calle2}
        };

        var response = await Http!.PostAsJsonAsync(Tool.GenerateQueryString(parametroUsuario!, _url + "USUARIOS"), _model) ?? new();

        if (response.IsSuccessStatusCode)
        {
            SnackBar.Add("Agregado exitosamente", Severity.Success);
            Navigator.NavigateTo("/Usuario");
            return;
        }
        SnackBar.Add("Ocurrió un error al agregar el registro", Severity.Error);
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
