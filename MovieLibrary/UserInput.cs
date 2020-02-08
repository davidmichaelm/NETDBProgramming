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

        public string GetMovieTitle()
        {
            Console.WriteLine("Enter the movie title:");
            return Console.ReadLine();
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
    }
}