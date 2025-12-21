using BankingSystem.Common.Validators;
using BankingSystem.Models.Contracts;

namespace BankingSystem.Models
{
    public abstract class BankAccount : IAccount
    {
        private readonly string _accountNumber;
        private readonly string _accountHolderName;
        private readonly List<Transaction> _transactionHistory = new();

        public decimal Balance { get; protected set; }

        public IReadOnlyCollection<Transaction> TransactionHistory => _transactionHistory.AsReadOnly();

        protected abstract string AccountPrefix { get; }

        protected BankAccount(string accountHolderName, decimal balance)
        {
            NameValidator.Validate(accountHolderName);

            _accountHolderName = accountHolderName;
            Balance = balance;
            _accountNumber = GenerateAccountNumber();
        }

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

        /// <summary>
        /// Requirement from the assignment: 
        /// /// Returns account info as a string instead of printing it. 
        /// /// Engine decides how to display it.
        /// </summary>
        public string GetAccountInfo()
        {
            return $"Account Number: {_accountNumber}{Environment.NewLine}Account Holder: {_accountHolderName}{Environment.NewLine}Balance: ${Balance:F2}";
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactionHistory.Add(transaction);
        }

        private string GenerateAccountNumber()
        {
            var guidPart = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            return $"{AccountPrefix}-{guidPart}";
        }
    }
}
