namespace SimpleCalculator
{
    internal static class Calculator
    {
        public static double Add(double a, double b) => a + b;
        public static double Subtract(double a, double b) => a - b;
        public static double Multiply(double a, double b) => a * b;
        public static double Divide(double a, double b) => a / b;
        public static void Modulus(double a, double b)
        {
            try
            {
                Console.WriteLine($"Modulus with double: {a} % {b} = {a % b}");
                Console.WriteLine($"Modulus with decimal: {a} % {b} = {(decimal)a % (decimal)b}");
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
        }
        public static double Exponentiate(double baseNumber, double exponent) => Math.Pow(baseNumber, exponent);
        public static double SquareRoot(double number) => Math.Sqrt(number);
    }
}
