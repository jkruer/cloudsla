using CloudSLAs.Models;

namespace CloudSLAs.Services
{
    public interface ICloudInfrastructureDataService
    {
        Task<List<CloudInfrastructure>> GetAllCloudComponentss();
    }
}