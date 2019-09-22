using System;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using FilmSuggestions.Models;
using System.Web;
using System.Net;
using FilmSuggestions.Controllers;
using FilmSuggestions.Services;

namespace FilmSuggestionsTests
{
    public class FilmControllerTests
    {
        [Fact]
        public void GenerateFilmSuggestion_ReturnsFilm()
        {
            // assert
            var film = new Film();
            var mockService = new Mock<FilmService>(null);
            // mockService.Setup(u => u.GenerateFilmSuggestion()).Returns();
            var controller = new FilmController(mockService.Object);

            // act
            // var result = controller.GenerateFilmSuggestion();

            // Assert.IsType<Film>(result);
        }
    }
}
