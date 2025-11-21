namespace MinorOrAdult
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int age;

            while (true)
            {
                Console.Write("Enter your age: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out age))
                {
                    if (age >= 0 && age <= 120)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid age. Please enter an integer value between 0 and 120.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid age as integer number.");
                }
            }

            //if (age < 18)
            //{
            //    Console.WriteLine("You are a minor.");
            //}
            //else
            //{
            //    Console.WriteLine("You are an adult.");
            //}

            string result = age < 18 ? "You are a minor." : "You are an adult.";
            Console.WriteLine(result);
        }
    }
}
