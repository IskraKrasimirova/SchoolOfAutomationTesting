namespace AddToList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string>();
            names.Add("Ana");
            names.Add("Bob");
            names.Add("George");
            names.AddRange(["Diana", "Eve", "Petar", "Ivan", "Viki", "Nikola", "Maria"]);

            Console.WriteLine(string.Join(", ", names));
        }
    }
}
