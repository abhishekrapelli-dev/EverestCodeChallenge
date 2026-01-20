using CourierCodeChallenge.Core.Models;

namespace CourierCodeChallenge.Core.Services.Abstraction
{
    public interface IEstimate
    {
        void Calculate(List<Package> packages);
    }
}
