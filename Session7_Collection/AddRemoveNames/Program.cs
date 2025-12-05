
namespace AddRemoveNames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a list of names separated by a comma: ");
            var names = Console.ReadLine()!
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(name => name.Trim())
                .Where(name => !string.IsNullOrEmpty(name))
                .ToList();

            PrintList(names);

            while (true)
            {
                Console.Write("Select an option Add / Remove / Print / Exit: ");
                var option = Console.ReadLine()!.Trim().ToLower();

                switch (option)
                {
                    case "add":
                        Console.Write("Enter a name to add to the list: ");
                        var nameToAdd = Console.ReadLine()!.Trim();
                        AddName(names, nameToAdd);
                        break;
                    case "remove":
                        Console.Write("Enter a name to remove from the list: ");
                        var nameToRemove = Console.ReadLine()!.Trim();
                        RemoveName(names, nameToRemove);
                        break;
                    case "print":
                        PrintList(names);
                        break;
                    case "exit":
                        Console.WriteLine("Exiting the program.");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void AddName(List<string> names, string nameToAdd)
        {
            if (!string.IsNullOrWhiteSpace(nameToAdd))
            {
                names.Add(nameToAdd);
                Console.WriteLine($"Added '{nameToAdd}' to the list.");
            }
            else
            {
                Console.WriteLine("Name cannot be empty.");
            }
        }

        private static void RemoveName(List<string> names, string nameToRemove)
        {
            if (names.Remove(nameToRemove))
            {
                Console.WriteLine($"Removed '{nameToRemove}' from the list.");
            }
            else
            {
                Console.WriteLine($"Name '{nameToRemove}' not found in the list.");
            }
        }

        private static void PrintList(List<string> names)
        {
            string message;

            if (names.Count == 0)
            {
                message = "The list is empty.";
            }
            else
            {
                message = $"The list has {names.Count} names: {string.Join(", ", names)}";
            }

            Console.WriteLine(message);
        }
    }
}
