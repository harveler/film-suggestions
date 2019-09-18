using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmSuggestions.Models;

namespace FilmSuggestions.Controllers
{
    [Route("api/[controller]")]
    public class FilmController : Controller
    {
        public Film GenerateFilmSuggestion() 
        {
            var film = new Film();
            return film;
        }
    }
}
