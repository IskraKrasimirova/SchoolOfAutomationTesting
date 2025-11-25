namespace PrintNames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var names = new string[]
            {
                "Iskra",
                "Maria",
                "Ani",
                "George",
                "Peter",
                "Ivan",
                "Ivan",
                "Dimitar"
            };

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
