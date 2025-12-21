using BankingSystem.Common.Validators;

namespace BankingSystem.Models
{
    public abstract class Transaction
    {
        protected Transaction(BankAccount account, decimal amount)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            Account = account;

            AmountValidator.ValidatePositive(amount, "Amount must be positive.");
            Amount = amount;
        }

        protected BankAccount Account { get; }
        protected decimal Amount { get; }

        public void Execute()
        {
            PerformTransaction();

            Console.WriteLine("Transaction successful!");
            Console.WriteLine($"Updated Balance: ${Account.Balance:F2}");
        }

        protected abstract void PerformTransaction();
    }
}
