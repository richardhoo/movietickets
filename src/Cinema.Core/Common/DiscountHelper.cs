using System;
using System.Collections.Generic;

namespace MovieTicketing.Shared
{
    public static class DiscountHelper
    {
        public static decimal GetDiscount(this decimal originalPrice, decimal discount)
        {
            var totalDicount = originalPrice * (discount / 100);
            return Math.Round(totalDicount, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal ApplyDiscount(this decimal originalPrice, List<decimal> Discounts)
        {
            foreach (var discount in Discounts)
            {
                originalPrice -= discount;
            }
            return originalPrice;
        }
    }

}
