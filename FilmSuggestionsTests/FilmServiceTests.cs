using FilmSuggestions.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Search;
using Xunit;

namespace FilmSuggestionsTests
{
    public class FilmServiceTests
    {
        private FilmService _service;

        public FilmServiceTests()
        {
            TMDbClient client = new TMDbClient("f6164895891c0436b46cf51cb281a28a");
            _service = new FilmService();
        }

        [Fact]
        public void GenerateFilmSuggestion_ReturnsSearchMovie()
        {
            // act
            var movie = _service.GenerateFilmSuggestion();
            var expectedType = typeof(SearchMovie);

            // assert
            Assert.NotNull(movie);
            Assert.IsType(expectedType, movie);
            Assert.True(movie.Adult == false);
            Assert.True(movie.Video == false);
        }

        [Fact]
        public void GetFilmSuggestionBasedOnGenre_ValidOneParameter_ReturnsSearchMovie()
        {
            // arrange
            List<int> genreIds = new List<int>()
            {
                12
            };

            // act
            var movie = _service.GetFilmSuggestionBasedOnGenre(genreIds);

            // assert
            Assert.NotNull(movie);
            Assert.Contains(12, movie.GenreIds);
        }

        [Fact]
        public void GetFilmSuggestionBasedOnGenre_ValidMultipleParameters_ReturnsSearchMovie()
        {
            // arrange
            List<int> genreIds = new List<int>()
            {
                12,
                35,
                36
            };

            // act
            var movie = _service.GetFilmSuggestionBasedOnGenre(genreIds);

            // assert
            Assert.NotNull(movie);
            Assert.Contains(12, movie.GenreIds);
            Assert.Contains(35, movie.GenreIds);
            Assert.Contains(36, movie.GenreIds);
        }

        [Fact]
        public void GetFilmSuggestionBasedOnGenre_InvalidParameters_ThrowsException()
        {
            // arrange
            List<int> genreId = new List<int>()
            {
                5
            };

            var task = Task.Run(() =>
            {
                var result = _service.GetFilmSuggestionBasedOnGenre(genreId);
            });

            // act
            var exception = Record.ExceptionAsync(async() => await task);

            // assert
            Assert.True(exception != null);
            Assert.True(exception.Result.Message == "Try again later.");
        }
    }
}