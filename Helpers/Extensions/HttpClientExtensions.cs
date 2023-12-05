using System.Text;
using Newtonsoft.Json;

namespace kwiz_api_game.Helpers.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T> GetAsync<T>(this HttpClient client, string uri)
    {
        var response = await client.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }
    
    public static async Task<T> PostAsync<T>(this HttpClient client, string uri, object body)
    {
        var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseContent);
    }
}