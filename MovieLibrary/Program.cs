using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MovieLibrary
{
    internal class Program
    {
        private UserInput _input = new UserInput();
        private FileOperations _fo = new FileOperations();
        private Dictionary<int, Movie> _movies = new Dictionary<int, Movie>();
        
        public static void Main(string[] args) { new Program(); }

        public Program()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            
            // open file and create existing movies
            List<string[]> movieArrays = _fo.ReadMovies();

            
            foreach (var movie in movieArrays)
            {
                try
                {
                    CreateMovie(movie[1], movie[2], movie[0]);
                }
                catch (ArgumentException e)
                {
                    logger.Error(e.Message);
                }
            }
            
            // Let the user decide what to do
            var keepRunning = true;
            while (keepRunning)
            {
                switch (_input.GetMenuOption())
                {
                    case "1": // Read current movies
                        Console.WriteLine($"{"ID", 10} | {"Movie", -90} | {"Genres", -40}");
                        foreach (var movie in _movies.Values)
                        {
                            Console.WriteLine($"{movie.Id, 10} | {movie.Title, -90} | {movie.Genres.Replace("|", ", "), -40}");
                        }
                        break;
                    case "2": // Add a movie
                        string title = _input.GetMovieTitle();
                        string genres = _input.GetMovieGenres();

                        try
                        {
                            var newMovie = CreateMovie(title, genres);
                            _fo.AppendMovie(newMovie);
                        }
                        catch (ArgumentException e)
                        {
                            logger.Error(e.Message);
                        }
                        break;
                    default:
                        keepRunning = false;
                        break;
                }
            }
        }

        public Movie CreateMovie(string title, string genres, string id = null)
        {
            if (MovieTitleExists(title)) // title duplicate check
            {
                throw new ArgumentException(title + " already exists");
            }
            
            var newMovie = new Movie(title, genres, id);

            try
            {
                _movies.Add(newMovie.Id, newMovie); // ID duplicate check when adding to Dictionary
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(newMovie.Id + " already exists");
            }

            return newMovie;
        }

        public bool MovieTitleExists(string title)
        {
            title = title.ToUpper();
            foreach (Movie m in _movies.Values)
            {
                if (m.Title.ToUpper().Equals(title))
                {
                    return true;
                }
            }
            return false;
        }
    }
}