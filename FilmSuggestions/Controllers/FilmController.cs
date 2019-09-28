using FilmSuggestions.Models;
using FilmSuggestions.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace FilmSuggestions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
        private FilmService _service;
        TMDbClient client = new TMDbClient("f6164895891c0436b46cf51cb281a28a");
        public FilmController(FilmService service)
        {
            _service = service;
        }

        [HttpGet]
        public Film GenerateFilmSuggestion()
        {
            var result = _service.GenerateFilmSuggestion();
            var film = CreateModelFromApiResult(result);

            return film;
        }

        [HttpGet]
        [Route("GetFilmSuggestionBasedOnGenre/{stringIds}")]
        public Film GetFilmSuggestionBasedOnGenre([FromRoute] string stringIds)
        {
            if (stringIds == "" || stringIds == null)
            {
                return GenerateFilmSuggestion();
            }

            var intIds = GetIntIdsFromString(stringIds);

            var result = _service.GetFilmSuggestionBasedOnGenre(intIds);

            if (result == null)
            {
                return new Film();
            }

            var movie = CreateModelFromApiResult(result);

            return movie;
        }

        [HttpGet]
        [Route("GetGenres")]
        public List<Genre> GetGenres()
        {
            var genres = _service.GetGenres();

            return genres;
        }

        private List<int> GetIntIdsFromString(string stringIds)
        {
            List<string> listOfStringIds = stringIds.Split(',').ToList<string>();

            List<int> intIds = new List<int>();

            foreach (var stringId in listOfStringIds)
            {
                int intId = Int32.Parse(stringId);
                intIds.Add(intId);
            }

            return intIds;
        }

        private Film CreateModelFromApiResult(SearchMovie searchedMovie)
        {
            Film result = new Film
            {
                Id = GetMovieImdbId(searchedMovie.Id),
                Title = searchedMovie.Title,
                Overview = searchedMovie.Overview,
                Year = searchedMovie.ReleaseDate.Value.Year,
                Genres = GetMovieGenres(searchedMovie.GenreIds),
            };

            return result;
        }

        private string GetMovieImdbId(int id)
        {
            return client.GetMovieExternalIdsAsync(id).Result.ImdbId;
        }
        private List<Genres> GetMovieGenres(List<int> genreIds)
        {
            List<Genres> genres = new List<Genres>();

            foreach (var genreId in genreIds)
            {
                genres.Add(new Genres
                {
                    Id = genreId,
                        Name = GetGenreName(genreId),
                });
            }

            return genres;
        }
        private string GetGenreName(int genreId)
        {
            List<Genre> getGenres = client.GetMovieGenresAsync().Result;

            return getGenres
                .Where(id => id.Id == genreId)
                .Select(name => name.Name)
                .SingleOrDefault();
        }
    }
}