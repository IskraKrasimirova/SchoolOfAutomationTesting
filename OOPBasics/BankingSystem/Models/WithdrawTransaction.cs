namespace BankingSystem.Models
{
    public class WithdrawTransaction : Transaction
    {
        public WithdrawTransaction(BankAccount account, decimal amount) 
            : base(account, amount)
        {
        }

        protected override void PerformTransaction()
        {
            Account.Withdraw(Amount);
        }
    }
}
