namespace MovieTicketing.Application.Audience
{
    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public AudienceType GetAudienceType()
        {
            if (Age <= 11)
            {
                return AudienceType.Children;
            }
            if (Age >= 65)
            {
                return AudienceType.Senior;
            }
            if (Age < 65 && Age >= 18)
            {
                return AudienceType.Adult;
            }

            return AudienceType.Teenager;
        }
    }
}
