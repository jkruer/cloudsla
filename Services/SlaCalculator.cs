using CloudSLAs.Models;

namespace CloudSLAs.Services
{
    public class SlaCalculator : ISlaCalculator
    {
        public double CalculateSla(IEnumerable<double> slas)
        {
            return slas.Aggregate((result, item) => result * (item * .01));
        }
        
        public double CalculateRedundantSla(IEnumerable<double> slas)
        {
            foreach(var sla in slas)
            {
                Console.WriteLine(sla);
            }
            var x = slas.Aggregate(1.0, (result, item) => result * (1 - (item * .01)));
            return (1-x) * 100;
        }

        public double CalculateSla(AppComponent appComponent)
        {
            if (appComponent is null || appComponent.HADeployments <= 0) return 0;

            if (appComponent.HADeployments == 1) return appComponent.CloudInfrastructure.SLA;

            return CalculateRedundantSla(Enumerable.Range(1, appComponent.HADeployments)
                .Select(x => appComponent.CloudInfrastructure.SLA));
        }
    }
}