using Microsoft.Extensions.DependencyInjection;
using MovieTicketing.Application.Cinema;
using MovieTicketing.Application.Dicounts;
using MovieTicketing.Application.MovieTransaction;
using MovieTicketing.Application.TicketTiers;
using MovieTicketing.Infrastructure.IO;
using System.Reflection;

namespace MovieTicketing
{
    public static class DependencyInjection
    {
        public static void AddMovieTicketingCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ITicketFactory, TicketFactory>();
            services.AddScoped<IPromotion, ChildrenGroupDiscount>();
            services.AddScoped<IMovieTransaction, GRatedMovieTransaction>();
            services.AddScoped<IMovieTransactionFactory, MovieTransactionFactory>();
            services.AddScoped<ICinemaTransactionProcessor,CinemaTransactionProcessor>();
            services.AddScoped<ITransactionPrinter, TransactionPrinter>();
        }
    }
}
