using BankingSystem.Models.Contracts;

namespace BankingSystem.Models
{
    public class SavingsAccount : BankAccount, IInterestAccount
    {
        protected SavingsAccount(string accountHolderName, decimal balance, double interestRate)
            : base(accountHolderName, balance)
        {
            if (interestRate < 0 || interestRate > 100)
            {
                throw new ArgumentException("Interest rate must be between 0 and 100.");
            }

            InterestRate = interestRate;
        }

        public double InterestRate { get; }

        public void ApplyInterest()
        {
            decimal interest = Balance * (decimal)(InterestRate / 100);
            Balance += interest;

            Console.WriteLine("Interest applied successfully!");
            Console.WriteLine($"Balance: ${Balance:F2}");
        }
    }
}
