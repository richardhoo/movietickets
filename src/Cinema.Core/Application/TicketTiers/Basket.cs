using System.Collections.Generic;
using System.Linq;

namespace MovieTicketing.Application.TicketTiers
{
    public class Basket
    {
        public List<Adult> Adults { get; set; } = new List<Adult>();
        public List<Senior> Seniors { get; set; } = new List<Senior>();
        public List<Teenager> Teens { get; set; } = new List<Teenager>();
        public List<Child> Children { get; set; } = new List<Child>();


        public Statistics[] GetAllStatistics()
        {
            return new Statistics[] { AdultStatistics(), ChildrenStatistics(), SeniorsStatistics(), TeensStatistics() };
        }
        public Statistics TotalStatistics()
        {
            return new Statistics()
            {
                Count = Adults.Count() + Seniors.Count() + Teens.Count() + Children.Count(),
                Total = Adults.Sum(x => x.GetTicketPrice()) + Seniors.Sum(x => x.GetTicketPrice()) + Teens.Sum(x => x.GetTicketPrice()) + Children.Sum(x => x.GetTicketPrice())
            };

        }

        private Statistics AdultStatistics()
        {
            return new Statistics()
            {
                Count = Adults.Count(),
                Total = Adults.Sum(x => x.GetTicketPrice()),
                Name = "Adult"

            };
        }

        private Statistics SeniorsStatistics()
        {
            return new Statistics()
            {
                Count = Seniors.Count(),
                Total = Seniors.Sum(x => x.GetTicketPrice()),
                Name = "Senior"

            };
        }

        private Statistics TeensStatistics()
        {
            return new Statistics()
            {
                Count = Teens.Count(),
                Total = Teens.Sum(x => x.GetTicketPrice()),
                Name = "Teen"

            };
        }

        private Statistics ChildrenStatistics()
        {
            return new Statistics()
            {
                Count = Teens.Count(),
                Total = Teens.Sum(x => x.GetTicketPrice()),
                Name = "Children"
            };
        }

    }


}