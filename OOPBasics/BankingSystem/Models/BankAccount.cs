using BankingSystem.Common.Validators;
using BankingSystem.Models.Contracts;

namespace BankingSystem.Models
{
    public abstract class BankAccount : IAccount
    {
        protected BankAccount(string accountHolderName, decimal balance)
        {
            NameValidator.Validate(accountHolderName);

            this.AccountHolderName = accountHolderName;
            this.Balance = balance;
            this.AccountNumber = GenerateAccountNumber();

        }
        protected string AccountNumber { get; }

        protected string AccountHolderName { get; }

        public decimal Balance { get; protected set; }

        protected virtual string AccountPrefix => "ACC";

        public void Deposit(decimal amount)
        {
            AmountValidator.ValidatePositive(amount, "Deposit amount must be positive.");
            this.Balance += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            AmountValidator.ValidatePositive(amount, "Withdraw amount must be positive.");

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

        protected string GenerateAccountNumber()
        {
            var guidPart = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            return $"{AccountPrefix}-{guidPart}";
        }
    }
}
