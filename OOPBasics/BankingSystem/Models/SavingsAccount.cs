using BankingSystem.Common.Validators;
using BankingSystem.Models.Contracts;

namespace BankingSystem.Models
{
    public class SavingsAccount : BankAccount, IInterestAccount
    {
        public SavingsAccount(string accountHolderName, decimal balance, double interestRate)
            : base(accountHolderName, balance)
        {
            InterestValidator.Validate(interestRate);
            InterestRate = interestRate;
        }

        public double InterestRate { get; }

        protected override string AccountPrefix => "SAV";

        public void ApplyInterest()
        {
            decimal interest = Balance * (decimal)(InterestRate / 100);
            Balance += interest;

            Console.WriteLine("Interest applied successfully!");
            Console.WriteLine($"Balance: ${Balance:F2}");
        }
    }
}
