using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NLog;

namespace MovieLibrary
{
    internal class Program
    {
        private MediaManager _mediaManager = new MediaManager();
        private UserInput _ui = new UserInput();
        Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        
        public static void Main(string[] args) { new Program(); }

        public Program()
        {
            // open file and create existing movies
            _mediaManager.ReadAllMovies();
            
            // Let the user decide what to do
            var keepRunning = true;
            while (keepRunning)
            {
                switch (_ui.GetMenuOption())
                {
                    case "1": // Display current movies
                        _ui.DisplayMovies(_mediaManager.Movies);
                        break;
                    case "2": // Add a movie
                        var movieInfo = _ui.GetMovieInfo();
                        var newMovie = _mediaManager.CreateMovie(movieInfo);
                        _mediaManager.FileOperations.AppendMovie(newMovie);
                        break;
                    default:
                        keepRunning = false;
                        break;
                }
            }
        }
    }
}