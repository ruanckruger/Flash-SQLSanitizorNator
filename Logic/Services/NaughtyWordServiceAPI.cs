using Microsoft.AspNetCore.Components;
using Microsoft.Net.Http.Headers;
using SQLSanitizorNator.Data.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SQLSanitizorNator.Logic.Services
{

    public class NaughtyWordServiceApi : INaughtyWordService<NaughtyWordServiceApi>
    {
        HttpClient _client;
        public NaughtyWordServiceApi(NavigationManager nav)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(nav.Uri + "api/sanitize/")
            };
        }

        public async Task<List<NaughtyWord>> GetUnderSeverity(int severity = 10, CancellationToken token = default)
        {
            var result = await _client.GetAsync($"?severity={severity}", token);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<List<NaughtyWord>>(token) ?? new();
            }
            throw new HttpRequestException("Failed to fetch naughty words from API.");
        }

        public async Task<string> Sanitize(string toSanitize, int severity = 10, CancellationToken token = default)
        {
            var content = new StringContent($"\"{toSanitize}\"", Encoding.UTF8, "application/json");
            var result = await _client.PostAsync($"sanitize/?severity={severity}", content, token);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync(token);
            }
            throw new HttpRequestException("Failed to Sanitize NaughtyWords.");
        }

        public async Task<NaughtyWord?> GetById(Guid id, CancellationToken token = default)
        {
            var result = await _client.GetAsync($"./{id}", token);

            if (result.IsSuccessStatusCode)
            {
                var toReturn = await result.Content.ReadAsStringAsync(token);
                return new();
            }
            throw new HttpRequestException("Failed to Create Naughty Word.");
        }
        public async Task<List<NaughtyWord>> GetAll(CancellationToken token = default)
        {
           var result = await _client.GetAsync($"./", token);

            if (result.IsSuccessStatusCode)
            {
                var toReturn = await result.Content.ReadAsStringAsync(token);
                return new();
            }
            throw new HttpRequestException("Failed to Create Naughty Word.");
        }

        public async Task<NaughtyWord?> Create(NaughtyWord word, CancellationToken token = default)
        {
            var result = await _client.PostAsJsonAsync($"./", word, token);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<NaughtyWord>(token);
            }
            throw new HttpRequestException("Failed to Create Naughty Word.");
        }

        public async Task<NaughtyWord?> Update(NaughtyWord word, CancellationToken token = default)
        {
            var result = await _client.PutAsJsonAsync($"./", word, token);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<NaughtyWord>(token);
            }
            throw new HttpRequestException("Failed to Update Naughty Word.");
        }

        public async Task<NaughtyWord?> Delete(NaughtyWord word, CancellationToken token = default)
        {
            var result = await _client.DeleteAsync($"./{word.Id}", token);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<NaughtyWord>(token);
            }
            throw new HttpRequestException("Failed to Delete Naughty Word.");
        }

        public async Task<NaughtyWord?> DeleteById(Guid id, CancellationToken token = default)
        {
            var result = await _client.DeleteAsync($"./{id}", token);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<NaughtyWord>(token);
            }
            throw new HttpRequestException("Failed to Delete Naughty Word.");
        }
    }
}
