using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using BlazorJwtAuthDemo.Client;
using BlazorJwtAuthDemo.Client.Shared;
using BlazorJwtAuthDemo.Shared;
using System.Net.Http.Headers;

namespace BlazorJwtAuthDemo.Client.Pages
{
    public partial class FetchData
    {
        private WeatherForecast[]? forecasts;
        protected override async Task OnInitializedAsync()
        {
            var ul = new UserLogins { UserName = "Admin", Password = "Admin" };

            var tokenRes = await Http.PostAsJsonAsync("api/Account/GetToken", ul, CancellationToken.None);

            if (tokenRes.IsSuccessStatusCode)
            {
                var userTokens = await tokenRes.Content.ReadFromJsonAsync<UserTokens>();
                Http.DefaultRequestHeaders.Clear();
                //Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userTokens.Token);
                Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {userTokens.Token}");
                forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
            }
        }
    }
}