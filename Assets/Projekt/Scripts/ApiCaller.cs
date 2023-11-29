using System.Net.Http;
using System.Threading.Tasks;

public class ApiCaller
{
    private HttpClient _httpClient;
    private string _url;

    public ApiCaller()
    {
        _httpClient = new HttpClient();
        _url = "https://fbmanage.dk/api/";
    }

    public async Task<T> Get<T>(string uri)
    {
        var result = await InvokeGet(uri);

        if (result.IsSuccessStatusCode)
            return await result.Content.ReadFromJsonAsync<T>();

        else
            return default(T);
    }
    private async Task<HttpResponseMessage> InvokeGet(string uri)
    {
        var response = await _httpClient.GetAsync(GetUrl(uri));
        return response;
    }

    public async Task<bool> Post<T>(string uri, T obj)
    {
        var response = await _httpClient.PostAsJsonAsync(GetUrl(uri), obj);
        return response.IsSuccessStatusCode;
    }


    private string GetUrl(string uri)
    {
        return _url + uri;
    }
}
