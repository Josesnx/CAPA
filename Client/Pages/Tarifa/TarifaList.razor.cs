using Client.Data;
using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Pages.Tarifa;

public partial class TarifaList : ComponentBase
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
    private MudTable<TarifaViewModel> _tarifaTable = null!;
    protected List<TarifaViewModel> _tarifa = new();
    private readonly int[] _pageSizeOptions = { 10, 20, 30, 40, 50 };
    private readonly string _infoFormat = "{first_item}-{last_item} de {all_items}";
    private bool _checkbox;

    private async Task<TableData<TarifaViewModel>> GetTarifaAsync(TableState tableState)
    {
        try
        {
            var apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel<TarifaViewModel>>(_url + "TARIFA") ?? new();
            _tarifa = apiResponse.Items;

            return new TableData<TarifaViewModel>
            {
                Items = _tarifa,
                TotalItems = apiResponse.Count
            };
        }
        catch (Exception)
        {
            SnackBar.Add("Error al obtener los Usuarios", Severity.Error);
        }

        return new TableData<TarifaViewModel>
        {
            Items = Enumerable.Empty<TarifaViewModel>(),
            TotalItems = 0
        };
    }

    private void NavigateToAddTarifaPage()
    {
        Navigator.NavigateTo("/Tarifa/Add");
    }

    private async void NavigateToEditTarifaPage(TarifaViewModel model)
    {
        Navigator.NavigateTo($"/Tarifa/Edit/{model.IdTarifa}");
    }

    protected async Task DeleteTarifaAsync(TarifaViewModel model)
    {
        var dialogoResultado = await DialogService.ShowMessageBox("Advertencia", $"¿Está seguro de inactivar la Tarifa {model.Tipo}?",
            yesText: "Inactivar".ToLower(), cancelText: "Cancelar".ToLower(), options: new DialogOptions { FullWidth = true });


        if (dialogoResultado.GetValueOrDefault())
        {
            var response = await Http!.DeleteAsync(_url + $"TARIFA?IdTarifa={model.IdTarifa}");

            if (response.IsSuccessStatusCode)
            {
                SnackBar.Add("Se actualizo exitosamente", Severity.Success);
                await _tarifaTable.ReloadServerData();
                return;
            }

            SnackBar.Add("Ocurrió un error al inactivar el registro", Severity.Error);
        }
    }
}
