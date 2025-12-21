using BankingSystem.Common.Validators;

namespace BankingSystem.Models
{
    public class CheckingAccount : BankAccount
    {
        private readonly decimal _overdraftLimit;

        protected override string AccountPrefix => "SHK";

        public CheckingAccount(string accountHolderName, decimal balance, decimal overdraftLimit)
            : base(accountHolderName, balance)
        {
            AmountValidator.ValidateNonNegative(overdraftLimit, "Overdraft limit cannot be negative.");
            _overdraftLimit = overdraftLimit;
        }

        public override void Withdraw(decimal amount)
        {
            AmountValidator.ValidatePositive(amount, "Withdraw amount must be positive.");

            decimal availableFunds = Balance + _overdraftLimit;

            if (amount > availableFunds)
            {
                throw new InvalidOperationException("Overdraft limit exceeded.");
            }

            Balance -= amount;
        }
    }
}
