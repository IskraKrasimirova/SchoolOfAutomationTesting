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

        protected virtual string AccountPrefix => "ACC";

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

        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Account Number: {_accountNumber}");
            Console.WriteLine($"Account Holder: {_accountHolderName}");
            Console.WriteLine($"Balance: ${Balance:F2}");
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactionHistory.Add(transaction);
        }

        public void DisplayTransactionHistory()
        {
            if (_transactionHistory.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            Console.WriteLine("Transaction History:");

            foreach (var transaction in _transactionHistory)
            {
                Console.WriteLine(transaction);
            }
        }

        protected string GenerateAccountNumber()
        {
            var guidPart = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            return $"{AccountPrefix}-{guidPart}";
        }
    }
}
