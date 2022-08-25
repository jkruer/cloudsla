using CloudSLAs.Models;

namespace CloudSLAs.Services
{
    public class AppComponentRepository : IAppComponentRepository
    {
        private List<AppComponent> _appComponents = new List<AppComponent>();

        public void AddAppComponent(AppComponent appComponent) => _appComponents.Add(appComponent);

        public void UpdateAppComponent(AppComponent appComponent)
        {
            var component = _appComponents.First(a => a.Id == appComponent.Id);
            component.Name = appComponent.Name;
            component.CloudInfrastructure = appComponent.CloudInfrastructure;
            component.SLA = appComponent.SLA;
            component.HADeployments = appComponent.HADeployments;
        }

        public List<AppComponent> GetAppComponents() => _appComponents;
    }
}