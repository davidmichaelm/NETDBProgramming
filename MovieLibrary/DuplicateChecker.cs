using System.Collections.Generic;

namespace MovieLibrary
{
    public static class DuplicateChecker
    {
        public static Dictionary<int, Movie> Movies;
        
        public static bool MovieTitleExists(string title)
        {
            title = title.ToUpper();
            foreach (Movie m in Movies.Values)
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