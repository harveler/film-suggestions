using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using FilmSuggestions.Models;
using FilmSuggestions.Services;
using Microsoft.AspNetCore.Mvc;
using TMDbLib.Client;
using TMDbLib.Objects.Authentication;
using TMDbLib.Objects.Changes;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
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
        [Route("")]
        public Film GenerateFilmSuggestion()
        {
            var result = _service.GenerateFilmSuggestion();
            var movie = CreateModelFromApiResult(result);

            return movie;
        }

        [HttpGet]
        [Route("GetFilmBasedOnGenre/{genre}")]
        public Film GetFilmBasedOnGenre([FromRoute]string genre)
        {
            List<string> generas = genre.Split(',').ToList<string>();
            List<int> ids = new List<int>();
            foreach (var genr in generas)
            {
                int newGenre = Int32.Parse(genr);
                ids.Add(newGenre);
            }

            var result = _service.GenerateFilmBasedOnGenre(ids);
            var movie = CreateModelFromApiResult(result);

            return movie;
        }

        [HttpGet]
        [Route("Genres")]
        public List<Genre> GetGenres()
        {
            var genres = _service.GetGenres();
            return genres;

        }

        private Film CreateModelFromApiResult(IEnumerable<SearchMovie> searchedMovie)
        {
            var query = searchedMovie.SingleOrDefault();

            List<Genres> genres = new List<Genres>();

            foreach (var genre in query.GenreIds)
            {
                Genres newGenre = new Genres
                {
                    Id = genre,
                    Name = GetGenreName(genre),
                };
                genres.Add(newGenre);
            }

            ExternalIdsMovie externalIds = client.GetMovieExternalIdsAsync(query.Id).Result;

            Film result = new Film
            {
                Id = externalIds.ImdbId,
                Title = query.Title,
                Overview = query.Overview,
                Year = query.ReleaseDate.Value.Year,
                Genres = genres,
            };

            return result;
        }

        private string GetGenreName(int genreId)
        {
            List<Genre> getGenres = client.GetMovieGenresAsync().Result;
            string genreName = getGenres.Where(i => i.Id == genreId).Select(name => name.Name).SingleOrDefault();
            return genreName;
        }
    }
}