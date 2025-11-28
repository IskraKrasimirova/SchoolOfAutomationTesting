namespace Greeting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter your name (Type End for exit): ");
                var name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("The name cannot empty or whitespace.");
                    continue;
                }

                if (name.Equals("end", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("End of program.");
                    break;
                }

                var greeting = MagicGreeting(name);
                Console.WriteLine(greeting);
            }
           
        }

        private static string MagicGreeting(string name)
        {
            string[] greetings =
            [
                $"Welcome, mighty {name}!",
                $"Greetings, traveler {name}!",
                $"Behold, the legendary {name} has arrived!",
                $"A new star {name} appeared!",
                $"{name} joined the party!",
                $"{name} appeared on the scene!",
                $"Cheers for {name}!"
            ];

            var random = new Random();
            int index = random.Next(greetings.Length);

            return greetings[index];
        }
    }
}

