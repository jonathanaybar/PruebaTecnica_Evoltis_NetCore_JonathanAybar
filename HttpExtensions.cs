using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public static class HttpExtensions
{
    public static async Task<T> ReadAsAsync<T>(this HttpResponseMessage response)
    {
        var result = await response.Content.ReadFromJsonAsync<T>();
        return result!;
    }
}
