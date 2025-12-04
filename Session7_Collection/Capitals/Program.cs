namespace Capitals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var capitals = new Dictionary<string, string>();
            capitals.Add("Bulgaria", "Sofia");
            capitals.Add("Germany", "Berlin");
            capitals.Add("France", "Paris");
            capitals.Add("Italy", "Rome");
            capitals["Spain"] = "Madrid";
            capitals["United Kingdom"] = "London";
            capitals["Greece"] = "Athens";
            capitals["Turkey"] = "Ankara";
            capitals["Japan"] = "Tokyo";
            capitals["Canada"] = "Ottawa";

            var capitalOfFrance = capitals["France"];
            Console.WriteLine($"The capital of France is {capitalOfFrance}.");

            foreach (var kvp in capitals)
            {
                Console.WriteLine($"The capital of {kvp.Key} is {kvp.Value}.");
            }
        }
    }
}
