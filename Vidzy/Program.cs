using System;
using System.Linq;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new VidzyContext();
            var actionMoviesSortedByName = context.Videos
                .Where(v => v.Genre.Name == "Action")
                .OrderBy(v => v.Name);

            Console.WriteLine("Action movies sorted by name: ");

            foreach (var movie in actionMoviesSortedByName)
                Console.WriteLine("{0}", movie.Name);

            Console.WriteLine("");

            var goldDramaMoviesSortedByReleaseDate = context.Videos
                .Where(v => v.Classification == Classification.Gold && v.Genre.Name == "Drama")
                .OrderByDescending(v => v.ReleaseDate);
            
            Console.WriteLine("Gold drama movies sorted by release date (newest first): ");

            foreach (var movie in goldDramaMoviesSortedByReleaseDate)
                Console.WriteLine("{0}", movie.Name);

            Console.WriteLine("");

            var allMovies = context.Videos.Select(v => new { MovieName = v.Name, Genre = v.Genre.Name });

            Console.WriteLine("All movies projected into an anonymous type with two properties - MovieName and Genre: ");

            foreach (var movie in allMovies)
                Console.WriteLine("{0}", movie.MovieName);

            Console.WriteLine("");

            var allMoviesGroupedByClassification = context.Videos
                .GroupBy(v => v.Classification)
                .Select(v => new { Classification = v.Key, Movies = v })
                .ToList();

            Console.WriteLine("All movies grouped by their classification: ");

            foreach (var classification in allMoviesGroupedByClassification)
            {
                Console.WriteLine("Classification: {0}", classification.Classification);
                foreach (var movie in classification.Movies)
                    Console.WriteLine("  {0}", movie.Name);

                Console.WriteLine("");
            }

            Console.WriteLine("");

            var classificationsWithCounts = context.Videos
                .GroupBy(v => v.Classification)
                .Select(v => new { Classification = v.Key, NumberOfVideos = v.Count() })
                .OrderBy(c => c.Classification)
                .ToList();

            Console.WriteLine("List of classifications sorted alphabetically and count of videos in them: ");

            foreach (var classification in classificationsWithCounts)
            {
                Console.WriteLine("{0} ({1})", classification.Classification, classification.NumberOfVideos);
            }

            Console.WriteLine("");

            var genresWithVideoCounts = context.Genres
                .Select(g => new { GenreName = g.Name, NumberOfVideos = g.Videos.Count() })
                .OrderByDescending(c => c.NumberOfVideos)
                .ToList();

            Console.WriteLine("List of genres and number of videos they include, sorted by the number of videos: ");

            foreach (var genre in genresWithVideoCounts)
            {
                Console.WriteLine("{0} ({1})", genre.GenreName, genre.NumberOfVideos);
            }
        }
    }
}
