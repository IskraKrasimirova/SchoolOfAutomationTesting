using BankingSystem.Models.Contracts;

namespace BankingSystem.Models
{
    public abstract class BankAccount : IAccount
    {
        private decimal _balance;

        protected BankAccount(string accountHolderName, decimal balance)
        {
            if (string.IsNullOrWhiteSpace(accountHolderName))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            this.AccountHolderName = accountHolderName;
            this.Balance = balance;
            this.AccountNumber = GenerateAccountNumber();

        }
        protected string AccountNumber { get; }

        protected string AccountHolderName { get; }

        public decimal Balance
        {
            get => this._balance;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Balance cannot be negative.");
                }

                this._balance = value;
            }
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }

            this.Balance += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdraw amount must be positive.");
            }

            if (this.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            this.Balance -= amount;
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Account Number: {this.AccountNumber}");
            Console.WriteLine($"Account Holder: {this.AccountHolderName}");
            Console.WriteLine($"Balance: ${this.Balance:F2}");
        }

        private static string GenerateAccountNumber()
        {
            return Guid.NewGuid().ToString().Substring(0, 10);
        }
    }
}
