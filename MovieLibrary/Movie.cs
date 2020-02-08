using System;
using System.Collections.Generic;
using NLog;

namespace MovieLibrary
{
    
    public class Movie
    {
        private static int _biggestMovieId;

        public Movie(string title, string genres, string id = null)
        {
            if (id == null)
            {
                Id = ++_biggestMovieId;
            }
            else
            {
                try
                {
                    Id = int.Parse(id);
                }
                catch (Exception e)
                {
                    var logger = NLog.LogManager.GetCurrentClassLogger();
                    logger.Fatal(e.Message+e.Data);
                    Environment.Exit(0);
                }
            }

            if (Id > _biggestMovieId)
            {
                _biggestMovieId = Id;
            }

            Title = title;
            Genres = genres;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }
        
        public override string ToString()
        {
            return $"{Id},{Title},{Genres}";
        }
    }
}