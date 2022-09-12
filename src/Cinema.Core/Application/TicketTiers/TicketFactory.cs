using MovieTicketing.Application.Audience;
using System.Collections.Generic;


namespace MovieTicketing.Application.TicketTiers
{
    public class TicketFactory : ITicketFactory
    {
        public ITicket GenerateTicket(Customer customer)
        {
            var type = customer.GetAudienceType();
            return AllTicketTiers(customer)[type];
        }
        
        private IDictionary<AudienceType, ITicket> AllTicketTiers(Customer customer)
        {
            return new Dictionary<AudienceType, ITicket>
            {
                { AudienceType.Adult, new Adult(customer) },
                { AudienceType.Senior, new Senior(customer) },
                { AudienceType.Teenager, new Teenager(customer) },
                { AudienceType.Children, new Child(customer) }
            };
        }

    }

}


