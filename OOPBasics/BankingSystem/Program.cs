using BankingSystem.Core;

namespace BankingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var engine = new BankingSystemEngine();
            engine.Run();
        }
    }
}
