using CourierCodeChallenge.Core.Discount.Impl.Abstraction;

namespace CourierCodeChallenge.Core.Discount.Impl
{
    public class Coupon7 : ICouponStrategy
    {
        public int GetDiscountRate(int distance, int weight)
        {
            if (distance >= 50 && distance <= 150 && weight >= 100 && weight <= 250)
                return 7;

            return 0;
        }
    }
}
