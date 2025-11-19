namespace HomeworkUtilities
{
    internal class Program
    {
        private const string HOMEWORK_NAME = "Session1-Namespaces";

        static void Main(string[] args)
        {
            Console.WriteLine(HOMEWORK_NAME);

            Printer printer = new Printer();
            printer.PrintMessage();

            var myVar = new Variable();
            myVar.PrintVariables();

            var operation = new IntOperation();
            operation.RunArithmeticOperations(10, 5);
            operation.RunArithmeticOperations(5, 10);
            operation.RunArithmeticOperations(-10, -3);
            operation.RunArithmeticOperations(10, -3);
            operation.RunArithmeticOperations(0, 5);
            operation.RunArithmeticOperations(0, -5);
            operation.RunArithmeticOperations(5, 5);
            operation.RunArithmeticOperations(3, 0);
            operation.RunArithmeticOperations(0, 0);
            // Overflow and not correct results. OverflowException for Division and Modulus
            //operation.RunArithmeticOperations(int.MaxValue, 1);
            //operation.RunArithmeticOperations(int.MinValue, -1);

            operation.RunLogicalOperations(10, 5);
            operation.RunLogicalOperations(5, 10);
            operation.RunLogicalOperations(-10, -3);
            operation.RunLogicalOperations(10, -3);
            operation.RunLogicalOperations(0, 5);
            operation.RunLogicalOperations(0, -5);
            operation.RunLogicalOperations(5, 5);
            operation.RunLogicalOperations(3, 0);
            operation.RunLogicalOperations(0, 0);
        }
    }

    public class Variable
    {
        private const double PI = Math.PI;
        static int counter = 0;

        public void PrintVariables()
        {
            int number = default;
            double doubleNumber = -123.00000009;
            decimal money = 123.4567m;
            string name = "Iskra";
            var text = "First homework";
            char sharpSymbol = '#';
            DateTime currentDate = DateTime.UtcNow;

            counter++;

            Console.WriteLine($"{name}: The {text} in C{sharpSymbol}");
            Console.WriteLine($"Default value of integer number -> {number}");
            Console.WriteLine($"Const PI -> {PI}. Double number -> {doubleNumber}. Double number MinValue: {double.MinValue}");
            Console.WriteLine($"Counter {counter}: {money}");
            Console.WriteLine($"Today is {currentDate.DayOfWeek}, {currentDate}. Year {currentDate.Year}, month {currentDate.Month}, day {currentDate.Day}, the {currentDate.DayOfYear} day of year.");
        }
    }
}
