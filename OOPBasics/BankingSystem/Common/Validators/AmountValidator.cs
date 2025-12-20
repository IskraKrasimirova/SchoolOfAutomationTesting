namespace BankingSystem.Common.Validators
{
    public static class AmountValidator
    {
        public static void ValidatePositive(decimal amount, string message)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(message);
            }
        }

        public static void ValidateNonNegative(decimal amount, string message)
        {
            if (amount < 0)
            {
                throw new ArgumentException(message);
            }  
        }
    }
}
