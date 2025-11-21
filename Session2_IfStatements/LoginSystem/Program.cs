using Microsoft.Extensions.Configuration;

namespace LoginSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            string registeredUsername = config["Username"];
            string registeredPassword = config["Password"];

            Console.Write("Enter your username: ");
            string? username = Console.ReadLine();

            if (username == registeredUsername)
            {
                Console.Write("Enter your password: ");
                string? password = Console.ReadLine();

                if (password == registeredPassword)
                {
                    Console.WriteLine("Login successful!");
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
        }
    }
}
