using System;
using System.Collections.Generic;
using NLog;

namespace MovieLibrary
{
    public class MediaManager
    {
        public FileOperations FileOperations = new FileOperations();
        public Dictionary<int, Movie> Movies = new Dictionary<int, Movie>(); 
        Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public MediaManager()
        {
            DuplicateChecker.Movies = Movies;
        }

        public void ReadAllMovies()
        {
            List<string[]> movieArrays = FileOperations.ReadMovies();

            foreach (var movie in movieArrays)
            {
                var movieInfo = new Dictionary<string, string>
                {
                    ["Title"] = movie[0],
                    ["Genres"] = movie[1]
                };
                try
                {
                    CreateMovie(movieInfo, movie[0]);
                }
                catch (ArgumentException e)
                {
                    _logger.Error(e.Message);
                }
            }
        }
        
        public Movie CreateMovie(Dictionary<string, string> movieInfo, string id = null)
        {
            var newMovie = new Movie(movieInfo["Title"], movieInfo["Genres"], id);

            try
            {
                Movies.Add(newMovie.Id, newMovie); // ID duplicate check when adding to Dictionary
            }
            catch (ArgumentException e)
            {
                _logger.Error("MovieId " + newMovie.Id + " already exists");
            }

            return newMovie;
        }
    }
}