using CourierCodeChallenge.Core.Discount.Impl.Abstraction;

namespace CourierCodeChallenge.Core.Discount.Impl
{
    public class InvalidOrNoCoupon : ICouponStrategy
    {
        public int GetDiscountRate(int distance, int weight)
        {
            return 0;
        }
    }
}
