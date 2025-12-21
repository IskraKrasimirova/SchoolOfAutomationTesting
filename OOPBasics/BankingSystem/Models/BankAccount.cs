using BankingSystem.Common.Validators;
using BankingSystem.Models.Contracts;

namespace BankingSystem.Models
{
    public abstract class BankAccount : IAccount
    {
        private readonly string _accountNumber;
        private readonly string _acountHolderName;

        protected BankAccount(string accountHolderName, decimal balance)
        {
            NameValidator.Validate(accountHolderName);

            _acountHolderName = accountHolderName;
            Balance = balance;
            _accountNumber = GenerateAccountNumber();
        }

        public decimal Balance { get; protected set; }

        protected virtual string AccountPrefix => "ACC";

        public void Deposit(decimal amount)
        {
            AmountValidator.ValidatePositive(amount, "Deposit amount must be positive.");
            Balance += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            AmountValidator.ValidatePositive(amount, "Withdraw amount must be positive.");

            if (Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            Balance -= amount;
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Account Number: {_accountNumber}");
            Console.WriteLine($"Account Holder: {_acountHolderName}");
            Console.WriteLine($"Balance: ${Balance:F2}");
        }

        protected string GenerateAccountNumber()
        {
            var guidPart = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            return $"{AccountPrefix}-{guidPart}";
        }
    }
}
