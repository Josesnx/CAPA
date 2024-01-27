using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Tarifa;

public partial class TarifaEdit : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    [Parameter]
    public int IdTarifa { get; set; }

    private const string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private TarifaViewModel _model = new();

    protected override async Task OnInitializedAsync()
    {
        var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<TarifaViewModel>>(_url + $"TARIFA?IdTarifa={IdTarifa}") ?? new();
        _model = apiResponse.Items.FirstOrDefault()!;
    }

    private void NavigateToTarifaPage()
    {
        Navigator.NavigateTo("/Tarifa");
    }

    protected async Task EditTarifaAsync()
    {
        var parametroTarifa = new Dictionary<string, object?>
        {
            { "IdTarifa", _model.IdTarifa },
            { "Tipo", _model.Tipo },
            { "Anio", _model.Anio},
            { "Precio", _model.Precio},
            { "Estatus", _model.Estatus}
        };

        var response = await Http!.PutAsJsonAsync(Tool.GenerateQueryString(parametroTarifa!, _url + "TARIFA"), _model) ?? new();

        if (response.IsSuccessStatusCode)
        {
            SnackBar.Add("Se actualizo exitosamente", Severity.Success);
            Navigator.NavigateTo("/Tarifa");
            return;
        }
        SnackBar.Add("Ocurrió un error al actualizar el registro", Severity.Error);
    }
}
