using System.Collections.Generic;

namespace FilmSuggestions.Models
{
    public class Film
    {
        public string Id;
        public string Title;
        public string Overview;
        public int Year;
        public List<Genres> Genres;
    }
}