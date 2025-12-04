namespace RemoveFromList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string> { "Diana", "Eve", "Petar", "Ivan", "Viki", "Nikola", "Maria", "George", "Tom", "Ana" };
            names.Remove("Tom");
            names.RemoveRange(1, 3);

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
