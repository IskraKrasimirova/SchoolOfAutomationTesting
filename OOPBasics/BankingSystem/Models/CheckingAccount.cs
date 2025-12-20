using BankingSystem.Common.Validators;
using BankingSystem.Models.Contracts;

namespace BankingSystem.Models
{
    public class CheckingAccount : BankAccount, IOverdraftAccount
    {
        public CheckingAccount(string accountHolderName, decimal balance, decimal overdraftLimit)
            : base(accountHolderName, balance)
        {
            AmountValidator.ValidateNonNegative(overdraftLimit, "Overdraft limit cannot be negative.");
            OverdraftLimit = overdraftLimit;
        }

        public decimal OverdraftLimit { get; }

        protected override string AccountPrefix => "SHK";

        public override void Withdraw(decimal amount)
        {
            AmountValidator.ValidatePositive(amount, "Withdraw amount must be positive.");

            decimal availableFunds = Balance + OverdraftLimit;

            if (amount > availableFunds)
            {
                throw new InvalidOperationException("Overdraft limit exceeded.");
            }

            Balance -= amount;
        }
    }
}
