namespace MusicShuffleMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<(string song, string duration)> songs = new()
            {
                ("Nothing Else Matters", "6:28"),               // Metallica
                ("November Rain", "8:57"),                      // Guns N' Roses
                ("Stairway to Heaven", "8:02"),                 // Led Zeppelin
                ("Smoke on the Water", "5:40"),                 // Deep Purple
                ("Sweet Child O' Mine", "5:56"),                // Guns N' Roses
                ("Still Loving You", "6:57"),                   // Scorpions
                ("Send Me an Angel", "4:33"),                   // Scorpions
                ("Bohemian Rhapsody", "5:55"),                  // Queen
                ("Brothers in Arms", "6:55"),                   // Dire Straits
                ("Black", "5:43"),                              // Pearl Jam
                ("Hotel California", "6:30"),                   // Eagles
                ("Dream On", "4:28"),                           // Aerosmith
                ("Crazy", "5:16"),                              // Aerosmith
                ("Knockin' on Heaven's Door", "5:36"),          // Bob Dylan / Guns N' Roses cover
                ("Wish You Were Here", "5:34"),                 // Pink Floyd
                ("With or Without You", "4:56"),                // U2
                ("Livin' on a Prayer", "4:10"),                 // Bon Jovi
                ("Always", "6:00"),                             // Bon Jovi
                ("Patience", "5:56"),                           // Guns N' Roses
                ("Creep", "3:58"),                              // Radiohead
                ("Every Breath You Take", "4:13"),              // The Police
                ("I Want to Know What Love Is", "5:04"),        // Foreigner
                ("(Everything I Do) I Do It for You", "6:38"),  // Bryan Adams
                ("Open Your Heart", "4:29"),                    // Madonna
                ("One", "4:36")                                 // U2
            };

            var shuffledSongs = ShuffleSongs(songs);

            foreach (var (song, duration) in shuffledSongs)
            {
                var durationInSeconds = ConvertSongDurationToSeconds(duration);
                Console.WriteLine($"{song} - {duration} -> {durationInSeconds}");
            }
        }

        private static List<(string song, string duration)> ShuffleSongs(List<(string song, string duration)> songs)
        {
            var shuffledSongs = new List<(string song, string duration)>(songs);
            var random = new Random();

            for (int i = shuffledSongs.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (shuffledSongs[i], shuffledSongs[j]) = (shuffledSongs[j], shuffledSongs[i]);
            }

            return shuffledSongs;
        }

        private static int ConvertSongDurationToSeconds(string duration)
        {
            var parts = duration.Split(':');
            var minutes = int.Parse(parts[0]);
            var seconds = int.Parse(parts[1]);

            return minutes * 60 + seconds;
        }
    }
}
