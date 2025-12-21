namespace BankingSystem.Models.Contracts
{
    internal interface IAccount
    {
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
    }
}
