namespace BankingSystem.Common.Validators
{
    public static class InterestValidator
    {
        public static void Validate(double rate)
        {
            if (rate < 0)
            {
                throw new ArgumentException("Interest rate cannot be negative.");
            }  

            if (rate > 100)
            {
                throw new ArgumentException("Interest rate cannot exceed 100%.");
            }   
        }
    }
}
