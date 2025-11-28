using System.Text;

namespace DiceBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartBattle();
        }

        private static int RollDice()
        {
            var random = new Random();
            return random.Next(1, 7);
        }

        private static void BattleRound(int playerRoll, int enemyRoll)
        {
            string result;

            if (playerRoll > enemyRoll)
            {
                result = "Player wins the round!";
            }
            else if (playerRoll < enemyRoll)
            {
                result = "Enemy wins the round!";
            }
            else
            {
                result = "It's a tie!";
            }

            Console.WriteLine(result);
        }

        private static void AnnounceWinner(int playerScore, int enemyScore)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Final Score - Player: {playerScore}, Enemy: {enemyScore}.");

            if (playerScore > enemyScore)
            {
                sb.AppendLine("Player wins the battle!");
            }
            else if (playerScore < enemyScore)
            {
                sb.AppendLine("Enemy wins the battle!");
            }
            else
            {
                sb.AppendLine("The battle ends in a draw!");
            }

            Console.WriteLine(sb.ToString());
        }

        private static void StartBattle()
        {
            int playerScore = 0;
            int enemyScore = 0;
            int rounds = 5;

            for (int i = 0; i < rounds; i++)
            {
                int playerRoll = RollDice();
                int enemyRoll = RollDice();

                Console.WriteLine($"Round {i + 1}: Player rolled {playerRoll}, Enemy rolled {enemyRoll}");
                BattleRound(playerRoll, enemyRoll);

                if (playerRoll > enemyRoll)
                {
                    playerScore++;
                }
                else if (playerRoll < enemyRoll)
                {
                    enemyScore++;
                }
            }

            AnnounceWinner(playerScore, enemyScore);
        }
    }
}
