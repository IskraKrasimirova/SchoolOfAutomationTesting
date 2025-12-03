namespace Email
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? email;

            while (true)
            {
                Console.Write("Enter email: ");
                email = Console.ReadLine();

                if (IsValidEmail(email))
                {
                    var domain = email?.Split('@')[1];
                    //var domain = email.Substring(email.IndexOf('@') + 1); // Another way to extract domain
                    Console.WriteLine($"The domain is '{domain}'.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid email address. Please try again.");
                }
            }
        }

        private static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (!email.Contains('@'))
                return false;

            return true;
        }
    }
}
// Extract domain from email