namespace BankingSystem.Models.Contracts
{
    public interface IInterestAccount
    {
        double InterestRate { get; }
        void ApplyInterest();
    }
}
