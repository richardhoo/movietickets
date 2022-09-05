using MovieTicketing.Application.Audience;
using MovieTicketing.Application.Dicounts;
using MovieTicketing.Application.TicketTiers;
using System.Collections.Generic;


namespace MovieTicketing.Application.MovieTransaction
{
    public class GRatedMovieTransaction : IMovieTransaction
    {
        private Basket _basket;
         
        public GRatedMovieTransaction()
        {
            _basket = new Basket();
        }

        public MovieRating MovieRating => MovieRating.G;
        Basket IMovieTransaction.Basket { get { return _basket; } set { _basket = value; } }
       
        IList<AudienceType> AudienceTypeAllowed = new List<AudienceType> { AudienceType.Adult, AudienceType.Teenager, AudienceType.Senior, AudienceType.Children };
        public void AddToBasket(Adult adult)
        {
            ValidateMovieAudience(adult);
            _basket.Adults.Add(adult);
        }

        public void AddToBasket(Child child)
        {
            ValidateMovieAudience(child);
            _basket.Children.Add(child);
        }

        public void AddToBasket(Senior senior)
        {
            ValidateMovieAudience(senior);
            _basket.Seniors.Add(senior);
        }

        public void AddToBasket(Teenager teen)
        {
            ValidateMovieAudience(teen);
            _basket.Teens.Add(teen);
        }

        public void ApplyDiscount(IPromotion discount)
        {
            discount.Apply(this);
        }

        void ValidateMovieAudience(ITicket ticketTier)
        {
            var customer = ticketTier.GetCustomer();
            if (!AudienceTypeAllowed.Contains(customer.GetAudienceType()))
            {
                throw new AgeGroupNotAllowedException($"Customer {customer.Name} is not allowed to watch the movie due to age ristrictions.");
            };
        }

        public void CreateNewTransaction()
        {
           _basket=new Basket();
        }
    }


}