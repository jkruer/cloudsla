using CloudSLAs.Components;
using CloudSLAs.Models;
using CloudSLAs.Services;
using Microsoft.AspNetCore.Components;

namespace CloudSLAs.Pages
{
    public partial class Calculate
    {
        
        [Inject]
        public ICloudInfrastructureDataService CloudInfrastructuretDataService { get; set; } = default!;
        [Inject]
        public ISlaCalculator SlaCalculator {get;set;} = default!;
        [Inject]
        public IAppComponentRepository AppComponentRepository {get;set;} = default!;
        public List<CloudInfrastructure> CloudInfrastructures {get;set;}
        public List<AppComponent> AppComponents {get;set;}
        public double CalculatedSla {get;set;}
        public double LowestSla {get;set;}
        
        protected AppComponentDialog AppComponentDialog { get; set; }

        protected async override Task OnInitializedAsync()
        {
            AppComponents = new List<AppComponent>();
            CloudInfrastructures = await CloudInfrastructuretDataService.GetAllCloudComponentss(); 
        }

        public void UpdateLowestSla(List<AppComponent> appComponents)
        {
            if (appComponents is null || !appComponents.Any())
            {
                LowestSla = 0;
                return;
            } 

            LowestSla = appComponents.Min(a => a.SLA);
        }

        public void UpdateCalculatedSla()
        {
            if (AppComponents is null || !AppComponents.Any())
            {
                    CalculatedSla = 0; 
                    return;
            }
            
            CalculatedSla = SlaCalculator.CalculateSla(AppComponents.Select(a => a.SLA));
        }        

        public async void AppComponentDialog_OnDialogClose()
        {
            AppComponents = AppComponentRepository.GetAppComponents();
            UpdateSLAs();
            StateHasChanged();
        }        

        protected void AddAppComponent()
        {
            AppComponentDialog.Show();
        }

        protected void EditAppComponent(AppComponent appComponent)
        {
            AppComponentDialog.Edit(appComponent);
        }

        protected void DeleteAppComponent(AppComponent appComponent)
        {
            AppComponents.Remove(appComponent);     
            UpdateSLAs();     
            StateHasChanged();
        }

        protected void UpdateSLAs()
        {         
            UpdateLowestSla(AppComponents);
            UpdateCalculatedSla();           
        }
    }
}