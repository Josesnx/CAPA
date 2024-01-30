using Client.Data.Herramienta;
using Client.Data;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.EstadoCuenta;

public partial class EstadoCuentaInfo : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    [Parameter]
    public int IdEstadoCuenta { get; set; }

    private const string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private CuentaViewModel _model = new();
    private bool _edit;

    protected override async Task OnInitializedAsync()
    {
        _model.EstadoCuenta = new EstadoCuentaViewModel();
        _model.Usuario = new UsuarioViewModel();

        var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<CuentaViewModel>>(_url + $"ESTADOCUENTA?IdEstadoCuenta={IdEstadoCuenta}") ?? new();
        _model = apiResponse.Items.FirstOrDefault()!;
        var apiResponseC = await Http!.GetFromJsonAsync<ApiResponseViewModel<EstadoCuentaViewModel>>(_url + $"ESTADOCUENTA?IdEstadoCuenta={IdEstadoCuenta}") ?? new();
        _model.EstadoCuenta = apiResponseC.Items.FirstOrDefault()!;
        var apiResponseU = await Http!.GetFromJsonAsync<ApiResponseViewModel<UsuarioViewModel>>(_url + $"ESTADOCUENTA?IdEstadoCuenta={IdEstadoCuenta}") ?? new();
        _model.Usuario = apiResponseU.Items.FirstOrDefault()!;
    }

    private void NavigateToCuentaPage()
    {
        Navigator.NavigateTo("/Cuenta");
    }

    protected async Task EditEstadoCuentaAsync()
    {
        var parametrosEsatdoCuenta = new Dictionary<string, object?>
        {
            { "IdEstadoCuenta", _model.IdEstadoCuenta },
            { "Anio", _model.EstadoCuenta.Anio },
            { "Meses", _model.EstadoCuenta.Meses },
            { "TotalMeses", _model.EstadoCuenta.TotalMeses},
            { "Estatus", _model.EstadoCuenta.Estatus},
            { "Total", _model.Total}
        };

        var response = await Http!.PutAsJsonAsync(Tool.GenerateQueryString(parametrosEsatdoCuenta!, _url + "EXPEDIENTE"), _model) ?? new();

        if (response.IsSuccessStatusCode)
        {
            SnackBar.Add("Se actualizo exitosamente", Severity.Success);
            Navigator.NavigateTo("/Cuenta");
            return;
        }
        SnackBar.Add("Ocurrió un error al actualizar el registro", Severity.Error);
    }
}
