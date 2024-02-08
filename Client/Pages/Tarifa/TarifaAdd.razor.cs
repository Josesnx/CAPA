using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Tarifa;

public partial class TarifaAdd : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    private readonly string _url = "https://apex.oracle.com/pls/apex/capa/SFA/";
    private readonly TarifaViewModel _model = new()
    {
        Anio = DateTime.Now.Year
    };

    private void NavigateToTarifaPage()
    {
        Navigator.NavigateTo("/Tarifa");
    }

    protected async Task SaveTarifaAsync()
    {
        var parametroTarifa = new Dictionary<string, object?>
        {
            { "Tipo", _model.Tipo },
            { "Anio", _model.Anio},
            { "Precio", _model.Precio}
        };

        var response = await Http!.PostAsJsonAsync(Tool.GenerateQueryString(parametroTarifa!, _url + "TARIFA"), _model) ?? new();

        if (response.IsSuccessStatusCode)
        {
            SnackBar.Add("Agregado exitosamente", Severity.Success);
            Navigator.NavigateTo("/Tarifa");
            return;
        }
        SnackBar.Add("Ocurrió un error al agregar el registro", Severity.Error);
    }
}
