using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Expediente;

public partial class ExpedienteInfo : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    [Parameter]
    public int IdExpediente { get; set; }

    private const string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private ExpedienteViewModel _model = new();
    private bool _edit;

    protected override async Task OnInitializedAsync()
    {
        _model.TipoToma = new TipoTomaViewModel();
        _model.Usuario = new UsuarioViewModel();
        _model.Cuenta = new CuentaViewModel();

        var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<ExpedienteViewModel>>(_url + $"EXPEDIENTE?IdExpediente={IdExpediente}") ?? new();
        _model = apiResponse.Items.FirstOrDefault()!;
        var apiResponseTt = await Http!.GetFromJsonAsync<ApiResponseViewModel<TipoTomaViewModel>>(_url + $"EXPEDIENTE?IdExpediente={IdExpediente}") ?? new();
        _model.TipoToma = apiResponseTt.Items.FirstOrDefault()!;
        var apiResponseU = await Http!.GetFromJsonAsync<ApiResponseViewModel<UsuarioViewModel>>(_url + $"EXPEDIENTE?IdExpediente={IdExpediente}") ?? new();
        _model.Usuario = apiResponseU.Items.FirstOrDefault()!;
    }

    private void NavigateToCuentaPage()
    {
        Navigator.NavigateTo("/Cuenta");
    }

    protected async Task EditExpedienteAsync()
    {
        var parametroExpediente = new Dictionary<string, object?>
        {
            { "IdExpediente", _model.IdExpediente },
            { "Contrato", _model.Contrato },
            { "Tarjeta", _model.Tarjeta },
            { "NoSolicitud", _model.NoSolicitud},
            { "IdTipoToma", _model.TipoToma.IdTipoToma},
            { "Toma", _model.TipoToma.Toma},
            { "NoToma", _model.TipoToma.NoToma}
        };

        var response = await Http!.PutAsJsonAsync(Tool.GenerateQueryString(parametroExpediente!, _url + "EXPEDIENTE"), _model) ?? new();

        if (response.IsSuccessStatusCode)
        {
            SnackBar.Add("Se actualizo exitosamente", Severity.Success);
            Navigator.NavigateTo("/Cuenta");
            return;
        }
        SnackBar.Add("Ocurrió un error al actualizar el registro", Severity.Error);
    }

    private void NavigateToReciboPage()
    {
        Navigator.NavigateTo($"/Recibo/{_model.IdCuenta}");
    }
}
