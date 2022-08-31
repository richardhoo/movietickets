using AutoMapper;
using MovieTicketing.Application.Dicounts;
using MovieTicketing.Application.MovieTransaction;
using MovieTicketing.Application.TicketTiers;
using MovieTicketing.Dto;
using MovieTicketing.Infrastructure.IO;
using System.Collections.Generic;
using System.Linq;

namespace MovieTicketing.Application.Cinema
{
    public class CinemaTransactionProcessor : ICinemaTransactionProcessor
    {
        private readonly ITicketFactory _ticketTierFactory;
        private readonly IMovieTransactionFactory _movieTransactionFactory;
        private readonly IEnumerable<IPromotion> _promotions;
        private readonly IMapper _mapper;
        private readonly ITransactionPrinter _transactionPrinter;

        public CinemaTransactionProcessor(
            ITicketFactory ticketTierFactory,
            IMovieTransactionFactory movieTransactionFactory,
            IEnumerable<IPromotion> promotions,
            IMapper mapper,
            ITransactionPrinter transactionPrinter)
        {
            _ticketTierFactory = ticketTierFactory;
            _movieTransactionFactory = movieTransactionFactory;
            _promotions = promotions;
            _mapper = mapper;
            _transactionPrinter = transactionPrinter;
        }
        public void ProcessTicketTransaction(TicketTransactionDto ticketTransactionDto)
        {
            ValidateTransaction(ticketTransactionDto);
            var movieTransaction = _movieTransactionFactory.CreateMovieTransaction(ticketTransactionDto);
            var ticketTierList = GenerateTicketForAudience(ticketTransactionDto);
            AddTicketsInTransaction(movieTransaction, ticketTierList);
            ApplyCurrentPromotions(movieTransaction);
            PrintTransaction(ticketTransactionDto, movieTransaction);
        }

        private void PrintTransaction(TicketTransactionDto ticketTransactionDto, IMovieTransaction movieTransaction)
        {
            _transactionPrinter.Print(ticketTransactionDto.TransactionId, movieTransaction);
        }

        private void ApplyCurrentPromotions(IMovieTransaction movieTransaction)
        {
            foreach (var promotion in _promotions)
            {
                promotion.Apply(movieTransaction);
            }
        }

        private static void AddTicketsInTransaction(IMovieTransaction movieTransaction, List<ITicket> ticketTierList)
        {
            foreach (var ticketTier in ticketTierList)
            {
                ticketTier.Apply(movieTransaction);
            }
        }

        private void ValidateTransaction(TicketTransactionDto ticketTransactionDto)
        {
            if (ticketTransactionDto == null)
            {
                throw new InvalidTransactionException("Transaction is null");
            }
            if (ticketTransactionDto.Customers == null || !ticketTransactionDto.Customers.Any())
            {
                throw new InvalidTransactionException("Transaction has no customers");
            }
        }

        private List<ITicket> GenerateTicketForAudience(TicketTransactionDto ticketTransactionDto)
        {
            var ticketTierList = new List<ITicket>();

            foreach (var item in ticketTransactionDto.Customers)
            {
                var customer = _mapper.Map<Audience.Customer>(item);
                var ticketTier = _ticketTierFactory.GenerateTicket(customer);
                ticketTierList.Add(ticketTier);
            }
            return ticketTierList;
        }
    }


}
