using BankingSystem.Common.Validators;
using BankingSystem.Models;
using BankingSystem.Models.Contracts;

namespace BankingSystem.Core
{
    public class BankingSystemEngine
    {
        private static readonly HashSet<string> validActions = ["1", "2", "3", "4", "5"];
        public void Run()
        {
            Console.WriteLine("Welcome to the Banking System!");

            var accountType = ReadAccountType();
            var accountHolderName = ReadAccountHolderName();
            var initialDeposit = ReadInitialDepositAmount();

            BankAccount account = CreateAccount(accountType, accountHolderName, initialDeposit);

            Console.WriteLine("Account created successfully!");
            account.DisplayAccountInfo();

            while (true)
            {
                var selectedAction = ReadAction();

                if (selectedAction == "5")
                {
                    break;
                }

                try
                {
                    ExecuteAction(account, selectedAction);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("Thank you for using the Banking System!");
        }

        private static void ExecuteAction(BankAccount account, string action)
        {
            Transaction transaction;

            switch (action)
            {
                case "1":
                    var deposit = ReadDepositAmount();
                    transaction = new DepositTransaction(account, deposit);
                    transaction.Execute();
                    break;
                case "2":
                    var withdraw = ReadWithdrawAmount();
                    transaction = new WithdrawTransaction(account, withdraw);
                    transaction.Execute();
                    break;
                case "3":
                    if (account is IInterestAccount interestAccount)
                    {
                        interestAccount.ApplyInterest();
                    }
                    else
                    {
                        Console.WriteLine("This account type does not support interest.");
                    }
                    break;
                case "4":
                    account.DisplayTransactionHistory();
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Unsupported action type.");
                    return;
            }
        }

        private static BankAccount CreateAccount(string accountType, string accountName, decimal initialDeposit)
        {
            BankAccount account;

            switch (accountType)
            {
                case "1":
                    var interest = ReadInterestRate();
                    account = new SavingsAccount(accountName, initialDeposit, interest);
                    break;
                case "2":
                    var overdraft = ReadOverdraftLimit();
                    account = new CheckingAccount(accountName, initialDeposit, overdraft);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported account type.");
            }

            return account;
        }

        private static string ReadAction()
        {
            string? action;

            while (true)
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Apply Interest");
                Console.WriteLine("4. Transaction History");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                action = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(action) && validActions.Contains(action))
                {
                    return action;
                }

                Console.WriteLine("Not supported action type. Please try again.");
            }
        }

        private static string ReadAccountType()
        {
            string? accountType;

            while (true)
            {
                Console.WriteLine("Choose an account type:");
                Console.WriteLine("Enter 1 for Savings Account");
                Console.WriteLine("Enter 2 for Checking Account");
                Console.Write("Enter your choice: ");

                accountType = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(accountType) && (accountType == "1" || accountType == "2"))
                {
                    return accountType;
                }

                Console.WriteLine("Not supported account type. Please try again.");
            }
        }

        private static double ReadInterestRate()
        {
            while (true)
            {
                Console.Write("Enter interest rate (e.g., 5 for 5%): ");
                var isValidInterest = double.TryParse(Console.ReadLine(), out var interest);

                if (!isValidInterest)
                {
                    Console.WriteLine("Please enter valid interest rate.");
                    continue;
                }

                try
                {
                    InterestValidator.Validate(interest);
                    return interest;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static decimal ReadAmount(string message, string errorMessage, bool mustBePositive)
        {
            while (true)
            {
                Console.Write(message);
                var isValidDepositAmount = decimal.TryParse(Console.ReadLine(), out decimal amount);

                if (!isValidDepositAmount)
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                try
                {
                    if (mustBePositive)
                    {
                        AmountValidator.ValidatePositive(amount, errorMessage);
                    }
                    else
                    {
                        AmountValidator.ValidateNonNegative(amount, errorMessage);
                    }

                    return amount;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static decimal ReadOverdraftLimit()
        {
            return ReadAmount(
                message: "Enter overdraft limit: ",
                errorMessage: "Overdraft limit cannot be negative.",
                mustBePositive: false);
        }

        private static decimal ReadWithdrawAmount()
        {
            return ReadAmount(
                message: "Enter withdraw amount: ",
                errorMessage: "Withdraw amount must be positive.",
                mustBePositive: true);
        }

        private static decimal ReadDepositAmount()
        {
            return ReadAmount(
                message: "Enter deposit amount: ",
                errorMessage: "Deposit amount must be positive.",
                mustBePositive: true);
        }

        private static decimal ReadInitialDepositAmount()
        {
            return ReadAmount(
                message: "Enter initial deposit amount: ",
                errorMessage: "Deposit amount cannot be negative.",
                mustBePositive: false);
        }

        private static string ReadAccountHolderName()
        {
            string? accountName;

            while (true)
            {
                Console.Write("Enter account holder name: ");
                accountName = Console.ReadLine();

                try
                {
                    NameValidator.Validate(accountName);
                    return accountName!;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

