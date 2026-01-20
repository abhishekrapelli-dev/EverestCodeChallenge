using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierCodeChallenge.Core.Discount.Impl.Abstraction
{
    public interface ICouponStrategy
    {
        public int GetDiscountRate(int distance, int weight);
    }
}
