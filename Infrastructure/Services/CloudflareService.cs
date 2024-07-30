using Domain.Repositories.Services;
using Shared.Exceptions.Messages;
using Shared.Exceptions;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace Infrastructure.Services;

public class CloudflareService : ICloudflareService
{
    private readonly HttpClient _httpClient;

    public CloudflareService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> AddDomain(string subdomain)
    {
        try
        {
            Appsettings appsettings = Appsettings.Instance;
            string apiToken = appsettings.GetValue("Cloudflare:APIToken");
            string zoneId = appsettings.GetValue("Cloudflare:ZoneId");

            _httpClient.BaseAddress = new Uri("https://api.cloudflare.com/client/v4/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            var data = new
            {
                type = "CNAME",
                name = subdomain,
                content = appsettings.GetValue("Cloudflare:CNAME"),
                ttl = 1,
                proxied = true
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"zones/{zoneId}/dns_records", content);

            if (!response.IsSuccessStatusCode)
                throw new CustomException(HttpStatusCode.BadRequest, ExceptionMessages.DOMAIN_CONFIGURATION_ISSUE);

            var jsonString = await response.Content.ReadAsStringAsync();
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                string recordId = root.GetProperty("result").GetProperty("id").GetString();
                return recordId ?? String.Empty;
            }
        }
        catch (Exception ex)
        {
            throw new CustomException(HttpStatusCode.BadRequest, ex.Message);
        }
    }

    public async Task DeleteDomain(string recordId)
    {
        try
        {
            Appsettings appsettings = Appsettings.Instance;
            string apiToken = appsettings.GetValue("Cloudflare:APIToken");
            string zoneId = appsettings.GetValue("Cloudflare:ZoneId");

            _httpClient.BaseAddress = new Uri("https://api.cloudflare.com/client/v4/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            var response = await _httpClient.DeleteAsync($"zones/{zoneId}/dns_records/{recordId}");

            if (!response.IsSuccessStatusCode)
                throw new CustomException(HttpStatusCode.BadRequest, ExceptionMessages.DOMAIN_CONFIGURATION_ISSUE);

        }
        catch (Exception ex)
        {
            throw new CustomException(HttpStatusCode.BadRequest, ex.Message);
        }
    }

    public async Task UpdateDomain(string recordId, string newSubdomain)
    {
        try
        {
            Appsettings appsettings = Appsettings.Instance;
            string apiToken = appsettings.GetValue("Cloudflare:APIToken");
            string zoneId = appsettings.GetValue("Cloudflare:ZoneId");


            _httpClient.BaseAddress = new Uri("https://api.cloudflare.com/client/v4/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            var data = new
            {
                type = "CNAME", // The type of DNS record (e.g., CNAME, A, etc.)
                name = newSubdomain, // The new subdomain name
                content = appsettings.GetValue("Cloudflare:CNAME"), // The new target of the CNAME
                ttl = 1, // Time to live (in seconds)
                proxied = true // Whether the record is proxied through Cloudflare
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"zones/{zoneId}/dns_records/{recordId}", content);

            if (!response.IsSuccessStatusCode)
                throw new CustomException(HttpStatusCode.BadRequest, ExceptionMessages.DOMAIN_CONFIGURATION_ISSUE);

        }
        catch (Exception ex)
        {
            throw new CustomException(HttpStatusCode.BadRequest, ex.Message);
        }
    }

}
