using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace FilmSuggestions.Services
{
    public class FilmService
    {
        TMDbClient client = new TMDbClient("f6164895891c0436b46cf51cb281a28a");
        public virtual SearchMovie GenerateFilmSuggestion()
        {
            SearchContainer<SearchMovie> results = client.DiscoverMoviesAsync()
                .IncludeVideoMovies(false)
                .IncludeAdultMovies(false)
                .Query(RandomNumber(500)).Result;

            if (results == null)
            {
                return GenerateFilmSuggestion();
            }

            var result = results.Results.Skip(RandomNumber(19)).Take(1).SingleOrDefault();

            if (result == null) {
                return GenerateFilmSuggestion();
            }

            return result;
        }

        public virtual SearchMovie GetFilmSuggestionBasedOnGenre(List<int> genreIds)
        {
            try
            {
                SearchContainer<SearchMovie> movies = client.DiscoverMoviesAsync()
                    .IncludeVideoMovies(false)
                    .IncludeAdultMovies(false)
                    .IncludeWithAllOfGenre(genreIds)
                    .Query(RandomNumber(500)).Result;

                if (movies == null)
                {
                    return GetFilmSuggestionBasedOnGenre(genreIds);
                }

                var movie = movies.Results.Skip(RandomNumber(19)).Take(1).SingleOrDefault();

                if (movie == null)
                {
                    return GetFilmSuggestionBasedOnGenre(genreIds);
                }

                return movie;
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.Flatten().InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
                throw new Exception("Try again later.");
            }
        }

        public virtual List<Genre> GetGenres()
        {
            List<Genre> genres = client.GetMovieGenresAsync().Result;
            return genres;
        }

        private int RandomNumber(int upperBound)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, upperBound);
            return number;
        }
    }
}