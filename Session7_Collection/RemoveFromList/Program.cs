namespace RemoveFromList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string> { "Eve", "Petar", "Diana", "Ivan", "Viki", "Nikola", "Maria", "George", "Tom", "Ana" };
            names.Remove("Tom");
            names.RemoveRange(1, 3);
            names.RemoveAt(2); // This line might cause an ArgumentOutOfRangeException depending on the entries left in the list
            names.Remove("Tom"); // This line will not cause an error, but it won't remove anything since "Tom" is already removed

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
