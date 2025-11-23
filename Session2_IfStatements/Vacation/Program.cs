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

        private enum Destination
        {
            Bulgaria,
            Balkans,
            Europe
        };

        private enum VacationType
        {
            Camp,
            Hotel
        };

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
                Console.WriteLine($"Budget must be between {MinBudget:F2} and {MaxBudget:F2}.");
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

            var (destination, vacationType, amountSpent) = GetVacationDetails(budget, season);

            Console.WriteLine($"Somewhere in {destination}");
            Console.WriteLine($"{vacationType} - {amountSpent:F2}");
        }

        private static (Destination destination, VacationType vacationType, decimal amountSpent) GetVacationDetails(decimal budget, string season)
        {
            Destination destination;
            VacationType vacationType;
            decimal amountSpent;

            if (budget <= BulgariaLimit)
            {
                destination = Destination.Bulgaria;

                if (season == "summer")
                {
                    vacationType = VacationType.Camp;
                    amountSpent = budget * BulgariaSummerPercent;
                }
                else // winter
                {
                    vacationType = VacationType.Hotel;
                    amountSpent = budget * BulgariaWinterPercent;
                }
            }
            else if (budget <= BalkansLimit)
            {
                destination = Destination.Balkans;

                if (season == "summer")
                {
                    vacationType = VacationType.Camp;
                    amountSpent = budget * BalkansSummerPercent;
                }
                else // winter
                {
                    vacationType = VacationType.Hotel;
                    amountSpent = budget * BalkansWinterPercent;
                }
            }
            else
            {
                destination = Destination.Europe;
                vacationType = VacationType.Hotel;
                amountSpent = budget * EuropePercent;
            }

            return (destination, vacationType, amountSpent);
        }
    }
}
