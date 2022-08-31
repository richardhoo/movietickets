using MovieTicketing.Application.Audience;
using MovieTicketing.Application.MovieTransaction;
using MovieTicketing.Shared;
using System.Collections.Generic;

namespace MovieTicketing.Application.TicketTiers
{
    public abstract class TicketBase
    {

        protected List<decimal> Discounts = new List<decimal>();
        private readonly Customer _customer;

        protected TicketBase(Customer customer)
        {
            _customer = customer;
        }
        public abstract decimal TicketPrice { get; }

        public void AddDiscount(decimal discountAmout)
        {
            Discounts.Add(discountAmout);
        }

        public abstract void Apply(IMovieTransaction checkoutBasket);

        public virtual decimal GetTicketPrice()
        {
            return TicketPrice.ApplyDiscount(Discounts);
        }

        public Customer GetCustomer() => _customer;
    }
}