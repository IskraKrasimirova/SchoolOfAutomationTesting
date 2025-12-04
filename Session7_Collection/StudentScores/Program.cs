namespace StudentScores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var studentScores = new Dictionary<string, int>();
            studentScores.Add("Nikola", 85);
            studentScores.Add("Bob", 92);
            studentScores.Add("George", 78);
            studentScores.Add("Ana", 91);
            studentScores.Add("Ivan", 76);
            studentScores["Diana"] = 90;
            studentScores["Eve"] = 88;
            studentScores["Maria"] = 95;
            studentScores["Petar"] = 70;
            studentScores["Viki"] = 87;

            var averageScore = studentScores.Values.Average();

            Console.WriteLine($"The average score of the students is {averageScore:F2}");
        }
    }
}
