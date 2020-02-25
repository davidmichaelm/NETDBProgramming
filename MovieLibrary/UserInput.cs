using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    public class UserInput
    {
        public string GetMenuOption()
        {
            Console.WriteLine("1) Read current movies.");
            Console.WriteLine("2) Add new movie to the file.");
            Console.WriteLine("Enter any other key to exit.");
            return Console.ReadLine();
        }

        public Dictionary<string, string> GetMovieInfo()
        {
            string title = GetMovieTitle();
            string genres = GetMovieGenres();
            return new Dictionary<string, string>
            {
                ["Title"] = title,
                ["Genres"] = genres
            };
        }

        public string GetMovieTitle()
        {
            Console.WriteLine("Enter the movie title:");
            var title = Console.ReadLine();
            while (DuplicateChecker.MovieTitleExists(title))
            {
                Console.WriteLine("Movie title already exists. Enter a different movie title:");
                title = Console.ReadLine();
            }
            return title;
        }

        public string GetMovieGenres()
        {
            List<string> genres = new List<string>();
            var keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Enter a genre? (Y/N)");
                var input = Console.ReadLine();
                
                if (input != null && input.ToUpper() == "Y")
                {
                    Console.WriteLine("Enter the genre:");
                    genres.Add(Console.ReadLine());
                }
                else
                {
                    keepRunning = false;
                }
            }

            return genres.Count == 0 ? "(no genres listed)" : String.Join("|", genres);
        }

        public void DisplayMovies(Dictionary<int, Movie> movies)
        {
            Console.WriteLine($"{"ID", 10} | {"Movie", -90} | {"Genres", -40}");
            foreach (var movie in movies.Values)
            {
                Console.WriteLine($"{movie.Id, 10} | {movie.Title, -90} | {movie.Genres.Replace("|", ", "), -40}");
            }
        }
    }
}