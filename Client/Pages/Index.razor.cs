﻿using Client.Data.Herramienta;
using Microsoft.AspNetCore.Components;

namespace Client.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    HttpClient? Http { get; set; }

    bool _errorApi;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ApiResponseViewModel apiResponse = await Http!.GetFromJsonAsync<ApiResponseViewModel>("https://apex.oracle.com/pls/apex/capa/SFA/USUARIOS") ?? new();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                _errorApi = true;
            }
        }
    }
}
