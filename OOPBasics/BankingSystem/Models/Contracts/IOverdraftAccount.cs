namespace BankingSystem.Models.Contracts
{
    internal interface IOverdraftAccount
    {
        decimal OverdraftLimit { get; }
    }
}
