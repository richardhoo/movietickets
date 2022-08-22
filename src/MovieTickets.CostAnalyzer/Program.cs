using Microsoft.Extensions.DependencyInjection;
using MovieTickets.CostAnalyzer.Infrastructure;
using MovieTickets.CostAnalyzer.Services;
using System;
using System.Threading.Tasks;

namespace MovieTickets.CostAnalyzer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //setup DI
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            //do the actual work here
            var processor = serviceProvider.GetRequiredService<ITicketTransactionProcessor>();

            try
            {
                var output = await processor.GenerateTransactionPriceProjectionAsync();
                Console.WriteLine(output);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging()
                    .AddSingleton<ITicketTransactionProcessor, TicketTransactionProcessor>()
                    .AddSingleton<ITicketTransactionRepository, TicketTransactionRepository>();
        }
    }
}
