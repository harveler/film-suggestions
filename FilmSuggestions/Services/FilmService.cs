using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using FilmSuggestions.Models;

namespace FilmSuggestions.Services
{
    public class FilmService
    {
        static string _address = "https://api.themoviedb.org/3/discover/movie?api_key=f6164895891c0436b46cf51cb281a28a";
        static int genreId { get; set; }
        static string genre = "&with_genres=" + genreId;
        private string result;
        public async Task<IEnumerable> GenerateFilmSuggestion()
        {
            var film = new Film();
            var result = await GetExternalResponse();

            return null;
        }

        private async Task<string> GetExternalResponse()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}