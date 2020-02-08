using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace MovieLibrary
{
    public class FileOperations
    {
        private string _moviesFile = "movies.csv";

        // returns a List with arrays of strings corresponding to movieId, title, genre
        public List<string[]> ReadMovies()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            
            List<string[]> movies = new List<string[]>();

            if (!File.Exists(_moviesFile))
            {
                try
                {
                    using (var sw = new StreamWriter(_moviesFile))
                    {
                        sw.WriteLine("movieId,title,genres");
                        logger.Info("Created new movies.csv");
                    }
                }
                catch (Exception e)
                {
                    logger.Fatal("Movie file could not be created");
                    Environment.Exit(0);
                }
            }

            try
            {
                using (StreamReader sr = new StreamReader(_moviesFile))
                {
                    string line;
                    bool firstLine = true;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (firstLine)
                        {
                            firstLine = false;
                            continue;
                        }
                        // check for quotes in the title before we split by comma
                        var index = line.IndexOf('"');
                        var title = "";
                        if (index != -1)
                        {
                            var lastIndex = line.LastIndexOf('"');
                            title = line.Substring(index, lastIndex - index).Trim('"');
                            line = line.Remove(index, lastIndex - index);
                        }

                        string[] lineArray = line.Split(',');
                        lineArray[1] =
                            title.Equals("")
                                ? lineArray[1]
                                : title; // 2nd array spot will be empty if quotes were found
                        movies.Add(lineArray);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Failed to load movie file");
            }
            
            
            logger.Info($"Loaded {movies.Count} movies");

            return movies;
        }
        
        public void AppendMovie(Movie movie)
        {
            try
            {
                using (var sw = new StreamWriter(_moviesFile, true))
                {
                    sw.WriteLine(movie.ToString());
                }
            }
            catch (Exception e)
            {
                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Fatal("Could not write to file");
                Environment.Exit(0);
            }
        }
    }
}