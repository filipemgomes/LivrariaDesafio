using System.Net.Http;
using System.Net.Http.Json;
using App.Models;

namespace App.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5251/api/v1/"); 
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {
            var response = await _httpClient.GetAsync("books");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<BookViewModel>>();
        }
    }
}
