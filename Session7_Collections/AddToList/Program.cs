namespace AddToList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string>();
            names.Add("Maria");
            names.Add("George");
            names.AddRange(["Ana", "Petar", "Ivan"]);

            Console.WriteLine(string.Join(", ", names));
        }
    }
}
