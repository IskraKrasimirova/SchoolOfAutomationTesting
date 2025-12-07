namespace Capitals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Simple dictionary usage with hardcoded country-capital pairs
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

            if (capitals.TryGetValue("China", out var capitalOfChina))
            {
                Console.WriteLine($"The capital of China is {capitalOfChina}.");
            }
            else
            {
                Console.WriteLine("China is not found in the dictionary.");
            }

            foreach (var kvp in capitals)
            {
                Console.WriteLine($"The capital of {kvp.Key} is {kvp.Value}.");
            }
        }
    }
}
