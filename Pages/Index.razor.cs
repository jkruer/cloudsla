using CloudSLAs.Components;
using CloudSLAs.Models;
using CloudSLAs.Services;
using Microsoft.AspNetCore.Components;

namespace CloudSLAs.Pages
{
    public partial class Index
    {
        
        [Inject]
        private ICloudInfrastructureDataService _cloudInfrastructureDataService { get; set; } = default!;
        private List<CloudInfrastructure> _cloudInfrastructures {get;set;} = default!;
        private List<CloudInfrastructure> _filteredInfrastructures => _cloudInfrastructures
                .Where(ci => ci.Name.ToLower().Contains(_filterText.ToLower()))
                .ToList();

        private string _filterText = "";

        protected async override Task OnInitializedAsync()
        {
            _cloudInfrastructures = await _cloudInfrastructureDataService.GetAllCloudComponentss(); 
        }
    }
}