namespace HomeworkUtilities
{
    internal class IntOperation
    {
        public void RunArithmeticOperations(int firstNumber, int secondNumber)
        {
            Console.WriteLine($"Arithmetic operations with integer numbers {firstNumber} and {secondNumber}:");
            Console.WriteLine($"Addition: {firstNumber + secondNumber}");
            Console.WriteLine($"Subtraction: {firstNumber - secondNumber}");
            Console.WriteLine($"Multiplication: {firstNumber * secondNumber}");

            if (secondNumber != 0)
            {
                Console.WriteLine($"Division: {firstNumber / secondNumber}");
                Console.WriteLine($"Modulus: {firstNumber % secondNumber}");
            }
            else
            {
                Console.WriteLine("Division by zero is not allowed.");
            }
        }

        public void RunLogicalOperations(int a, int b)
        {
            Console.WriteLine($"Logical operations between {a} and {b}:");
            Console.WriteLine($"{a} > {b}: {a > b}");
            Console.WriteLine($"{a} < {b}: {a < b}");
            Console.WriteLine($"{a} == {b}: {a == b}");
            Console.WriteLine($"{a} != {b}: {a != b}");
            Console.WriteLine($"Both positive: {a > 0 && b > 0}");
            Console.WriteLine($"At least one negative: {a < 0 || b < 0}");
        }
    }
}
