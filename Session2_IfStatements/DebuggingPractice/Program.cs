//using System; -> Not required from .NET 6 because it is automatically added in new project templates.
// It is flagged as an unnecessary using. When code style rules are enforced, unused usings can be highlighted as errors or warnings (e.g., underlined in red), and should be cleaned.
namespace DebuggingPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // int score = "85"; -> This line causes a compilation error: invalid assignment, cannot assign a string to an int variable.
            int score = 85;
            if (score > 90) // Logical error because the condition is never true for score 85 and produces no output.
                Console.WriteLine("Greate job!");
            else  // A possible solution is to add else block to provide output for scores 90 or below.
                Console.WriteLine("Try again!");
        }
    }
}
