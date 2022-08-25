namespace CloudSLAs.Models
{
    public class CloudInfrastructure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double SLA { get; set; }

        public CloudInfrastructure()
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}