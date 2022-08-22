using MovieTickets.CostAnalyzer.Domains;
using MovieTickets.CostAnalyzer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieTickets.CostAnalyzer.Services
{
    public static class TicketTransactionCalculator
    {
        private const decimal AdultPrice = 25M;
        private const decimal SeniorPrice = AdultPrice * 0.7M;
        private const decimal TeenagerPrice = 12M;
        private const decimal ChildrenPrice = 5M;
        private const int ChildrenDiscountMinQuantity = 3;
        private const decimal ChildrenDiscount = 0.25M;

        public static TransactionPriceProjection CaculateTransactionPrice(TicketTransaction ticketTransaction)
        {
            var ticketTypeSubTotalProjections = GetTicketTypeSubTotalProjections(ticketTransaction);

            var projection = new TransactionPriceProjection
            {
                TransactionId = ticketTransaction.TransactionId,
                TicketTypePriceProjections = ticketTypeSubTotalProjections,
                TotalPrice = ticketTypeSubTotalProjections.Sum(x => x.SubTotalPrice)
            };

            return projection;
        }

        private static List<TicketTypePriceProjection> GetTicketTypeSubTotalProjections(TicketTransaction ticketTransaction)
        {
            var ticketTypeSubTotalProjections = ticketTransaction
                .Customers
                .Select(c => CalculateTicketPriceByAge(c.Age))
                .GroupBy(t => t.TicketType)
                .Select(x => new TicketTypePriceProjection
                    {
                        TicketType = x.Key,
                        Quantity = x.Count(),
                        SubTotalPrice = x.Sum(t => t.Price)
                    }
                )
                .ToList();

            var childrenTicket = ticketTypeSubTotalProjections.SingleOrDefault(t => t.TicketType == TicketType.Children);
            if (childrenTicket != null && childrenTicket.Quantity >= ChildrenDiscountMinQuantity)
            {
                childrenTicket.SubTotalPrice *= (1 - ChildrenDiscount);
            }

            return ticketTypeSubTotalProjections;
        }

        private static (TicketType TicketType, decimal Price) CalculateTicketPriceByAge(int age)
        {
            if (age >= 18 && age < 65)
                return (TicketType.Adult, AdultPrice);

            if (age >= 65)
                return (TicketType.Senior, SeniorPrice);

            if (age >= 11 && age < 18)
                return (TicketType.Teenager, TeenagerPrice);

            if (age >= 0 && age < 11)
                return (TicketType.Children, ChildrenPrice);

            throw new ArgumentOutOfRangeException($"Invalid age: {age} passed in when calling {nameof(CalculateTicketPriceByAge)}");
        }
    }
}
