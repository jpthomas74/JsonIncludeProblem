using LearnData;
using LearnMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LearnMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient _httpGatewayClient;
        private JsonSerializerOptions _options;

        public HomeController(ILogger<HomeController> logger)
        {
            _httpGatewayClient = new HttpClient();
            _httpGatewayClient.BaseAddress = new Uri("https://localhost:44333/api/Customers/");
            _logger = logger;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy(int isIncluded)
        {
            if (isIncluded == 1)
            {
                var includedData = await GetEnumerableDataAsyncBySend("getincluded", null);

                ViewBag.IncludedData = includedData;
            }
            else
            {
                var excludedData = await GetEnumerableDataAsyncBySend("getexcluded", null);

                ViewBag.ExcludedData = excludedData;
            }

            return View();

        }

    
        private async Task<IEnumerable<Customer>> GetEnumerableDataAsyncBySend(string apiEndpoint, object[] parameters)
        {
            var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync(ms, parameters);
            ms.Seek(0, SeekOrigin.Begin);
            var request = new HttpRequestMessage(HttpMethod.Post, apiEndpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var requestContent = new StreamContent(ms))
            {
                request.Content = requestContent;
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var response = await _httpGatewayClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStreamAsync();
                        var data = await JsonSerializer.DeserializeAsync<IEnumerable<Customer>>(content, _options);
                        return data;
                    }
                    else
                    {
                        throw new Exception($"Error getting data from {apiEndpoint}. Error status is {response.StatusCode}");
                    }
                }
            }
        }

    }
}
