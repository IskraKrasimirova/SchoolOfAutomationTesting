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

            string result;

            if (grade == 6)
            {
                result = "Excellent";
            }
            else if (grade == 5)
            {
                result = "Very good";
            }
            else if (grade == 4)
            {
                result = "Good";
            }
            else if (grade == 3)
            {
                result = "Satisfactory";
            }
            else  // grade == 2
            {
                result = "Poor";
            }

            Console.WriteLine(result);

            // 2. With ternary operator
            //string result = grade == 6 ? "Excellent" :
            //    grade == 5 ? "Very good" :
            //    grade == 4 ? "Good" :
            //    grade == 3 ? "Satisfactory" :
            //    "Poor";

            //Console.WriteLine(result);

            //3. Intellicence suggestion for switch expression
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
