using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FilmSuggestions.Models;
using Microsoft.AspNetCore.Mvc;
using TMDbLib.Client;
using TMDbLib.Objects.Discover;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Utilities;

namespace FilmSuggestions.Services
{
    public class FilmService
    {
        TMDbClient client = new TMDbClient("f6164895891c0436b46cf51cb281a28a");
        public IEnumerable<SearchMovie> GenerateFilmSuggestion()
        {
            SearchContainer<SearchMovie> results = client.DiscoverMoviesAsync()
                .IncludeVideoMovies(false)
                .IncludeAdultMovies(false)
                .Query(RandomNumber(500)).Result;

            if (results == null)
            {
                return GenerateFilmSuggestion();
            }

            var result = results.Results.Skip(RandomNumber(19)).Take(1);

            return result;
        }

        public IEnumerable<SearchMovie> GenerateFilmBasedOnGenre(List<int> genreIds)
        {
            try
            {
                SearchContainer<SearchMovie> results = client.DiscoverMoviesAsync()
                    .IncludeVideoMovies(false)
                    .IncludeAdultMovies(false)
                    .IncludeWithAllOfGenre(genreIds)
                    .Query(RandomNumber(500)).Result;

                if (results == null)
                {
                    return GenerateFilmBasedOnGenre(genreIds);
                }

                var result = results.Results.Skip(RandomNumber(19)).Take(1);

                if (result.Count() == 0)
                {
                    return GenerateFilmBasedOnGenre(genreIds);
                }

                return result;
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

        public List<Genre> GetGenres()
        {
            List<Genre> result = client.GetMovieGenresAsync().Result;
            return result;
        }

        private int RandomNumber(int upperBound)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, upperBound);
            return number;
        }
    }
}