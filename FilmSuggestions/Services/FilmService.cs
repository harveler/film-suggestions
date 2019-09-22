using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using FilmSuggestions.Models;

namespace FilmSuggestions.Services
{
    public class FilmService
    {
        static string _address = "http://www.omdbapi.com/?i=tt3896198&apikey=bf29ea07";
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