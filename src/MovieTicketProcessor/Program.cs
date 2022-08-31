using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using MovieTicketing;
using MovieTicketing.Application.Cinema;

namespace MovieTicketProcessor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddMovieTicketingCore();
                    services.AddSingleton<ITransactionReader, TransactionReader>();
                });
    }

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHost _host;
        private readonly ITransactionReader _transactionReader;
        private readonly ICinemaTransactionProcessor _cinemaTransactionProcessor;

        public Worker(
            ILogger<Worker> logger,
            IHost host, 
            ITransactionReader transactionReader,
            ICinemaTransactionProcessor cinemaTransactionProcessor)
        {
            _logger = logger;
            _host = host;
            _transactionReader = transactionReader;
            _cinemaTransactionProcessor = cinemaTransactionProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var allTransactions = _transactionReader.LoadTransactions();
            foreach (var transaction in allTransactions)
            {
                _cinemaTransactionProcessor.ProcessTicketTransaction(transaction);
            }    
            _host.StopAsync();
        }
    }
}
