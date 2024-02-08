using System.Text.Json;
using FilmTracker.DTOs;
using FilmTracker.Models;
using Newtonsoft.Json.Linq;

namespace FilmTracker.Services
{
    public interface IOmdbApiService
    {
        Task<List<MovieApi>> GetMovieListBySearch(string searchQuery);
        Task<MovieDetailsApi> GetMovieDetails(string imdbID);
    }
    public class OmdbApiService : IOmdbApiService
    {
        private readonly HttpClient httpClient = new HttpClient();
        
        public async Task<List<MovieApi>> GetMovieListBySearch(string searchQuery)
        {
            var request = new HttpRequestMessage();
            var url = $"http://www.omdbapi.com/?s={searchQuery}&apikey=8d653100";
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Get;
            var response = await httpClient.SendAsync(request);
            var contentString = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(contentString);
            var omdbApiResponse = responseObject.ToObject<OmdbApiResponse>()!;

            return omdbApiResponse.Search;
        }

        public async Task<MovieDetailsApi> GetMovieDetails(string imdbID)
        {
            var request = new HttpRequestMessage();
            var url = $"http://www.omdbapi.com/?i={imdbID}&apikey=8d653100";
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Get;
            var response = await httpClient.SendAsync(request);
            var contentString = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(contentString);
            var omdbApiResponse = responseObject.ToObject<MovieDetailsApi>()!;

            return omdbApiResponse;
        }
    }
}