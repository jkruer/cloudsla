using CloudSLAs.Models;

namespace CloudSLAs.Services
{
    public interface IAppComponentRepository
    {
        public List<AppComponent> GetAppComponents();
        public void AddAppComponent(AppComponent appComponent);
        public void UpdateAppComponent(AppComponent appComponent);
    }
}