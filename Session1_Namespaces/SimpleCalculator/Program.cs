namespace SimpleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select one of the allowed calculator operations");
                Console.WriteLine("Press 1 for Addition, 2 Subtraction, 3 Multiplication, 4 Division, 5 Modulus, 0 Exit");

                string? input = Console.ReadLine();
                bool isValidInt = Int32.TryParse(input, out int operation);

                if (!isValidInt)
                {
                    Console.WriteLine("The input is not a valid integer number!");
                    continue;
                }

                if (operation == 0)
                {
                    Console.WriteLine("Exit the calculator");
                    break;
                }

                if (operation < 0 || operation > 5)
                {
                    Console.WriteLine("Selected operation is not supported.");
                    continue;
                }

                Console.Write("Enter first number: ");
                string? firstInput = Console.ReadLine();
                bool isValidFirst = double.TryParse(firstInput, out double firstNumber);

                Console.Write("Enter second number: ");
                string? secondInput = Console.ReadLine();
                bool isValidSecond = double.TryParse(secondInput, out double secondNumber);

                if (!isValidFirst || !isValidSecond)
                {
                    Console.WriteLine("One or both input values are not valid numbers.");
                    continue;
                }

                switch (operation)
                {
                    case 1:
                        Console.WriteLine($"{firstNumber} + {secondNumber} = {firstNumber + secondNumber}");
                        break;
                    case 2:
                        Console.WriteLine($"{firstNumber} - {secondNumber} = {firstNumber - secondNumber}");
                        break;
                    case 3:
                        Console.WriteLine($"{firstNumber} * {secondNumber} = {firstNumber * secondNumber}");
                        break;
                    case 4:
                        if (secondNumber != 0)
                            Console.WriteLine($"{firstNumber} / {secondNumber} = {firstNumber / secondNumber}");
                        else
                            Console.WriteLine("Division by zero is not allowed.");
                        break;
                    case 5:
                        if (secondNumber != 0)
                            Console.WriteLine($"{firstNumber} % {secondNumber} = {firstNumber % secondNumber}");
                        else
                            Console.WriteLine("Modulus by zero is not allowed.");
                        break;
                    default:
                        Console.WriteLine("Selected operation is not supported.");
                        break;
                }
            }
        }
    }
}
