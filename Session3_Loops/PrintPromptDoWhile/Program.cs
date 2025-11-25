namespace PrintPromptDoWhile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? input;

            do
            {
                Console.Write("Enter something (N for Exit): ");
                input = Console.ReadLine();
            } while (input != "N");
        }
    }
}
