using BankingSystem.Common.Validators;

namespace BankingSystem.Models
{
    public abstract class Transaction
    {
        public DateTime ExecutedAt { get; }
        protected BankAccount Account { get; }
        protected decimal Amount { get; }

        protected Transaction(BankAccount account, decimal amount)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            Account = account;

            AmountValidator.ValidatePositive(amount, "Amount must be positive.");
            Amount = amount;
            ExecutedAt = DateTime.UtcNow;
        }

        public void Execute()
        {
            PerformTransaction();
            Account.AddTransaction(this);

            Console.WriteLine("Transaction successful!");
            Console.WriteLine($"Updated Balance: ${Account.Balance:F2}");
        }

        protected abstract void PerformTransaction();

        public override string ToString()
        {
            return $"{GetType().Name} of ${Amount:F2} on {ExecutedAt:G}";
        }
    }
}
