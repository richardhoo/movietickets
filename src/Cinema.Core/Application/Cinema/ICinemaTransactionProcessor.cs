using MovieTicketing.Dto;

namespace MovieTicketing.Application.Cinema
{
    public interface ICinemaTransactionProcessor
    {
        void ProcessTicketTransaction(TicketTransactionDto ticketTransactionDto);
    }
}
