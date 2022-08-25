using CloudSLAs.Models;

namespace CloudSLAs.Services
{
    public interface ISlaCalculator
    {
        public double CalculateSla(IEnumerable<double> slas);
        public double CalculateSla(AppComponent appComponent);
        public double CalculateRedundantSla(IEnumerable<double> slas);
    }
}