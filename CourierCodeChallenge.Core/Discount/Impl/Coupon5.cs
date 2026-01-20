using CourierCodeChallenge.Core.Discount.Impl.Abstraction;

namespace CourierCodeChallenge.Core.Discount.Impl
{
    public class Coupon5 : ICouponStrategy
    {
        public int GetDiscountRate(int distance, int weight)
        {
            if (distance >= 50 && distance <= 250 && weight >= 10 && weight <= 150)
                return 5;

            return 0;
        }
    }
}
