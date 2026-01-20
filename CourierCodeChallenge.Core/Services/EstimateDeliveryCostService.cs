using CourierCodeChallenge.Core.Discount;
using CourierCodeChallenge.Core.Models;
using CourierCodeChallenge.Core.Services.Abstraction;

namespace CourierCodeChallenge.Core.Services
{
    public class EstimateDeliveryCostService : IEstimate
    {
        public double _basePrice { get; }

        public EstimateDeliveryCostService(double basePrice)
        {
            _basePrice = basePrice;
        }

        public virtual void Calculate(List<Package> packages)
        {
            foreach (var package in packages)
            {
                var couponStrategy = DiscountCouponFactory.Create(package.OfferCode.ToUpper());
                var discountInPercent = couponStrategy.GetDiscountRate(package.Distance, package.Weight);

                var deliveryCost = GetDeliveryCostPackageWise(_basePrice, package);
                var discountCost = deliveryCost * (Convert.ToDouble(discountInPercent) / 100);

                package.DeliveryCost = deliveryCost - discountCost;
                package.DiscountPrice = discountCost;
            }
        }

        private double GetDeliveryCostPackageWise(double basePrice, Package package)
        {
            return basePrice + package.Weight * 10 + package.Distance * 5;
        }
    }
}
