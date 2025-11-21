namespace Vacation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your budget: ");
            var budgetInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(budgetInput))
            {
                Console.WriteLine("Budget cannot be empty or whitespace.");
                return;
            }

            budgetInput = budgetInput.Trim();

            if (!decimal.TryParse(budgetInput, out decimal budget))
            {
                Console.WriteLine("Invalid budget. Please enter a valid decimal number.");
                return;
            }

            if (budget < 10 || budget > 5000)
            {
                Console.WriteLine("Budget must be between 10.00 and 5000.00.");
                return;
            }

            Console.Write("Enter the season (Summer/Winter): ");
            var season = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(season))
            {
                Console.WriteLine("Season cannot be empty or whitespace.");
                return;
            }

            season = season.Trim().ToLower();

            if (season != "summer" && season != "winter")
            {
                Console.WriteLine("Invalid season. Please enter 'Summer' or 'Winter'.");
                return;
            }

            string destination;
            string vacationType;
            decimal amountSpent;

            if (budget <= 100)
            {
                destination = "Bulgaria";

                if (season == "summer")
                {
                    vacationType = "Camp";
                    amountSpent = budget * 0.3m;
                }
                else // winter
                {
                    vacationType = "Hotel";
                    amountSpent = budget * 0.7m;
                }
            }
            else if (budget <= 1000)
            {
                destination = "Balkans";

                if (season == "summer")
                {
                    vacationType = "Camp";
                    amountSpent = budget * 0.4m;
                }
                else // winter
                {
                    vacationType = "Hotel";
                    amountSpent = budget * 0.8m;
                }
            }
            else
            {
                destination = "Europe";
                vacationType = "Hotel";
                amountSpent = budget * 0.9m;
            }

            Console.WriteLine($"Somewhere in {destination}");
            Console.WriteLine($"{vacationType} - {amountSpent:F2}");
        }
    }
}
