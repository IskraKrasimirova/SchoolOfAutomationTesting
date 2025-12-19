using BankingSystem.Models.Contracts;

namespace BankingSystem.Models
{
    internal class CheckingAccount : BankAccount, IOverdraftAccount
    {
        protected CheckingAccount(string accountHolderName, decimal balance, decimal overdraftLimit)
            : base(accountHolderName, balance)
        {
            if (overdraftLimit < 0)
            {
                throw new ArgumentException("Overdraft Limit cannot be negative.");
            }

            OverdraftLimit = overdraftLimit;
        }

        public decimal OverdraftLimit { get; }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdraw amount must be positive.");
            }

            decimal availableFunds = Balance + OverdraftLimit;

            if (amount > availableFunds)
            {
                throw new InvalidOperationException("Overdraft limit exceeded.");
            }

            Balance -= amount;
        }
    }
}
