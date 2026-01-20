
using CourierCodeChallenge.Core.Discount.Impl;
using CourierCodeChallenge.Core.Discount.Impl.Abstraction;

namespace CourierCodeChallenge.Core.Discount
{
    public class DiscountCouponFactory
    {
        public static ICouponStrategy Create(string offerCode)
        {
            return offerCode switch
            {
                "OFR001" => new Coupon10(),
                "OFR002" => new Coupon7(),
                "OFR003" => new Coupon5(),
                _ => new InvalidOrNoCoupon()
            };
        }
    }
}
