using BankingSystem.Common.Validators;

namespace BankingSystem.Models
{
    public abstract class Transaction
    {
        protected BankAccount Account { get; }
        protected decimal Amount { get; }
        protected DateTime ExecutedAt { get; }
        protected decimal BalanceAfterTransaction { get; private set; }

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
            BalanceAfterTransaction = Account.Balance;
            Account.AddTransaction(this);
        }

        protected abstract void PerformTransaction();

        public override string ToString()
        {
            return $"{GetType().Name} of ${Amount:F2} on {ExecutedAt:G}. Balance after: ${BalanceAfterTransaction:F2}";
        }
    }
}
