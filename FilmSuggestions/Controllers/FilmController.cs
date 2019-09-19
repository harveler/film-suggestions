using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmSuggestions.Models;
using FilmSuggestions.Services;

namespace FilmSuggestions.Controllers
{
    [Route("api/[controller]")]
    public class FilmController : Controller
    {
        private FilmService _service;
        public FilmController(FilmService service) {
            _service = service;
        }
        public Film GenerateFilmSuggestion() 
        {
            var film = _service.GenerateFilmSuggestion();
            return film;
        }
    }
}
