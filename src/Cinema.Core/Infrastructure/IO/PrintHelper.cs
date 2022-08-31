using MovieTicketing.Application.MovieTransaction;
using MovieTicketing.Application.TicketTiers;
using MovieTicketing.Dto;
using System;

namespace MovieTicketing.Infrastructure.IO
{
    public class TransactionPrinter : ITransactionPrinter
    {
        public void Print(long id, IMovieTransaction movieCheckoutBasket)
        {
            var basket = movieCheckoutBasket.Basket;
            Console.WriteLine($"## Transaction {id} ##");
            foreach (var statistics in basket.GetAllStatistics())
            {
                StatsPrinter(statistics);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Projected total cost: ${basket.TotalStatistics().Total}");
            Console.WriteLine();
            Console.WriteLine();
        }
        private void StatsPrinter(Statistics statistics)
        {
            if (statistics.Count == 0) return;
            Console.WriteLine($"{statistics.Name} ticket x {statistics.Count}: ${statistics.Total}");
        }
    }
}