using FilmSuggestions.Controllers;
using FilmSuggestions.Models;
using FilmSuggestions.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xunit;

namespace FilmSuggestionsTests
{
    public class FilmControllerTests
    {
        [Fact]
        public void GenerateFilmSuggestion_ReturnsFilm()
        {
            // arrange
            var mockService = new Mock<FilmService>();
            var searchedMovie = CreateSearchedMovie();
            mockService.Setup(u => u.GenerateFilmSuggestion()).Returns(searchedMovie);
            var controller = new FilmController(mockService.Object);

            // act
            var result = controller.GenerateFilmSuggestion();

            // assert
            Assert.IsType<Film>(result);
        }

        [Fact]
        public void GetFilmSuggestionBasedOnGenre_ValidParameters_ReturnsFilm()
        {
            // arrange
            var mockService = new Mock<FilmService>();
            var searchedMovie = CreateSearchedMovie();
            var listOfIds = new List<int>
            {
                12,
                35,
                36
            };
            mockService.Setup(u => u.GetFilmSuggestionBasedOnGenre(listOfIds)).Returns(searchedMovie);
            var controller = new FilmController(mockService.Object);

            // act
            var result = controller.GetFilmSuggestionBasedOnGenre("12,35,36");

            // assert
            Assert.IsType<Film>(result);
        }

        [Fact]
        public void GetFilmSuggestionBasedOnGenre_InvalidParameters_ReturnsEmptyFilm()
        {
            // arrange
            var mockService = new Mock<FilmService>();
            var searchedMovie = CreateSearchedMovie();
            var listOfIds = new List<int>
            {
                12,
                35,
                36
            };
            mockService.Setup(u => u.GetFilmSuggestionBasedOnGenre(listOfIds)).Returns(searchedMovie);
            var controller = new FilmController(mockService.Object);

            // act
            var result = controller.GetFilmSuggestionBasedOnGenre("12,45,36");

            // assert
            Assert.True(result.Id == null);
            Assert.True(result.Title == null);
            Assert.True(result.Overview == null);
            Assert.True(result.Genres == null);
            Assert.True(result.Year == 0);
        }

        [Fact]
        public void GetGenres_ReturnsList()
        {
            // arrange
            var mockService = new Mock<FilmService>();
            var genres = CreateGenres();
            mockService.Setup(u => u.GetGenres()).Returns(genres);
            var controller = new FilmController(mockService.Object);

            // act
            var result = controller.GetGenres();

            // assert
            Assert.IsType<List<Genre>>(result);
            Assert.True(result.Count() == 3);
            Assert.True(result.First().Id == 12);
        }

        private static SearchMovie CreateSearchedMovie()
        {
            SearchMovie result = new SearchMovie
            { 
                Id = 21345,
                Adult = false,
                Title = "Rambo",
                Overview = "Vietnam veteran and drifter John J. Rambo (Sylvester Stallone) wanders into a small Washington town in search of an old friend, but is met with intolerance and brutality by the local sheriff, Will Teasle (Brian Dennehy). When Teasle and his deputies restrain and shave Rambo, he flashes back to his time as a prisoner of war and unleashes his fury on the officers. He narrowly escapes the manhunt, but it will take his former commander (Richard Crenna) to save the hunters from the hunted.",
                ReleaseDate = new DateTime(1974, 7, 10, 7, 10, 24),
                GenreIds = new List<int>()
                {
                12,
                35
                },
                Video = false,
            };

            return result;
        }

        private static List<Genre> CreateGenres()
        {
            return new List<Genre>()
            {
                new Genre
                {
                    Id = 12,
                        Name = "Drama"
                },
                new Genre
                {
                    Id = 13,
                        Name = "Horror"
                },
                new Genre
                {
                    Id = 14,
                        Name = "Comedy"
                }
            };
        }
    }

}