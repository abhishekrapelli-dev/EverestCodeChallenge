using CourierCodeChallenge.Core.Discount.Impl.Abstraction;

namespace CourierCodeChallenge.Core.Discount.Impl
{
    public class Coupon10 : ICouponStrategy
    {
        public int GetDiscountRate(int distance, int weight)
        {
            if (distance < 200 && weight >= 70 && weight <= 200)
                return 10;

            return 0;
        }
    }
}
