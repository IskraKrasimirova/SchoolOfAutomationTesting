# Banking System

## How to Run the Program
1. Open the solution in Visual Studio or any C# IDE.
2. Set the project as the startup project.
3. Build and run the application (Ctrl + F5).
4. Follow the on-screen instructions:
   - Choose account type (Savings or Checking)
   - Enter account holder name
   - Enter initial deposit
   - Provide interest rate or overdraft limit
   - Perform transactions (Deposit, Withdraw, Apply Interest)

## Assumptions
- Account numbers are auto-generated using a prefix (ACC/SAV/CHK) and 8 characters from a GUID.
- Names must contain only letters and spaces, and must be between 2 and 50 characters after trimming.
- Deposits and withdrawals must be positive amounts.
- Savings accounts apply interest immediately when selected.
- Checking accounts allow negative balance up to the overdraft limit.
- Invalid inputs are handled with validation and error messages.
- The system runs in a console environment and does not persist data between runs.

## Project Structure
Solution 'OOPBasics'
│
├── Solution Items
│   └── README.md
│
└── BankingSystem
    ├── Common
    │   └── Validators
    │       ├── AmountValidator.cs
    │       ├── InterestValidator.cs
    │       └── NameValidator.cs
    │
    ├── Core
    │   └── BankingSystemEngine.cs
    │
    ├── Models
    │   ├── Contracts
    │   │   ├── IAccount.cs
    │   │   ├── IInterestAccount.cs
    │   │   └── IOverdraftAccount.cs
    │   │
    │   ├── BankAccount.cs
    │   ├── CheckingAccount.cs
    │   ├── SavingsAccount.cs
    │   ├── Transaction.cs
    │   ├── DepositTransaction.cs
    │   └── WithdrawTransaction.cs
    │
    └── Program.cs

## Design Decisions
- **Encapsulation**: All fields are private or protected, with validation handled through centralized validators.
- **Inheritance**: SavingsAccount and CheckingAccount inherit from BankAccount and extend its behavior.
- **Abstraction**: Transaction is an abstract class with polymorphic behavior via Execute().
- **Polymorphism**: DepositTransaction and WithdrawTransaction are executed dynamically through a common Transaction reference.
- **Validation**: All input is validated before use, and exceptions are caught in the engine to prevent crashes.
- **Account Number Generation**: Uses a clean Template Method pattern with overridable prefixes per account type.
- **User Experience**: Console prompts guide the user clearly, and errors are handled gracefully.

## UML Class Diagram (Text Representation)

Interfaces
----------
IAccount
IInterestAccount
IOverdraftAccount

Abstract Classes
----------------
BankAccount : IAccount
Transaction

Concrete Account Types
----------------------
SavingsAccount : BankAccount, IInterestAccount
CheckingAccount : BankAccount, IOverdraftAccount

Concrete Transactions
---------------------
DepositTransaction : Transaction
WithdrawTransaction : Transaction

Relationships
-------------
IAccount
   ↑
BankAccount (abstract)
   ├── SavingsAccount
   └── CheckingAccount

Transaction (abstract)
   ├── DepositTransaction
   └── WithdrawTransaction


 ## File Responsibilities

### Common/Validators
- **AmountValidator.cs** – Validates positive and non-negative monetary values.
- **InterestValidator.cs** – Ensures interest rates are within valid bounds.
- **NameValidator.cs** – Validates account holder names (letters + spaces, trimmed length).

### Core
- **BankingSystemEngine.cs** – Handles user interaction, input reading, menu logic, and exception handling.

### Models/Contracts
- **IAccount.cs** – Base interface for all accounts.
- **IInterestAccount.cs** – Interface for accounts supporting interest.
- **IOverdraftAccount.cs** – Interface for accounts supporting overdraft.

### Models (Classes)
- **BankAccount.cs** – Abstract base class for all accounts. Holds shared logic and validation.
- **SavingsAccount.cs** – Adds interest rate and ApplyInterest().
- **CheckingAccount.cs** – Adds overdraft limit and overrides Withdraw().
- **Transaction.cs** – Abstract base class for all transactions.
- **DepositTransaction.cs** – Implements Execute() to perform deposits.
- **WithdrawTransaction.cs** – Implements Execute() to perform withdrawals.

### Root
- **Program.cs** – Entry point that initializes and runs the engine.
