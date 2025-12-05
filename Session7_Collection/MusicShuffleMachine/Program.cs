using System.ComponentModel;

namespace MusicShuffleMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<(string, string)> songs =
            [
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
                ("Everything I Do", "6:38"),                    // Bryan Adams
                ("Open Your Heart", "4:29"),                    // Madonna
                ("One", "4:36")                                 // U2
            ];

            Console.Write("Enter desired playlist length in minutes: ");
            var isValidLength = int.TryParse(Console.ReadLine(), out int desiredLength);

            if (isValidLength && desiredLength > 0)
            {
                var customPlaylist = GetCustomPlaylistByDesiredLenght(songs, desiredLength);
                Console.WriteLine($"Custom playlist for {desiredLength} minutes and {customPlaylist.Count} songs:");

                foreach (var (song, durationInSeconds) in customPlaylist)
                {
                    Console.WriteLine($"{song} - {durationInSeconds / 60}:{durationInSeconds % 60:D2}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for playlist length.");
            }

            var groupedSongs = GroupSongsByFirstLetter(songs);
            var sortedGroupedSongs = groupedSongs.OrderBy(k => k.Key).ToDictionary();
            Console.WriteLine("Songs grouped by first letter:");

            foreach (var kvp in sortedGroupedSongs)
            {
                var quatedSong = kvp.Value.Select(s => $"\"{s}\"");
                Console.WriteLine($"{kvp.Key}: {string.Join(", ", quatedSong)}");
            }
        }

        private static Dictionary<char, List<string>> GroupSongsByFirstLetter(List<(string, string)> songs)
        {
            var groupedSongs = new Dictionary<char, List<string>>();

            foreach (var (song, duration) in songs)
            {
                var firstLetter = char.ToUpper(song[0]);

                if (!groupedSongs.ContainsKey(firstLetter))
                {
                    groupedSongs[firstLetter] = new List<string>();
                }

                groupedSongs[firstLetter].Add(song);
            }

            return groupedSongs;
        }

        private static List<(string, int)> GetCustomPlaylistByDesiredLenght(List<(string, string)> songs, int length)
        {
            var legthInSeconds = length * 60;
            var totalDuration = 0;
            var customPlaylist = new List<(string, int)>();
            var shuffledSongs = ShuffleSongs(songs);

            foreach (var (song, duration) in shuffledSongs)
            {
                var durationInSeconds = ConvertSongDurationToSeconds(duration);

                if (totalDuration + durationInSeconds <= legthInSeconds)
                {
                    customPlaylist.Add((song, durationInSeconds));
                    totalDuration += durationInSeconds;
                }

                if (totalDuration >= legthInSeconds)
                {
                    break;
                }
            }

            return customPlaylist;
        }

        private static List<(string song, string duration)> ShuffleSongs(List<(string, string)> songs)
        {
            var shuffledSongs = new List<(string, string)>(songs);
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