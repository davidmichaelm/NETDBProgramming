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
                try
                {
                    CreateMovie(movie[1], movie[2], movie[0]);
                }
                catch (ArgumentException e)
                {
                    _logger.Error(e.Message);
                }
            }
        }
        
        public Movie CreateMovie(string title, string genres, string id = null)
        {
            var newMovie = new Movie(title, genres, id);

            try
            {
                Movies.Add(newMovie.Id, newMovie); // ID duplicate check when adding to Dictionary
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("MovieId " + newMovie.Id + " already exists");
            }

            return newMovie;
        }
    }
}