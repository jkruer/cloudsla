using System.Net.Http.Json;
using CloudSLAs.Models;

namespace CloudSLAs.Services
{
    public class CloudInfrastructureDataService : ICloudInfrastructureDataService
    {
        private readonly HttpClient _httpClient;
        private List<CloudInfrastructure> _cloudInfrastructures; 

        public CloudInfrastructureDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _cloudInfrastructures = new List<CloudInfrastructure>();
        }

        public async Task<List<CloudInfrastructure>> GetAllCloudComponentss()
        {
            if (_cloudInfrastructures.Any()) return _cloudInfrastructures;

            var cloudComponents = await _httpClient.GetFromJsonAsync<CloudInfrastructure[]>("sample-data/slas.json");
            if (cloudComponents == null) return _cloudInfrastructures;

            _cloudInfrastructures = cloudComponents.OrderBy(m => m.Name).ToList();

            return _cloudInfrastructures;
        }
    }
}