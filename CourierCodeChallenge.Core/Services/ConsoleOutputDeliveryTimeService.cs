using CourierCodeChallenge.Core.Models;
using CourierCodeChallenge.Core.Services.Abstraction;

namespace CourierCodeChallenge.Core.Services
{
    public class ConsoleOutputDeliveryTimeService : IConsoleOutput
    {
        public void WriteLine(List<Package> packages)
        {
            foreach (var package in packages)
            {
                Console.WriteLine($"{package.Id} {package.DiscountPrice.ToString("F0")} {package.DeliveryCost.ToString("F0")} {package.DeliveryTime.ToString("F2")}");
            }
        }
    }
}
