namespace StudentScores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var studentScores = new Dictionary<string, int>();

            // Simple decision with hardcoded student names and scores
            //studentScores.Add("Nikola", 85);
            //studentScores.Add("Bob", 92);
            //studentScores.Add("George", 78);
            //studentScores.Add("Ana", 91);
            //studentScores.Add("Ivan", 76);
            //studentScores["Diana"] = 90;
            //studentScores["Eve"] = 88;
            //studentScores["Maria"] = 95;
            //studentScores["Petar"] = 70;
            //studentScores["Viki"] = 87;

            //var averageScore = studentScores.Values.Average();
            //Console.WriteLine($"The average score of the students is {averageScore:F2}");

            // Decision based on user input
            Console.WriteLine("Enter students names and scores in format: Name, Score");

            while (true)
            {
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("End of input data.");
                    break;
                }

                var parts = input.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2 || !int.TryParse(parts[1].Trim(), out int score))
                {
                    Console.WriteLine("Invalid input. Please enter data in format: Name, Score");
                    continue;
                }

                var name = parts[0].Trim();

                if (!studentScores.ContainsKey(name))
                {
                    studentScores.Add(name, score);
                    Console.WriteLine($"Added student {name} with score {score}");
                }
                else
                {
                    studentScores[name] = score;
                    Console.WriteLine($"Student {name} already exists with score {studentScores[name]}. The score will be replaced with {score}");
                }
            }

            if (studentScores.Count > 0)
            {
                Console.WriteLine("Students scores: ");

                foreach (var kvp in studentScores)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }

                var averageScore = studentScores.Values.Average();
                Console.WriteLine($"The average score of the students is {averageScore:F2}");
            }
            else
            {
                Console.WriteLine("No student data was entered.");
            }
        }
    }
}
