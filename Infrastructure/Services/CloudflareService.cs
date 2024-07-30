using Domain.Repositories.Services;
using System.Net.Http.Headers;
using System.Text;


namespace Infrastructure.Services
{
    public class CloudflareService : ICloudflareService
    {
        public async Task<bool> DomainConfig(string Domain, string CName)
        {
            string apiToken = "c_cU62m8QyZgqEcuJ1dgSSFmt7uRTbZdAq7waE95"; // Replace with your API token
            string zoneId = "86422e5b473c4a0f0f12cee049e27fe6"; // Replace with your Zone ID
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.cloudflare.com/client/v4/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

                var data = new
                {
                    type = "CNAME",
                    name = Domain,
                    content = CName,
                    ttl = 1, // Set TTL to 1 to indicate 'auto' in Cloudflare
                    proxied = true // Set to true to enable Cloudflare's proxy
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"zones/{zoneId}/dns_records", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error creating CNAME record: {result}");
                    return false;
                }
            }
        }
    }
}
