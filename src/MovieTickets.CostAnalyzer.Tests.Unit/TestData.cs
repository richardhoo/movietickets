using MovieTickets.CostAnalyzer.Domains;
using System.Collections.Generic;

namespace MovieTickets.CostAnalyzer.Tests.Unit
{
    internal static class TestData
    {
        internal static List<TicketTransaction> GetTicketTransactions()
        {
            var transactions = new List<TicketTransaction>
            {
                GetTransacitonWithTwoChildrenTicket(),
                GetTransacitonWithThreeChildrenTickets(),
                GetTransactionWithAllTicketTypes()
            };

            return transactions;
        }

        internal static TicketTransaction GetTransacitonWithTwoChildrenTicket()
        {
            return new TicketTransaction
            {
                TransactionId = 1,
                Customers = new List<Customer>
                    {
                        new Customer {
                            Age = 70
                        },
                        new Customer {
                            Age = 5
                        },
                        new Customer {
                            Age = 6
                        },
                    }
            };
        }

        internal static TicketTransaction GetTransacitonWithThreeChildrenTickets()
        {
            return new TicketTransaction {
                TransactionId = 2,
                Customers = new List<Customer>
                {
                    new Customer {
                        Age = 36
                    },
                    new Customer {
                        Age = 3
                    },
                    new Customer
                    {
                        Age = 8
                    },
                    new Customer
                    {
                        Age = 9
                    },
                    new Customer
                    {
                        Age = 17
                    },
                }
            };
        }

        internal static TicketTransaction GetTransactionWithAllTicketTypes()
        {
            return new TicketTransaction
            {
                TransactionId = 3,
                Customers = new List<Customer>
                    {
                        new Customer {
                            Age = 36
                        },
                        new Customer {
                            Age = 95
                        },
                        new Customer {
                            Age = 15
                        },
                        new Customer {
                            Age = 10
                        },
                    }
            };
        }
    }
}
