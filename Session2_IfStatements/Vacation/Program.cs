namespace Vacation
{
    internal class Program
    {
        private const decimal MinBudget = 10.00m;
        private const decimal MaxBudget = 5000.00m;
        private const decimal BulgariaLimit = 100.00m;
        private const decimal BalkansLimit = 1000.00m;
        private const decimal BulgariaSummerPercent = 0.3m;
        private const decimal BulgariaWinterPercent = 0.7m;
        private const decimal BalkansSummerPercent = 0.4m;
        private const decimal BalkansWinterPercent = 0.8m;
        private const decimal EuropePercent = 0.9m;

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

            if (budget < MinBudget || budget > MaxBudget)
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

            if (budget <= BulgariaLimit)
            {
                destination = "Bulgaria";

                if (season == "summer")
                {
                    vacationType = "Camp";
                    amountSpent = budget * BulgariaSummerPercent;
                }
                else // winter
                {
                    vacationType = "Hotel";
                    amountSpent = budget * BulgariaWinterPercent;
                }
            }
            else if (budget <= BalkansLimit)
            {
                destination = "Balkans";

                if (season == "summer")
                {
                    vacationType = "Camp";
                    amountSpent = budget * BalkansSummerPercent;
                }
                else // winter
                {
                    vacationType = "Hotel";
                    amountSpent = budget * BalkansWinterPercent;
                }
            }
            else
            {
                destination = "Europe";
                vacationType = "Hotel";
                amountSpent = budget * EuropePercent;
            }

            Console.WriteLine($"Somewhere in {destination}");
            Console.WriteLine($"{vacationType} - {amountSpent:F2}");
        }
    }
}
