using Domain.Repositories.Services;
using Shared.Exceptions.Messages;
using Shared.Exceptions;
using System.Net;
using System.Net.Http.Headers;
using System.Text;


namespace Infrastructure.Services
{
    public class CloudflareService : ICloudflareService
    {
        public async Task DomainConfig(string subdomain)
        {
            try
            {
                Appsettings appsettings = Appsettings.Instance;
                string apiToken = appsettings.GetValue("Cloudflare:APIToken"); 
                string zoneId = appsettings.GetValue("Cloudflare:ZoneId"); 
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.cloudflare.com/client/v4/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

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

                    var response = await client.PostAsync($"zones/{zoneId}/dns_records", content);

                    if (!response.IsSuccessStatusCode)
                        throw new CustomException(HttpStatusCode.BadRequest, ExceptionMessages.DOMAIN_CONFIGURATION_ISSUE);
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
