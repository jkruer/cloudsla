using CloudSLAs.Models;
using CloudSLAs.Services;
using Microsoft.AspNetCore.Components;

namespace CloudSLAs.Components
{
    public partial class AppComponentDialog
    {
        public AppComponent AppComponent {get;set;}
        private bool _showDialog {get;set;}        
        private string _modalDisplay {
            get {
                return _showDialog ? "block;" : "none;";
            }
        }        
        private string _modalClass {
            get {
                return _showDialog ? "show" : "";
            }
        }

        private bool _isEdit {get;set;}
        private int SelectedInfrastructureId;

        [Inject]
        public IAppComponentRepository AppComponentRepository {get;set;} = default!;
        [Inject]
        public ISlaCalculator SlaCalculator {get;set;} = default!;

        [Parameter]
        public List<CloudInfrastructure> CloundInfrastructures {get;set;} = default!;
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        
        
        protected async override Task OnInitializedAsync()
        {
            ResetDialog();
        }

        public void Show()
        {
            ResetDialog();
            _showDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            _showDialog = false;
            StateHasChanged();
        }

        public void Edit(AppComponent appComponent)
        {
            Show();
            _isEdit = true;
            SelectedInfrastructureId = appComponent.CloudInfrastructure.Id;
            AppComponent = new AppComponent{
                Id = appComponent.Id,
                Name = appComponent.Name,
                CloudInfrastructure = appComponent.CloudInfrastructure,
                HADeployments = appComponent.HADeployments
            };            
            StateHasChanged();
        }

        public void ResetDialog()
        {
            AppComponent = CreateDefaultAppComponent();
            _isEdit = false;
            SelectedInfrastructureId = CloundInfrastructures.First().Id;
            CalculateAppComponentSla();
        }

        protected async Task HandleValidSubmit()
        {
            AppComponent.CloudInfrastructure = CloundInfrastructures.First(ci => ci.Id == SelectedInfrastructureId);

            CalculateAppComponentSla();

            if(_isEdit)
            {                
                AppComponentRepository.UpdateAppComponent(AppComponent);
            }
            else
            {
                AppComponentRepository.AddAppComponent(new AppComponent{
                    Id = Guid.NewGuid(),
                    Name = AppComponent.Name,
                    CloudInfrastructure = AppComponent.CloudInfrastructure,
                    HADeployments = AppComponent.HADeployments,
                    SLA = AppComponent.SLA
                });
            }

            Close();

            await CloseEventCallback.InvokeAsync(true);            
            StateHasChanged();
        }

        private AppComponent CreateDefaultAppComponent()
        {
            return new AppComponent
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                HADeployments = 1,
                CloudInfrastructure = CloundInfrastructures.First(),
                SLA = 0
            };
        }

        private void CalculateAppComponentSla()
        {
            AppComponent.CloudInfrastructure = CloundInfrastructures.First(ci => ci.Id == SelectedInfrastructureId);
            Console.WriteLine(AppComponent.CloudInfrastructure.Name);
            Console.WriteLine($"HADeployments: {AppComponent.HADeployments}");
            Console.WriteLine($"Infrastructure SLA: {AppComponent.CloudInfrastructure.SLA}");
            var appComponentSLA = SlaCalculator.CalculateSla(AppComponent);
            Console.WriteLine($"AppComponent SLA: {appComponentSLA}");
            AppComponent.SLA = appComponentSLA;
        }
    }
}