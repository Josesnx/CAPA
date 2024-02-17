using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Usuario;

public partial class UsuarioAdd : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    private readonly string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private readonly DireccionViewModel _model = new();
    private List<EstadoViewModel> _listEstado = new();
    private List<MunicipioViewModel> _listMunicipio = new();
    private List<ColoniaViewModel> _listColonia = new();
    private List<TipoVialidadViewModel> _listVialidad = new();

    protected override async Task OnInitializedAsync()
    {
        _model.Usuario = new UsuarioViewModel();
        _model.TipoVialidad = new TipoVialidadViewModel();
        _model.Colonia = new ColoniaViewModel
        {
            Municipio = new MunicipioViewModel()
            {
                Estado = new EstadoViewModel()
            }
        };

        ApiResponseViewModel<EstadoViewModel> apiResponseE = await Http!.GetFromJsonAsync<ApiResponseViewModel<EstadoViewModel>>(_url + "CAT_E") ?? new();
        _listEstado = apiResponseE.Items;

        ApiResponseViewModel<TipoVialidadViewModel> apiResponseTv = await Http!.GetFromJsonAsync<ApiResponseViewModel<TipoVialidadViewModel>>(_url + "CAT_TV") ?? new();
        _listVialidad = apiResponseTv.Items;
    }

    private void NavigateToUsuarioPage()
    {
        Navigator.NavigateTo("/Usuario");
    }

    protected async Task SaveUsuarioAsync()
    {
        var parametroUsuario = new Dictionary<string, object?>
        {
            { "Nombre", _model.Usuario.Nombre },
            { "ApellidoPaterno", _model.Usuario.ApellidoPaterno },
            { "ApellidoMaterno", _model.Usuario.ApellidoMaterno },
            { "CURP", _model.Usuario.Curp },
            { "RFC", _model.Usuario.Rfc },
            { "Telefono", _model.Usuario.Telefono },
            { "TelefonoFijo", _model.Usuario.TelefonoFijo },
            { "Correo", _model.Usuario.Correo },
            { "IdColonia", _model.Colonia.IdColonia },
            { "IdVialidad", _model.TipoVialidad.IdTipoVialidad },
            { "Calle", _model.Calle },
            { "NumeroInterior", _model.NumeroInterior },
            { "NumeroExterior", _model.NumeroExterior },
            { "Calle1", _model.Calle1 },
            { "Calle2", _model.Calle2}
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
        ApiResponseViewModel<MunicipioViewModel> apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<MunicipioViewModel>>(_url + $"CAT_M?IdEstado={_model.Colonia.Municipio.Estado.IdEstado}") ?? new();
        _listMunicipio = apiResponse.Items;
    }

    protected async Task GetColoniaXMunicipio()
    {
        ApiResponseViewModel<ColoniaViewModel> apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<ColoniaViewModel>>(_url + $"CAT_C?IdMunicipio={_model.Colonia.Municipio.IdMunicipio}") ?? new();
        _listColonia = apiResponse.Items;
    }
}
