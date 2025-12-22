# Banking System

## Overview
This is a simple object‑oriented banking system implemented in C#. 
It demonstrates core OOP principles such as abstraction, inheritance, polymorphism, and encapsulation. 
Users can create accounts, perform transactions, and view transaction history through a console interface.

## Key Features
- Create Savings or Checking accounts
- Automatic account number generation
- Deposit and withdraw money
- Apply interest (Savings accounts)
- Overdraft support (Checking accounts)
- Full transaction history with timestamps
- Input validation and error handling

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
- Account numbers are auto-generated using a prefix (SAV/CHK) and 8 characters from a GUID.
- Names must contain only letters and spaces, and must be between 2 and 50 characters after trimming.
- Deposits and withdrawals must be positive amounts.
- Savings accounts apply interest immediately when selected.
- Checking accounts allow negative balance up to the overdraft limit.
- Invalid inputs are handled with validation and error messages.
- The system runs in a console environment and does not persist data between runs.

## Project Structure

```
OOPBasics/
├── README.md
└── BankingSystem/
    ├── Program.cs
    ├── Core/
    │   └── BankingSystemEngine.cs
    ├── Common/
    │   └── Validators/
    │       ├── AmountValidator.cs
    │       ├── InterestValidator.cs
    │       └── NameValidator.cs
    ├── Models/
    │   ├── Contracts/
    │   │   ├── IAccount.cs
    │   │   └── IInterestAccount.cs
    │   ├── BankAccount.cs
    │   ├── CheckingAccount.cs
    │   ├── SavingsAccount.cs
    │   ├── Transaction.cs
    │   ├── DepositTransaction.cs
    │   └── WithdrawTransaction.cs

```


## Design Decisions

- **Encapsulation**: Sensitive data such as account number and account holder name is stored in private readonly fields, while the account balance is exposed through a public read-only property with a protected setter. Validation is centralized in dedicated validator classes.
- **Inheritance**: `SavingsAccount` and `CheckingAccount` inherit from the abstract base class `BankAccount`, reusing shared logic and extending behavior.
- **Abstraction**: `BankAccount` and `Transaction` are abstract classes that define common structure and enforce required behavior in derived classes.
- **Polymorphism**: `DepositTransaction` and `WithdrawTransaction` are executed through a shared `Transaction` reference, enabling dynamic behavior at runtime.
- **Validation**: All user input is validated before processing. The engine handles exceptions to prevent crashes and provide feedback.
- **Account Number Generation**: A Template Method pattern is used to generate account numbers, allowing each account type to define its own prefix.
- **User Experience**: The console interface guides the user step-by-step. Errors are handled without terminating the program, ensuring smooth interaction.
- All user‑interface logic is fully separated from the business logic and handled exclusively by the BankingSystemEngine.


### Transaction History
- Every account maintains its own transaction history stored internally in a private list.
- Each transaction records the amount, the account it operates on, and the exact execution timestamp.
- The history is exposed as a read‑only collection to preserve encapsulation.
- Transactions are logged automatically when executed, using polymorphism and the `ToString()` override in the `Transaction` class.
- Users can view the full history through the console menu.


## UML Class Diagram (Text Representation)

```
Interfaces
----------
IAccount
IInterestAccount

Abstract Classes
----------------
BankAccount : IAccount
Transaction

Concrete Account Types
----------------------
SavingsAccount : BankAccount, IInterestAccount
CheckingAccount : BankAccount

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
```

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

### Models (Classes)
- **BankAccount.cs** – Abstract base class for all accounts. Holds shared logic and validation.
- **SavingsAccount.cs** – Adds interest rate and ApplyInterest().
- **CheckingAccount.cs** – Adds overdraft limit and overrides Withdraw().
- **Transaction.cs** – Abstract base class for all transactions.
- **DepositTransaction.cs** – Implements Execute() to perform deposits.
- **WithdrawTransaction.cs** – Implements Execute() to perform withdrawals.

### Root
- **Program.cs** – Entry point that initializes and runs the engine.