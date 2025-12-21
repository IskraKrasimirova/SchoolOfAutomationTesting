using BankingSystem.Common.Validators;
using BankingSystem.Models.Contracts;

namespace BankingSystem.Models
{
    public class SavingsAccount : BankAccount, IInterestAccount
    {
        private readonly double _interestRate;

        protected override string AccountPrefix => "SAV";

        public SavingsAccount(string accountHolderName, decimal balance, double interestRate)
            : base(accountHolderName, balance)
        {
            InterestValidator.Validate(interestRate);
            _interestRate = interestRate;
        }

        public void ApplyInterest()
        {
            decimal interest = Balance * (decimal)(_interestRate / 100);
            Balance += interest;
        }
    }
}
