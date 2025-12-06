namespace MusicShuffleMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<(string, string)> songs =
            [
                ("Nothing Else Matters", "6:28"),               
                ("November Rain", "8:57"),                      
                ("Stairway to Heaven", "8:02"),                 
                ("Smoke on the Water", "5:40"),                 
                ("Sweet Child O' Mine", "5:56"),                
                ("Still Loving You", "6:57"),                   
                ("Send Me an Angel", "4:33"),                   
                ("Bohemian Rhapsody", "5:55"),                  
                ("Brothers in Arms", "6:55"),                   
                ("Black", "5:43"),                              
                ("Hotel California", "6:30"),                   
                ("Dream On", "4:28"),                           
                ("Crazy", "5:16"),                              
                ("Knockin' on Heaven's Door", "5:36"),          
                ("Wish You Were Here", "5:34"),                 
                ("With or Without You", "4:56"),                
                ("Livin' on a Prayer", "4:10"),                 
                ("Always", "6:00"),                             
                ("Patience", "5:56"),                           
                ("Creep", "3:58"),                              
                ("Every Breath You Take", "4:13"),              
                ("I Want to Know What Love Is", "5:04"),        
                ("Everything I Do", "6:38"),                    
                ("Open Your Heart", "4:29"),                    
                ("One", "4:36")                                 
            ];

            Console.Write("Enter desired playlist length in minutes: ");
            var isValidLength = int.TryParse(Console.ReadLine(), out int desiredLength);

            if (isValidLength && desiredLength > 0)
            {
                // Simple greedy playlist generation
                var customPlaylist = GetCustomPlaylistByDesiredLength(songs, desiredLength);
                PrintPlaylist("Simple playlist generation:", customPlaylist, desiredLength);

                // Randomized greedy playlist generation
                var randomizedPlaylist = GetRandomizedGreedyPlaylist(songs, desiredLength, iterations: 200);
                PrintPlaylist("Randomized Greedy Playlist:", randomizedPlaylist, desiredLength);

                // More optimal playlist using dynamic programming (Knapsack problem)
                var optimalPlaylist = GetOptimalPlaylist(songs, desiredLength);
                PrintPlaylist("Optimal playlist generation:", optimalPlaylist, desiredLength);
            }
            else
            {
                Console.WriteLine("Invalid input for playlist length.");
            }

            var groupedSongs = GroupSongsByFirstLetter(songs);
            var sortedGroupedSongs = groupedSongs.OrderBy(k => k.Key).ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("Songs grouped by first letter:");

            foreach (var kvp in sortedGroupedSongs)
            {
                var quatedSongs = kvp.Value.Select(s => $"\"{s}\"");
                Console.WriteLine($"{kvp.Key}: {string.Join(", ", quatedSongs)}");
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

        private static void PrintPlaylist(string title, List<(string song, int duration)> playlist, int desiredLength)
        {
            var totalPlaylistDuration = playlist.Sum(s => s.duration);

            Console.WriteLine(title);
            Console.WriteLine($"The playlist has {playlist.Count} songs and a duration of {totalPlaylistDuration / 60}:{totalPlaylistDuration % 60:D2} minutes");
            Console.WriteLine($"The playlist for desired {desiredLength} minutes:");

            foreach (var (song, duration) in playlist)
            {
                Console.WriteLine($"{song} - {duration / 60}:{duration % 60:D2}");
            }
        }

        // Simple greedy playlist generation
        private static List<(string song, int duration)> GetCustomPlaylistByDesiredLength(List<(string, string)> songs, int length)
        {
            int legthInSeconds = length * 60;
            int totalDuration = 0;
            var customPlaylist = new List<(string, int)>();
            var shuffledSongs = ShuffleSongs(songs);

            foreach (var (song, duration) in shuffledSongs)
            {
                int durationInSeconds = ConvertSongDurationToSeconds(duration);

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

        // Randomized greedy playlist generation
        private static List<(string song, int duration)> GetRandomizedGreedyPlaylist(List<(string, string)> songs, int desiredMinutes, int iterations = 100)
        {
            int targetSeconds = desiredMinutes * 60;
            List<(string song, int duration)> bestPlaylist = new();
            int bestDuration = 0;

            for (int attempt = 0; attempt < iterations; attempt++)
            {
                // Generate a new playlist using the greedy approach on a shuffled list
                var currentPlaylist = GetCustomPlaylistByDesiredLength(songs, desiredMinutes);
                int currentDuration = currentPlaylist.Sum(s => s.duration);

                if (currentDuration > bestDuration)
                {
                    bestDuration = currentDuration;
                    bestPlaylist = currentPlaylist;
                }

                if (bestDuration == targetSeconds)
                {
                    break;
                }
            }

            return bestPlaylist;
        }

        // More optimal playlist using dynamic programming (Knapsack problem)
        private static List<(string song, int duration)> GetOptimalPlaylist(List<(string songName, string duration)> songs, int desiredMinutes)
        {
            int targetSeconds = desiredMinutes * 60;
            int songCount = songs.Count;
            int[] songDurations = songs.Select(s => ConvertSongDurationToSeconds(s.duration)).ToArray();

            int[] bestDurations = new int[targetSeconds + 1];
            int[] selectedSongIndex = new int[targetSeconds + 1]; 
            Array.Fill(selectedSongIndex, -1);

            for (int songIndex = 0; songIndex < songCount; songIndex++)
            {
                int currentDuration = songDurations[songIndex];

                for (int time = targetSeconds; time >= currentDuration; time--)
                {
                    int possibleDuration = bestDurations[time - currentDuration] + currentDuration;

                    if (possibleDuration > bestDurations[time])
                    {
                        bestDurations[time] = possibleDuration;
                        selectedSongIndex[time] = songIndex;

                        // Early exit if targetSeconds is reached
                        if (bestDurations[time] == targetSeconds)
                        {
                            return BuildPlaylist(songs, songDurations, selectedSongIndex, targetSeconds);
                        }
                    }
                }
            }

            // If there is no perfect match, return the best found
            int bestTime = bestDurations.Max();
            return BuildPlaylist(songs, songDurations, selectedSongIndex, bestTime);
        }

        // Helper method to reconstruct the playlist from DP arrays
        private static List<(string song, int duration)> BuildPlaylist(
            List<(string songName, string duration)> songs,
            int[] songDurations,
            int[] selectedSongIndex,
            int bestTime)
        {
            var playlist = new List<(string song, int duration)>();
            int remainingTime = bestTime;

            while (remainingTime > 0 && selectedSongIndex[remainingTime] != -1)
            {
                int songIndex = selectedSongIndex[remainingTime];
                int duration = songDurations[songIndex];
                playlist.Add((songs[songIndex].songName, duration));
                remainingTime -= duration;
            }

            playlist.Reverse();
            return playlist;
        }

        // Shuffle the list of songs using Fisher-Yates algorithm
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

        // Convert song duration from "mm:ss" format to total seconds
        private static int ConvertSongDurationToSeconds(string duration)
        {
            var parts = duration.Split(':');
            var minutes = int.Parse(parts[0]);
            var seconds = int.Parse(parts[1]);

            return minutes * 60 + seconds;
        }
    }
}