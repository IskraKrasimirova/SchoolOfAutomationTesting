using Microsoft.Extensions.Configuration;

namespace LoginSystem
{
    internal class Program
    {
        private const int MaxLoginAttempts = 3;

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var registeredUsername = config["Username"];
            var registeredPassword = config["Password"];

            if (string.IsNullOrEmpty(registeredUsername) || string.IsNullOrEmpty(registeredPassword))
            {
                Console.WriteLine("User Secrets are not configured.");
                return;
            }

            var loginAttempts = 0;

            while (loginAttempts < MaxLoginAttempts)
            {
                Console.Write("Enter your username: ");
                var username = Console.ReadLine();

                if (username == registeredUsername)
                {
                    Console.Write("Enter your password: ");
                    var password = Console.ReadLine();

                    if (password == registeredPassword)
                    {
                        Console.WriteLine("Login successful!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect password.");
                    }
                }
                else
                {
                    Console.WriteLine("Username not found.");
                }

                loginAttempts++;

                if (loginAttempts == MaxLoginAttempts)
                {
                    Console.WriteLine("Maximum login attempts exceeded. Access denied.");
                }
                else
                {
                    Console.WriteLine($"Login attempts remaining: {MaxLoginAttempts - loginAttempts}");
                } 
            } 
        }
    }
}
