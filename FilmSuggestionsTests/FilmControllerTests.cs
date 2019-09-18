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

namespace FilmSuggestionsTests
{
    public class FilmControllerTests
    {
        [Fact]
        public void GenerateFilmSuggestion_ReturnsObject()
        {
            // assert
            var controller = new FilmController();

            // act
            var result = controller.GenerateFilmSuggestion();

            Assert.IsType<Film>(result);
        }
    }
}
