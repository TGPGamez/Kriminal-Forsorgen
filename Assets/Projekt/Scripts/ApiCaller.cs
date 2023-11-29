using Newtonsoft.Json;
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
        {
            string jsonResponse = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
        else
        {
            // Handle non-success status code here if needed
            // For example, you might want to throw an exception
            throw new HttpRequestException($"Failed to retrieve data. Status code: {result.StatusCode}");
        }
    }
    private async Task<HttpResponseMessage> InvokeGet(string uri)
    {
        var response = await _httpClient.GetAsync(GetUrl(uri));
        return response;
    }


    private string GetUrl(string uri)
    {
        return _url + uri;
    }
}
