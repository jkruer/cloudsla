using System.ComponentModel.DataAnnotations;

namespace CloudSLAs.Models
{
    public class AppComponent
    {
        public Guid Id {get;set;}
        [Required]
        public string Name {get;set;}
        public CloudInfrastructure CloudInfrastructure {get;set;}
        [Range(1, 100)]
        public int HADeployments {get;set;}
        public double SLA {get;set;}
    }
}