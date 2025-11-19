namespace SimpleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select one of the allowed calculator operations:");
                Console.WriteLine("Press 1 for Addition, 2 Subtraction, 3 Multiplication, 4 Division, 5 Modulus, 6 Exponentiation, 7 Square Root, 0 Exit");

                string? input = Console.ReadLine();
                bool isValidInt = Int32.TryParse(input, out int operation);

                if (!isValidInt)
                {
                    Console.WriteLine("The input is not a valid integer number!");
                    continue;
                }

                if (operation == 0)
                {
                    Console.WriteLine("Exit calculator");
                    break;
                }

                if (operation < 0 || operation > 7)
                {
                    Console.WriteLine("Selected operation is not supported.");
                    continue;
                }

                switch (operation)
                {
                    case 1:
                        {
                            var (firstNumber, secondNumber) = GetTwoDoubleNumbers();
                            Console.WriteLine($"{firstNumber} + {secondNumber} = {firstNumber + secondNumber}");
                            break;
                        }
                    case 2:
                        {
                            var (firstNumber, secondNumber) = GetTwoDoubleNumbers();
                            Console.WriteLine($"{firstNumber} - {secondNumber} = {firstNumber - secondNumber}");
                            break;
                        }
                    case 3:
                        {
                            var (firstNumber, secondNumber) = GetTwoDoubleNumbers();
                            Console.WriteLine($"{firstNumber} * {secondNumber} = {firstNumber * secondNumber}");
                            break;
                        }
                    case 4:
                        {
                            var (firstNumber, secondNumber) = GetTwoDoubleNumbers();
                            Console.WriteLine($"{firstNumber} / {secondNumber} = {firstNumber / secondNumber}");
                            break;
                        }
                    case 5:
                        {
                            var (firstNumber, secondNumber) = GetTwoDoubleNumbers();

                            try
                            {
                                Console.WriteLine($"Modulus with double: {firstNumber} % {secondNumber} = {firstNumber % secondNumber}");
                                Console.WriteLine($"Modulus with decimal: {firstNumber} % {secondNumber} = {(decimal)firstNumber % (decimal)secondNumber}");
                            }
                            catch (DivideByZeroException ex) 
                            {
                                Console.WriteLine($"Modulus by zero is not allowed. {ex.Message}");
                            }
                            catch (OverflowException ex)
                            {
                                Console.WriteLine($"OverflowException: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }
                    case 6:
                        {
                            double baseNumber = ReadValidDoubleNumber("Enter base number: ");
                            double exponent = ReadValidDoubleNumber("Enter exponent: ");
                            Console.WriteLine($"{baseNumber} ^ {exponent} = {Math.Pow(baseNumber, exponent)}");
                            break;
                        }
                    case 7:
                        {
                            // Square root of negative number is NaN
                            double number = ReadValidDoubleNumber("Enter number to calculate square root: ");
                            Console.WriteLine($"Square root of {number} = {Math.Sqrt(number)}");
                            break;
                        }
                    default:
                        Console.WriteLine("Selected operation is not supported.");
                        break;
                }
            }
        }

        private static double ReadValidDoubleNumber(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();
                bool isValidDoubleNumber = double.TryParse(input, out double number);

                if (isValidDoubleNumber)
                {
                    // doubleNumber > double.MaxValue or doubleNumber < double.MinValue is shown as Infinity
                    if (double.IsInfinity(number))
                    {
                        Console.WriteLine("Warning: You entered a number that is too large and will be treated as Infinity.");
                    }

                    return number;
                }

                Console.WriteLine("Input is not a valid number. Please try again.");
            }
        }

        private static (double firstNumber, double secondNumber) GetTwoDoubleNumbers()
        {
            double firstNumber = ReadValidDoubleNumber("Enter first number: ");
            double secondNumber = ReadValidDoubleNumber("Enter second number: ");
            return (firstNumber, secondNumber);
        }
    }
}
