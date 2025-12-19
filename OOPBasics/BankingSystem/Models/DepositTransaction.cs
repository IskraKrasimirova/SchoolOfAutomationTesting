namespace BankingSystem.Models
{
    public class DepositTransaction : Transaction
    {
        public DepositTransaction(BankAccount account, decimal amount) : 
            base(account, amount)
        {
        }

        protected override void PerformTransaction()
        {
            Account.Deposit(Amount);
        }
    }
}
