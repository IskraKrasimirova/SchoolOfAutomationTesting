namespace Grade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int grade;

            while (true)
            {
                Console.Write("Enter your grade (2–6): ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out grade))
                {
                    if (grade >= 2 && grade <= 6)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid grade. Please enter an integer value between 2 and 6.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid grade as integer number.");
                }
            }

            if (grade == 6)
            {
                Console.WriteLine("Excellent");
            }    
            else if (grade == 5)
            {
                Console.WriteLine("Very good");
            }
            else if (grade == 4)
            {
                Console.WriteLine("Good");
            }
            else if (grade == 3)
            {
                Console.WriteLine("Satisfactory");
            }
            else
            {
                // grade == 2
                Console.WriteLine("Poor");
            }

            //Intellicence suggestion for switch expression
            //string result = grade switch
            //{
            //    2 => "Fail",
            //    3 => "Poor",
            //    4 => "Good",
            //    5 => "Very Good",
            //    6 => "Excellent",
            //    _ => "Unknown grade"
            //};

            //Console.WriteLine(result);
        }
    }
}
